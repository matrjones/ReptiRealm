"use client";
import { createReptile } from "@/app/actions/reptile";
import Button from "@/components/base/Button";
import { AnimalForm } from "@/types/types";
import { useState } from "react";

export default function Add() {
  const [isLoading, setIsLoading] = useState(false);
  const [formData, setFormData] = useState<AnimalForm>({
    name: "",
    sex: "",
    dob: new Date().toISOString().split("T")[0],
    species: {
      name: "",
    },
    morphs: [
      {
        name: "",
      },
    ],
  });

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    const nameParts = name.split(".");

    if (nameParts.length === 2 && nameParts[0] === "species") {
      setFormData((prevData) => ({
        ...prevData,
        species: {
          ...prevData.species,
          [nameParts[1]]: value,
        },
      }));
    } else if (nameParts.length === 3 && nameParts[0] === "morphs") {
      const index = parseInt(nameParts[1], 10);
      setFormData((prevData) => {
        const updatedMorphs = [...prevData.morphs];
        updatedMorphs[index] = {
          ...updatedMorphs[index],
          [nameParts[2]]: value,
        };
        return { ...prevData, morphs: updatedMorphs };
      });
    } else {
      setFormData((prevData) => ({
        ...prevData,
        [name]: value,
      }));
    }
  };

  const handleAddMorph = () => {
    setFormData((prevData) => ({
      ...prevData,
      morphs: [
        ...prevData.morphs,
        {
          name: "",
        },
      ],
    }));
  };

  const handleRemoveMorph = (index: number) => {
    setFormData((prevData) => {
      const updatedMorphs = prevData.morphs.filter((_, i) => i !== index);
      return { ...prevData, morphs: updatedMorphs };
    });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsLoading(true);
    try {
      const response = await createReptile(JSON.stringify(formData));

      console.log(response.error);

      if (response.success) {
        window.location.href = "/dashboard";
      }
    } catch (error: any) {
      setIsLoading(false);
      console.log(error);
    }
  };

  return (
    <div className="w-full h-full pl-64 mt-12 p-6 bg-gray-100">
      <h2 className="text-2xl font-bold ml-4 mt-4">Add Animal Data</h2>
      <form onSubmit={handleSubmit} className="space-y-6 p-4">
        <div className="flex space-x-6">
          <div className="flex-1">
            <label className="block text-sm font-medium text-gray-700">
              Animal Name:
            </label>
            <input
              type="text"
              name="name"
              value={formData.name}
              onChange={handleChange}
              className="w-full mt-2 p-3 border border-gray-300 rounded-md"
            />
          </div>
          <div className="flex-1">
            <label className="block text-sm font-medium text-gray-700">
              Sex:
            </label>
            <input
              type="text"
              name="sex"
              value={formData.sex}
              onChange={handleChange}
              className="w-full mt-2 p-3 border border-gray-300 rounded-md"
            />
          </div>
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700">
            Date of Birth:
          </label>
          <input
            type="date"
            name="dob"
            value={formData.dob}
            onChange={handleChange}
            className="w-full mt-2 p-3 border border-gray-300 rounded-md"
          />
        </div>

        <div>
          <h3 className="text-xl font-semibold text-gray-800 mt-4 mb-2">
            Species
          </h3>
          <label className="block text-sm font-medium text-gray-700">
            Species Name:
          </label>
          <input
            type="text"
            name="species.name"
            value={formData.species.name}
            onChange={handleChange}
            className="w-full mt-2 p-3 border border-gray-300 rounded-md"
          />
        </div>

        <div>
          <h3 className="text-xl font-semibold text-gray-800 mt-4 mb-2">
            Morphs
          </h3>
          {formData.morphs.map((morph, index) => (
            <div key={index} className="space-y-4">
              <div>
                <label className="block text-sm font-medium text-gray-700">
                  Morph Name:
                </label>
                <input
                  type="text"
                  name={`morphs.${index}.name`}
                  value={morph.name}
                  onChange={handleChange}
                  className="w-full mt-2 p-3 border border-gray-300 rounded-md"
                />
                {formData.morphs.length > 1 && (
                  <button
                    type="button"
                    onClick={() => handleRemoveMorph(index)}
                    className="text-red-600 mt-2"
                  >
                    Remove Morph
                  </button>
                )}
              </div>
            </div>
          ))}
          <button
            type="button"
            onClick={handleAddMorph}
            className="px-4 py-2 bg-green-600 text-white rounded-md mt-4"
          >
            Add Morph
          </button>
        </div>

        <div className="flex justify-center mt-6">
          <Button isLoading={isLoading} name={"Add Reptile"} />
        </div>
      </form>
    </div>
  );
}
