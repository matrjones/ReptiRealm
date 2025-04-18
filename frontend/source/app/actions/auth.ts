import axios from "axios";
import { API_BASE_URL } from "utils/globals";
import Cookies from "js-cookie";

// Helper function to get token from cookies
export function getToken() {
  return Cookies.get("token");
}

// Helper function to set token in cookies
export function setToken(token: string, expiration: string) {
  Cookies.set("token", token, {
    path: "/",
    secure: process.env.NODE_ENV === "production",
    sameSite: "strict",
    expires: new Date(expiration),
  });
}

// Helper function to remove token from cookies
export function removeToken() {
  Cookies.remove("token", { path: "/" });
}

// Helper function to get auth headers
export function getAuthHeaders() {
  const token = getToken();
  return {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  };
}

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

    // Set the token in cookies with proper options
    setToken(data.token, data.expiration);

    return { success: true };
  } catch (error: any) {
    console.error("Login error:", error);
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
    removeToken();
    return { success: true };
  } catch (error: any) {
    console.error("Sign out error:", error);
    return { error: error.message || "An error occurred during sign out" };
  }
}
