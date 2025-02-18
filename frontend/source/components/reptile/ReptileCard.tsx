import { AnimalForm } from "@/types/types";
import ImageNotFound from "@/public/placeholder-image.jpg";
import { GET_GENDER } from "utils/globals";
import { FaMars, FaVenus } from "react-icons/fa";

export default function ReptileCard({ reptile }: { reptile: AnimalForm }) {
  return (
    <div className="relative bg-white p-6 rounded-lg shadow-md">
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
    </div>
  );
}
