"use client";
import { deleteReptileById, getReptiles } from "@/app/actions/reptile";
import Filter from "@/components/global/Filter";
import ReptileCard from "@/components/reptile/ReptileCard";
import { AnimalForm } from "@/types/types";
import { useEffect, useState } from "react";
import { Plus, Filter as FilterIcon } from "lucide-react";
import { Button } from "@/components/components/ui/button";
import { Card, CardHeader, CardTitle } from "@/components/components/ui/card";
import { Skeleton } from "@/components/components/ui/skeleton";

export default function Home() {
  const [reptiles, setReptiles] = useState<AnimalForm[]>([]);
  const [showFilter, setShowFilter] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const removeReptileFromList = async (id: string): Promise<void> => {
    await deleteReptileById(id);
    setReptiles((prevReptiles) => prevReptiles.filter((r) => r.id !== id));
  };

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
    <div className="w-full h-full pl-64">
      <main className="w-full h-full p-5">
        <Card className="mb-6">
          <CardHeader>
            <div className="flex items-center justify-between">
              <CardTitle>Your Reptiles</CardTitle>
              <div className="flex gap-4">
                <Button
                  variant="outline"
                  onClick={() => setShowFilter(!showFilter)}
                  className="flex items-center gap-2"
                >
                  <FilterIcon size={18} />
                  Filter
                </Button>
                <Button
                  onClick={() => {
                    window.location.href = "/dashboard/reptiles/add";
                  }}
                  className="flex items-center gap-2"
                >
                  <Plus size={18} />
                  Add Reptile
                </Button>
              </div>
            </div>
          </CardHeader>
        </Card>
        {showFilter && <Filter />}
        {isLoading ? (
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
            <Skeleton className="h-[400px] rounded-lg" />
            <Skeleton className="h-[400px] rounded-lg" />
            <Skeleton className="h-[400px] rounded-lg" />
          </div>
        ) : (
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
            {reptiles.map((reptile, index) => (
              <ReptileCard
                key={index}
                reptile={reptile}
                onDelete={removeReptileFromList}
              />
            ))}
          </div>
        )}
      </main>
    </div>
  );
}
