import React from "react";
import { Row, Col, Button } from "antd";
import UserImage from "../../assets/images/frog.jpg";

const UserInfo: React.FC = () => {
  return (
    <Row justify="center" align="middle" className="m-4 w-full sm:w-10/12">
      <Col
        xs={24}
        sm={8}
        className="flex justify-center items-center sm:justify-start"
      >
        <div className="relative w-40 min-w-[115px] min-h-[115px] border-4 border-white overflow-hidden">
          <img
            src={UserImage}
            alt="UserImage"
            className="object-cover w-full h-full"
          />
        </div>
      </Col>
      <Col xs={24} sm={16} className="flex justify-center items-center">
        <div className="w-full">
          <div className="flex justify-center">
            <p className="font-bold">User Id</p>
          </div>
          <div className="flex flex-col justify-center items-center mt-4 sm:flex-row">
            <Button type="primary" ghost className="m-1">
              編輯頭像
            </Button>
            <Button type="primary" ghost className="m-1">
              變更密碼
            </Button>
            <Button type="primary" ghost className="m-1">
              變更Email
            </Button>
          </div>
        </div>
      </Col>
    </Row>
  );
};
export default UserInfo;
