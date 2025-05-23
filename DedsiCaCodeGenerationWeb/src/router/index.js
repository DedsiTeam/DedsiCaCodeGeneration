import { createRouter, createWebHistory } from 'vue-router'

import viewRoutes from '../views/viewRoutes.js'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
      ...viewRoutes
  ]
})

export default router
