import { createApp } from 'vue';
import { createStore } from 'vuex'

import App from './App.vue';
import { router } from './router';
import 'vant/lib/index.css'

import { Button } from 'vant';
import { NavBar } from 'vant';
import { Tabbar, TabbarItem } from 'vant';
import { Toast } from 'vant';
import { Space } from 'vant';
import { Image as VanImage } from 'vant';
import { Form, Card, List, Divider } from 'vant';
import { Field, Cell, CellGroup, Col, Row } from 'vant';
import { Picker, Popup, RadioGroup, Radio, Slider, Switch } from 'vant';

const app = createApp(App);
app.use(router);
app.use(Button);
app.use(Form);
app.use(NavBar);
app.use(Tabbar);
app.use(TabbarItem);
app.use(Toast);
app.use(Space);
app.use(VanImage);
app.use(Card);
app.use(List);
app.use(Cell);
app.use(CellGroup);
app.use(Divider);
app.use(Field);
app.use(Picker);
app.use(Popup);
app.use(RadioGroup);
app.use(Radio);
app.use(Slider);
app.use(Switch);
app.use(Col);
app.use(Row);

//app.use(showConfirmDialog);

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