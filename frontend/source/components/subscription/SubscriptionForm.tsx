"use client";

import { useState } from "react";
import { subscriptionPlans } from "@/libs/stripe";
import { loadStripe } from "@stripe/stripe-js";
import {
  Elements,
  CardElement,
  useStripe,
  useElements,
} from "@stripe/react-stripe-js";
import { Button } from "@/components/components/ui/button";
import { GET_TOKEN } from "utils/globals";

const stripePromise = loadStripe(
  process.env.NEXT_PUBLIC_STRIPE_PUBLISHABLE_KEY!
);

function CheckoutForm({
  plan,
}: {
  plan: (typeof subscriptionPlans)[keyof typeof subscriptionPlans];
}) {
  const stripe = useStripe();
  const elements = useElements();
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    setLoading(true);
    setError(null);

    try {
      // Get user email from API
      const userResponse = await fetch("/api/user", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${GET_TOKEN}`,
        },
      });

      if (!userResponse.ok) {
        throw new Error("Failed to get user information");
      }

      const { email } = await userResponse.json();

      // Create the subscription
      const response = await fetch("/api/create-subscription", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${GET_TOKEN}`,
        },
        body: JSON.stringify({ priceId: plan.priceId }),
      });

      if (!response.ok) {
        throw new Error("Failed to create subscription");
      }

      const { clientSecret } = await response.json();

      // Confirm the payment
      const result = await stripe?.confirmCardPayment(clientSecret, {
        payment_method: {
          card: elements?.getElement(CardElement)!,
          billing_details: {
            email: email,
          },
        },
      });

      if (result?.error) {
        setError(result.error.message || "An error occurred");
        return;
      }

      if (result?.paymentIntent?.status === "succeeded") {
        window.location.href = "/payment-success";
      }
    } catch (error) {
      console.error("Error:", error);
      setError("An error occurred while processing your payment");
    } finally {
      setLoading(false);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <div className="border border-gray-200 dark:border-gray-800 rounded-md p-4 bg-white dark:bg-gray-900">
        <CardElement
          options={{
            style: {
              base: {
                fontSize: "16px",
                color: "#424770",
                "::placeholder": {
                  color: "#aab7c4",
                },
                backgroundColor: "transparent",
              },
              invalid: {
                color: "#9e2146",
              },
            },
          }}
        />
      </div>
      {error && <div className="text-red-500 text-sm">{error}</div>}
      <Button type="submit" disabled={!stripe || loading} className="w-full">
        {loading
          ? "Processing..."
          : `Subscribe for $${plan.price}/${plan.interval}`}
      </Button>
    </form>
  );
}

export function SubscriptionForm() {
  return (
    <div className="grid gap-6 md:grid-cols-2">
      {Object.entries(subscriptionPlans).map(([key, plan]) => (
        <div
          key={key}
          className="flex flex-col p-6 bg-white dark:bg-gray-900 rounded-lg shadow-lg border border-gray-200 dark:border-gray-800"
        >
          <h3 className="text-xl font-bold mb-2 text-gray-900 dark:text-white">
            {plan.name}
          </h3>
          <p className="text-3xl font-bold mb-4 text-gray-900 dark:text-white">
            ${plan.price}
            <span className="text-sm font-normal text-gray-500 dark:text-gray-400">
              /{plan.interval}
            </span>
          </p>
          <ul className="space-y-2 mb-6">
            <li className="flex items-center text-gray-900 dark:text-white">
              <svg
                className="w-5 h-5 text-green-500 mr-2"
                fill="none"
                stroke="currentColor"
                viewBox="0 0 24 24"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth="2"
                  d="M5 13l4 4L19 7"
                />
              </svg>
              Unlimited car searches
            </li>
            <li className="flex items-center text-gray-900 dark:text-white">
              <svg
                className="w-5 h-5 text-green-500 mr-2"
                fill="none"
                stroke="currentColor"
                viewBox="0 0 24 24"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth="2"
                  d="M5 13l4 4L19 7"
                />
              </svg>
              Advanced AI recommendations
            </li>
            <li className="flex items-center text-gray-900 dark:text-white">
              <svg
                className="w-5 h-5 text-green-500 mr-2"
                fill="none"
                stroke="currentColor"
                viewBox="0 0 24 24"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth="2"
                  d="M5 13l4 4L19 7"
                />
              </svg>
              Market value analysis
            </li>
          </ul>
          <Elements stripe={stripePromise}>
            <CheckoutForm plan={plan} />
          </Elements>
        </div>
      ))}
    </div>
  );
}
