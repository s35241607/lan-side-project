import axios from "axios";

const baseURL = `${import.meta.env.VITE_API_URL}/api/v1/`;

const axiosInstance = axios.create({
  baseURL,
});

// 登入之後有了cookie，進入使用者介面才需要帶上
// axiosInstance.interceptors.request.use((config) => {
//   const token = document.cookie
//     .split("; ")
//     .find((row) => row.startsWith("token="))
//     ?.split("=")[1];

//   if (token) {
//     config.headers.Authorization = `Bearer ${token}`;
//   }
//   return config;
// });

// 匯出baseURL的網址字串
export const baseURLAPI = baseURL;

// 匯出已接axios的baseURL
export const apiHelper = axiosInstance;
