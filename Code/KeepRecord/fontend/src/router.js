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

const keepadd = () =>
    import ('@/view/keep/index.vue')
const keepview = () =>
    import ('@/view/keep/view.vue')

const routes = [{
        path: '/',
        redirect: '/keep/add'
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
        name: 'keepadd',
        path: '/keep',
        // component: keepadd,
        children: [
            { name: 'add', path: 'add', component: keepadd },
            { name: 'view', path: 'view', component: keepview },
        ]
    },
    // {
    //     name: 'keepview',
    //     path: '/keep/view',
    //     component: keepview,
    //     meta: {
    //         title: '购物车',
    //     },
    // },
    // {
    //     name: 'keepadd',
    //     path: '/keep/add',
    //     component: keepadd,
    //     meta: {
    //         title: '购物车',
    //     },
    // },

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