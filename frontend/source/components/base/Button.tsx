import Spinner from "@/components/global/Spinner";

export default function Button({
  isLoading,
  name,
}: {
  isLoading: boolean;
  name: string;
}) {
  return (
    <button
      type="submit"
      disabled={isLoading}
      className="flex w-full justify-center rounded-md bg-green-600 px-3 py-1.5 text-sm font-semibold text-white shadow-xs hover:bg-green-500 focus-visible:outline-2 focus-visible:outline-green-600 disabled:bg-green-400 disabled:cursor-not-allowed"
    >
      {isLoading ? <Spinner w={5} h={5} /> : name}
    </button>
  );
}
