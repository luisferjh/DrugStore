import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/Home.vue';
import Product from './views/Product.vue'
import Login from './views/Login.vue'
import About from './views/about.vue'
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
      path: '/about',
      name: 'about',
      component: About,
      meta:{
        free:true
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