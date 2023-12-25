import { createRouter, createWebHashHistory, createWebHistory, } from 'vue-router';
// const home = () =>
//     import ('@/view/home/index.vue')


const netconfigpage = () =>
    import ('@/view/netconfig/index.vue')
const loginpage = () =>
    import ('@/view/login/index.vue')

const routes = [{
        path: '/',
        redirect: '/login'
    },
    {
        name: 'netconfig',
        path: '/netconfig',
        component: netconfigpage,
    },
    {
        name: 'login',
        path: '/login',
        component: loginpage,
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