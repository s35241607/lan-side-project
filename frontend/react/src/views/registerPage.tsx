import React from "react";
import { Row, Col, Form, Input, Button, message } from "antd";
import { UserOutlined, LockOutlined, MailOutlined } from "@ant-design/icons";
import { RegisterRequest } from "../types/Api";
import { useNavigate } from "react-router-dom";
import { useGetRegister } from "../hooks/useGetRegister";

const RegisterPage: React.FC = () => {
  const [form] = Form.useForm();

  const navigate = useNavigate();

  const onFinishFailed = async (errorInfo: any) => {
    console.log("表單驗證失敗:", errorInfo);
    message.error("請檢查資料是否輸入完整");
  };

  const { fetchRegister } = useGetRegister();
  const onFinish = async (values: RegisterRequest) => {
    fetchRegister(values);
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
            會員註冊
          </div>

          <Form.Item
            label="使用者名稱"
            name="username"
            rules={[
              { required: true, message: "請輸入使用者名稱" },
              { min: 3, message: "使用者名稱至少需要3個字元！" },
              { max: 20, message: "使用者名稱最多20個字元！" },
            ]}
          >
            <Input placeholder="請輸入使用者名稱" prefix={<UserOutlined />} />
          </Form.Item>

          <Form.Item
            label="Email"
            name="email"
            rules={[
              { required: true, message: "請輸入Email" },
              { type: "email", message: "Email格式要正確！" },
            ]}
          >
            <Input placeholder="請輸入Email" prefix={<MailOutlined />} />
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

          <Form.Item>
            <Button type="primary" htmlType="submit" block>
              確定註冊
            </Button>
          </Form.Item>

          <Button block onClick={() => navigate("/login")}>
            返回登入頁面
          </Button>
        </Form>
      </Col>
    </Row>
  );
};

export default RegisterPage;
