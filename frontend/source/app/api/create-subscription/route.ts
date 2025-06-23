import { NextResponse } from "next/server";
import { stripe } from "@/libs/stripe";
import { NextRequest } from "next/server";
import Stripe from "stripe";
import axios from "axios";

export async function POST(req: NextRequest) {
  try {
    console.log("[Stripe] Starting subscription creation process");

    if (!stripe) {
      console.error("[Stripe] Stripe is not configured");
      return new NextResponse("Stripe is not configured", { status: 500 });
    }

    const authHeader = req.headers.get("Authorization");
    console.log(
      "[Stripe] Auth header received:",
      authHeader ? "Present" : "Missing"
    );

    if (!authHeader) {
      console.error("[Stripe] No authorization header provided");
      return new NextResponse("Unauthorized", { status: 401 });
    }

    const { priceId } = await req.json();
    console.log("[Stripe] Price ID received:", priceId);

    console.log("[Stripe] Fetching user information from backend");
    console.log("[Stripe] API URL:", process.env.NEXT_PUBLIC_API_URL);

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

    console.log("[Stripe] User response status:", userResponse.status);
    console.log("[Stripe] User data:", userResponse.data);

    if (userResponse.status !== 200) {
      console.error("[Stripe] Failed to get user information");
      return new NextResponse("Failed to get user information", {
        status: 401,
      });
    }

    const user = userResponse.data;
    console.log("[Stripe] User email:", user.email);

    console.log("[Stripe] Checking for existing Stripe customer");
    const customers = await stripe.customers.list({
      email: user.email,
      limit: 1,
    });

    let customer;
    if (customers.data.length > 0) {
      console.log("[Stripe] Found existing customer");
      customer = customers.data[0];
    } else {
      console.log("[Stripe] Creating new customer");
      customer = await stripe.customers.create({
        email: user.email,
      });
    }
    console.log("[Stripe] Customer ID:", customer.id);

    console.log("[Stripe] Creating subscription");
    const subscription = await stripe.subscriptions.create({
      customer: customer.id,
      items: [{ price: priceId }],
      payment_behavior: "default_incomplete",
      payment_settings: { save_default_payment_method: "on_subscription" },
      expand: ["latest_invoice.confirmation_secret"],
    });

    console.log("[Stripe] Subscription created successfully");
    console.log("[Stripe] Subscription ID:", subscription.id);

    const clientSecret = (subscription.latest_invoice as Stripe.Invoice)
      .confirmation_secret?.client_secret;
    console.log("[Stripe] Client secret generated");

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

    console.log("[Stripe] Creating subscription in backend");
    console.log("[Stripe] Subscription details:", json);

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

    console.log("[Stripe] Backend subscription created successfully");
    console.log("[Stripe] Process completed successfully");

    return NextResponse.json({
      subscriptionId: subscription.id,
      clientSecret,
    });
  } catch (error) {
    console.error("[Stripe] Error in subscription creation:", error);
    return new NextResponse("Internal Server Error", { status: 500 });
  }
}
