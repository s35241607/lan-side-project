import React, { useState } from "react";
import {
  MenuFoldOutlined,
  MenuUnfoldOutlined,
  UploadOutlined,
  UserOutlined,
  VideoCameraOutlined,
  LogoutOutlined,
} from "@ant-design/icons";
import { Button, Layout, Menu, theme, message } from "antd";
import { useNavigate } from "react-router-dom";
const { Header, Sider, Content } = Layout;

interface LayoutProps {
  children: React.ReactNode;
}

const LayoutComponent: React.FC<LayoutProps> = ({ children }) => {
  const navigate = useNavigate();
  const [collapsed, setCollapsed] = useState(false);
  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();

  const logOutHandler = (): void => {
    document.cookie = "token=; max-age=0; path=/;";
    navigate("/loginPage");
    message.success("登出成功");
  };

  return (
    <Layout className="h-screen">
      <Sider trigger={null} collapsible collapsed={collapsed}>
        <div className="demo-logo-vertical" />
        <Menu
          theme="dark"
          mode="inline"
          defaultSelectedKeys={["1"]}
          onClick={(e) => {
            if (e.key === "4") {
              logOutHandler();
            }
          }}
          items={[
            {
              key: "1",
              icon: <UserOutlined />,
              label: "基本資料",
            },
            {
              key: "2",
              icon: <VideoCameraOutlined />,
              label: "我的作品",
            },
            {
              key: "3",
              icon: <UploadOutlined />,
              label: "新增作品",
            },
            {
              key: "4",
              icon: <LogoutOutlined />,
              label: "LogOut",
            },
          ]}
        />
      </Sider>
      <Layout>
        <Header style={{ padding: 0, background: colorBgContainer }}>
          <Button
            type="text"
            icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
            onClick={() => setCollapsed(!collapsed)}
            style={{
              fontSize: "16px",
              width: 64,
              height: 64,
            }}
          />
        </Header>
        <Content
          style={{
            margin: "24px 16px",
            padding: 24,
            minHeight: 280,
            background: colorBgContainer,
            borderRadius: borderRadiusLG,
          }}
        >
          <div className="w-full h-full overflow-hidden">
            <div className="max-w-full whitespace-normal">{children}</div>
          </div>
        </Content>
      </Layout>
    </Layout>
  );
};

export default LayoutComponent;
