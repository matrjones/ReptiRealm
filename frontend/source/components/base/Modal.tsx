"use client";

import React from "react";

interface ModalProps {
  children: React.ReactNode;
  onDone: () => void;
  onClose: () => void;
  isOpen: boolean;
}

const Modal: React.FC<ModalProps> = ({ children, onDone, onClose, isOpen }) => {
  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 flex items-center justify-center bg-opacity-50">
      <div className="bg-slate-800 rounded-lg shadow-lg w-[400px] relative">
        {children}

        <div className="flex-col justify-end gap-2 mt-4">
          <button
            onClick={onDone}
            className="w-full h-[50px] bg-yellow-600 text-white hover:bg-yellow-700 "
          >
            Done
          </button>
        </div>
      </div>
    </div>
  );
};

export default Modal;
