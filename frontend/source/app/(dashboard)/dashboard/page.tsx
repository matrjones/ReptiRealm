"use client";
import Filter from "@/components/global/Filter";
import ImageNotFound from "@/public/placeholder-image.jpg";
import { useState } from "react";

const reptiles = [
  { name: "Bearded Dragon", temp: "85°F", feedIn: "3 days", progress: "60%" },
  { name: "Leopard Gecko", temp: "78°F", feedIn: "1 day", progress: "90%" },
  { name: "Ball Python", temp: "80°F", feedIn: "5 days", progress: "40%" },
  { name: "Crested Gecko", temp: "75°F", feedIn: "2 days", progress: "70%" },
  { name: "Red-Eared Slider", temp: "78°F", feedIn: "4 days", progress: "50%" },
  { name: "Green Iguana", temp: "88°F", feedIn: "6 days", progress: "30%" },
  {
    name: "Blue-Tongue Skink",
    temp: "82°F",
    feedIn: "2 days",
    progress: "65%",
  },
  { name: "King Cobra", temp: "85°F", feedIn: "7 days", progress: "20%" },
];

export default function Home() {
  const [showFilter, setShowFilter] = useState<boolean>(false);

  return (
    <div className="w-full h-full pl-64 mt-12 p-6">
      <main className="w-full h-full p-5">
        <div className="mb-6">
          <div className="flex items-center justify-between justw-full rounded-xl border border-gray-200 bg-white p-6">
            <h2 className="text-stone-700 text-xl font-bold">Dashboard</h2>
            <div className="flex gap-4">
              <button
                onClick={() => setShowFilter(!showFilter)}
                className="px-4 py-2 bg-gray-200 text-gray-700 rounded-lg shadow-md hover:bg-gray-300 transition"
              >
                Filter
              </button>
              <button
                onClick={() => {
                  window.location.href = "/dashboard/add";
                }}
                className="px-4 py-2 bg-blue-500 text-white rounded-lg shadow-md hover:bg-blue-600 transition"
              >
                Add
              </button>
            </div>
          </div>
        </div>
        {showFilter && <Filter />}
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
          {reptiles.map((reptile, index) => (
            <div key={index} className="bg-white p-6 rounded-lg shadow-md">
              <img
                src={ImageNotFound.src}
                alt={reptile.name}
                className="w-full h-48 object-cover rounded-md"
              />
              <h2 className="text-xl font-semibold mt-4">{reptile.name}</h2>
              <p className="text-gray-500 mt-1">
                Tracking progress & feeding schedule
              </p>
              <div className="mt-4">
                <p className="text-sm text-gray-600 mb-1">
                  Next Feed In: {reptile.feedIn}
                </p>
                <div className="w-full bg-gray-200 rounded-full h-4 overflow-hidden">
                  <div
                    className="h-full bg-green-500 transition-all duration-500"
                    style={{ width: reptile.progress }}
                  ></div>
                </div>
              </div>
              <div className="mt-4 flex justify-between text-sm text-gray-700">
                <span>Temperature:</span>
                <span className="font-medium">{reptile.temp}</span>
              </div>
            </div>
          ))}
        </div>
      </main>
    </div>
  );
}
