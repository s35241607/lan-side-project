import { apiHelper } from "./HelperApi";
import { RegisterRequest } from "../types/Api";
export default {
  // 註冊
  register(data: RegisterRequest) {
    return apiHelper.post("/register", data);
  },
};
