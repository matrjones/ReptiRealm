import { useState } from "react";
import { AnimalForm } from "@/types/types";
import ImageNotFound from "@/public/placeholder-image.jpg";
import { GET_GENDER } from "utils/globals";
import { FaMars, FaVenus } from "react-icons/fa";
import { HiDotsVertical } from "react-icons/hi";
import Spinner from "../global/Spinner";
import Link from "next/link";

export default function ReptileCard({
  reptile,
  onDelete,
}: {
  reptile: AnimalForm;
  onDelete?: (id: string) => Promise<void>;
}) {
  const [menuOpen, setMenuOpen] = useState(false);
  const [loading, setIsLoading] = useState(false);

  const deleteReptile = async (id: string) => {
    setIsLoading(true);
    await onDelete?.(id);
    setIsLoading(false);
  };

  return (
    <div className="relative bg-white p-6 rounded-lg shadow-md">
      {loading && (
        <div className="flex-col absolute inset-0 flex items-center justify-center bg-black bg-opacity-30">
          <Spinner w={20} h={20} />
          <h2 className=" text-lg font-semibold text-black">Removing</h2>
        </div>
      )}

      <div className="absolute top-2 right-2">
        <button
          onClick={(e) => {
            e.stopPropagation();
            setMenuOpen(!menuOpen);
          }}
          className="p-2 rounded-full hover:bg-gray-200"
        >
          <HiDotsVertical size={20} />
        </button>
        {menuOpen && (
          <div className="absolute right-0 mt-2 w-24 bg-white shadow-lg rounded-md text-sm">
            <button
              onClick={(e) => {
                e.stopPropagation();
                window.location.href = "/dashboard/reptiles/edit/" + reptile.id;
              }}
              className="block w-full text-left px-4 py-2 hover:bg-gray-100"
            >
              Edit
            </button>
            <button
              onClick={async (e) => {
                e.stopPropagation();
                await deleteReptile(reptile.id as string);
              }}
              className="block w-full text-left px-4 py-2 hover:bg-gray-100"
            >
              Delete
            </button>
          </div>
        )}
      </div>

      <Link href={"/dashboard/reptile/" + reptile.id} className="block">
        <img
          src={ImageNotFound.src}
          alt={reptile.name}
          className="w-full h-48 object-cover rounded-md"
        />

        <h2 className="text-xl font-semibold mt-4">{reptile.name}</h2>

        <div className="flex justify-between items-center">
          <p className="text-gray-500 italic">{reptile.species.name}</p>
          <div
            className={`p-2 rounded-lg flex items-center gap-2 ${
              reptile.sex === "m" ? "bg-blue-500" : "bg-pink-500"
            } text-white`}
          >
            <p className="text-white">{GET_GENDER(reptile.sex)}</p>
            {reptile.sex === "m" ? <FaMars /> : <FaVenus />}
          </div>
        </div>

        {reptile.morphs.length > 0 && (
          <div className="mt-4">
            <p className="text-sm font-semibold text-gray-700">Morphs:</p>
            <div className="flex flex-wrap gap-2 mt-1">
              {reptile.morphs.map((morph, index) => (
                <span
                  key={index}
                  className="bg-gray-200 text-gray-700 px-3 py-1 text-xs rounded-full"
                >
                  {morph.name}
                </span>
              ))}
            </div>
          </div>
        )}

        <div className="mt-4">
          <p className="text-sm text-gray-600 mb-1">
            Next Feed In: {reptile.sex}
          </p>
          <div className="w-full bg-gray-200 rounded-full h-4 overflow-hidden">
            <div
              className="h-full bg-green-500 transition-all duration-500"
              style={{ width: `${reptile.morphs.length * 10}%` }}
            ></div>
          </div>
        </div>

        {reptile.morphs.length > 0 && (
          <div className="mt-4 flex justify-between text-sm text-gray-700">
            <span>Temperature:</span>
            <span className="font-medium">{reptile.morphs[0].name}</span>
          </div>
        )}
      </Link>
    </div>
  );
}
