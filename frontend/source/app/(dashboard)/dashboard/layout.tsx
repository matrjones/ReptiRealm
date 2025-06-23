import type { Metadata } from "next";
import "@/styles/globals.css";
import Navbar from "@/components/global/dashboard/Navbar";
import AuthGuard from "@/components/global/dashboard/AuthGuard";

export const metadata: Metadata = {
  title: "ReptiRealm",
  description: "A reptile tracking app",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body className="bg-slate-100">
        <AuthGuard>
          <Navbar />
          {children}
        </AuthGuard>
      </body>
    </html>
  );
}
