"use client";

export default function Home() {
  return (
    <div className="w-full h-full bg-slate-100 ml-64 p-6">
      <main className="w-full h-full">
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
          <div className="bg-white p-6 rounded-lg shadow-md">
            <img
              src="/images/placeholder.jpg"
              alt="Reptile"
              className="w-full h-48 object-cover rounded-md"
            />
            <h2 className="text-xl font-semibold mt-4">Reptile Name</h2>
            <p className="text-gray-500 mt-1">
              Tracking progress & feeding schedule
            </p>

            <div className="mt-4">
              <p className="text-sm text-gray-600 mb-1">Next Feed In: 2 days</p>
              <div className="w-full bg-gray-200 rounded-full h-4 overflow-hidden">
                <div
                  className="h-full bg-green-500 transition-all duration-500"
                  style={{ width: "50%" }}
                ></div>
              </div>
            </div>

            <div className="mt-4 flex justify-between text-sm text-gray-700">
              <span>Temperature:</span>
              <span className="font-medium">80°F</span>
            </div>
          </div>
        </div>
      </main>
    </div>
  );
}
