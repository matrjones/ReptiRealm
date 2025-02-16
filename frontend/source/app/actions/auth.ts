"use server";
import axios from "axios";

export async function login(formData: FormData) {
  const email = formData.get("email") as string;
  const password = formData.get("password") as string;

  if (!email || !password) {
    return { error: "Email and password are required" };
  }

  try {
    const response = await fetch(
      "https://api-stage.pineappleexplorers.com/login",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ email, password }),
      }
    );

    const data = await response.json();
    console.log(data);
    if (!response.ok) {
      throw new Error(data.message || "Login failed");
    }

    return { success: true, token: data.token };
  } catch (error: any) {
    return { error: error.message || "An error occurred" };
  }
}

export async function register(formData: FormData) {
  const email = formData.get("email") as string;
  const password = formData.get("password") as string;

  if (!email || !password) {
    return { error: "Email and password are required" };
  }

  try {
    const response = await axios.post(
      "https://api-stage.pineappleexplorers.com/register",
      { email, password },
      { headers: { "Content-Type": "application/json" } }
    );

    console.log("Response data:", response.data);
    return response.data; // Assuming the API returns { success: true, token: "..." }
  } catch (error: any) {
    console.error("Axios error:", error); // Log full error object

    return {
      error:
        error.response?.data?.message ||
        error.message ||
        "An unknown error occurred",
      status: error.response?.status || 500, // Capture status code
    };
  }
}
