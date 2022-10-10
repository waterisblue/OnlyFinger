import Vue from 'vue'
import VueRouter from 'vue-router'


Vue.use(VueRouter)


import IndexOld from '../components/page/IndexOld'
import TodayCheckOld from '../components/page/TodayCheckOld'
// import AnyOneFind from '../components/page/AnyOneFind'
// import AnyOneChart from '../components/page/AnyOneChart'
// import FindByDate from '../components/page/FindByDate'
// import TaskView from '../components/page/TaskView'

// const routes = [
//   {
//     path: '/',
//     component: Index,
//     children: [
//       {path: '/index', component: TodayCheck},
//       {path: '/anyonefind', component: AnyOneFind},
//       {path: '/anyonechart', component: AnyOneChart},
//       {path: '/findbydate', component: FindByDate},
//       {path: '/taskview', component: TaskView}
//     ]
//   }
// ]

import Index from '../pages/Index'
import DashBroad from '../pages/DashBroad'

const routes = [
  {
    path: '/',
    component: Index,
    children: [
      {path: "/", component: DashBroad},
      {path: '/ew', component: TodayCheckOld},
    ]
  },
  {
    path: '/old',
    component: IndexOld,
    children: [
      {path: '/', component: TodayCheckOld},
    ]
  }
]

const router = new VueRouter({
  routes
})

//挂载路由导航守卫
/*router.beforeEach((to, from, next)=>{// to 将要访问  from 从哪来   next  接着淦的事next(url) 重定向到url上，如果next无参数，则放行
  if(to.path == '/login'){
    //如果访问首页，就放行
    return next()
  }
  //如果访问的不是首页,检查session
  const userFlag = window.sessionStorage.getItem("user") //取出session中存的user
  if(!userFlag){ //如果userFlag没有数据，返回登录页
    return next("/login")
  }
  next() // 符合要求，直接放行
})*/

export default router
