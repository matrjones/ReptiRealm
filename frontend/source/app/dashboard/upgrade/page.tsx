"use client";

import { useState } from "react";
import { Button } from "@/components/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/components/ui/card";
import { Check } from "lucide-react";

const features = [
  "Unlimited reptile tracking",
  "Advanced habitat monitoring",
  "Custom feeding schedules",
  "Priority support",
  "Data export capabilities",
  "Community access",
];

export default function UpgradePage() {
  const [loading, setLoading] = useState(false);

  const handleUpgrade = async () => {
    setLoading(true);
    try {
      // TODO: Implement Stripe checkout session creation
      const response = await fetch("/api/stripe/create-checkout-session", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          priceId: "price_1REocM4DeWuqPh7puK3n78pG", // Replace with your actual Stripe price ID
        }),
      });

      const { url } = await response.json();
      window.location.href = url;
    } catch (error) {
      console.error("Error creating checkout session:", error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="container mx-auto max-w-4xl py-8">
      <div className="text-center mb-8">
        <h1 className="text-3xl font-bold mb-2">Upgrade to Pro</h1>
        <p className="text-muted-foreground">
          Get access to all premium features and support our development
        </p>
      </div>

      <div className="grid gap-6 md:grid-cols-2">
        <Card>
          <CardHeader>
            <CardTitle>Free Plan</CardTitle>
            <CardDescription>
              Basic features for getting started
            </CardDescription>
          </CardHeader>
          <CardContent>
            <div className="text-3xl font-bold mb-4">$0</div>
            <ul className="space-y-2">
              {features.map((feature, index) => (
                <li
                  key={index}
                  className="flex items-center text-muted-foreground"
                >
                  <Check className="mr-2 h-4 w-4" />
                  {feature}
                </li>
              ))}
            </ul>
          </CardContent>
        </Card>

        <Card className="border-primary">
          <CardHeader>
            <CardTitle>Pro Plan</CardTitle>
            <CardDescription>
              Everything you need for professional reptile care
            </CardDescription>
          </CardHeader>
          <CardContent>
            <div className="text-3xl font-bold mb-4">
              $9.99
              <span className="text-sm font-normal text-muted-foreground">
                /month
              </span>
            </div>
            <ul className="space-y-2">
              {features.map((feature, index) => (
                <li key={index} className="flex items-center">
                  <Check className="mr-2 h-4 w-4 text-primary" />
                  {feature}
                </li>
              ))}
            </ul>
          </CardContent>
          <CardFooter>
            <Button
              className="w-full"
              onClick={handleUpgrade}
              disabled={loading}
            >
              {loading ? "Processing..." : "Upgrade to Pro"}
            </Button>
          </CardFooter>
        </Card>
      </div>
    </div>
  );
}
