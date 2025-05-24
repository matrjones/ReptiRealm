import { NextResponse } from "next/server";
import { stripe } from "@/libs/stripe";
import { headers } from "next/headers";
import Stripe from "stripe";

// Add logging to verify environment variables
console.log("Webhook secret available:", !!process.env.STRIPE_WEBHOOK_SECRET);
console.log("Stripe secret key available:", !!process.env.STRIPE_SECRET_KEY);

const endpointSecret = process.env.STRIPE_WEBHOOK_SECRET;

if (!endpointSecret) {
  console.error("STRIPE_WEBHOOK_SECRET is not set in environment variables");
}

export async function POST(req: Request) {
  try {
    if (!stripe) {
      console.error("Stripe client is not initialized");
      return new NextResponse("Stripe is not configured", { status: 500 });
    }

    const body = await req.text();
    const headersList = await headers();
    const signature = headersList.get("stripe-signature");

    if (!signature) {
      console.error("Stripe signature is missing from headers");
      return new NextResponse("Webhook signature missing", { status: 400 });
    }

    if (!endpointSecret) {
      console.error("Webhook secret is not configured");
      return new NextResponse("Webhook secret not configured", { status: 500 });
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
    const authHeader = req.headers.get("authorization");

    switch (event.type) {
      case "customer.subscription.created":
      case "customer.subscription.updated": {
        const subscription = event.data.object as Stripe.Subscription;
        const customer = (await stripe.customers.retrieve(
          subscription.customer as string
        )) as Stripe.Customer;

        if (subscription.status === "active") {
          await fetch(
            `${process.env.NEXT_PUBLIC_API_URL}/Subscription/Create`,
            {
              method: "POST",
              headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${authHeader}`,
              },
              body: JSON.stringify({
                email: customer.email,
                status: "active",
                interval:
                  subscription.items.data[0].price.recurring?.interval ||
                  "month",
                planName: subscription.items.data[0].price.nickname || "Basic",
              }),
            }
          );
        }
        break;
      }
      case "customer.subscription.deleted": {
        const subscription = event.data.object as Stripe.Subscription;
        const customer = (await stripe.customers.retrieve(
          subscription.customer as string
        )) as Stripe.Customer;

        // Get user's JWT token from your backend using customer email
        const userResponse = await fetch(
          `${process.env.NEXT_PUBLIC_API_URL}/User/GetByEmail`,
          {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify({
              email: customer.email,
            }),
          }
        );

        if (!userResponse.ok) {
          throw new Error("Failed to get user token");
        }

        const { token } = await userResponse.json();

        await fetch(`${process.env.NEXT_PUBLIC_API_URL}/Subscription/Update`, {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
          body: JSON.stringify({
            status: "cancelled",
            interval:
              subscription.items.data[0].price.recurring?.interval || "month",
            planName: subscription.items.data[0].price.nickname || "Basic",
          }),
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
