import { NextResponse } from "next/server";
import Stripe from "stripe";
import { headers } from "next/headers";

const stripe = new Stripe(process.env.STRIPE_SECRET_KEY!, {
  apiVersion: "2025-03-31.basil",
});

const webhookSecret = process.env.STRIPE_WEBHOOK_SECRET!;
const backendUrl = process.env.NEXT_PUBLIC_API_URL!;

export async function POST(req: Request) {
  const body = await req.text();
  const headersList = await headers();
  const signature = headersList.get("stripe-signature")!;

  let event: Stripe.Event;

  try {
    event = stripe.webhooks.constructEvent(body, signature, webhookSecret);
  } catch (err) {
    console.error(`Webhook signature verification failed:`, err);
    return NextResponse.json(
      { error: "Webhook signature verification failed" },
      { status: 400 }
    );
  }

  try {
    switch (event.type) {
      case "checkout.session.completed": {
        const session = event.data.object as Stripe.Checkout.Session;
        const userId = session.metadata?.userId;

        if (!userId) {
          console.error("No userId found in session metadata");
          return NextResponse.json(
            { error: "No userId found" },
            { status: 400 }
          );
        }

        // Get the subscription details
        const subscription = await stripe.subscriptions.retrieve(
          session.subscription as string
        );
        const subscriptionData = subscription as unknown as {
          current_period_end: number;
        };

        // Create subscription in .NET API
        const response = await fetch(`${backendUrl}/api/subscription`, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            stripeCustomerId: session.customer,
            stripeSubscriptionId: subscription.id,
            plan: subscription.items.data[0].price.nickname || "pro",
            status: subscription.status,
            currentPeriodEnd: new Date(
              subscriptionData.current_period_end * 1000
            ).toISOString(),
            cancelAtPeriodEnd: subscription.cancel_at_period_end,
          }),
        });

        if (!response.ok) {
          throw new Error(
            `Failed to create subscription: ${response.statusText}`
          );
        }

        console.log("Subscription created successfully");
        break;
      }

      case "customer.subscription.updated":
      case "customer.subscription.deleted": {
        const subscription = event.data.object as Stripe.Subscription;
        const subscriptionData = subscription as unknown as {
          current_period_end: number;
        };
        const userId = subscription.metadata?.userId;

        if (!userId) {
          console.error("No userId found in subscription metadata");
          return NextResponse.json(
            { error: "No userId found" },
            { status: 400 }
          );
        }

        // Update subscription in .NET API
        const response = await fetch(`${backendUrl}/api/subscription`, {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            stripeCustomerId: subscription.customer,
            stripeSubscriptionId: subscription.id,
            plan: subscription.items.data[0].price.nickname || "pro",
            status: subscription.status,
            currentPeriodEnd: new Date(
              subscriptionData.current_period_end * 1000
            ).toISOString(),
            cancelAtPeriodEnd: subscription.cancel_at_period_end,
          }),
        });

        if (!response.ok) {
          throw new Error(
            `Failed to update subscription: ${response.statusText}`
          );
        }

        console.log("Subscription updated successfully");
        break;
      }
    }

    return NextResponse.json({ received: true });
  } catch (error) {
    console.error("Error processing webhook:", error);
    return NextResponse.json(
      { error: "Error processing webhook" },
      { status: 500 }
    );
  }
}
