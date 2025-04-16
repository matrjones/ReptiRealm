import axios from "axios";
import { API_BASE_URL } from "utils/globals";

export async function login(formData: FormData) {
  const email = formData.get("email") as string;
  const password = formData.get("password") as string;

  if (!email || !password) {
    return { error: "Email and password are required" };
  }

  try {
    const response = await fetch(`${API_BASE_URL}/identity/login`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ email, password }),
    });

    const data = await response.json();

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
  const name = formData.get("name") as string;
  const confirmPassword = formData.get("confirmPassword") as string;

  if (!email || !password || !name || !confirmPassword) {
    return "All fields are required";
  }

  try {
    const response = await axios.post(
      `${API_BASE_URL}/identity/register`,
      {
        email,
        password,
        name,
        confirmPassword,
      },
      { headers: { "Content-Type": "application/json" } }
    );

    return "success";
  } catch (error: any) {
    console.error("Registration error:", error.response?.data || error.message);
    return (
      error.response?.data ||
      error.message ||
      "An error occurred during registration"
    );
  }
}

export async function signOut() {
  try {
    // Clear any stored tokens or user data
    localStorage.removeItem("token");
    localStorage.removeItem("userName");
    return { success: true };
  } catch (error: any) {
    return { error: error.message || "An error occurred during sign out" };
  }
}
