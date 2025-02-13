"use client";
import { useState, useEffect, useRef } from "react";
import { motion } from "framer-motion";
import * as signalR from "@microsoft/signalr";
import { useSearchParams } from "next/navigation";
import { ThumbsUp, Heart, Laugh } from "lucide-react";
import { EmojiType, Picker } from "ms-3d-emoji-picker";
import { v4 as uuidv4 } from "uuid";

interface Message {
  guid: string;
  text: string;
  timestamp: string;
  likes: number;
  likedBy: string[];
  reactions: EmojiType[];
  selector: boolean;
  sender: string;
}

export default function ChatPage() {
  const [messages, setMessages] = useState<Message[]>([]);
  const [input, setInput] = useState("");
  const messagesEndRef = useRef<HTMLDivElement | null>(null);
  const [connection, setConnection] = useState<signalR.HubConnection | null>(
    null
  );

  const searchParams = useSearchParams();
  const username = searchParams.get("username") || "Guest";

  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:55161/chatHub")
      .withAutomaticReconnect()
      .build();

    newConnection
      .start()
      .then(() => console.log("Connected to SignalR"))
      .catch((err) => console.error("SignalR Connection Error:", err));

    newConnection.on("ReceiveMessage", (user: string, message: Message) => {
      if (user === username) return;

      console.log(message.guid);

      setMessages((prev) => [...prev, { ...message, sender: user }]);
    });

    setConnection(newConnection);

    return () => {
      newConnection.stop();
    };
  }, [username]);

  useEffect(() => {
    messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
  }, [messages]);

  const handleSendMessage = async () => {
    if (!input.trim() || !connection) return;

    const newMessage: Message = {
      guid: uuidv4(),
      text: input,
      timestamp: new Date().toLocaleTimeString(),
      likes: 0,
      likedBy: [],
      reactions: [],
      selector: false,
      sender: username,
    };

    setMessages((prev) => [...prev, newMessage]);
    setInput("");

    try {
      await connection.invoke("SendMessage", username, newMessage);
    } catch (err) {
      console.error("Error sending message:", err);
    }
  };

  return (
    <div className="w-screen h-screen flex flex-col bg-slate-900 text-white">
      <div className="flex-1 overflow-y-auto p-4 space-y-4">
        {messages.map((msg, index) => (
          <div
            key={msg.guid}
            className={`max-w-lg p-3 rounded-lg flex flex-col ${
              msg.sender === username
                ? "bg-blue-700 ml-auto text-white"
                : "bg-gray-700 mr-auto text-white"
            }`}
          >
            <div className="flex justify-between text-gray-400 text-sm mb-1">
              <span>{msg.sender}</span>
              <span>{msg.timestamp}</span>
            </div>

            <motion.div
              initial={{ opacity: 0, y: 10 }}
              animate={{ opacity: 1, y: 0 }}
              transition={{ duration: 0.3 }}
            >
              {msg.text}
            </motion.div>
            <div className="flex gap-2 mt-1 text-gray-400 self-end">
              <ThumbsUp className="w-5 h-5 cursor-pointer hover:text-blue-400" />
              <Heart className="w-5 h-5 cursor-pointer hover:text-red-400" />
              <Laugh className="w-5 h-5 cursor-pointer hover:text-yellow-400" />
              <div className="fixed my-10">
                <Picker
                  isOpen={true}
                  handleEmojiSelect={(selectedEmoji: EmojiType) =>
                    console.log(selectedEmoji)
                  }
                />
              </div>
            </div>
          </div>
        ))}
        <div ref={messagesEndRef} />
      </div>

      <div className="p-4 border-t border-gray-700 bg-slate-800">
        <div className="flex items-center gap-2">
          <input
            type="text"
            className="flex-1 p-2 bg-gray-900 text-white rounded-lg outline-none border border-gray-600 focus:border-blue-400"
            placeholder="Type a message..."
            value={input}
            onChange={(e) => setInput(e.target.value)}
            onKeyDown={(e) => e.key === "Enter" && handleSendMessage()}
          />
          <button
            onClick={handleSendMessage}
            className="px-4 py-2 bg-blue-600 rounded-lg hover:bg-blue-500 transition"
          >
            Send
          </button>
        </div>
      </div>
    </div>
  );
}
