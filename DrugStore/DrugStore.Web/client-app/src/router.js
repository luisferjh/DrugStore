import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/Home.vue';
import Product from './views/Product.vue'
import Laboratory from './views/Laboratory.vue'
import Order from './views/Order.vue'
import User from './views/User.vue'
import Provider from './views/Provider.vue'
import Login from './views/Login.vue'
import Client from './views/Client.vue'
import store from './store'

Vue.use(Router);

var router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
      meta:{
        free:true
      }     
    },  
    {
      path: '/product',
      name: 'product',
      component: Product,
      meta:{
        admin:true,
        seller:true
      }
    },
    {
      path: '/lab',
      name: 'laboratory',
      component: Laboratory,
      meta:{
        admin:true,
        seller:true
      }
    },
    {
      path: '/provider',
      name: 'provider',
      component: Provider,
      meta:{
        admin:true,
        seller:true
      }
    },
    {
      path: '/client',
      name: 'client',
      component: Client,
      meta:{
        admin:true,
        seller:true
      }
    },   
    {
      path: '/user',
      name: 'user',
      component: User,
      meta:{
        admin:true      
      }
    },
    {
      path: '/order',
      name: 'order',
      component: Order,
      meta:{
        admin:true        
      }
    },   
    {
      path: '/login',
      name: 'login',
      component: Login,
      meta:{
        free:true
      }
    }
  ]
});

router.beforeEach((to, from, next)=>{
  store.dispatch('autoLogin')
  if (to.matched.some(record => record.meta.free)) {
    next()
  } 
  else if(store.state.user && store.state.user.Role == 'Admin'){
    if(to.matched.some(record => record.meta.admin)){
      next()
    } 
  }
  else if(store.state.user && store.state.user.Role == 'Seller'){
    if(to.matched.some(record => record.meta.voter)){
      next()
    }
  }  
})

export default router