import React, { useState, useEffect } from "react";
import { Row, Col, Form, Input, Button, message } from "antd";
import { LockOutlined } from "@ant-design/icons";
import { useNavigate } from "react-router-dom";

const ResetPasswordPage: React.FC = () => {
  const navegate = useNavigate();
  const [form] = Form.useForm();

  interface Password {
    password: string;
    againPassword: string;
  }

  const [password, setPassword] = useState<Password>({
    password: "",
    againPassword: "",
  });

  // 以下等串接丟入api後可以刪除
  useEffect(() => {
    if (password) {
      console.log("輸入內容為", password);
    }
  }, [password]);

  const onFinish = (values: Password) => {
    setPassword(values);
    console.log("您輸入的密碼為", values);
    message.success("重設密碼成功");
  };

  const onFinishFailed = (errorInfo: any) => {
    console.log("輸入密碼失敗:", errorInfo);
    message.error("請檢查是否輸入完整");
  };

  return (
    <>
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
            className="border border-slate-300 rounded-md bg-slate-100 p-6"
          >
            <div className="flex justify-center text-2xl mb-4 font-bold">
              重設密碼
            </div>

            <Form.Item
              label="設置新密碼"
              name="password"
              rules={[
                { required: true, message: "必填" },
                { min: 8, message: "密碼至少需要8個字元！" },
              ]}
            >
              <Input placeholder="請輸入新密碼" prefix={<LockOutlined />} />
            </Form.Item>

            <Form.Item
              label="再次輸入新密碼"
              name="againPassword"
              rules={[
                { required: true, message: "必填" },
                { min: 8, message: "密碼至少需要8個字元！" },
              ]}
            >
              <Input placeholder="請輸入新密碼" prefix={<LockOutlined />} />
            </Form.Item>

            <Form.Item>
              <Button type="primary" htmlType="submit" block>
                確定
              </Button>
            </Form.Item>

            <div className="w-full my-2 flex justify-center">
              <p
                className=" text-blue-900 cursor-pointer font-bold transition-colors hover:text-blue-700"
                onClick={() => navegate("/loginPage")}
              >
                回登入頁
              </p>
            </div>
          </Form>
        </Col>
      </Row>
    </>
  );
};

export default ResetPasswordPage;
