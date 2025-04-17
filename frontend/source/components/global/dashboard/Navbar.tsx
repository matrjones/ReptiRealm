"use client";
import Logo from "@/public/logo.svg";
import Image from "next/image";
import { useEffect, useState } from "react";
import {
  LayoutDashboard,
  Calendar,
  Settings,
  User,
  LogOut,
  ChevronDown,
} from "lucide-react";
import { Separator } from "@/components/components/ui/separator";
import Link from "next/link";
import { usePathname, useRouter } from "next/navigation";
import { cn } from "@/libs/utils";
import { Subscription } from "@/types/types";
import { motion, AnimatePresence } from "framer-motion";

function getInitial(name: string | null) {
  return name ? name.charAt(0).toUpperCase() : "U";
}

interface NavItem {
  href: string;
  label: string;
  icon: React.ReactNode;
}

const navItems: NavItem[] = [
  {
    href: "/dashboard",
    label: "Dashboard",
    icon: <LayoutDashboard className="w-5 h-5" />,
  },
  {
    href: "/dashboard/calendar",
    label: "Calendar",
    icon: <Calendar className="w-5 h-5" />,
  },
  {
    href: "/dashboard/settings",
    label: "Settings",
    icon: <Settings className="w-5 h-5" />,
  },
];

export default function Navbar() {
  const [userName, setUserName] = useState<string | null>(null);
  const [subscription, setSubscription] = useState<Subscription | undefined>(
    undefined
  );
  const [isProfileOpen, setIsProfileOpen] = useState(false);
  const pathname = usePathname();
  const router = useRouter();
  const initial = getInitial(userName);

  useEffect(() => {
    const name = localStorage.getItem("userName");
    const sub = localStorage.getItem("subscription");
    if (name) {
      setUserName(name);
    }
    if (sub) {
      try {
        setSubscription(JSON.parse(sub));
      } catch (error) {
        console.error("Error parsing subscription:", error);
      }
    }
  }, []);

  const handleLogout = () => {
    localStorage.removeItem("userName");
    localStorage.removeItem("subscription");
    router.push("/login");
  };

  return (
    <nav className="fixed left-0 top-0 h-full w-64 bg-white border-r shadow-sm">
      <div className="flex flex-col h-full">
        {/* Logo and main navigation */}
        <div className="flex-1">
          <div className="items-center justify-center flex w-full py-4">
            <Image
              src={Logo}
              width={100}
              height={100}
              alt="Logo"
              className="hover:scale-105 transition-transform"
            />
          </div>
          <Separator className="my-3" />
          <div className="h-full px-3 pb-4 overflow-y-auto">
            <ul className="space-y-2">
              {navItems.map((item) => (
                <li key={item.href}>
                  <Link
                    href={item.href}
                    className={cn(
                      "flex items-center p-2 text-gray-900 rounded-lg hover:bg-gray-100 group transition-colors",
                      pathname === item.href && "bg-gray-100"
                    )}
                  >
                    <span className="text-gray-500 group-hover:text-gray-900 transition-colors">
                      {item.icon}
                    </span>
                    <span className="ms-3">{item.label}</span>
                  </Link>
                </li>
              ))}
            </ul>
          </div>
        </div>

        {/* Profile section */}
        <div className="border-t p-4">
          <div className="space-y-2">
            <button
              onClick={() => setIsProfileOpen(!isProfileOpen)}
              className="flex items-center justify-between w-full p-2 text-sm font-medium text-gray-700 hover:text-gray-900 rounded-lg hover:bg-gray-100 transition-colors"
            >
              <div className="flex items-center gap-2">
                <div className="w-8 h-8 rounded-full bg-primary flex items-center justify-center text-white">
                  {initial}
                </div>
                <span>{userName || "User"}</span>
              </div>
              <ChevronDown
                className={cn(
                  "w-4 h-4 transition-transform",
                  isProfileOpen && "rotate-180"
                )}
              />
            </button>

            <AnimatePresence>
              {isProfileOpen && (
                <motion.div
                  initial={{ opacity: 0, height: 0 }}
                  animate={{ opacity: 1, height: "auto" }}
                  exit={{ opacity: 0, height: 0 }}
                  transition={{ duration: 0.2 }}
                  className="space-y-2"
                >
                  <Link
                    href="/dashboard/profile"
                    className="flex items-center gap-2 text-sm font-medium text-gray-700 hover:text-gray-900 p-2 rounded-lg hover:bg-gray-100 transition-colors"
                  >
                    <User size={16} />
                    Profile
                  </Link>
                  <Link
                    href="/dashboard/profile?tab=settings"
                    className="flex items-center gap-2 text-sm font-medium text-gray-700 hover:text-gray-900 p-2 rounded-lg hover:bg-gray-100 transition-colors"
                  >
                    <Settings size={16} />
                    Settings
                  </Link>
                  <button
                    onClick={handleLogout}
                    className="flex items-center gap-2 text-sm font-medium text-red-600 hover:text-red-700 w-full p-2 rounded-lg hover:bg-red-50 transition-colors"
                  >
                    <LogOut size={16} />
                    Logout
                  </button>
                </motion.div>
              )}
            </AnimatePresence>
          </div>
        </div>
      </div>
    </nav>
  );
}
