import { useState } from "react";
import baseApi from "../api/baseApi";
import { ForgotPasswordRequest } from "../types/Api";
import { message } from "antd";
// import { useNavigate } from "react-router-dom";

export const useGetForgotPassword = () => {
  // const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const fetchForgotPassword = async (data: ForgotPasswordRequest) => {
    try {
      setLoading(true);
      const res = await baseApi.forgotPassword({
        email: data.email,
      });
      if (res.data) {
        console.log("寄重設密碼至Email", res.data);
        message.success("已寄信至Email，請查閱");
        // navigate("/login");
      }
    } catch (e: any) {
      console.log(e);
    } finally {
      setLoading(false);
    }
  };
  return { fetchForgotPassword, loading };
};
