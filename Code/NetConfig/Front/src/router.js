import { createRouter, createWebHashHistory, createWebHistory, } from 'vue-router';
// const home = () =>
//     import ('@/view/home/index.vue')


const weathershow = () =>
    import ('@/view/weather/index.vue')

const routes = [{
        path: '/',
        redirect: '/weather'
    },
    {
        name: 'weather',
        path: '/weather',
        component: weathershow,
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