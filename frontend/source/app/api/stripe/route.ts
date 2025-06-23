import { NextResponse } from "next/server";
import { stripe } from "@/libs/stripe";
import { headers } from "next/headers";
import Stripe from "stripe";

// This is your Stripe CLI webhook secret for testing your webhook locally.
const endpointSecret = process.env.STRIPE_WEBHOOK_SECRET;

export async function POST(req: Request) {
  try {
    const body = await req.text();
    const headersList = await headers();
    const signature = headersList.get("stripe-signature");

    if (!signature || !endpointSecret) {
      console.error("Webhook signature or secret missing");
      return new NextResponse("Webhook signature or secret missing", {
        status: 400,
      });
    }

    let event;

    try {
      event = stripe.webhooks.constructEvent(body, signature, endpointSecret);
    } catch (err) {
      console.error("Webhook signature verification failed:", err);
      return new NextResponse("Webhook signature verification failed", {
        status: 400,
      });
    }

    // Handle the event
    switch (event.type) {
      case "customer.subscription.created":
      case "customer.subscription.updated": {
        const subscription = event.data.object as Stripe.Subscription;
        const customer = (await stripe.customers.retrieve(
          subscription.customer as string
        )) as Stripe.Customer;

        if (subscription.status === "active") {
          await prisma.subscription.upsert({
            where: { email: customer.email as string },
            update: {
              status: "active",
              planName: subscription.items.data[0].price.nickname || "Basic",
              interval:
                subscription.items.data[0].price.recurring?.interval || "month",
            },
            create: {
              userId: customer.id,
              email: customer.email as string,
              status: "active",
              planName: subscription.items.data[0].price.nickname || "Basic",
              interval:
                subscription.items.data[0].price.recurring?.interval || "month",
            },
          });
        }
        break;
      }
      case "customer.subscription.deleted": {
        const subscription = event.data.object as Stripe.Subscription;
        const customer = (await stripe.customers.retrieve(
          subscription.customer as string
        )) as Stripe.Customer;

        await prisma.subscription.upsert({
          where: { email: customer.email as string },
          update: {
            status: "cancelled",
            planName: subscription.items.data[0].price.nickname || "Basic",
            interval:
              subscription.items.data[0].price.recurring?.interval || "month",
          },
          create: {
            userId: customer.id,
            email: customer.email as string,
            status: "cancelled",
            planName: subscription.items.data[0].price.nickname || "Basic",
            interval:
              subscription.items.data[0].price.recurring?.interval || "month",
          },
        });
        break;
      }
      default:
        console.log(`Unhandled event type ${event.type}`);
    }

    return new NextResponse(null, { status: 200 });
  } catch (error) {
    console.error("Webhook error:", error);
    return new NextResponse("Webhook error", { status: 500 });
  }
}
