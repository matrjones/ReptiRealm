"use client";
import { getReptiles } from "@/app/actions/reptile";
import Filter from "@/components/global/Filter";
import Spinner from "@/components/global/Spinner";
import ImageNotFound from "@/public/placeholder-image.jpg";
import { AnimalForm } from "@/types/types";
import { useEffect, useState } from "react";

export default function Home() {
  const [showFilter, setShowFilter] = useState<boolean>(false);
  const [reptiles, setReptiles] = useState<AnimalForm[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);

  useEffect(() => {
    const fetchReptiles = async () => {
      setIsLoading(true);
      const response = (await getReptiles()) as AnimalForm[];
      setReptiles(response);
      setIsLoading(false);
    };

    fetchReptiles();
  }, []);

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
        {isLoading ? (
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6 h-[400px]">
            <div className="bg-white animate-pulse p-6 rounded-lg shadow-md items-center justify-center flex" />
            <div className="bg-white animate-pulse p-6 rounded-lg shadow-md items-center justify-center flex" />
            <div className="bg-white animate-pulse p-6 rounded-lg shadow-md items-center justify-center flex" />
          </div>
        ) : (
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
                    Next Feed In: {reptile.sex}
                  </p>
                  <div className="w-full bg-gray-200 rounded-full h-4 overflow-hidden">
                    <div
                      className="h-full bg-green-500 transition-all duration-500"
                      style={{ width: reptile.morphs.length }}
                    ></div>
                  </div>
                </div>
                <div className="mt-4 flex justify-between text-sm text-gray-700">
                  <span>Temperature:</span>
                  <span className="font-medium">{reptile.morphs[0].name}</span>
                </div>
              </div>
            ))}
          </div>
        )}
      </main>
    </div>
  );
}
