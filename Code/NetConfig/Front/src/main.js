import { createApp } from 'vue';
import { createStore } from 'vuex'

import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'

import App from './App.vue';
import { router } from './router';


const app = createApp(App);
app.use(router);
app.use(ElementPlus)

// 创建一个新的 store 实例
const store = createStore({
    state() {
        return {
            count: 0,
            userid: '',
            token: ''
        }
    },
    mutations: {
        increment(state) {
            state.count++
        },
        setToken(state, _token) {
            state.token = _token;
        },
    }
})

app.use(store);
app.mount('#app');