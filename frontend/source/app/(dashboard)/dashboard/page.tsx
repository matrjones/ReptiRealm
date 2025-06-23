"use client";
import { deleteReptileById, getReptiles } from "@/app/actions/reptile";
import Filter from "@/components/global/Filter";
import ReptileCard from "@/components/reptile/ReptileCard";
import { AnimalForm } from "@/types/types";
import { useEffect, useState } from "react";
import { Plus, Filter as FilterIcon, AlertCircle } from "lucide-react";
import { Button } from "@/components/components/ui/button";
import {
  Card,
  CardHeader,
  CardTitle,
  CardContent,
} from "@/components/components/ui/card";
import { Skeleton } from "@/components/components/ui/skeleton";
import { useRouter } from "next/navigation";
import { signOut } from "@/app/actions/auth";
import { motion, AnimatePresence } from "framer-motion";

export default function Home() {
  const [reptiles, setReptiles] = useState<AnimalForm[]>([]);
  const [showFilter, setShowFilter] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const router = useRouter();

  const handleAuthError = async () => {
    await signOut();
    router.push("/login?expired=true");
  };

  const removeReptileFromList = async (id: string): Promise<void> => {
    try {
      await deleteReptileById(id);
      setReptiles((prevReptiles) => prevReptiles.filter((r) => r.id !== id));
    } catch (error: any) {
      if (error.response?.status === 401) {
        handleAuthError();
      } else {
        setError("Failed to delete reptile. Please try again.");
      }
    }
  };

  useEffect(() => {
    const fetchReptiles = async () => {
      setIsLoading(true);
      setError(null);
      try {
        const response = (await getReptiles()) as AnimalForm[];
        setReptiles(response);
      } catch (error: any) {
        if (error.response?.status === 401) {
          handleAuthError();
        } else {
          setError("Failed to load reptiles. Please try again.");
        }
      } finally {
        setIsLoading(false);
      }
    };

    fetchReptiles();
  }, []);

  return (
    <div className="w-full min-h-screen bg-gray-50">
      <main className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <Card className="mb-8 shadow-sm">
          <CardHeader className="pb-4">
            <div className="flex items-center justify-between">
              <div>
                <CardTitle className="text-xl">Your Reptiles</CardTitle>
                <p className="text-sm text-gray-500 mt-1">
                  {reptiles.length}{" "}
                  {reptiles.length === 1 ? "reptile" : "reptiles"} in your
                  collection
                </p>
              </div>
              <div className="flex gap-3">
                <Button
                  variant="outline"
                  onClick={() => setShowFilter(!showFilter)}
                  className="flex items-center gap-2 hover:bg-gray-50 transition-colors"
                >
                  <FilterIcon size={18} />
                  Filter
                </Button>
                <Button
                  onClick={() => router.push("/dashboard/reptiles/add")}
                  className="flex items-center gap-2 bg-green-600 hover:bg-green-700 transition-colors"
                >
                  <Plus size={18} />
                  Add Reptile
                </Button>
              </div>
            </div>
          </CardHeader>
        </Card>

        {/* Filter Section */}
        <AnimatePresence>
          {showFilter && (
            <motion.div
              initial={{ opacity: 0, y: -20 }}
              animate={{ opacity: 1, y: 0 }}
              exit={{ opacity: 0, y: -20 }}
              transition={{ duration: 0.2 }}
            >
              <Filter />
            </motion.div>
          )}
        </AnimatePresence>

        {/* Error Message */}
        {error && (
          <motion.div
            initial={{ opacity: 0, y: -10 }}
            animate={{ opacity: 1, y: 0 }}
            className="mb-6 p-4 bg-red-50 border border-red-200 rounded-lg flex items-center gap-3"
          >
            <AlertCircle className="text-red-500" size={20} />
            <p className="text-red-600">{error}</p>
          </motion.div>
        )}

        {/* Content Grid */}
        {isLoading ? (
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
            {[...Array(6)].map((_, i) => (
              <Card key={i} className="overflow-hidden">
                <Skeleton className="h-48 w-full rounded-t-lg" />
                <div className="p-4">
                  <Skeleton className="h-6 w-3/4 mb-2" />
                  <Skeleton className="h-4 w-1/2" />
                </div>
              </Card>
            ))}
          </div>
        ) : reptiles.length === 0 ? (
          <Card className="p-8 text-center">
            <CardContent className="flex flex-col items-center justify-center gap-4">
              <div className="w-16 h-16 rounded-full bg-gray-100 flex items-center justify-center">
                <Plus className="text-gray-400" size={24} />
              </div>
              <h3 className="text-xl font-semibold text-gray-900">
                No Reptiles Yet
              </h3>
              <p className="text-gray-500 max-w-md">
                Start building your reptile collection by adding your first
                reptile.
              </p>
              <Button
                onClick={() => router.push("/dashboard/reptiles/add")}
                className="mt-4 bg-green-600 hover:bg-green-700"
              >
                Add Your First Reptile
              </Button>
            </CardContent>
          </Card>
        ) : (
          <motion.div
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6"
          >
            {reptiles.map((reptile, index) => (
              <motion.div
                key={reptile.id}
                initial={{ opacity: 0, y: 20 }}
                animate={{ opacity: 1, y: 0 }}
                transition={{ delay: index * 0.1 }}
              >
                <ReptileCard
                  reptile={reptile}
                  onDelete={removeReptileFromList}
                />
              </motion.div>
            ))}
          </motion.div>
        )}
      </main>
    </div>
  );
}
