"use client";
import { getReptiles } from "@/app/actions/reptile";
import Filter from "@/components/global/Filter";
import Spinner from "@/components/global/Spinner";
import ReptileCard from "@/components/reptile/ReptileCard";
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
            <div className="bg-gray-300 animate-pulse p-6 rounded-lg shadow-md items-center justify-center flex" />
            <div className="bg-gray-300 animate-pulse p-6 rounded-lg shadow-md items-center justify-center flex" />
            <div className="bg-gray-300 animate-pulse p-6 rounded-lg shadow-md items-center justify-center flex" />
          </div>
        ) : (
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
            {reptiles.map((reptile, index) => (
              <ReptileCard key={index} reptile={reptile} />
            ))}
          </div>
        )}
      </main>
    </div>
  );
}
