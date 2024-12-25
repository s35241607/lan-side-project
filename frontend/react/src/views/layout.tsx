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
import BreadCrumb from "../components/layout/breadCrumb";
import { Button, Layout, Menu, message, Dropdown } from "antd";
import { useNavigate, Outlet } from "react-router-dom";
import { clearToken } from "../stores/userSlice";
import { useDispatch } from "react-redux";
import type { AppDispatch } from "../store";
const { Header, Sider, Content } = Layout;

const LayoutComponent: React.FC = () => {
  const dispatch: AppDispatch = useDispatch();
  const navigate = useNavigate();
  const [collapsed, setCollapsed] = useState(true);

  const logOutHandler = (): void => {
    document.cookie = "token=; max-age=0; path=/;";
    dispatch(clearToken());
    navigate("/login");
    message.success("登出成功");
  };

  const items = {
    siderItems: [
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
    ],
    navItems: [
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
    ],
    userItems: [
      {
        key: "1",
        label: "首頁",
        onClick: () => navigate("/layout/home"),
      },
      {
        key: "2",
        label: "個人資訊",
        onClick: () => navigate("/layout/user-info"),
      },
      {
        key: "3",
        label: "登出",
        onClick: logOutHandler,
      },
    ],
  };

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
          items={items.siderItems}
        />
      </Sider>

      <Layout>
        <Header className="flex items-center justify-between px-0 bg-slate-200 font-bold border-b border-gray-300">
          <Button
            type="text"
            icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
            onClick={() => setCollapsed(!collapsed)}
            className="flex sm:hidden"
          />

          <Menu
            mode="horizontal"
            theme="light"
            defaultSelectedKeys={["1"]}
            className="flex-1 mx-4 justify-end bg-transparent sticky hidden border-0 sm:flex"
            items={items.navItems}
          />

          <Dropdown
            menu={{ items: items.userItems }}
            trigger={["click"]}
            arrow
            placement="bottomRight"
            className="mx-3 cursor-pointer sm:block"
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
            <div className="max-w-full whitespace-normal p-4">
              <BreadCrumb />
              <div className=" flex justify-center">
                <Outlet />
              </div>
            </div>
          </div>
        </Content>
      </Layout>
    </Layout>
  );
};

export default LayoutComponent;
