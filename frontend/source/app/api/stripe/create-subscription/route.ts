import { NextResponse } from "next/server";
import { stripe } from "@/libs/stripe";
import { NextRequest } from "next/server";
import Stripe from "stripe";
import axios from "axios";

export async function POST(req: NextRequest) {
  try {
    if (!stripe) {
      return new NextResponse("Stripe is not configured", { status: 500 });
    }
    const authHeader = req.headers.get("Authorization");

    console.log(authHeader);

    if (!authHeader) {
      return new NextResponse("Unauthorized", { status: 401 });
    }
    const { priceId } = await req.json();

    // Get customer email from your backend
    console.log(authHeader);
    console.log(process.env.NEXT_PUBLIC_API_URL);
    console.log(process.env.NEXT_PUBLIC_API_URL + "/Identity/Get");

    const userResponse = await axios.get(
      `${process.env.NEXT_PUBLIC_API_URL}/Identity/Get`,
      {
        headers: {
          authorization: authHeader,
        },
        httpsAgent: new (require("https").Agent)({
          rejectUnauthorized: false,
        }),
      }
    );

    console.log(userResponse);

    if (userResponse.status !== 200) {
      return new NextResponse("Failed to get user information", {
        status: 401,
      });
    }

    const user = userResponse.data;
    console.log(user.email);
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
    const json = {
      email: user.email,
      status: "active",
      interval: interval,
      planName: planName,
    };

    console.log(JSON.stringify(json));
    await axios.post(
      `${process.env.NEXT_PUBLIC_API_URL}/Subscription/Create`,
      JSON.stringify(json),
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: authHeader,
        },
        httpsAgent: new (require("https").Agent)({
          rejectUnauthorized: false,
        }),
      }
    );

    return NextResponse.json({
      subscriptionId: subscription.id,
      clientSecret,
    });
  } catch (error) {
    console.error("Error creating subscription:", error);
    return new NextResponse("Internal Server Error", { status: 500 });
  }
}
