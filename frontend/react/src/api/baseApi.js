import { apiHelper } from "./HelperApi";

export default {
  // 註冊
  register(data) {
    return apiHelper.post("/register", data);
  },
};
