import { NextResponse } from "next/server";
import { stripe } from "@/libs/stripe";
import { NextRequest } from "next/server";
import Stripe from "stripe";

export async function POST(req: NextRequest) {
  try {
    const authHeader = req.headers.get("authorization");

    if (!authHeader) {
      return new NextResponse("Unauthorized", { status: 401 });
    }

    const { priceId } = await req.json();

    // Get customer email from your backend
    const userResponse = await fetch(
      `${process.env.NEXT_PUBLIC_API_URL}/User/GetCurrent`,
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

    let customer;
    if (customers.data.length > 0) {
      customer = customers.data[0];
    } else {
      customer = await stripe.customers.create({
        email: user.email,
      });
    }

    const subscription = await stripe.subscriptions.create({
      customer: customer.id,
      items: [{ price: priceId }],
      payment_behavior: "default_incomplete",
      payment_settings: { save_default_payment_method: "on_subscription" },
      expand: ["latest_invoice.confirmation_secret"],
    });

    const clientSecret = (subscription.latest_invoice as Stripe.Invoice)
      .confirmation_secret?.client_secret;

    // Create subscription in our backend
    const planName = priceId.includes("monthly")
      ? "Premium Monthly"
      : "Premium Yearly";
    const interval = priceId.includes("monthly") ? "month" : "year";

    await fetch(`${process.env.NEXT_PUBLIC_API_URL}/Subscription/Create`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: authHeader,
      },
      body: JSON.stringify({
        email: user.email,
        status: "active",
        interval: interval,
        planName: planName,
      }),
    });

    return NextResponse.json({
      subscriptionId: subscription.id,
      clientSecret,
    });
  } catch (error) {
    console.error("Error creating subscription:", error);
    return new NextResponse("Internal Server Error", { status: 500 });
  }
}
