"use client";
import ModalWrapper from "@/components/base/ModalWrapper";
import Background from "@/components/effects/Background";

export default function Home() {
  return (
    <div className="w-full h-full bg-slate-900">
      <main className="w-full h-full">
        <Background />
        <ModalWrapper />
      </main>
    </div>
  );
}
