<template>
  <div class="login">
    <h1>登入頁面</h1>

    <!-- Google 登入按鈕 -->
    <button @click="googleLogin" class="google-login-button">使用 Google 登入</button>

    <a :href="googleLoginUrl" class="google-login-button">Google 登入</a>
    <!-- 可以加入其他內容或登錄表單 -->
  </div>
</template>

<script setup lang="ts">
import axios from 'axios'

const returnUrl: string = import.meta.env.VITE_BASE_URL

// 從 .env 讀取 Base URL
const apiUrl: string = import.meta.env.VITE_API_URL

// 建立 Google 登入的 URL，並包含 returnUrl 參數
const googleLoginUrl: string = `${apiUrl}/api/v1/auth/google-auth?returnUrl=${returnUrl}`

const googleLogin: Function = async () => {
  try {
    // 向後端請求 Google 登入 URL
    const response = await axios.get(`${apiUrl}/api/v1/auth/google-auth`)
    console.log('Google Login URL:', response.data.url)  // 顯示取得的 URL

    if (response.data && response.data.url) {
      // 成功獲取到 URL，重定向到 Google 登入頁面
      window.location.href = response.data.url
    } else {
      console.error('未能獲取 Google 登入 URL')
    }
  } catch (error) {
    console.error('Google 登入請求失敗', error)
  }
}
</script>

<style scoped>
.login {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 100vh;
}

.google-login-button {
  padding: 10px 20px;
  background-color: #4285F4;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 16px;
}

.google-login-button:hover {
  background-color: #357ae8;
}
</style>
