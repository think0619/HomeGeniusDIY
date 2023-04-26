import { createApp } from 'vue';
import App from './App.vue';
import { router } from './router';
import 'vant/lib/index.css'
import * as buffer from "buffer";

import { Button } from 'vant';
import { NavBar } from 'vant';
import { Tabbar, TabbarItem } from 'vant';
import { Toast } from 'vant';
import { Image as VanImage } from 'vant';
import { Space } from 'vant';
import { Col, Row } from 'vant';
const app = createApp(App);
app.use(router);
app.use(Button);
app.use(NavBar);
app.use(Tabbar);
app.use(TabbarItem);
app.use(Toast);
app.use(VanImage);
app.use(Space);
app.use(Col);
app.use(Row);
app.mount('#app');