import React, { useState } from "react";
import UserImage from "../assets/images/frog.jpg";
import {
  MenuFoldOutlined,
  MenuUnfoldOutlined,
  UploadOutlined,
  UserOutlined,
  VideoCameraOutlined,
  LogoutOutlined,
} from "@ant-design/icons";
import { Button, Layout, Menu, message, Dropdown } from "antd";
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

  const siderItems = [
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
  ];

  const navItems = [
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
  ];

  const userItems = [
    {
      key: "1",
      label: <div>個人資訊</div>,
    },
    {
      key: "2",
      label: <div onClick={logOutHandler}>登出</div>,
    },
  ];

  return (
    <Layout className="h-screen">
      <Sider
        trigger={null}
        collapsed={collapsed}
        breakpoint="lg"
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
          items={siderItems}
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
            items={navItems}
          />

          <Dropdown
            menu={{ items: userItems }}
            placement="bottom"
            className="cursor-pointer sm:block"
          >
            <div className="w-12 h-12 border-2 border-white rounded-full flex items-center justify-center overflow-hidden">
              <img
                src={UserImage}
                alt="UserImage"
                className="object-cover w-full h-full"
              />
            </div>
          </Dropdown>
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
