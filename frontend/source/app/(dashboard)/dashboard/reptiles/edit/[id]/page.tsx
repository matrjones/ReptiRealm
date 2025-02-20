"use client";
import React, { useEffect, useState, use } from "react";
import { ChevronDown, ChevronUp } from "lucide-react";
import { getReptileById } from "@/app/actions/reptile";

const EditSection = ({
  title,
  children,
  expand = false,
}: {
  title: string;
  children: React.ReactNode;
  expand?: boolean;
}) => {
  const [expanded, setExpanded] = useState(expand);

  return (
    <div className="border-b border-gray-300 py-4 bg-white mt-2 ml-4">
      <div
        className="flex justify-between items-center cursor-pointer px-4 py-2 bg-white rounded-md"
        onClick={() => setExpanded(!expanded)}
      >
        <span className="text-lg font-medium">{title}</span>
        {expanded ? <ChevronUp size={20} /> : <ChevronDown size={20} />}
      </div>
      {expanded && (
        <div className="mt-2 px-4 bottom-0 py-2 bg-gray-50 rounded-md">
          {children}
        </div>
      )}
    </div>
  );
};

function EditPage({ params }: { params: Promise<{ id: string }> }) {
  const { id } = use(params); // ✅ Fix: Unwrapping params properly
  const [reptile, setReptile] = useState<any>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchReptile = async () => {
      try {
        const data = await getReptileById(id);
        setReptile(data);
      } catch (error) {
        console.error("Error fetching reptile:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchReptile();
  }, [id]);

  if (loading) {
    return <div className="p-8">Loading...</div>;
  }

  return (
    <div className="w-full h-full pl-64 p-8">
      <div className="flex items-center mb-4 ml-4 justify-between w-full rounded-lg shadow-md border border-gray-200 bg-white p-6">
        <h2 className="text-stone-700 text-xl font-bold">Edit Animal</h2>
        <div className="flex gap-4">
          <button
            onClick={() => {
              window.location.href = "/dashboard/reptiles/add";
            }}
            className="px-4 py-2 bg-green-500 text-white rounded-lg shadow-md hover:bg-green-600 transition"
          >
            Save
          </button>
        </div>
      </div>

      <EditSection title="Profile" expand={true}>
        <div className="space-y-4">
          <div>
            <label className="block text-gray-700 font-medium">Name</label>
            <input
              type="text"
              value={reptile?.name || ""}
              onChange={(e) => setReptile({ ...reptile, name: e.target.value })}
              className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-400 focus:outline-none"
            />
          </div>

          <div>
            <label className="block text-gray-700 font-medium">Species</label>
            <input
              type="text"
              value={reptile?.species?.name || ""}
              onChange={(e) =>
                setReptile({
                  ...reptile,
                  species: { ...reptile.species, name: e.target.value },
                })
              }
              className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-400 focus:outline-none"
            />
          </div>

          <div>
            <label className="block text-gray-700 font-medium">Sex</label>
            <select
              value={reptile?.sex || "m"}
              onChange={(e) => setReptile({ ...reptile, sex: e.target.value })}
              className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-400 focus:outline-none"
            >
              <option value="m">Male</option>
              <option value="f">Female</option>
            </select>
          </div>

          <div>
            <label className="block text-gray-700 font-medium">
              Date of Birth
            </label>
            <input
              type="date"
              value={reptile?.dob || ""}
              onChange={(e) => setReptile({ ...reptile, dob: e.target.value })}
              className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-400 focus:outline-none"
            />
          </div>
        </div>
      </EditSection>

      <EditSection title="Feeds">Feeds edit content here.</EditSection>
      <EditSection title="Morphs">Morphs edit content here.</EditSection>
      <EditSection title="Defications">
        Defications edit content here.
      </EditSection>
      <EditSection title="Weights">Weights edit content here.</EditSection>
    </div>
  );
}

export default EditPage;
