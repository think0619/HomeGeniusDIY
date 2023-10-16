import { createApp } from 'vue';
import App from './App.vue';
import { router } from './router';
import 'vant/lib/index.css'

import { Button } from 'vant';
import { NavBar } from 'vant';
import { Tabbar, TabbarItem } from 'vant';
import { Toast } from 'vant';
import { Space } from 'vant';
import { Image as VanImage } from 'vant';
import { Form, Card, List, } from 'vant';
import { Cell, CellGroup } from 'vant';

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
//app.use(showConfirmDialog);


app.mount('#app');