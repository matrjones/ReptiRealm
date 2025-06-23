import { NextRequest, NextResponse } from "next/server";
import { stripe } from "@/libs/stripe";

export async function POST(req: NextRequest) {
  try {
    if (!stripe) {
      return new NextResponse("Stripe is not configured", { status: 500 });
    }

    const authHeader = req.headers.get("authorization");

    if (!authHeader) {
      return new NextResponse("Unauthorized", { status: 401 });
    }

    // Get customer email from your backend
    const userResponse = await fetch(
      `${process.env.NEXT_PUBLIC_API_URL}/Identity/Get`,
      {
        headers: {
          Authorization: authHeader,
        },
      }
    );

    if (!userResponse.ok) {
      return new NextResponse("Failed to get user information", {
        status: 401,
      });
    }

    const user = await userResponse.json();

    const customers = await stripe.customers.list({
      email: user.email,
      limit: 1,
    });

    if (customers.data.length === 0) {
      console.log("No customer found");
      return new NextResponse("No subscription found", { status: 404 });
    }

    const customer = customers.data[0];

    const subscriptions = await stripe.subscriptions.list({
      customer: customer.id,
      limit: 1,
      status: "active",
    });

    if (subscriptions.data.length > 0) {
      const subscription = subscriptions.data[0];
      await stripe.subscriptions.update(subscription.id, {
        cancel_at_period_end: true,
      });
    }

    // Update subscription in our backend
    const response = await fetch(
      `${process.env.NEXT_PUBLIC_API_URL}/Subscription/Update`,
      {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          Authorization: authHeader,
        },
        body: JSON.stringify({
          status: "cancelled",
          interval: "",
          planName: "",
        }),
      }
    );

    if (!response.ok) {
      throw new Error("Failed to update subscription");
    }

    const updatedSubscription = await response.json();

    const subscriptionData = updatedSubscription
      ? {
          status: updatedSubscription.status,
          plan: {
            name: updatedSubscription.planName,
            interval: updatedSubscription.interval,
          },
        }
      : null;

    return NextResponse.json({
      success: true,
      subscription: subscriptionData,
    });
  } catch (error) {
    console.error("Error canceling subscription:", error);
    return new NextResponse("Internal Server Error", { status: 500 });
  }
}
