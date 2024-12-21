import { HomeOutlined, UserOutlined } from "@ant-design/icons";
import { Breadcrumb } from "antd";
import { useNavigate, useLocation } from "react-router-dom";

const BreadCrumb = () => {
  const location = useLocation();
  const navigate = useNavigate();

  const BreadcrumbItems = [
    {
      title: (
        <>
          <HomeOutlined />
          <span>首頁</span>
        </>
      ),
      className:
        "cursor-pointer hover:bg-gray-300 px-2 py-1 rounded transition-colors",
      onClick: () => navigate("/layout/home"),
      path: "/layout/home",
    },
    {
      title: (
        <>
          <UserOutlined />
          <span>個人資訊</span>
        </>
      ),
      className:
        "cursor-pointer hover:bg-gray-300 px-2 py-1 rounded transition-colors",
      onClick: () => navigate("/layout/userInfo"),
      path: "/layout/userInfo",
    },
  ];

  const items = [];
  if (location.pathname === "/layout/home") {
    items.push({
      key: "home",
      title: (
        <span
          className={BreadcrumbItems[0].className}
          onClick={BreadcrumbItems[0].onClick}
        >
          {BreadcrumbItems[0].title}
        </span>
      ),
    });
  } else if (location.pathname === "/layout/userInfo") {
    items.push(
      {
        key: "home",
        title: (
          <span
            className={BreadcrumbItems[0].className}
            onClick={BreadcrumbItems[0].onClick}
          >
            {BreadcrumbItems[0].title}
          </span>
        ),
      },
      {
        key: "userInfo",
        title: (
          <span
            className="cursor-pointer hover:bg-gray-300 px-2 py-1 rounded transition-colors"
            onClick={BreadcrumbItems[1].onClick}
          >
            {BreadcrumbItems[1].title}
          </span>
        ),
      }
    );
  }

  return (
    <>
      <Breadcrumb items={items} className="mb-2" />
    </>
  );
};

export default BreadCrumb;
