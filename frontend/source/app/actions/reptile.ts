import { AnimalForm } from "@/types/types";
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

export async function getReptiles(): Promise<AnimalForm[] | string> {
  try {
    const response = await axios.get(`${API_BASE_URL}/reptile/getall`, {
      headers: {
        "Content-Type": "application/json",
        authorization: `Bearer ${GET_TOKEN}`,
      },
    });

    const data = response.data as AnimalForm[];
    return data;
  } catch (error: any) {
    console.error("Axios error:", error);
    return `An unknown error occurred ${error}`;
  }
}

export async function deleteReptileById(id: string): Promise<boolean | string> {
  try {
    const response = await axios.post(
      `${API_BASE_URL}/Reptile/Delete?id=${id}`,
      null,
      {
        headers: {
          "Content-Type": "application/json",
          authorization: `Bearer ${GET_TOKEN}`,
        },
      }
    );

    const data = response.data;
    return data;
  } catch (error: any) {
    console.error("Axios error:", error);
    return `An unknown error occurred ${error}`;
  }
}

export async function getReptileById(id: string): Promise<boolean | string> {
  try {
    const response = await axios.get(`${API_BASE_URL}/Reptile/Get?id=${id}`, {
      headers: {
        "Content-Type": "application/json",
        authorization: `Bearer ${GET_TOKEN}`,
      },
    });

    const data = response.data;
    return data;
  } catch (error: any) {
    console.error("Axios error:", error);
    return `An unknown error occurred ${error}`;
  }
}
