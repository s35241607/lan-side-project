import React, { useState } from "react";
import { Upload, Modal, message } from "antd";
import type { RcFile } from "antd/es/upload/interface";

interface ChangeImageProps {
  children: React.ReactNode;
  setAvatarUrl: React.Dispatch<React.SetStateAction<string>>;
}

const ChangeImage: React.FC<ChangeImageProps> = ({
  children,
  setAvatarUrl,
}) => {
  const [previewImage, setPreviewImage] = useState(""); // 預覽圖片
  const [tempFile, setTempFile] = useState<RcFile | null>(null); // 存放即將上傳圖片
  const [isModalVisible, setIsModalVisible] = useState(false); // 顯示modal

  // 將上傳的圖片轉換格式
  const getBase64 = (file: RcFile): Promise<string> =>
    new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result as string);
      reader.onerror = (error) => reject(error);
    });

  // 檢查上傳的圖片格式大小
  const beforeUpload = (file: RcFile) => {
    const isJpgOrPng = file.type === "image/jpeg" || file.type === "image/png";
    if (!isJpgOrPng) {
      message.error("只能上傳 JPG/PNG 格式的圖片!");
      return false;
    }
    const isLt2M = file.size / 1024 / 1024 < 2;
    if (!isLt2M) {
      message.error("圖片必須小於 2MB!");
      return false;
    }
    return true;
  };

  // 選擇圖片
  const handleFileSelect = async (file: RcFile) => {
    if (!beforeUpload(file)) {
      return false;
    }
    try {
      const preview = await getBase64(file);
      setPreviewImage(preview);
      setTempFile(file);
      setIsModalVisible(true);
      return false; // 阻止自動上傳
    } catch (error) {
      message.error("圖片預覽失敗");
      return false;
    }
  };

  // 確定更換頭像
  const handleConfirmUpload = async () => {
    if (!tempFile) return;
    try {
      // 這裡應該是實際的上傳邏輯，目前僅預覽圖替換頭像
      const base64 = await getBase64(tempFile);
      setAvatarUrl(base64);
      message.success("頭像更新成功");
      handleCancel();
    } catch (error) {
      message.error("頭像上傳失敗");
    }
  };

  // 取消更換頭像
  const handleCancel = () => {
    setIsModalVisible(false);
    setPreviewImage("");
    setTempFile(null);
  };

  return (
    <div>
      <Upload
        name="avatar"
        showUploadList={false}
        beforeUpload={handleFileSelect}
      >
        {children}
      </Upload>

      <Modal
        title="預覽頭像"
        open={isModalVisible}
        onOk={handleConfirmUpload}
        onCancel={handleCancel}
        okText="確認"
        cancelText="取消"
      >
        {previewImage && (
          <div className="flex justify-center">
            <div className="relative w-40 min-w-[115px] min-h-[115px] border-4 border-white overflow-hidden">
              <img
                src={previewImage}
                alt="Preview"
                className="object-cover w-full h-full"
              />
            </div>
          </div>
        )}
      </Modal>
    </div>
  );
};

export default ChangeImage;
