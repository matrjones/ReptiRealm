import { loadStripe } from "@stripe/stripe-js";
import Stripe from "stripe";

// Add logging to verify environment variables
console.log("Stripe secret key available:", !!process.env.STRIPE_SECRET_KEY);
console.log(
  "Stripe publishable key available:",
  !!process.env.NEXT_PUBLIC_STRIPE_PUBLISHABLE_KEY
);

if (!process.env.STRIPE_SECRET_KEY) {
  console.error("STRIPE_SECRET_KEY is not set in environment variables");
}

if (!process.env.NEXT_PUBLIC_STRIPE_PUBLISHABLE_KEY) {
  console.error(
    "NEXT_PUBLIC_STRIPE_PUBLISHABLE_KEY is not set in environment variables"
  );
}

// Initialize stripe client only if secret key is available
export const stripe = process.env.STRIPE_SECRET_KEY
  ? new Stripe(process.env.STRIPE_SECRET_KEY, {
      apiVersion: "2025-03-31.basil",
    })
  : null;

export const stripePromise = process.env.NEXT_PUBLIC_STRIPE_PUBLISHABLE_KEY
  ? loadStripe(process.env.NEXT_PUBLIC_STRIPE_PUBLISHABLE_KEY)
  : null;

export const subscriptionPlans = {
  monthly: {
    priceId: process.env.STRIPE_MONTHLY_PRICE_ID,
    name: "Monthly",
    price: 4.99,
    interval: "month",
  },
  yearly: {
    priceId: process.env.STRIPE_YEARLY_PRICE_ID,
    name: "Yearly",
    price: 49.99,
    interval: "year",
  },
};
