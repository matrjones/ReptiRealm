"use client";

import { useEffect } from "react";
import { useRouter } from "next/navigation";
import Cookies from "js-cookie";

export default function AuthGuard({ children }: { children: React.ReactNode }) {
  const router = useRouter();

  useEffect(() => {
    const token = Cookies.get("token");
    if (!token) {
      router.push("/login");
      return;
    }

    try {
      // Parse the token to check expiration
      const tokenData = JSON.parse(atob(token.split(".")[1]));
      const expirationTime = tokenData.exp * 1000; // Convert to milliseconds
      const currentTime = Date.now();

      if (currentTime >= expirationTime) {
        // Token is expired, redirect to login with expired flag
        router.push("/login?expired=true");
      }
    } catch (error) {
      // If token is invalid, redirect to login
      router.push("/login");
    }
  }, [router]);

  return <>{children}</>;
}
