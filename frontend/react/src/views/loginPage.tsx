import { useState } from "react";
import { Row, Col, Button, Input } from "antd";
import { EyeOutlined, EyeInvisibleOutlined } from "@ant-design/icons";
import { LoginRequest } from "../types/Api";

const LoginPage = () => {
  const [showPassword, setShowPassword] = useState<boolean>(false);

  const [account, setAccount] = useState<LoginRequest>({
    login: "",
    password: "",
  });

  const loginHandler = (): void => {
    console.log("輸入的帳密", account);
    setAccount({ login: "", password: "" });
  };

  return (
    <>
      <Row justify="center" align="middle" className="h-screen">
        <Col xs={20} sm={16} md={12} xl={8}>
          <form
            action=""
            className="border border-slate-300 rounded-md bg-slate-100 p-6"
          >
            <div className="flex justify-center">登入</div>

            <div className="my-2">
              <label htmlFor="username">帳號</label>
              <Input
                id="username"
                type="text"
                value={account.login}
                onChange={(e) =>
                  setAccount((prev: LoginRequest) => ({
                    ...prev,
                    username: e.target.value,
                  }))
                }
              />
            </div>

            <div className="my-2">
              <label htmlFor="password">密碼</label>
              <div className="relative">
                <Input
                  id="password"
                  type={showPassword ? "text" : "password"}
                  value={account.password}
                  onChange={(e) =>
                    setAccount((prev: LoginRequest) => ({
                      ...prev,
                      password: e.target.value,
                    }))
                  }
                />
                <EyeOutlined
                  style={{ display: showPassword ? "block" : "none" }}
                  className="absolute right-2.5 top-1/2 -translate-y-1/2 cursor-pointer"
                  onClick={() => setShowPassword(!showPassword)}
                />
                <EyeInvisibleOutlined
                  style={{ display: showPassword ? "none" : "block" }}
                  className="absolute right-2.5 top-1/2 -translate-y-1/2 cursor-pointer"
                  onClick={() => setShowPassword(!showPassword)}
                />
              </div>
            </div>

            <Button
              type="primary"
              className="w-full my-2"
              htmlType="button"
              onClick={loginHandler}
            >
              Login
            </Button>
          </form>
        </Col>
      </Row>
    </>
  );
};

export default LoginPage;
