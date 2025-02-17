import type { Metadata } from "next";
import "@/styles/globals.css";
import Navbar from "@/components/global/dashboard/Navbar";

export const metadata: Metadata = {
  title: "ReptRealm",
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
        <Navbar />
        {children}
      </body>
    </html>
  );
}
