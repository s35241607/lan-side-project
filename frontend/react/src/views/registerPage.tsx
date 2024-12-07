import React, { useEffect, useState } from "react";
import { Row, Col, Form, Input, Button, message } from "antd";
import { RegisterRequest } from "../types/Api";
import { useNavigate } from "react-router-dom";

const RegisterPage: React.FC = () => {
  const [form] = Form.useForm();

  const navigate = useNavigate();
  const [account, setAccount] = useState<RegisterRequest>({
    email: "",
    username: "",
    password: "",
  });

  // 以下等串接丟入api後可以刪除
  useEffect(() => {
    if (account.email && account.username && account.password) {
      console.log("輸入內容為", account);
    }
  }, [account]);

  const onFinish = (values: RegisterRequest) => {
    setAccount(values);
    message.success("註冊成功");
  };

  const onFinishFailed = (errorInfo: any) => {
    console.log("表單驗證失敗:", errorInfo);
    message.error("請檢查資料是否輸入完整");
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
          hideRequiredMark={true}
          className="border border-slate-300 rounded-md bg-slate-100 p-6"
        >
          <div className="flex justify-center font-bold text-2xl my-2">
            註冊會員
          </div>

          <Form.Item
            label="使用者名稱"
            name="username"
            rules={[
              { required: true, message: "請輸入使用者名稱" },
              { min: 8, message: "使用者名稱至少需要8個字元！" },
            ]}
          >
            <Input placeholder="請輸入使用者名稱" />
          </Form.Item>

          <Form.Item
            label="Email"
            name="email"
            rules={[
              { required: true, message: "請輸入Email" },
              { min: 3, message: "Email至少需要3個字元！" },
            ]}
          >
            <Input placeholder="請輸入Email" />
          </Form.Item>

          <Form.Item
            label="密碼"
            name="password"
            rules={[
              { required: true, message: "請輸入密碼" },
              { min: 6, message: "密碼至少需要6個字元！" },
            ]}
          >
            <Input.Password placeholder="請輸入密碼" />
          </Form.Item>

          <Form.Item>
            <Button type="primary" htmlType="submit" block>
              確定註冊
            </Button>
          </Form.Item>

          <Button type="primary" block onClick={() => navigate("/loginPage")}>
            返回登入頁面
          </Button>
        </Form>
      </Col>
    </Row>
  );
};

export default RegisterPage;
