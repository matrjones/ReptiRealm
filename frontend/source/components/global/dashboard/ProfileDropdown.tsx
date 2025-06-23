"use client";

import { useState } from "react";
import { Subscription } from "@/types/types";
import { Button } from "@/components/components/ui/button";
import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
} from "@/components/components/ui/card";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/components/ui/popover";
import { User, CreditCard, LogOut } from "lucide-react";
import Link from "next/link";
import { signOut } from "@/app/actions/auth";

interface ProfileDropdownProps {
  userName: string;
  subscription?: Subscription;
}

export default function ProfileDropdown({
  userName,
  subscription,
}: ProfileDropdownProps) {
  const [isOpen, setIsOpen] = useState(false);

  const handleSignOut = async () => {
    await signOut();
    window.location.href = "/login";
  };

  return (
    <Popover open={isOpen} onOpenChange={setIsOpen}>
      <PopoverTrigger asChild>
        <Button variant="ghost" className="relative h-8 w-8 rounded-full">
          <div className="flex h-8 w-8 items-center justify-center rounded-full bg-primary text-primary-foreground">
            {userName.charAt(0).toUpperCase()}
          </div>
        </Button>
      </PopoverTrigger>
      <PopoverContent className="w-80" align="end">
        <div className="flex flex-col gap-4">
          <div className="flex items-center gap-4">
            <div className="flex h-10 w-10 items-center justify-center rounded-full bg-primary text-primary-foreground">
              {userName.charAt(0).toUpperCase()}
            </div>
            <div className="flex flex-col">
              <span className="text-sm font-medium">{userName}</span>
              <span className="text-xs text-muted-foreground">
                {subscription?.plan === "pro" ? "Pro Plan" : "Free Plan"}
              </span>
            </div>
          </div>

          <Card>
            <CardHeader className="p-4">
              <CardTitle className="text-sm font-medium">
                Subscription
              </CardTitle>
            </CardHeader>
            <CardContent className="p-4 pt-0">
              <div className="flex flex-col gap-2">
                <div className="flex items-center justify-between">
                  <span className="text-sm text-muted-foreground">
                    Current Plan
                  </span>
                  <span className="text-sm font-medium">
                    {subscription?.plan === "pro" ? "Pro" : "Free"}
                  </span>
                </div>
                {subscription?.plan === "pro" && (
                  <div className="flex items-center justify-between">
                    <span className="text-sm text-muted-foreground">
                      Renewal Date
                    </span>
                    <span className="text-sm font-medium">
                      {new Date(
                        subscription.currentPeriodEnd
                      ).toLocaleDateString()}
                    </span>
                  </div>
                )}
              </div>
            </CardContent>
          </Card>

          <div className="flex flex-col gap-2">
            <Link href="/dashboard/profile">
              <Button variant="ghost" className="w-full justify-start">
                <User className="mr-2 h-4 w-4" />
                Profile Settings
              </Button>
            </Link>
            {subscription?.plan !== "pro" && (
              <Link href="/dashboard/upgrade">
                <Button variant="ghost" className="w-full justify-start">
                  <CreditCard className="mr-2 h-4 w-4" />
                  Upgrade to Pro
                </Button>
              </Link>
            )}
            <Button
              variant="ghost"
              className="w-full justify-start text-destructive hover:text-destructive"
              onClick={handleSignOut}
            >
              <LogOut className="mr-2 h-4 w-4" />
              Sign Out
            </Button>
          </div>
        </div>
      </PopoverContent>
    </Popover>
  );
}
