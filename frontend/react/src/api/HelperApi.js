import axios from "axios";

const baseURL = `${import.meta.env.VITE_API_URL}`;

const axiosInstance = axios.create({
  baseURL,
});

// 匯出baseURL的網址字串
export const baseURLAPI = baseURL;

// 匯出已接axios的baseURL
export const apiHelper = axiosInstance;
