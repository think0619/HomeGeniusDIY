import { createApp } from 'vue';
import App from './App.vue';
import { router } from './router';
import 'vant/lib/index.css'

import { Button } from 'vant';
import { NavBar } from 'vant';
import { Tabbar, TabbarItem } from 'vant';
import { Toast } from 'vant';
import { Image as VanImage } from 'vant';
import { Space } from 'vant';
import { Col, Row } from 'vant';
import { Form, Calendar, Cell, Field, CellGroup, Picker, Popup, Card, SwipeCell, List } from 'vant';



const app = createApp(App);
app.use(router);
app.use(Button);
app.use(Form);
app.use(NavBar);
app.use(Tabbar);
app.use(TabbarItem);
app.use(Toast);
app.use(VanImage);
app.use(Space);
app.use(Col);
app.use(Row);
app.use(Calendar);
app.use(Cell);
app.use(CellGroup);
app.use(Field);
app.use(Popup);
app.use(Picker);
app.use(Card);
app.use(SwipeCell);
app.use(List);


app.mount('#app');