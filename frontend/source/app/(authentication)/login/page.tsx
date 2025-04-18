"use client";
import { useState, useEffect, Suspense } from "react";
import { login } from "@/app/actions/auth";
import Logo from "@/public/logo.svg";
import Image from "next/image";
import Spinner from "@/components/global/Spinner";
import { useSearchParams, useRouter } from "next/navigation";

function LoginForm() {
  const [isLoading, setIsLoading] = useState(false);
  const [status, setStatus] = useState("");
  const searchParams = useSearchParams();
  const router = useRouter();
  const redirect = searchParams.get("redirect");
  const expired = searchParams.get("expired");

  useEffect(() => {
    if (expired === "true") {
      setStatus("Your session has expired. Please log in again.");
    }
  }, [expired]);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setIsLoading(true);
    setStatus("");

    const formData = new FormData(event.currentTarget);

    try {
      const { success, error } = await login(formData);

      if (success) {
        // Use router.push for client-side navigation
        router.push(redirect || "/dashboard");
      } else {
        setStatus(error || "Incorrect username or password");
      }
    } catch (error: any) {
      setStatus(error.message || "An error occurred");
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="flex min-h-full flex-1 flex-col justify-center px-6 py-12 lg:px-8">
      <Image src={Logo} alt="ReptiRealm Logo" className="h-32 w-auto" />

      <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm">
        <form onSubmit={handleSubmit} className="space-y-6">
          <div>
            <label
              htmlFor="email"
              className="block text-sm font-medium text-gray-900"
            >
              Email address
            </label>
            <div className="mt-2">
              <input
                id="email"
                name="email"
                type="email"
                required
                autoComplete="email"
                disabled={isLoading}
                className="block w-full border rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:outline-green-600 disabled:opacity-50"
              />
            </div>
          </div>

          <div>
            <label
              htmlFor="password"
              className="block text-sm font-medium text-gray-900"
            >
              Password
            </label>
            <div className="mt-2">
              <input
                id="password"
                name="password"
                type="password"
                required
                autoComplete="current-password"
                disabled={isLoading}
                className="block w-full border rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:outline-green-600 disabled:opacity-50"
              />
            </div>
          </div>

          <div>
            <button
              type="submit"
              disabled={isLoading}
              className="flex w-full justify-center rounded-md bg-green-600 px-3 py-1.5 text-sm font-semibold text-white shadow-xs hover:bg-green-500 focus-visible:outline-2 focus-visible:outline-green-600 disabled:bg-green-400 disabled:cursor-not-allowed"
            >
              {isLoading ? <Spinner w={20} h={20} /> : "Sign in"}
            </button>
          </div>
          {status && (
            <div className="text-center">
              <p className="text-sm text-red-600">{status}</p>
            </div>
          )}
        </form>
      </div>
    </div>
  );
}

export default function Login() {
  return (
    <Suspense
      fallback={
        <div className="flex min-h-full flex-1 flex-col justify-center items-center px-6 py-12 lg:px-8">
          <Spinner w={40} h={40} />
        </div>
      }
    >
      <LoginForm />
    </Suspense>
  );
}
