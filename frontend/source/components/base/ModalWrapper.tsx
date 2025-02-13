"use client";

import React, { useState } from "react";
import Modal from "@/components/base/Modal";

const ModalWrapper: React.FC = () => {
  const [username, setUsername] = useState<string>("");
  const [isOpen, setIsOpen] = useState<boolean>(true);

  const handleDone = () => {
    window.location.href = "/chat?username=" + username;
  };

  const handleClose = () => {
    console.log("Close clicked");
  };

  return (
    <Modal isOpen={isOpen} onDone={handleDone} onClose={handleClose}>
      <div className="w-full h-full bg-slate-800 text-white p-4">
        <h2 className="text-lg font-bold">Username</h2>
        <input
          className="  p-2 text-lg bg-slate-700 text-white w-full mt-2"
          onChange={(e) => setUsername(e.target.value)}
          value={username}
          type="text"
        ></input>
      </div>
    </Modal>
  );
};

export default ModalWrapper;
