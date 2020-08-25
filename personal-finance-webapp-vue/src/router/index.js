import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/views/Home'
import AddAccount from '@/views/AddAccount'
import AddTransaction from '@/views/AddTransaction'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Home
    },
    {
      path: '/AddAccount',
      name: 'AddAccount',
      component: AddAccount
    },
    {
      path: '/AddTransaction',
      name: 'AddTransaction',
      component: AddTransaction
    }
  ]
})
