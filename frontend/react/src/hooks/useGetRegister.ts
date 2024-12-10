import { useState } from "react";
import baseApi from "../api/baseApi";
import { RegisterRequest } from "../types/Api";

export const useGetRegister = () => {
  const [userData, setUserData] = useState<string>("");
  const fetchRegister = async (data: RegisterRequest) => {
    try {
      const res = await baseApi.register({
        email: data.email,
        username: data.username,
        password: data.password,
      });
      if (res.data) {
        console.log(res.data);
        setUserData(res.data);
      }
    } catch (e) {
      console.error(e);
    }
  };
  return { fetchRegister, userData };
};
