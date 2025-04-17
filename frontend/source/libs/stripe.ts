import { loadStripe } from "@stripe/stripe-js";

const stripePromise = loadStripe(
  process.env.NEXT_PUBLIC_STRIPE_PUBLISHABLE_KEY!
);

export const createCheckoutSession = async (priceId: string) => {
  try {
    const response = await fetch("/api/stripe/create-checkout-session", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ priceId }),
    });

    if (!response.ok) {
      const errorData = await response.json();
      throw new Error(errorData.details || "Failed to create checkout session");
    }

    const { url } = await response.json();

    if (!url) {
      throw new Error("No checkout URL returned");
    }

    // Redirect to the checkout URL
    window.location.href = url;
  } catch (error) {
    console.error("Error creating checkout session:", error);
    throw error;
  }
};

export const getSubscriptionStatus = async () => {
  try {
    const response = await fetch("/api/subscription/status");
    if (!response.ok) {
      throw new Error("Failed to get subscription status");
    }
    return await response.json();
  } catch (error) {
    console.error("Error:", error);
    throw error;
  }
};
