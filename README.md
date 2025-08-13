# Enterprise Ticket System

這是一個企業內部使用的 Ticket 系統，具備完整的 CRUD 功能、狀態管理以及多級簽核流程。

## 專案特色

- **前後端分離**：前端使用 Vue 3，後端使用 FastAPI，架構清晰。
- **容器化部署**：所有服務皆透過 Docker 容器化，並使用 Docker Compose 一鍵啟動。
- **非同步架構**：後端採用 `async/await`，充分發揮非同步 IO 的高效能。
- **分層設計**：後端遵循 `controllers` -> `services` -> `repositories` -> `models` 的分層架構。
- **可擴充性**：整合 Redis (快取) 與 Kafka (異步通知)，易於擴充。
- **開發者友善**：支援 VS Code Remote Containers，並內建 linting 與格式化工具。

## 技術棧

- **後端**: Python, FastAPI, SQLAlchemy, PostgreSQL
- **前端**: Vite, Vue 3, Vuetify (可選)
- **中介軟體**: Redis, Kafka
- **部署**: Docker, Docker Compose
- **CI/CD**: GitLab CI

## 如何啟動

1.  **安裝 Docker 與 Docker Compose**
    請確保您的系統已安裝最新版本的 Docker。

2.  **Clone 專案**
    ```bash
    git clone <your-repo-url>
    cd enterprise-ticket-system
    ```

3.  **啟動服務**
    在專案根目錄下，執行以下指令：
    ```bash
    docker-compose up --build -d
    ```
    此指令會建置並啟動所有服務，包含前端、後端、資料庫等。

4.  **檢查服務狀態**
    ```bash
    docker-compose ps
    ```
    確認所有 container 的狀態為 `running` 或 `healthy`。

5.  **訪問應用**
    - **後端 API 文件 (Swagger UI)**: [http://localhost:8000/docs](http://localhost:8000/docs)
    - **前端應用**: [http://localhost:5173](http://localhost:5173)

## 開發

### 後端開發

- 直接在 `backend` 資料夾中修改程式碼，`uvicorn` 會自動重載。
- 使用 VS Code Remote Containers 可直接在容器內開發與除錯。

### 前端開發

- 進入 `frontend` 資料夾，可執行 `npm` 指令。
- Vite 開發伺服器支援熱模組替換 (HMR)。

## 測試

- **後端測試**:
  ```bash
  docker-compose exec backend pytest
  ```
- **前端 Lint**:
  ```bash
  docker-compose exec frontend npm run lint
  ```
