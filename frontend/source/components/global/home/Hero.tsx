"use client";
import Image from "next/image";
import { motion } from "framer-motion";
import Link from "next/link";
import HeroImage from "@/public/hero.jpg";

export default function Hero() {
  return (
    <div className="relative isolate overflow-hidden bg-white">
      <div className="mx-auto max-w-7xl px-6 pb-24 pt-10 sm:pb-32 lg:flex lg:px-8 lg:py-40">
        <div className="mx-auto max-w-2xl lg:mx-0 lg:max-w-xl lg:flex-shrink-0 lg:pt-8">
          <motion.div
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.8 }}
          >
            <div className="flex items-center gap-x-4">
              <span className="rounded-full bg-green-600/10 px-3 py-1 text-sm font-semibold leading-6 text-green-600 ring-1 ring-inset ring-green-600/10">
                New Release
              </span>
              <span className="text-sm leading-6 text-gray-600">
                Join our growing community
              </span>
            </div>
            <h1 className="mt-8 text-4xl font-bold tracking-tight text-gray-900 sm:text-6xl">
              Your Reptile's Perfect Habitat
            </h1>
            <p className="mt-6 text-lg leading-8 text-gray-600">
              Create the ideal environment for your scaly friends with our
              comprehensive habitat management system. Monitor, control, and
              optimize their living conditions with ease.
            </p>
            <div className="mt-8 flex items-center gap-x-6">
              <Link
                href="/register"
                className="rounded-md bg-green-600 px-4 py-2.5 text-sm font-semibold text-white shadow-sm hover:bg-green-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-green-600 transition-all duration-200"
              >
                Get started
              </Link>
              <Link
                href="#features"
                className="text-sm font-semibold leading-6 text-gray-900 group"
              >
                Learn more{" "}
                <span className="inline-block transition-transform group-hover:translate-x-1">
                  →
                </span>
              </Link>
            </div>
          </motion.div>
        </div>
        <div className="mx-auto mt-16 flex max-w-2xl sm:mt-24 lg:ml-10 lg:mr-0 lg:mt-0 lg:max-w-none lg:flex-none xl:ml-32">
          <motion.div
            initial={{ opacity: 0, scale: 0.9 }}
            animate={{ opacity: 1, scale: 1 }}
            transition={{ delay: 0.5, duration: 0.8 }}
            className="max-w-3xl flex-none sm:max-w-5xl lg:max-w-none"
          >
            <div className="relative">
              <div className="absolute -inset-2 rounded-2xl bg-gradient-to-r from-green-600/20 to-green-400/20 blur-2xl" />
              <Image
                src={HeroImage}
                alt="Reptile care dashboard"
                className="relative rounded-2xl bg-white/5 shadow-2xl ring-1 ring-white/10"
                priority
                width={800}
                height={600}
              />
            </div>
          </motion.div>
        </div>
      </div>
    </div>
  );
}
