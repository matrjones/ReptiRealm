"use client";
import { useState } from "react";
import { login } from "@/app/actions/auth";
import Logo from "@/public/logo.svg";
import Image from "next/image";
import Spinner from "@/components/global/Spinner";
import Cookies from "js-cookie";

export default function Login() {
  const [isLoading, setIsLoading] = useState(false);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setIsLoading(true);

    const formData = new FormData(event.currentTarget);
    const { success, token } = await login(formData);

    if (success) {
      Cookies.set("token", token, { path: "/", secure: true });
      await new Promise((resolve) => setTimeout(resolve, 1000));
      window.location.href = "/dashboard";
    }

    setIsLoading(false);
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
              {isLoading ? <Spinner w={5} h={5} /> : "Sign in"}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
