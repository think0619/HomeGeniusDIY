<template>
    <div id="content">
        <van-button type="primary" size="normal" class="btn1 cusbtn" block @click="goto('/main/t1')">智能井口</van-button>
        <van-button type="primary" size="normal" class="btn2 cusbtn" block @click="goto('/main/t2')">绿色低碳智慧园区</van-button>
        <!-- <van-button type="primary" size="normal" class="btn3 cusbtn" block @click="goto('/main/t3')">环保工艺</van-button>
        <van-button type="primary" size="normal" class="btn4 cusbtn" block @click="goto('/main/t4')">巷道新材料</van-button> -->
    </div>
</template>

<script setup>
import { useRouter, useRoute } from 'vue-router'
import { reactive } from 'vue'; 
const router = useRouter()
// const route = useRoute()
// console.log('路由', route.path)
const goto = (path) => {
    router.replace(path)
}
const themeVars = reactive({
    buttonRadius: '30px',
    buttonBorderWidth: 10,
    buttonPrimaryBackground: 'linear-gradient(#655dfe, #4ca3ff)',
});
</script>

<script>
// import { createApp,reactive } from 'vue';
// import { ConfigProvider } from 'vant';

// const app = createApp();
// app.use(ConfigProvider);

// export default {
//     setup() {
//         // 比如 sliderBarHeight 会转换成 `--van-slider-bar-height`
//         const themeVars = reactive({
//             buttonRadius: '30px',
//             buttonBorderWidth: 10,
//             buttonPrimaryBackground: 'linear-gradient(#655dfe, #4ca3ff)',
//         });
//         return {
//             themeVars,
//         };
//     },
// };

</script> 

<style scoped> 
#content {
     height: 100%;
     margin: 0;
     padding: 0;
     overflow: hidden;
     background: url(../../assets/img/indexbg-01.jpg);
     background-repeat: no-repeat;
     background-size: 100vw 100vh;
 }

 .btn1 {
     position: absolute;
     left: 2.5vw;
     top: 64.5vh; 
     width: 35vw;
 } 
 
 .btn2 {
     position: absolute;
     left: 39vw; 
     top: 57vh;
     width: 47vw;
 } 
 /* .btn3 {
     position: absolute;
     left: 54vw; 
     top: 65vh;
     width: 35vw;
 } 
 .btn4 {
     position: absolute;
     left: 64vw; 
     top: 47vh;
     width: 35vw;
 } 
 */ 

 .cusbtn {
     --van-button-primary-background: linear-gradient(#625dff, #4ba2ff);
     --van-button-radius: 30px;
     --van-button-border-width: 0;
     --van-button-default-height: 5.3vh;
     --van-button-normal-font-size: 1rem;
 }
</style>
