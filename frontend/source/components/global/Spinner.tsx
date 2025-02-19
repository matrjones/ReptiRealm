export default function Spinner({
  w = 12,
  h = 12,
}: {
  w?: number;
  h?: number;
}) {
  return (
    <div className="flex items-center justify-center">
      <div
        className="inline-block animate-spin rounded-full border-4 border-solid border-current border-e-transparent align-[-0.125em] text-surface motion-reduce:animate-[spin_1.5s_linear_infinite] dark:text-white"
        style={{ width: `${w}px`, height: `${h}px`, borderWidth: `${w / 6}px` }}
        role="status"
      >
        <span className="sr-only">Loading...</span>
      </div>
    </div>
  );
}
