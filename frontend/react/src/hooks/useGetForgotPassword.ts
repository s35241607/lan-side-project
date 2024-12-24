import baseApi from "../api/baseApi";
import { ForgotPasswordRequest } from "../types/Api";
import { message } from "antd";
// import { useNavigate } from "react-router-dom";

export const useGetForgotPassword = () => {
  // const navigate = useNavigate();
  const fetchForgotPassword = async (data: ForgotPasswordRequest) => {
    try {
      const res = await baseApi.forgotPassword({
        email: data.email,
      });
      if (res.data) {
        console.log("寄mail成功", res.data);
        message.success("寄mail成功");
        // navigate("/login");
      }
    } catch (e: any) {
      console.log(e);
    }
  };
  return { fetchForgotPassword };
};
