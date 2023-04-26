<template>
  <div class="content">
    <van-nav-bar class="vannav" :border=false :title="str1" left-text="" left-arrow @click-left="onClickLeft" />
    <router-view ref="child" @onChangeTitle="changeTitle" @onDoCMDSend="doCMDSend" class="" />
  </div>
  <div>
    <!--@change="onChange" -->
    <van-tabbar v-model="active" :border="false" safe-area-inset-bottom="true" inactive-color="#FFF" class="custabbar">
        <van-tabbar-item icon="home-o" name="/main/t1" to="/main/t1" replace="true" :dot="tabshow.t1">信号源选择1</van-tabbar-item>
        <van-tabbar-item icon="hotel-o" name="/main/t2" to="/main/t2" replace="true" :dot="tabshow.t2">信号源选择2</van-tabbar-item>
        <van-tabbar-item icon="home-o" name="/main/t3" to="/main/t3" replace="true" :dot="tabshow.t3">预留</van-tabbar-item>
        <van-tabbar-item icon="home-o" name="/main/t4" to="/main/t4" replace="true" :dot="tabshow.t4">系统设置</van-tabbar-item>
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

import { sendCMDMsg } from "@/api/index";
import { showToast } from 'vant'; 
import mqtt from "mqtt";
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
          { key: 'page2_btn1', cmd: ['cmd$PC$YUIDX43_Video_single','cmd$PC$YUIDX43_Video_PV01',] }, //工业互联网平台·介绍
 
        ],
       url: 'ws://127.0.0.1:8083/mqtt',
       client :null,

    };
  },
  computed: {
  },
  setup() {
  },
  mounted() {
    //establishMQTT();
  },
  methods: {

    establishMQTT(){
      this.client = mqtt.connect(this.url)
      this.client.on('connect', function() {
        this.client.subscribe('CMD', function(err) {
                if (!err) {
                    // client.publish('CMD', 'Hello mqtt')
                }
            })
        })

        this.client.on('message', function(topic, message) {
            // message is Buffer
            console.log(message.toString())
                // client.end()
        })
    },

    
    onClickLeft() {
      this.$router.replace('/home')
    },
    changeTitle(_title) {
      this.str1 = _title;
    },
    doCMDSend(btnKey) {
      let that = this;
      var sendContent = "";
      that.cmds.forEach(function (value, index, array) {
        if (btnKey == value.key) {
          sendContent = value.cmd;
        }
      });
      if (sendContent.length > 0) {
        sendCMDMsg({
          Msg: sendContent,
          Sender: 'zhkjdev',
        }).then((res) => {
          console.log(res);
          if (res != null && res.data != null) {
            console.log(res.data);
            showToast('提示内容');
            var qresult = res.data;
            if (qresult != null) {
              //success
              if (qresult.Status == 1) {
                showToast({
                  position: 'top',
                  message: '发送成功',
                  duration: 500,
                });
              } else {
                showToast({
                  position: 'top',
                  message: '发送失败',
                  duration: 500,
                });
              }
            } else {
              that.errorToast();
            }
          } else {
            that.errorToast();
          }
        }).catch(function (err) {
          console.log(err);
          that.errorToast();
        });
      }
      else {
        showToast('系统错误');
      }
    },
    errorToast() {
      showToast({
        position: 'top',
        message: '发送异常',
        duration: 500,
      });
    },
    onChange(item) {
      alert(item);
    }
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
 }

 .cmdbtn1-1 {
   background: url(@/assets/img/btn/01-1.jpg);
   background-repeat: no-repeat;
   background-size: cover;
 }

 .cmdbtn1-2 {
   background: url(@/assets/img/btn/01-2.jpg);
   background-repeat: no-repeat;
   background-size: cover;
 }

 .cmdbtn1-3 {
   background: url(@/assets/img/btn/01-3.jpg);
   background-repeat: no-repeat;
   background-size: cover;
 }

 .cmdbtn1-4 {
   background: url(@/assets/img/btn/01-4.jpg);
   background-repeat: no-repeat;
   background-size: cover;
 }

 .cmdbtn2-1 {
   background: url(@/assets/img/btn/02-1.jpg);
   background-repeat: no-repeat;
   background-size: cover;
 }

 .cmdbtn2-2 {
   background: url(@/assets/img/btn/02-2.jpg);
   background-repeat: no-repeat;
   background-size: cover;
 }

 .cmdbtn2-3 {
   background: url(@/assets/img/btn/02-3.jpg);
   background-repeat: no-repeat;
   background-size: cover;
 }

 .cmdbtn2-4 {
   background: url(@/assets/img/btn/02-4.jpg);
   background-repeat: no-repeat;
   background-size: cover;
 }

 .cmdbtn3-1 {
   background: url(@/assets/img/btn/03-1.jpg);
   background-repeat: no-repeat;
   background-size: cover;
 }

 .cmdbtn3-2 {
   background: url(@/assets/img/btn/03-2.jpg);
   background-repeat: no-repeat;
   background-size: cover;
 }

 .cmdbtn3-3 {
   background: url(@/assets/img/btn/03-3.jpg);
   background-repeat: no-repeat;
   background-size: cover;
 }

 .cmdbtn3-4 {
   background: url(@/assets/img/btn/03-4.jpg);
   background-repeat: no-repeat;
   background-size: cover;
 }</style>
