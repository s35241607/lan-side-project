import React from "react";
import { LoadingOutlined } from "@ant-design/icons";
import { Flex, Spin } from "antd";

const Loading: React.FC = () => {
  return (
    <>
      <Flex
        align="center"
        gap="middle"
        className="fixed inset-0 flex items-center justify-center z-50 bg-black bg-opacity-50"
        style={{ backdropFilter: "blur(1px)" }}
      >
        <Spin
          indicator={
            <LoadingOutlined spin style={{ fontSize: 48, color: "white" }} />
          }
          size="large"
        />
      </Flex>
    </>
  );
};

export default Loading;
