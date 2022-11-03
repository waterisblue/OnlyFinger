import Vue from 'vue'
import App from './App.vue'
import './assets/tailwindcss.css'
import router from './router'
import * as echarts from 'echarts'
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
import './assets/global.css'

import * as transform from './api/transform'

import axios from 'axios'

Vue.use(ElementUI)
Vue.prototype.$http = axios
axios.defaults.baseURL = "https://localhost:7259"

Vue.prototype.$echarts = echarts

Vue.config.productionTip = false

Vue.prototype.$transform = transform

new Vue({
  router,
  render: h => h(App),
}).$mount('#app')