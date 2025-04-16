"use client";
import Logo from "@/public/logo.svg";
import Image from "next/image";
import { useMemo, useState } from "react";
import { signOut } from "@/app/actions/auth";
import {
  LogOut,
  LayoutDashboard,
  Calendar,
  Settings,
  User,
} from "lucide-react";
import { Button } from "@/components/components/ui/button";
import {
  Avatar,
  AvatarFallback,
  AvatarImage,
} from "@/components/components/ui/avatar";
import { Separator } from "@/components/components/ui/separator";
import Link from "next/link";
import { usePathname } from "next/navigation";

function getInitial(name: string) {
  return name.charAt(0).toUpperCase();
}

export default function Navbar() {
  const [userName, setUserName] = useState<string>("");
  const pathname = usePathname();
  const initial = useMemo(() => getInitial(userName), [userName]);

  const handleSignOut = async () => {
    await signOut();
    window.location.href = "/login";
  };

  // Get user's name from localStorage
  useState(() => {
    const name = localStorage.getItem("userName");
    if (name) {
      setUserName(name);
    }
  });

  return (
    <aside className="fixed top-0 left-0 z-40 w-64 h-full pt-8 bg-white border-r border-gray-200">
      <div className="items-center justify-center flex w-full">
        <Image src={Logo} width={100} height={100} alt="Logo" />
      </div>
      <Separator className="my-3" />
      <div className="h-full px-3 pb-4 overflow-y-auto">
        <ul className="space-y-2 font-medium">
          <li>
            <Link
              href="/dashboard"
              className={`flex items-center p-2 text-gray-900 rounded-lg hover:bg-gray-100 group ${
                pathname === "/dashboard" ? "bg-gray-100" : ""
              }`}
            >
              <LayoutDashboard className="w-5 h-5 text-gray-500 group-hover:text-gray-900" />
              <span className="ms-3">Dashboard</span>
            </Link>
          </li>
          <li>
            <Link
              href="/dashboard/calendar"
              className={`flex items-center p-2 text-gray-900 rounded-lg hover:bg-gray-100 group ${
                pathname === "/dashboard/calendar" ? "bg-gray-100" : ""
              }`}
            >
              <Calendar className="w-5 h-5 text-gray-500 group-hover:text-gray-900" />
              <span className="ms-3">Calendar</span>
            </Link>
          </li>
          <li>
            <Link
              href="/dashboard/settings"
              className={`flex items-center p-2 text-gray-900 rounded-lg hover:bg-gray-100 group ${
                pathname === "/dashboard/settings" ? "bg-gray-100" : ""
              }`}
            >
              <Settings className="w-5 h-5 text-gray-500 group-hover:text-gray-900" />
              <span className="ms-3">Settings</span>
            </Link>
          </li>
        </ul>
      </div>
      <div className="absolute bottom-0 left-0 right-0 p-4 border-t border-gray-200">
        <div className="flex items-center justify-between">
          <div className="flex items-center gap-2">
            <Avatar>
              <AvatarImage src="" />
              <AvatarFallback className="bg-primary text-primary-foreground">
                {initial}
              </AvatarFallback>
            </Avatar>
            <div className="flex flex-col">
              <span className="text-sm font-medium">{userName}</span>
              <span className="text-xs text-gray-500">User</span>
            </div>
          </div>
          <Button
            variant="ghost"
            size="icon"
            onClick={handleSignOut}
            className="text-gray-500 hover:text-red-500"
          >
            <LogOut className="h-5 w-5" />
          </Button>
        </div>
      </div>
    </aside>
  );
}
