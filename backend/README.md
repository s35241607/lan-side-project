# .NET 8 Web API

## 設定與啟動專案

### 1. 設定 `.env` 檔案

複製 `.env.sample` 檔案並重新命名為 `.env`，然後根據您的環境設置環境變數：

```bash
cp .env.sample .env
```

### 2. 使用 Docker compose 啟動專案

```bash
docker-compose up --build
```

## 使用 EF Core 執行 Migrations

### 1. 產生 Migration

在專案根目錄中，打開終端機並執行以下命令來產生新的 Migration：

```bash
dotnet ef migrations add InitialCreate
```

### 2. 更新資料庫

執行以下命令來應用 Migration，更新資料庫結構：

```bash
dotnet ef database update
```
