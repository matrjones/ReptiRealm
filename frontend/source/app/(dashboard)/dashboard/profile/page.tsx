"use client";

import { useEffect, useState, useCallback, Suspense } from "react";
import { SubscriptionForm } from "@/components/subscription/SubscriptionForm";
import { SubscriptionDetails } from "@/components/subscription/SubscriptionDetails";
import { LoadingSpinner } from "@/components/subscription/LoadingSpinner";
import { useSearchParams } from "next/navigation";

interface Subscription {
  status: string;
  plan: {
    name: string;
    interval: string;
  };
}

function SubscriptionContent() {
  const [subscription, setSubscription] = useState<Subscription | null>(null);
  const [loading, setLoading] = useState(true);
  const searchParams = useSearchParams();
  const success = searchParams.get("success");

  const fetchSubscription = useCallback(async () => {
    try {
      const response = await fetch("/api/subscription");
      const data = await response.json();
      setSubscription(data.subscription);
    } catch (error) {
      console.error("Error fetching subscription:", error);
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    fetchSubscription();
  }, [fetchSubscription]);

  useEffect(() => {
    if (success === "true") {
      fetchSubscription();
    }
  }, [success, fetchSubscription]);

  const handleCancelSubscription = async () => {
    try {
      await fetch("/api/cancel-subscription", {
        method: "POST",
      });
      setSubscription(null);
    } catch (error) {
      console.error("Error canceling subscription:", error);
    }
  };

  //if (!session) return <SignInPrompt />;
  if (loading) return <LoadingSpinner />;

  return (
    <div className="min-h-screen bg-gray-50 dark:bg-gray-950 py-12">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="text-center mb-12">
          <h1 className="text-3xl font-bold text-gray-900 dark:text-white">
            Subscription Management
          </h1>
          <p className="mt-4 text-lg text-gray-600 dark:text-gray-400">
            Manage your CarWorks subscription and billing information
          </p>
        </div>

        {subscription?.status === "active" && subscription.plan ? (
          <SubscriptionDetails
            subscription={subscription}
            onCancel={handleCancelSubscription}
          />
        ) : (
          <div>
            <h2 className="text-2xl font-bold mb-8 text-gray-900 dark:text-white">
              Choose a Plan
            </h2>
            <SubscriptionForm />
          </div>
        )}
      </div>
    </div>
  );
}

export default function SubscriptionPage() {
  return (
    <Suspense fallback={<LoadingSpinner />}>
      <SubscriptionContent />
    </Suspense>
  );
}
