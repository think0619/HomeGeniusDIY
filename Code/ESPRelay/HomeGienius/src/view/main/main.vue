<template>
  <div class="content">
    <van-nav-bar class="vannav" :border=false :title="str1" left-text="" left-arrow @click-left="onClickLeft" />
    <router-view ref="child" @onChangeTitle="changeTitle" @onDoCMDSend="doCMDSend" class="" />
  </div>
  <div>
    <!--@change="onChange" -->
    <van-tabbar v-model="active" :border="false" safe-area-inset-bottom="true" inactive-color="#FFF" class="custabbar">
      <van-tabbar-item icon="home-o" name="/keep/add" to="/keep/add" replace="true" :dot="tabshow.t1">智能井口</van-tabbar-item>
      <van-tabbar-item icon="hotel-o" name="/esp" to="/esp" replace="true"  :dot="tabshow.t2">绿色展区主屏幕</van-tabbar-item>
      <van-tabbar-item icon="hotel-o" name="/keep/add" to="/keep/add" replace="true"  :dot="tabshow.t2">绿色展区主屏幕</van-tabbar-item>
      <van-tabbar-item icon="hotel-o" name="/keep/add" to="/keep/add" replace="true"  :dot="tabshow.t2">绿色展区主屏幕</van-tabbar-item> 
    </van-tabbar>
  </div>
</template> 

<script setup  lang="jsx">
import { ref } from 'vue';
import { useRouter, useRoute } from 'vue-router';
const router = useRouter();
const route = useRoute();
const active = ref(route.path);  
</script>

<script lang="jsx"> 
import { showToast } from 'vant';
// cmd$PC${GUID}_video_pv01
// cmd$PC${GUID}_video_pause
// cmd$PC${GUID}_video_start
// cmd$PC${GUID}_video_single
// cmd$PC${GUID}_video_list 
// cmd$PC${GUID}_video_next 
// cmd$PC${GUID}_video_previous
// cmd$Model$m1_open01 cmd$Model${GUID}_open01  
//"cmd$Model$m1_open01", "cmd$PC$YUIDX43_Video_PV01",
export default {
  components: {
  },
  data() {
    return {
      str1: '',
      active: 0,
      tabshow: {
        t1: false,
        t2: false,
        t3: false,
        t4: false,
      },
      cmds:
        [ 
        ]
    };
  },
  computed: {
  },
  setup() {
  },
  mounted() {

  },
  methods: {
    onClickLeft() {
      this.$router.replace('/home')
    },
    changeTitle(_title) {
      this.str1 = _title;
    }, 
    errorToast() {
      showToast({
        position: 'top',
        message: '发送异常',
        duration: 500,
      });
    }, 
  }
}; 
</script>

<style> :root:root {
   --van-nav-bar-title-text-color: #fff;
   --van-nav-bar-icon-color: #fff;
   --van-nav-bar-arrow-size: 20px;
   --van-tabbar-background: transparent;
   --van-tabbar-item-icon-size: 22px;
   --van-tabbar-item-active-background: transparent;
 }

 .van-nav-bar {
   /* background: url(../../assets/img/navbarbg.png); */
   background-color: rgba(13, 37, 111, 0.800);
   background-repeat: no-repeat;
   background-size: 100vw 100vh;
   --van-nav-bar-height: 6vh
 }

 .content {
   height: 100%;
   background: url(@/assets/img/back/itembg.png);
   background-repeat: no-repeat;
   background-size: 100vw 100vh;
 }

 .contentTitle {
   width: 100vw;
   margin: 0 auto;
   color: #fffefc;
   margin: 2vh auto 0 auto;
   display: flex;
   flex-direction: row;
   justify-content: center;
   font-size: 2rem;
   /* font-family: Youshe; */
 }

 .btnArea {
   max-height: 58vh;
   overflow: scroll;
   margin: 2.5vh auto;
 }

 .cmdCustomBtn1{
   margin: 0 auto;
   width: 90vw;
   height: 12vh;
   --van-button-border-width: 0;
   display: flex;

 }

 .cmdCustomBtn1 .btn_name {
   line-height: 12vh;
   font-size: 1.5rem;
   font-weight: 600;
   color: #fff;
   margin-left: 30px;
 }

.cmdCustomBtn2{ 
  width: 90vw;
  line-height: 50px; 
   font-size: 1.5rem;
   font-weight: 600;
   color: #fff; 
   margin: 10px auto;
}

 .cmdResetBtn {
   position: absolute;
   top: 7vh;
   right: 5vw;
   width: 3rem;
   height: 3rem;
 }



 
 .arrowdiv {
   position: absolute;
   bottom: 18vh;
   width: 100vw;
   height: 6vh;
   margin: 0 auto;

 }

 .arrowCol {
   text-align: center;
 }

 .crtIcon {
   margin-left: auto;
   margin-top: 2vh;
   margin-right: 2vw;
   width: 2rem;
   height: 2rem;
 }

 .custabbar {
   --van-tabbar-height: 7.5vh
 }

 .cmdbtn-size {
   background-repeat: no-repeat;
   background-size: cover;
 }

 .cmdbtn1 {
   background: url(@/assets/img/btn/01-1.jpg);
   background-repeat: no-repeat;
   background-size: cover;
 } </style>
