import { loadStripe } from "@stripe/stripe-js";
import Stripe from "stripe";

if (!process.env.STRIPE_SECRET_KEY) {
  throw new Error("STRIPE_SECRET_KEY is not set");
}

if (!process.env.NEXT_PUBLIC_STRIPE_PUBLISHABLE_KEY) {
  throw new Error("NEXT_PUBLIC_STRIPE_PUBLISHABLE_KEY is not set");
}

export const stripe = new Stripe(process.env.STRIPE_SECRET_KEY, {
  apiVersion: "2025-03-31.basil",
});

export const stripePromise = loadStripe(
  process.env.NEXT_PUBLIC_STRIPE_PUBLISHABLE_KEY
);

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
