import baseApi from "../api/baseApi";
import { RegisterRequest } from "../types/Api";
import { message } from "antd";
import { useNavigate } from "react-router-dom";

export const useGetRegister = () => {
  const navigate = useNavigate();
  const fetchRegister = async (data: RegisterRequest) => {
    try {
      const res = await baseApi.register({
        email: data.email,
        username: data.username,
        password: data.password,
      });
      if (res.data) {
        console.log("註冊成功資料", res.data);
        message.success("註冊成功");
        navigate("/login");
      }
    } catch (e: any) {
      const { code } = e.response.data;
      if (code === "UsernameAlreadyExists") {
        message.error("此用戶名稱已存在");
      } else if (code === "EmailAlreadyExists") {
        message.error("此用戶Email已存在");
      } else {
        message.error("註冊失敗");
      }
    }
  };
  return { fetchRegister };
};
