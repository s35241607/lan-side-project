import { Breadcrumb } from "antd";
import { Link, useLocation } from "react-router-dom";
import { HomeOutlined } from "@ant-design/icons";

// Record用法:key類型,value類型
const breadcrumbMap: Record<string, string | JSX.Element> = {
  "/layout": <HomeOutlined />,
  "/layout/home": "首頁",
  "/layout/user-info": "個人資訊",
};

const BreadCrumb = () => {
  const location = useLocation();

  // 將路徑分割成階層
  const pathSnippets = location.pathname.split("/").filter((i) => i);

  // 生成麵包屑數據
  const breadcrumbItems = pathSnippets.map((_, index) => {
    const url = `/${pathSnippets.slice(0, index + 1).join("/")}`;

    // 最後一個項目不要用Link，才能帶出內建的黑字體
    const lastItem = index === pathSnippets.length - 1;
    return {
      key: url,
      title: lastItem ? (
        breadcrumbMap[url]
      ) : (
        <Link to={url}>{breadcrumbMap[url]}</Link>
      ),
    };
  });

  return (
    <div>
      <Breadcrumb items={breadcrumbItems} />
    </div>
  );
};

export default BreadCrumb;
