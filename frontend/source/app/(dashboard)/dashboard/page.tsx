import ImageNotFound from "@/public/placeholder-image.jpg";

const reptiles = [
  { name: "Bearded Dragon", temp: "85°F", feedIn: "3 days", progress: "60%" },
  { name: "Leopard Gecko", temp: "78°F", feedIn: "1 day", progress: "90%" },
  { name: "Ball Python", temp: "80°F", feedIn: "5 days", progress: "40%" },
  { name: "Crested Gecko", temp: "75°F", feedIn: "2 days", progress: "70%" },
  { name: "Red-Eared Slider", temp: "78°F", feedIn: "4 days", progress: "50%" },
  { name: "Green Iguana", temp: "88°F", feedIn: "6 days", progress: "30%" },
  {
    name: "Blue-Tongue Skink",
    temp: "82°F",
    feedIn: "2 days",
    progress: "65%",
  },
  { name: "King Cobra", temp: "85°F", feedIn: "7 days", progress: "20%" },
];

export default function Home() {
  return (
    <div className="w-full h-full  pl-64 mt-12 p-6">
      <main className="w-full h-full p-5 ">
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
                  Next Feed In: {reptile.feedIn}
                </p>
                <div className="w-full bg-gray-200 rounded-full h-4 overflow-hidden">
                  <div
                    className="h-full bg-green-500 transition-all duration-500"
                    style={{ width: reptile.progress }}
                  ></div>
                </div>
              </div>
              <div className="mt-4 flex justify-between text-sm text-gray-700">
                <span>Temperature:</span>
                <span className="font-medium">{reptile.temp}</span>
              </div>
            </div>
          ))}
        </div>
      </main>
    </div>
  );
}
