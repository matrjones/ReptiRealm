import axios from "axios";
import { API_BASE_URL, GET_TOKEN } from "utils/globals";

export async function createReptile(body: string) {
  try {
    const response = await axios.post(`${API_BASE_URL}/reptile/create`, body, {
      headers: {
        "Content-Type": "application/json",
        authorization: `Bearer ${GET_TOKEN}`,
      },
    });

    console.log(response.data);

    return { success: true };
  } catch (error: any) {
    console.error("Axios error:", error);

    return {
      error:
        error.response?.data?.message ||
        error.message ||
        "An unknown error occurred",
      status: error.response?.status || 500,
    };
  }
}
