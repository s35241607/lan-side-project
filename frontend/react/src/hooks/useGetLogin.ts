import { useState } from "react";
import baseApi from "../api/baseApi";
import { LoginRequest, LoginResponse } from "../types/Api";
import { message } from "antd";
import { useNavigate } from "react-router-dom";

export const useGetLogin = () => {
  const navigate = useNavigate();
  const [token, setToken] = useState<LoginResponse>({}); // 似乎用不到
  const fetchLogin = async (data: LoginRequest) => {
    try {
      const res = await baseApi.login({
        login: data.login,
        password: data.password,
      });
      if (res.data) {
        console.log("登入回復", res.data);
        setToken(res.data);

        const { accessToken } = res.data;
        const tommorrow = new Date();
        tommorrow.setDate(tommorrow.getDate() + 1);
        document.cookie = `token=${accessToken};expires = ${tommorrow.toUTCString()}`;

        message.success("登入成功");
        navigate("/home");
      }
    } catch (e: any) {
      const { code } = e.response.data;
      if (code === "InvalidPassword") {
        message.error("密碼錯誤，請重新輸入！");
      } else if (code === "UserNotFound") {
        message.error("查無此用戶");
      } else {
        message.error("發生其他登入錯誤");
      }
    }
  };
  return { fetchLogin, token };
};
