import React from "react";
import { Row, Col, Form, Input, Button, message } from "antd";
import { LockOutlined } from "@ant-design/icons";
import { useNavigate } from "react-router-dom";
import { useGetResetPassword } from "../hooks/useGetResetPassword";
import { getCookie } from "../utils/getCookie";

const ResetPasswordPage: React.FC = () => {
  const navegate = useNavigate();
  const [form] = Form.useForm();

  interface Password {
    password: string;
    againPassword: string;
  }

  const { fetchResetPassword } = useGetResetPassword();

  // token似乎不能放在redux 還是要從cookie取出才行
  const onFinish = (values: Password) => {
    console.log("您輸入的密碼為", values);

    if (values.password !== values.againPassword) {
      message.error("前後密碼不一致");
      return;
    }

    const token = getCookie("token"); // 不同的token?

    if (token) {
      fetchResetPassword({
        token,
        newPassword: values.password,
      });
    } else {
      message.error("發生錯誤，查無token");
    }
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
            className="border border-slate-300 rounded-md bg-slate-100 p-6 max-w-md"
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
              <Input.Password
                placeholder="請輸入新密碼"
                prefix={<LockOutlined />}
              />
            </Form.Item>

            <Form.Item
              label="再次輸入新密碼"
              name="againPassword"
              rules={[
                { required: true, message: "必填" },
                { min: 8, message: "密碼至少需要8個字元！" },
              ]}
            >
              <Input.Password
                placeholder="請輸入新密碼"
                prefix={<LockOutlined />}
              />
            </Form.Item>

            <Form.Item>
              <Button type="primary" htmlType="submit" block>
                確定
              </Button>
            </Form.Item>

            <div className="w-full my-2 flex justify-center">
              <p
                className=" text-blue-900 cursor-pointer font-bold transition-colors hover:text-blue-700"
                onClick={() => navegate("/login")}
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
