# 基於 .NET ASP.NET 運行時映像檔
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# 安裝必要工具以生成 HTTPS 憑證
RUN apt-get update && apt-get install -y curl && apt-get clean

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# 基於 .NET SDK 映像檔構建應用程式
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# 設定工作目錄
WORKDIR /src

# 複製 csproj 並還原 NuGet 套件
COPY ["lan-side-project.csproj", "."]
RUN dotnet restore "./lan-side-project.csproj"

# 複製剩餘的源碼並構建應用程式
COPY . . 

# 執行 dotnet dev-certs 來創建並信任 HTTPS 憑證
RUN dotnet dev-certs https --clean && \
    dotnet dev-certs https && \
    dotnet dev-certs https --trust

# 設定編譯的目錄並執行 build
RUN dotnet build "./lan-side-project.csproj" -c Release -o /app/build

# 發佈應用程式
FROM build AS publish
RUN dotnet publish "./lan-side-project.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 基於 ASP.NET 運行時的最終映像
FROM base AS final
WORKDIR /app

# 複製已發佈的應用程式
COPY --from=publish /app/publish .

# 設定容器啟動的入口點
ENTRYPOINT ["dotnet", "lan-side-project.dll"]