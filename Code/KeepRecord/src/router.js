import { createRouter, createWebHashHistory, createWebHistory, } from 'vue-router';
const home = () =>
    import ('@/view/home/index.vue')
const main = () =>
    import ('@/view/main/main.vue')
const tab1 = () =>
    import ('@/view/main/tab1/index.vue')
const tab2 = () =>
    import ('@/view/main/tab2/index.vue')
const tab3 = () =>
    import ('@/view/main/tab3/index.vue')
const tab4 = () =>
    import ('@/view/main/tab4/index.vue')
const cart = () =>
    import ('@/view/cart/index.vue')
const goods = () =>
    import ('@/view/goods/index.vue')

const keep = () =>
    import ('@/view/keep/index.vue')


const routes = [{
        path: '/',
        redirect: '/keep'
    },
    {
        path: '/home',
        name: '首页',
        component: home,
        meta: {
            title: '新技术、新工艺、新装备',
        },
    },
    {
        path: '/main',
        name: '标签页面',
        component: main,
        children: [{
                name: 't1',
                path: 't1',
                component: tab1,

            },
            {
                name: 't2',
                path: 't2',
                component: tab2,

            },
            {
                name: 't3',
                path: 't3',
                component: tab3,

            },
            {
                name: 't4',
                path: 't4',
                component: tab4,

            },
        ]
    },
    {
        name: 'keep',
        path: '/keep',
        component: keep,
        meta: {
            title: '购物车',
        },
    },
    {
        name: 'cart',
        path: '/cart',
        component: cart,
        meta: {
            title: '购物车',
        },
    },
    {
        name: 'goods',
        path: '/goods',
        component: goods,
        meta: {
            title: '商品详情',
        },
    },
];

const router = createRouter({
    routes,
    history: createWebHashHistory(), //createWebHistory(), createWebHashHistory
});

router.beforeEach((to, from, next) => {
    const title = to.meta && to.meta.title;
    if (title) {
        document.title = title;
    }
    next();
});

export { router };