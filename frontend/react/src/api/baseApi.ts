import { apiHelper } from "./HelperApi";
import {
  RegisterRequest,
  LoginRequest,
  ForgotPasswordRequest,
} from "../types/Api";
export default {
  // 註冊
  register(data: RegisterRequest) {
    return apiHelper.post("auth/register", data);
  },
  // 登入
  login(data: LoginRequest) {
    return apiHelper.post("auth/login", data);
  },
  // 忘記密碼，寄信
  forgotPassword(data: ForgotPasswordRequest) {
    return apiHelper.post("auth/forgot-password", data);
  },
};
