# **Lan side project**

![license](https://img.shields.io/github/license/s35241607/lan-side-project.svg)
![issues](https://img.shields.io/github/issues/s35241607/lan-side-project.svg)
![issues-pr](https://img.shields.io/github/issues-pr/s35241607/lan-side-project.svg)

這是一個採用 **前後端分離架構** 的開源專案，致力於解決 [問題描述]，提供高效、現代化的解決方案。

-   前端使用 [Vite](https://vitejs.dev/) 與 [Vue 3](https://vuejs.org/)，實現快速且高效的單頁應用。
-   後端基於 [C# .NET 8](https://learn.microsoft.com/en-us/dotnet/) 開發，提供穩定且安全的 API。
-   資料庫使用 [PostgreSQL](https://www.postgresql.org/)，保證資料一致性與高性能。

---

## **目錄**

-   [功能](#功能)
-   [技術架構](#技術架構)
-   [安裝與使用](#安裝與使用)
-   [貢獻指南](#貢獻指南)
-   [專案結構](#專案結構)
-   [未來規劃](#未來規劃)
-   [授權](#授權)

---

## **功能**

-   🖥️ **前端功能**：
    -   用戶註冊、登入與驗證。
    -   互動式的 UI，提供動態內容更新。
    -   即時通知與狀態更新。
-   ⚙️ **後端功能**：
    -   完整的 RESTful API。
    -   使用 JWT 實現安全的身份驗證。
    -   提供多角色權限管理（RBAC）。
-   📊 **資料庫功能**：
    -   高效的資料查詢與關聯。
    -   完整的數據遷移與版本控制。
    -   支持大數據處理與擴展。

---

## **技術架構**

### 前端

-   **框架**：Vue 3
-   **工具**：Vite、Pinia（狀態管理）、Axios（HTTP 請求）
-   **樣式**：TailwindCSS

### 後端

-   **語言與框架**：C# .NET 8
-   **認證與授權**：JWT
-   **架構模式**：Clean Architecture（分層結構）
-   **日誌與監控**：Serilog

### 資料庫

-   **類型**：PostgreSQL
-   **ORM**：Entity Framework Core
-   **資料遷移**：EF Core Migration

---

## **安裝與使用**

### 1. **環境需求**

-   **Node.js**: 版本 >= 18
-   **.NET SDK**: 版本 >= 8.0
-   **PostgreSQL**: 版本 >= 15

### 2. **後端部署**

1. Clone 儲存庫並進入目錄：

    ```bash
    git clone https://github.com/s35241607/lan-side-project.git
    cd lan-side-project/backend

    ```

2. 安裝依賴並啟動伺服器：

    ```bash
    dotnet restore
    dotnet run

    ```

3. 確認後端 API 運行在 `http://localhost:5000`。

### 3. **前端部署**

1. 進入前端目錄：

    ```bash
    cd ../frontend

    ```

2. 安裝依賴並啟動開發伺服器：

    ```bash
    npm install
    npm run dev

    ```

3. 前端應用會運行在 `http://localhost:5173`。

---

## **專案結構**

```
lan-side-project/
├── backend/               # 後端程式碼
│   ├── Controllers/       # API 控制器
│   ├── Models/            # 資料模型
│   ├── Services/          # 業務邏輯
│   └── appsettings.json   # 配置文件
├── frontend/              # 前端程式碼
│   ├── src/
│   │   ├── components/    # Vue 組件
│   │   ├── store/         # 狀態管理
│   │   ├── views/         # 頁面視圖
│   └── vite.config.js     # Vite 配置
├── README.md              # 專案說明
└── LICENSE                # 授權文件

```

---

## **貢獻指南**

我們非常歡迎任何形式的貢獻！請參考以下流程：

1. Fork 此專案到你的帳號。
2. 建立分支並進行修改：

    ```bash
    git checkout -b feature/你的功能名稱

    ```

3. 提交 PR，並描述你做了什麼變更。

提交 Issue 前，請確保：

-   檢查是否有相關 Issue 已存在。
-   提供清楚的描述與重現步驟。

---

## **未來規劃**

-   添加 GraphQL 支持。
-   引入 WebSocket 進行即時更新。
-   支持多語系功能。
-   提供 Docker Compose 支持，一鍵啟動環境。

---

## **授權**

此專案採用 [MIT License](https://github.com/s35241607/lan-side-project/blob/main/LICENSE) 授權，歡迎自由使用、修改與分發。
