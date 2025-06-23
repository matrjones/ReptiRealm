"use client";
import Logo from "@/public/logo.svg";
import Image from "next/image";
import { useEffect, useState } from "react";
import Cookies from "js-cookie";
import Link from "next/link";
import { motion, AnimatePresence } from "framer-motion";
import { Menu, X, ChevronDown } from "lucide-react";

const navigation = [
  { name: "Features", href: "#features" },
  { name: "About", href: "#about" },
  {
    name: "Resources",
    href: "#",
    children: [
      { name: "Care Guides", href: "/guides" },
      { name: "Community", href: "/community" },
      { name: "Blog", href: "/blog" },
    ],
  },
  { name: "Contact", href: "#contact" },
];

export default function Navbar() {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false);
  const [activeDropdown, setActiveDropdown] = useState<string | null>(null);
  const [isScrolled, setIsScrolled] = useState(false);

  useEffect(() => {
    const checkAuth = () => {
      const token = Cookies.get("token");
      if (!token) {
        setIsAuthenticated(false);
        return;
      }

      try {
        // Parse the token to check expiration
        const tokenData = JSON.parse(atob(token.split(".")[1]));
        const expirationTime = tokenData.exp * 1000; // Convert to milliseconds
        const currentTime = Date.now();

        setIsAuthenticated(currentTime < expirationTime);
      } catch (error) {
        setIsAuthenticated(false);
      }
    };

    checkAuth();

    const handleScroll = () => {
      setIsScrolled(window.scrollY > 10);
    };

    window.addEventListener("scroll", handleScroll);
    return () => window.removeEventListener("scroll", handleScroll);
  }, []);

  return (
    <motion.div
      className={`fixed w-full z-50 transition-all duration-300 ${
        isScrolled ? "bg-white/90 backdrop-blur-md shadow-sm" : "bg-transparent"
      }`}
      initial={{ y: -100 }}
      animate={{ y: 0 }}
      transition={{ duration: 0.5 }}
    >
      <nav className="container mx-auto px-4 sm:px-6 lg:px-8">
        <motion.div
          className="flex items-center justify-between"
          animate={{
            height: isScrolled ? "60px" : "80px",
          }}
          transition={{ duration: 0.3 }}
        >
          <motion.div
            className="flex-shrink-0"
            animate={{
              scale: isScrolled ? 0.8 : 1,
            }}
            transition={{ duration: 0.3 }}
          >
            <Link href="/" className="flex items-center">
              <Image
                className="h-14 w-auto"
                alt="ReptiRealm Logo"
                src={Logo}
                priority
              />
            </Link>
          </motion.div>

          {/* Desktop Navigation */}
          <motion.div
            className="hidden lg:flex lg:items-center lg:gap-x-8"
            animate={{
              gap: isScrolled ? "1.5rem" : "2rem",
            }}
            transition={{ duration: 0.3 }}
          >
            {navigation.map((item) => (
              <div
                key={item.name}
                className="relative"
                onMouseEnter={() => setActiveDropdown(item.name)}
                onMouseLeave={() => setActiveDropdown(null)}
              >
                <Link
                  href={item.href}
                  className={`text-sm font-medium text-gray-900 hover:text-green-600 transition-colors duration-200 py-2 flex items-center gap-1 ${
                    isScrolled ? "text-sm" : "text-base"
                  }`}
                >
                  {item.name}
                  {item.children && (
                    <ChevronDown className="h-4 w-4 transition-transform duration-200" />
                  )}
                </Link>

                {item.children && activeDropdown === item.name && (
                  <motion.div
                    initial={{ opacity: 0, y: 10 }}
                    animate={{ opacity: 1, y: 0 }}
                    exit={{ opacity: 0, y: 10 }}
                    transition={{ duration: 0.2 }}
                    className="absolute left-0 mt-2 w-48 rounded-md bg-white shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none"
                  >
                    <div className="py-1">
                      {item.children.map((child) => (
                        <Link
                          key={child.name}
                          href={child.href}
                          className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 hover:text-green-600 transition-colors duration-200"
                        >
                          {child.name}
                        </Link>
                      ))}
                    </div>
                  </motion.div>
                )}
              </div>
            ))}
          </motion.div>

          <motion.div
            className="hidden lg:flex lg:items-center lg:gap-x-6"
            animate={{
              gap: isScrolled ? "1rem" : "1.5rem",
            }}
            transition={{ duration: 0.3 }}
          >
            {isAuthenticated ? (
              <Link
                href="/dashboard"
                className={`inline-flex items-center justify-center rounded-md bg-green-600 px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-green-500 transition-colors duration-200 ${
                  isScrolled ? "text-sm" : "text-base"
                }`}
              >
                Dashboard
              </Link>
            ) : (
              <div className="flex items-center gap-x-4">
                <Link
                  href="/login"
                  className={`text-sm font-semibold text-gray-900 hover:text-green-600 transition-colors duration-200 ${
                    isScrolled ? "text-sm" : "text-base"
                  }`}
                >
                  Sign in
                </Link>
                <Link
                  href="/register"
                  className={`inline-flex items-center justify-center rounded-md bg-green-600 px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-green-500 transition-colors duration-200 ${
                    isScrolled ? "text-sm" : "text-base"
                  }`}
                >
                  Get started
                </Link>
              </div>
            )}
          </motion.div>

          {/* Mobile menu button */}
          <div className="flex lg:hidden">
            <button
              type="button"
              className="-m-2.5 inline-flex items-center justify-center rounded-md p-2.5 text-gray-700"
              onClick={() => setIsMobileMenuOpen(!isMobileMenuOpen)}
            >
              <span className="sr-only">Toggle menu</span>
              {isMobileMenuOpen ? (
                <X className="h-6 w-6" aria-hidden="true" />
              ) : (
                <Menu className="h-6 w-6" aria-hidden="true" />
              )}
            </button>
          </div>
        </motion.div>
      </nav>

      {/* Mobile menu */}
      <AnimatePresence>
        {isMobileMenuOpen && (
          <motion.div
            initial={{ opacity: 0, height: 0 }}
            animate={{ opacity: 1, height: "auto" }}
            exit={{ opacity: 0, height: 0 }}
            transition={{ duration: 0.3 }}
            className="lg:hidden bg-white border-t"
          >
            <div className="space-y-1 px-4 pb-3 pt-2">
              {navigation.map((item) => (
                <div key={item.name}>
                  <button
                    onClick={() =>
                      setActiveDropdown(
                        activeDropdown === item.name ? null : item.name
                      )
                    }
                    className="w-full text-left px-3 py-2 text-base font-medium text-gray-900 hover:bg-gray-50 rounded-md flex items-center justify-between"
                  >
                    {item.name}
                    {item.children && (
                      <ChevronDown
                        className={`h-5 w-5 transition-transform duration-200 ${
                          activeDropdown === item.name ? "rotate-180" : ""
                        }`}
                      />
                    )}
                  </button>
                  {item.children && activeDropdown === item.name && (
                    <motion.div
                      initial={{ opacity: 0, height: 0 }}
                      animate={{ opacity: 1, height: "auto" }}
                      exit={{ opacity: 0, height: 0 }}
                      className="ml-4 space-y-1"
                    >
                      {item.children.map((child) => (
                        <Link
                          key={child.name}
                          href={child.href}
                          className="block px-3 py-2 text-base font-medium text-gray-500 hover:bg-gray-50 hover:text-green-600 rounded-md"
                          onClick={() => setIsMobileMenuOpen(false)}
                        >
                          {child.name}
                        </Link>
                      ))}
                    </motion.div>
                  )}
                </div>
              ))}
              <div className="mt-4 space-y-2">
                {isAuthenticated ? (
                  <Link
                    href="/dashboard"
                    className="block w-full text-center rounded-md bg-green-600 px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-green-500"
                    onClick={() => setIsMobileMenuOpen(false)}
                  >
                    Dashboard
                  </Link>
                ) : (
                  <>
                    <Link
                      href="/login"
                      className="block w-full text-center rounded-md border border-gray-300 px-4 py-2 text-sm font-semibold text-gray-900 shadow-sm hover:bg-gray-50"
                      onClick={() => setIsMobileMenuOpen(false)}
                    >
                      Sign in
                    </Link>
                    <Link
                      href="/register"
                      className="block w-full text-center rounded-md bg-green-600 px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-green-500"
                      onClick={() => setIsMobileMenuOpen(false)}
                    >
                      Get started
                    </Link>
                  </>
                )}
              </div>
            </div>
          </motion.div>
        )}
      </AnimatePresence>
    </motion.div>
  );
}
