import React, { useState, useEffect } from "react";
import { Row, Col, Form, Input, Button, message } from "antd";
import { MailOutlined } from "@ant-design/icons";
import { LoginProps } from "../types/props";

const ForgotPassword: React.FC<LoginProps> = ({ setShowForgot }) => {
  const [form] = Form.useForm();

  const [forgetEmail, setForgetEmail] = useState<string>("");

  // 以下等串接丟入api後可以刪除
  useEffect(() => {
    if (forgetEmail) {
      console.log("輸入內容為", forgetEmail);
    }
  }, [forgetEmail]);

  const onFinish = (values: { email: string }) => {
    setForgetEmail(values.email);
    console.log("您輸入的email為", values.email);
    message.success("寄信至email成功");
  };

  const onFinishFailed = (errorInfo: any) => {
    console.log("輸入email失敗:", errorInfo);
    message.error("請檢查是否格式正確");
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
              忘記密碼?
            </div>

            <Form.Item
              // label="Email"
              name="email"
              rules={[
                { required: true, message: "請輸入正確的Email" },
                { type: "email", message: "Email格式要正確！" },
              ]}
            >
              <Input
                placeholder="請輸入帳戶的Email"
                prefix={<MailOutlined />}
              />
            </Form.Item>

            <Form.Item>
              <Button type="primary" htmlType="submit" block>
                發送
              </Button>
            </Form.Item>

            <div className="w-full my-2 flex justify-center">
              <p
                className=" text-blue-900 cursor-pointer font-bold transition-colors hover:text-blue-700"
                onClick={() => setShowForgot(false)}
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

export default ForgotPassword;
