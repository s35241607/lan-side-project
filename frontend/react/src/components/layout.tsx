import React, { useState } from "react";
import {
  MenuFoldOutlined,
  MenuUnfoldOutlined,
  UploadOutlined,
  UserOutlined,
  VideoCameraOutlined,
  LogoutOutlined,
} from "@ant-design/icons";
import { Button, Layout, Menu, message } from "antd";
import { useNavigate } from "react-router-dom";
const { Header, Sider, Content } = Layout;

interface LayoutProps {
  children: React.ReactNode;
}

const LayoutComponent: React.FC<LayoutProps> = ({ children }) => {
  const navigate = useNavigate();
  const [collapsed, setCollapsed] = useState(false);

  const logOutHandler = (): void => {
    document.cookie = "token=; max-age=0; path=/;";
    navigate("/loginPage");
    message.success("登出成功");
  };

  return (
    <Layout className="h-screen">
      <Sider
        trigger={null}
        collapsed={collapsed}
        breakpoint="lg"
        // breakpoint="xs" // 設定斷點為最小寬度
        // className="hidden xs:block"
        collapsedWidth="0"
      >
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
        <Header className="flex items-center justify-between px-4 bg-slate-200 font-bold border-b border-gray-300">
          <Button
            type="text"
            icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
            onClick={() => setCollapsed(!collapsed)}
          />

          <Menu
            mode="horizontal"
            theme="light"
            defaultSelectedKeys={["1"]}
            className="flex-1 ml-4 justify-center bg-transparent sticky hidden border-0 sm:flex"
            items={[
              {
                key: "1",
                label: "基本資料",
              },
              {
                key: "2",
                label: "我的作品",
              },
              {
                key: "3",
                label: "新增作品",
              },
            ]}
          />

          <Button
            type="primary"
            onClick={logOutHandler}
            className="hidden sm:block"
          >
            登出
          </Button>
        </Header>

        <Content className="bg-slate-200">
          <div className="w-full h-full overflow-y-auto">
            <div className="max-w-full whitespace-normal p-4">{children}</div>
          </div>
        </Content>
      </Layout>
    </Layout>
  );
};

export default LayoutComponent;
