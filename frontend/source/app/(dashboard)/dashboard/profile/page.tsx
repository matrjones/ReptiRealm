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
import { useEffect, useState } from "react";
import { Badge } from "@/components/components/ui/badge";
import { subscriptionPlans } from "@/libs/stripe";
import Cookies from "js-cookie";

interface Subscription {
  status: string;
  plan: {
    name: string;
    interval: string;
  };
  currentPeriodEnd: string;
}

export default function ProfilePage() {
  const [subscription, setSubscription] = useState<Subscription | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchSubscription = async () => {
      try {
        const token = Cookies.get("token");
        console.log("Token from cookies:", token); // Debug log

        if (!token) {
          console.error("No token found in cookies");
          return;
        }

        const response = await fetch("/api/stripe/subscription", {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });

        if (!response.ok) {
          throw new Error("Failed to fetch subscription");
        }

        const data = await response.json();
        setSubscription(data.subscription);
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
      const response = await fetch("/api/stripe/create-subscription", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${Cookies.get("token")}`,
        },
        body: JSON.stringify({ priceId }),
      });

      if (!response.ok) {
        throw new Error("Failed to create subscription");
      }

      const { clientSecret } = await response.json();
      // Handle Stripe checkout here
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
                                {subscription?.plan.name || "Free"}
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
                    {Object.values(subscriptionPlans).map((plan) => (
                      <Card
                        key={plan.name}
                        className={`${
                          subscription?.plan.name === plan.name.toLowerCase()
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
                                ${plan.price}
                              </span>
                              <span className="text-muted-foreground">
                                /{plan.interval}
                              </span>
                            </div>
                            <ul className="space-y-2">
                              {[
                                "Unlimited reptiles",
                                "Advanced analytics",
                                "Priority support",
                                "Custom themes",
                              ].map((feature) => (
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
                                subscription?.plan.name ===
                                plan.name.toLowerCase()
                                  ? "outline"
                                  : "default"
                              }
                              disabled={
                                subscription?.plan.name ===
                                  plan.name.toLowerCase() ||
                                (subscription?.status === "active" &&
                                  plan.name === "Free")
                              }
                              onClick={() => handleUpgrade(plan.priceId!)}
                            >
                              {subscription?.plan.name ===
                              plan.name.toLowerCase()
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
