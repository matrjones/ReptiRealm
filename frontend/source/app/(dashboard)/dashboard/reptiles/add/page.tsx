"use client";
import { createReptile } from "@/app/actions/reptile";
import Button from "@/components/base/Button";
import { AnimalForm } from "@/types/types";
import { useState } from "react";

export default function Add() {
  const [isLoading, setIsLoading] = useState(false);
  const [formData, setFormData] = useState<AnimalForm>({
    name: "",
    sex: "m",
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
    e: React.ChangeEvent<
      HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement
    >
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
    <div className="w-full h-full pl-64 mt-12 p-6 bg-gradient-to-br from-gray-50 to-gray-100">
      <div className="max-w-4xl mx-auto">
        <div className="bg-white rounded-2xl shadow-xl overflow-hidden">
          <div className="bg-gradient-to-r from-emerald-600 to-emerald-800 p-8">
            <h2 className="text-3xl font-bold text-white">Add New Reptile</h2>
            <p className="text-emerald-100 mt-2">
              Fill in the details below to add a new reptile to your collection
            </p>
          </div>

          <form onSubmit={handleSubmit} className="p-8 space-y-8">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
              <div className="space-y-2">
                <label className="block text-sm font-medium text-gray-700">
                  Animal Name
                </label>
                <input
                  type="text"
                  name="name"
                  value={formData.name}
                  onChange={handleChange}
                  className="w-full p-3 border border-gray-200 rounded-lg focus:ring-2 focus:ring-emerald-500 focus:border-transparent transition-all duration-200"
                  placeholder="Enter reptile name"
                />
              </div>
              <div className="space-y-2">
                <label className="block text-sm font-medium text-gray-700">
                  Sex
                </label>
                <select
                  name="sex"
                  value={formData.sex}
                  onChange={handleChange}
                  className="w-full p-3 border border-gray-200 rounded-lg focus:ring-2 focus:ring-emerald-500 focus:border-transparent transition-all duration-200 appearance-none bg-white"
                >
                  <option value="m">Male</option>
                  <option value="f">Female</option>
                </select>
              </div>
            </div>

            <div className="space-y-2">
              <label className="block text-sm font-medium text-gray-700">
                Date of Birth
              </label>
              <input
                type="date"
                name="dob"
                value={formData.dob}
                onChange={handleChange}
                className="w-full p-3 border border-gray-200 rounded-lg focus:ring-2 focus:ring-emerald-500 focus:border-transparent transition-all duration-200"
              />
            </div>

            <div className="bg-gray-50 p-6 rounded-xl border border-gray-100">
              <h3 className="text-xl font-semibold text-gray-800 mb-4 flex items-center">
                <svg
                  className="w-5 h-5 mr-2 text-emerald-600"
                  fill="none"
                  stroke="currentColor"
                  viewBox="0 0 24 24"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth="2"
                    d="M19 11H5m14 0a2 2 0 012 2v6a2 2 0 01-2 2H5a2 2 0 01-2-2v-6a2 2 0 012-2m14 0V9a2 2 0 00-2-2M5 11V9a2 2 0 012-2m0 0V5a2 2 0 012-2h6a2 2 0 012 2v2M7 7h10"
                  />
                </svg>
                Species Information
              </h3>
              <div className="space-y-2">
                <label className="block text-sm font-medium text-gray-700">
                  Species Name
                </label>
                <input
                  type="text"
                  name="species.name"
                  value={formData.species.name}
                  onChange={handleChange}
                  className="w-full p-3 border border-gray-200 rounded-lg focus:ring-2 focus:ring-emerald-500 focus:border-transparent transition-all duration-200"
                  placeholder="Enter species name"
                />
              </div>
            </div>

            <div className="bg-gray-50 p-6 rounded-xl border border-gray-100">
              <div className="flex justify-between items-center mb-4">
                <h3 className="text-xl font-semibold text-gray-800 flex items-center">
                  <svg
                    className="w-5 h-5 mr-2 text-emerald-600"
                    fill="none"
                    stroke="currentColor"
                    viewBox="0 0 24 24"
                  >
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      strokeWidth="2"
                      d="M4 6a2 2 0 012-2h2a2 2 0 012 2v2a2 2 0 01-2 2H6a2 2 0 01-2-2V6zM14 6a2 2 0 012-2h2a2 2 0 012 2v2a2 2 0 01-2 2h-2a2 2 0 01-2-2V6zM4 16a2 2 0 012-2h2a2 2 0 012 2v2a2 2 0 01-2 2H6a2 2 0 01-2-2v-2zM14 16a2 2 0 012-2h2a2 2 0 012 2v2a2 2 0 01-2 2h-2a2 2 0 01-2-2v-2z"
                    />
                  </svg>
                  Morphs
                </h3>
                <button
                  type="button"
                  onClick={handleAddMorph}
                  className="px-4 py-2 bg-emerald-600 text-white rounded-lg hover:bg-emerald-700 transition-colors duration-200 flex items-center"
                >
                  <svg
                    className="w-4 h-4 mr-2"
                    fill="none"
                    stroke="currentColor"
                    viewBox="0 0 24 24"
                  >
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      strokeWidth="2"
                      d="M12 4v16m8-8H4"
                    />
                  </svg>
                  Add Morph
                </button>
              </div>
              {formData.morphs.map((morph, index) => (
                <div key={index} className="space-y-4 mb-4">
                  <div className="flex items-center gap-4">
                    <div className="flex-1">
                      <label className="block text-sm font-medium text-gray-700">
                        Morph Name
                      </label>
                      <input
                        type="text"
                        name={`morphs.${index}.name`}
                        value={morph.name}
                        onChange={handleChange}
                        className="w-full p-3 border border-gray-200 rounded-lg focus:ring-2 focus:ring-emerald-500 focus:border-transparent transition-all duration-200"
                        placeholder="Enter morph name"
                      />
                    </div>
                    {formData.morphs.length > 1 && (
                      <button
                        type="button"
                        onClick={() => handleRemoveMorph(index)}
                        className="mt-6 px-3 py-2 text-red-600 hover:text-red-800 transition-colors duration-200 flex items-center"
                      >
                        <svg
                          className="w-4 h-4 mr-1"
                          fill="none"
                          stroke="currentColor"
                          viewBox="0 0 24 24"
                        >
                          <path
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            strokeWidth="2"
                            d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16"
                          />
                        </svg>
                        Remove
                      </button>
                    )}
                  </div>
                </div>
              ))}
            </div>

            <div className="flex justify-end">
              <Button isLoading={isLoading} name="Add Reptile" />
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}
