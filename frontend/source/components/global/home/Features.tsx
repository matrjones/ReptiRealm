"use client";
import { motion } from "framer-motion";
import {
  Calendar,
  Thermometer,
  Droplets,
  Activity,
  Heart,
  Users,
} from "lucide-react";

const features = [
  {
    name: "Health Tracking",
    description:
      "Monitor your reptile's health metrics, weight, and behavior patterns to ensure optimal well-being.",
    icon: Heart,
    color: "bg-pink-500",
  },
  {
    name: "Feeding Schedule",
    description:
      "Set up and track feeding schedules, including food types, quantities, and feeding times.",
    icon: Calendar,
    color: "bg-blue-500",
  },
  {
    name: "Habitat Monitoring",
    description:
      "Track temperature, humidity, and lighting conditions to maintain the perfect habitat.",
    icon: Thermometer,
    color: "bg-orange-500",
  },
  {
    name: "Hydration Tracking",
    description:
      "Monitor water intake and misting schedules to ensure proper hydration.",
    icon: Droplets,
    color: "bg-cyan-500",
  },
  {
    name: "Activity Log",
    description:
      "Record and track your reptile's activity levels and behavior patterns.",
    icon: Activity,
    color: "bg-purple-500",
  },
  {
    name: "Community",
    description:
      "Connect with other reptile enthusiasts, share experiences, and get expert advice.",
    icon: Users,
    color: "bg-green-500",
  },
];

const container = {
  hidden: { opacity: 0 },
  show: {
    opacity: 1,
    transition: {
      staggerChildren: 0.1,
    },
  },
};

const item = {
  hidden: { opacity: 0, y: 20 },
  show: { opacity: 1, y: 0 },
};

export default function Features() {
  return (
    <div className="bg-white py-24 sm:py-32" id="features">
      <div className="mx-auto max-w-7xl px-6 lg:px-8">
        <motion.div
          className="mx-auto max-w-2xl lg:text-center"
          initial={{ opacity: 0, y: 20 }}
          whileInView={{ opacity: 1, y: 0 }}
          viewport={{ once: true }}
          transition={{ duration: 0.8 }}
        >
          <h2 className="text-base font-semibold leading-7 text-green-600">
            Everything you need
          </h2>
          <p className="mt-2 text-3xl font-bold tracking-tight text-gray-900 sm:text-4xl">
            Comprehensive Reptile Care Management
          </p>
          <p className="mt-6 text-lg leading-8 text-gray-600">
            ReptiRealm provides all the tools you need to ensure your reptile
            thrives. From health tracking to habitat management, we've got you
            covered.
          </p>
        </motion.div>
        <motion.div
          className="mx-auto mt-16 max-w-2xl sm:mt-20 lg:mt-24 lg:max-w-4xl"
          variants={container}
          initial="hidden"
          whileInView="show"
          viewport={{ once: true }}
        >
          <dl className="grid max-w-xl grid-cols-1 gap-x-8 gap-y-10 lg:max-w-none lg:grid-cols-2 lg:gap-y-16">
            {features.map((feature) => (
              <motion.div
                key={feature.name}
                className="relative pl-16 group"
                variants={item}
              >
                <dt className="text-base font-semibold leading-7 text-gray-900">
                  <div
                    className={`absolute left-0 top-0 flex h-10 w-10 items-center justify-center rounded-lg ${feature.color} group-hover:scale-110 transition-transform duration-200`}
                  >
                    <feature.icon
                      className="h-6 w-6 text-white"
                      aria-hidden="true"
                    />
                  </div>
                  {feature.name}
                </dt>
                <dd className="mt-2 text-base leading-7 text-gray-600">
                  {feature.description}
                </dd>
              </motion.div>
            ))}
          </dl>
        </motion.div>
      </div>
    </div>
  );
}
