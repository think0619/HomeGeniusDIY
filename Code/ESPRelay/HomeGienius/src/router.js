import { createRouter, createWebHashHistory, createWebHistory, } from 'vue-router';



const login = () =>
    import ('@/view/login/index.vue')

const keepadd = () =>
    import ('@/view/keep/index.vue')
const keepview = () =>
    import ('@/view/keep/view.vue')

const espcontrol = () =>
    import ('@/view/espcontroller/index.vue')
const weathershow = () =>
    import ('@/view/weather/index.vue')
const raspclock = () =>
    import ('@/view/raspclock/index.vue')


const aircondition = () =>
    import ('@/view/aircondition/index.vue')



const aircondition2 = () =>
    import ('@/view/aircondition2/index.vue')

const misccontrol = () =>
    import ('@/view/misc/index.vue')


const routes = [{
        path: '/',
        redirect: '/login'
    },
    // {
    //     path: '/home',
    //     name: '首页',
    //     component: home,
    //     meta: {
    //         title: '新技术、新工艺、新装备',
    //     },
    // },
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
    //     name: 'main',
    //     path: '/main',
    //     component: main,
    // },
    {
        name: 'espcontrol',
        path: '/esp',
        component: espcontrol,
    }, {
        name: 'irmachine',
        path: '/ir',
        component: aircondition,
    }, {
        name: 'irmachine2',
        path: '/ir2',
        component: aircondition2,
    },
    {
        name: 'weather',
        path: '/weather',
        component: weathershow,
    },
    {
        name: 'raspclock',
        path: '/raspclock',
        component: raspclock,
    },
    {
        name: 'loginpage',
        path: '/login',
        component: login,
    },
    {
        name: 'miscpage',
        path: '/miscctrl',
        component: misccontrol,
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