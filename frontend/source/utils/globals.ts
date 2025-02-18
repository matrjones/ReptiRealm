import Cookies from "js-cookie";

export const API_BASE_URL = `${process.env.NEXT_PUBLIC_API_URL}`;
export const GET_TOKEN = Cookies.get("token");
