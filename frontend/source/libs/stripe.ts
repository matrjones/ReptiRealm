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

const getCookie = (name: string) => {
  const value = `; ${document.cookie}`;
  const parts = value.split(`; ${name}=`);
  if (parts.length === 2) return parts.pop()?.split(";").shift();
  return null;
};

const parseJwt = (token: string) => {
  try {
    const base64Url = token.split(".")[1];
    const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split("")
        .map(function (c) {
          return "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2);
        })
        .join("")
    );

    return JSON.parse(jsonPayload);
  } catch (e) {
    return null;
  }
};

export const getSubscriptionStatus = async () => {
  try {
    console.log("Making subscription status request...");
    const token = getCookie("token");
    console.log("Raw token:", token);

    if (token) {
      const decodedToken = parseJwt(token);
      console.log("Decoded token:", decodedToken);
      console.log("Token issuer:", decodedToken?.iss);
      console.log("Token audience:", decodedToken?.aud);
    }

    if (!token) {
      throw new Error("No authentication token found in cookies");
    }

    const response = await fetch(
      `${process.env.NEXT_PUBLIC_API_URL}/api/subscription`,
      {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
          Authorization: `Bearer ${token}`,
        },
      }
    );

    console.log("Response status:", response.status);
    console.log(
      "Response headers:",
      Object.fromEntries(response.headers.entries())
    );

    if (!response.ok) {
      if (response.status === 404) {
        // Return default values for users without a subscription
        return {
          status: "inactive",
          plan: "free",
          currentPeriodEnd: new Date(
            Date.now() + 7 * 24 * 60 * 60 * 1000
          ).toISOString(), // 7 days from now
        };
      }
      throw new Error("Failed to get subscription status");
    }

    const subscription = await response.json();
    return {
      status: subscription.status,
      plan: subscription.plan,
      currentPeriodEnd: subscription.currentPeriodEnd,
    };
  } catch (error) {
    console.error("Error:", error);
    throw error;
  }
};
