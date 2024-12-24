import baseApi from "../api/baseApi";
import { ResetPasswordRequest } from "../types/Api";
import { message } from "antd";

export const useGetResetPassword = () => {
  const fetchResetPassword = async (data: ResetPasswordRequest) => {
    try {
      const res = await baseApi.resetPassword(data);
      if (res.data) {
        console.log("寄mail成功", res.data);
        message.success("寄mail成功");
        // navigate("/login");
      }
    } catch (e: any) {
      console.log(e);
    }
  };
  return { fetchResetPassword };
};
