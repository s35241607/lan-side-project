import React, { useState } from "react";
import { Row, Col, Button } from "antd";
import UserImage from "../../assets/images/frog.jpg";
import ChangeImage from "./changeImage";

const UserInfo: React.FC = () => {
  const [avatarUrl, setAvatarUrl] = useState(UserImage);

  return (
    <Row justify="center" align="middle" className="m-4 w-full sm:w-10/12">
      <Col
        xs={24}
        sm={8}
        className="flex justify-center items-center sm:justify-start"
      >
        <div className="relative w-40 min-w-[115px] min-h-[115px] border-4 border-white overflow-hidden">
          <img
            src={avatarUrl}
            alt="UserImage"
            className="object-cover w-full h-full"
          />
        </div>
      </Col>
      <Col xs={24} sm={16} className="flex justify-center items-center">
        <div className="w-full">
          <div className="flex justify-center mt-2">
            <p className="font-bold">User Id</p>
          </div>
          <div className="flex flex-col justify-center items-center mt-2 sm:flex-row">
            <ChangeImage setAvatarUrl={setAvatarUrl}>
              <Button type="primary" ghost className="m-1">
                編輯頭像
              </Button>
            </ChangeImage>
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
