<template>
  <div>
    <Home></Home>
    <h1>歡迎來到我的應用程式</h1>
    <p>這是一個預設頁面，沒有其他登入功能。</p>
    <Login></Login>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import axios from 'axios'
import Home from './views/Home.vue';
import Login from './views/Login.vue';

</script>



<style scoped>
.logo {
  height: 6em;
  padding: 1.5em;
  will-change: filter;
  transition: filter 300ms;
}

.logo:hover {
  filter: drop-shadow(0 0 2em #646cffaa);
}

.logo.vue:hover {
  filter: drop-shadow(0 0 2em #42b883aa);
}
</style>
