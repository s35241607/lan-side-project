import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import { jwtDecode } from 'jwt-decode'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

// 檢查 JWT 是否有效
function isTokenValid(token: string): boolean {
  try {
    const decoded = jwtDecode(token) as { exp: number }
    const currentTime = Math.floor(Date.now() / 1000)
    return decoded.exp > currentTime // 確保 token 沒有過期
  } catch (error) {
    console.error('Invalid token:', error)
    return false
  }
}

// 全域導航守衛
router.beforeEach((to, _from, next) => {
  const token = localStorage.getItem('token')

  // 如果路由需要登入權限
  if (to.meta.requiresAuth) {
    // 檢查是否已登入
    if (!token || !isTokenValid(token)) {
      // 未登入，跳轉到登入頁
      return next({ name: 'Login', query: { redirect: to.fullPath } })
    }
  }

  next() // 允許訪問
})

export default router
