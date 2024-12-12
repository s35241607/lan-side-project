import { apiHelper } from "./HelperApi";
import { RegisterRequest, LoginRequest } from "../types/Api";
export default {
  // 註冊
  register(data: RegisterRequest) {
    return apiHelper.post("auth/register", data);
  },
  // 登入
  login(data: LoginRequest) {
    return apiHelper.post("auth/login", data);
  },
};
