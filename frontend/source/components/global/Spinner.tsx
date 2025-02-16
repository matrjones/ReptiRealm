export default function Spinner({
  w: w = 12,
  h: h = 12,
}: Readonly<{ w?: number; h?: number }>) {
  return (
    <div className="flex items-center justify-center">
      <div
        className={`inline-block w-${w} h-${h} animate-spin rounded-full border-4 border-solid border-current border-e-transparent align-[-0.125em] text-surface motion-reduce:animate-[spin_1.5s_linear_infinite] dark:text-white`}
        role="status"
      >
        <span className="!absolute !-m-px !h-px !w-px !overflow-hidden !whitespace-nowrap !border-0 !p-0 ![clip:rect(0,0,0,0)]">
          Loading...
        </span>
      </div>
    </div>
  );
}
