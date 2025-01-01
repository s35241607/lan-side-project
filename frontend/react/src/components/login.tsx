import React from "react";
import { Row, Col, Form, Input, Button, message } from "antd";
import {
  UserOutlined,
  LockOutlined,
  GoogleSquareFilled,
  GithubFilled,
  FacebookFilled,
} from "@ant-design/icons";
import { LoginRequest } from "../types/Api";
import { useNavigate } from "react-router-dom";
import { useGetLogin } from "../hooks/useGetLogin";

interface LoginProps {
  setShowForgot: React.Dispatch<React.SetStateAction<boolean>>;
}

const LoginForm: React.FC<LoginProps> = ({ setShowForgot }) => {
  const [form] = Form.useForm();

  const navigate = useNavigate();

  const { fetchLogin } = useGetLogin();
  const onFinish = (values: LoginRequest) => {
    fetchLogin(values);
  };

  const onFinishFailed = (errorInfo: any) => {
    console.log("表單驗證失敗:", errorInfo);
    message.error("請檢查是否輸入完整");
  };

  return (
    <Row justify="center" align="middle" className="h-screen">
      <Col xs={20} sm={16} md={12} xl={8}>
        <Form
          form={form}
          name="login_form"
          layout="vertical"
          initialValues={{ remember: true }}
          onFinish={onFinish}
          onFinishFailed={onFinishFailed}
          requiredMark={false}
          className="border border-slate-300 rounded-md bg-slate-100 p-6 max-w-md"
        >
          <div className="flex justify-center font-bold text-2xl my-2">
            會員登入
          </div>
          <Form.Item
            label="帳號/Email"
            name="login"
            rules={[
              { required: true, message: "請輸入帳號/Email" },
              { min: 3, message: "使用者名稱至少需要3個字元！" },
            ]}
          >
            <Input
              placeholder="請輸入使用者名稱/Email"
              prefix={<UserOutlined />}
            />
          </Form.Item>

          <Form.Item
            label="密碼"
            name="password"
            rules={[
              { required: true, message: "請輸入密碼" },
              { min: 8, message: "密碼至少需要8個字元！" },
            ]}
          >
            <Input.Password
              placeholder="請輸入密碼"
              prefix={<LockOutlined />}
            />
          </Form.Item>

          {/* <Form.Item name="remember" valuePropName="checked">
            <Checkbox>記住我</Checkbox>
          </Form.Item> */}

          <Form.Item className="flex justify-end my-4">
            <p
              className="text-slate-400 cursor-pointer transition-colors hover:text-slate-600"
              onClick={() => setShowForgot(true)}
            >
              forgot password?
            </p>
          </Form.Item>

          {/* <div className="flex justify-end my-4">
            <p
              className="text-slate-400 cursor-pointer transition-colors hover:text-slate-600"
              onClick={() => navigate("/resetPasswordPage")}
            >
              重設密碼
            </p>
          </div> */}

          <Form.Item>
            <Button type="primary" htmlType="submit" block>
              Sign In
            </Button>
          </Form.Item>

          <Form.Item>
            <div className="flex items-center justify-center w-full mt-4">
              <div className="flex-1 h-px bg-slate-200" />
              <span className="mx-2 text-slate-500">OR</span>
              <div className="flex-1 h-px bg-slate-200 " />
            </div>
          </Form.Item>

          <Form.Item>
            <div className="flex justify-around mb-4">
              <GoogleSquareFilled className="text-[#4285F4] bg-white text-3xl cursor-pointer" />
              <GithubFilled className="text-3xl text-black cursor-pointer" />
              <FacebookFilled className="text-3xl text-blue-900 cursor-pointer" />
            </div>
          </Form.Item>

          <Button block onClick={() => navigate("/register")}>
            還不是會員? 前往註冊
          </Button>
        </Form>
      </Col>
    </Row>
  );
};

export default LoginForm;
