"use client";
import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
} from "@/components/components/ui/card";
import { Button } from "@/components/components/ui/button";
import {
  Tabs,
  TabsContent,
  TabsList,
  TabsTrigger,
} from "@/components/components/ui/tabs";
import { Settings, CreditCard, User, Check } from "lucide-react";
import { createCheckoutSession, getSubscriptionStatus } from "@/libs/stripe";
import { useEffect, useState } from "react";
import { Badge } from "@/components/components/ui/badge";

interface SubscriptionStatus {
  status: string;
  plan: string;
  currentPeriodEnd: string;
}

const PLANS = [
  {
    name: "Free",
    price: "£0",
    period: "7 days",
    features: ["Basic reptile tracking", "Up to 3 reptiles", "Basic analytics"],
    priceId: "free",
  },
  {
    name: "Pro",
    price: "£4.99",
    period: "month",
    features: [
      "Unlimited reptile tracking",
      "Unlimited reptiles",
      "Advanced analytics",
      "Custom reminders",
      "Priority support",
    ],
    priceId: "price_1REocM4DeWuqPh7puK3n78pG", // Replace with your actual Stripe price ID
  },
];

export default function ProfilePage() {
  const [subscription, setSubscription] = useState<SubscriptionStatus | null>(
    null
  );
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchSubscription = async () => {
      try {
        const status = await getSubscriptionStatus();
        setSubscription(status);
      } catch (error) {
        console.error("Error fetching subscription:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchSubscription();
  }, []);

  const handleUpgrade = async (priceId: string) => {
    try {
      await createCheckoutSession(priceId);
    } catch (error) {
      console.error("Error creating checkout session:", error);
    }
  };

  return (
    <div className="w-full h-full pl-64">
      <main className="w-full h-full p-5">
        <Card className="mb-6">
          <CardHeader>
            <CardTitle>Profile Settings</CardTitle>
          </CardHeader>
          <CardContent>
            <Tabs defaultValue="subscription" className="w-full">
              <TabsList className="grid w-full grid-cols-3">
                <TabsTrigger
                  value="subscription"
                  className="flex items-center gap-2"
                >
                  <CreditCard size={16} />
                  Subscription
                </TabsTrigger>
                <TabsTrigger
                  value="account"
                  className="flex items-center gap-2"
                >
                  <User size={16} />
                  Account
                </TabsTrigger>
                <TabsTrigger
                  value="settings"
                  className="flex items-center gap-2"
                >
                  <Settings size={16} />
                  Settings
                </TabsTrigger>
              </TabsList>

              <TabsContent value="subscription">
                <div className="space-y-6">
                  <Card>
                    <CardHeader>
                      <CardTitle>Current Plan</CardTitle>
                    </CardHeader>
                    <CardContent>
                      {loading ? (
                        <div>Loading...</div>
                      ) : (
                        <div className="space-y-2">
                          <div className="flex items-center justify-between">
                            <div>
                              <h3 className="text-lg font-medium">
                                {subscription?.plan || "Free"}
                              </h3>
                              <p className="text-sm text-muted-foreground">
                                {subscription?.status === "active"
                                  ? `Renews on ${new Date(
                                      subscription.currentPeriodEnd
                                    ).toLocaleDateString()}`
                                  : "7-day trial"}
                              </p>
                            </div>
                            <Badge
                              variant={
                                subscription?.status === "active"
                                  ? "default"
                                  : "secondary"
                              }
                            >
                              {subscription?.status === "active"
                                ? "Active"
                                : "Trial"}
                            </Badge>
                          </div>
                        </div>
                      )}
                    </CardContent>
                  </Card>

                  <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                    {PLANS.map((plan) => (
                      <Card
                        key={plan.name}
                        className={`${
                          subscription?.plan === plan.name.toLowerCase()
                            ? "border-primary"
                            : ""
                        }`}
                      >
                        <CardHeader>
                          <CardTitle>{plan.name}</CardTitle>
                        </CardHeader>
                        <CardContent>
                          <div className="space-y-4">
                            <div className="flex items-baseline gap-1">
                              <span className="text-3xl font-bold">
                                {plan.price}
                              </span>
                              <span className="text-muted-foreground">
                                /{plan.period}
                              </span>
                            </div>
                            <ul className="space-y-2">
                              {plan.features.map((feature) => (
                                <li
                                  key={feature}
                                  className="flex items-center gap-2"
                                >
                                  <Check className="h-4 w-4 text-primary" />
                                  <span>{feature}</span>
                                </li>
                              ))}
                            </ul>
                            <Button
                              className="w-full"
                              variant={
                                subscription?.plan === plan.name.toLowerCase()
                                  ? "outline"
                                  : "default"
                              }
                              disabled={
                                subscription?.plan ===
                                  plan.name.toLowerCase() ||
                                (subscription?.status === "active" &&
                                  plan.name === "Free")
                              }
                              onClick={() => handleUpgrade(plan.priceId)}
                            >
                              {subscription?.plan === plan.name.toLowerCase()
                                ? "Current Plan"
                                : subscription?.status === "active"
                                ? "Switch Plan"
                                : "Get Started"}
                            </Button>
                          </div>
                        </CardContent>
                      </Card>
                    ))}
                  </div>
                </div>
              </TabsContent>

              <TabsContent value="account">
                <Card className="mt-4">
                  <CardHeader>
                    <CardTitle>Account Information</CardTitle>
                  </CardHeader>
                  <CardContent>
                    <div className="space-y-4">
                      <div>
                        <h3 className="text-lg font-medium">Email</h3>
                        <p className="text-sm text-muted-foreground">
                          user@example.com
                        </p>
                      </div>
                      <div>
                        <h3 className="text-lg font-medium">Account Created</h3>
                        <p className="text-sm text-muted-foreground">
                          January 1, 2024
                        </p>
                      </div>
                    </div>
                  </CardContent>
                </Card>
              </TabsContent>

              <TabsContent value="settings">
                <Card className="mt-4">
                  <CardHeader>
                    <CardTitle>Application Settings</CardTitle>
                  </CardHeader>
                  <CardContent>
                    <div className="space-y-4">
                      <div>
                        <h3 className="text-lg font-medium">Notifications</h3>
                        <p className="text-sm text-muted-foreground">
                          Manage your notification preferences
                        </p>
                      </div>
                      <div>
                        <h3 className="text-lg font-medium">Theme</h3>
                        <p className="text-sm text-muted-foreground">
                          Choose your preferred theme
                        </p>
                      </div>
                    </div>
                  </CardContent>
                </Card>
              </TabsContent>
            </Tabs>
          </CardContent>
        </Card>
      </main>
    </div>
  );
}
