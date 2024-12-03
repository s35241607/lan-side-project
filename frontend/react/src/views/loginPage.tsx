import { useState } from "react";
import { Row, Col, Button, Input } from "antd";
import { EyeOutlined, EyeInvisibleOutlined } from "@ant-design/icons";
import { accountType } from "../types/loginType";

const LoginPage = () => {
  const [showPassword, setShowPassword] = useState<boolean>(false);

  const [account, setAccount] = useState<accountType>({
    username: "",
    password: "",
  });

  const loginHandler = (): void => {
    console.log("輸入的帳密", account);
    setAccount({ username: "", password: "" });
  };

  return (
    <>
      <Row justify="center" align="middle" className="h-100vh">
        <Col xs={20} sm={16} md={12} xl={8}>
          <form action="" className="login-form">
            <div className="d-flex justify-center">登入</div>

            <div className="my-3">
              <label htmlFor="username">帳號</label>
              <Input
                id="username"
                type="text"
                value={account.username}
                className="w-100"
                onChange={(e) =>
                  setAccount((prev: accountType) => ({
                    ...prev,
                    username: e.target.value,
                  }))
                }
              />
            </div>

            <div className="my-3">
              <label htmlFor="password">密碼</label>
              <div className="password-container">
                <Input
                  id="password"
                  type={showPassword ? "text" : "password"}
                  value={account.password}
                  className="w-100"
                  onChange={(e) =>
                    setAccount((prev: accountType) => ({
                      ...prev,
                      password: e.target.value,
                    }))
                  }
                />
                <EyeOutlined
                  style={{ display: showPassword ? "block" : "none" }}
                  className="password-icon"
                  onClick={() => setShowPassword(!showPassword)}
                />
                <EyeInvisibleOutlined
                  style={{ display: showPassword ? "none" : "block" }}
                  className="password-icon"
                  onClick={() => setShowPassword(!showPassword)}
                />
              </div>
            </div>

            <Button
              type="primary"
              className="w-100 my-3"
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
