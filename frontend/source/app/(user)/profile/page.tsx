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
import { Settings, CreditCard, User } from "lucide-react";

export default function ProfilePage() {
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
                <Card className="mt-4">
                  <CardHeader>
                    <CardTitle>Subscription Management</CardTitle>
                  </CardHeader>
                  <CardContent>
                    {/* Add your subscription management UI here */}
                    <div className="space-y-4">
                      <div className="flex items-center justify-between">
                        <div>
                          <h3 className="text-lg font-medium">Current Plan</h3>
                          <p className="text-sm text-muted-foreground">
                            Free Plan
                          </p>
                        </div>
                        <Button variant="default">Upgrade Plan</Button>
                      </div>
                    </div>
                  </CardContent>
                </Card>
              </TabsContent>

              <TabsContent value="account">
                <Card className="mt-4">
                  <CardHeader>
                    <CardTitle>Account Information</CardTitle>
                  </CardHeader>
                  <CardContent>
                    {/* Add your account information UI here */}
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
                    {/* Add your settings UI here */}
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
