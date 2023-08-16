<template>
  <div class="content">
    <van-nav-bar class="vannav" :border=false :title="str1" left-text="" left-arrow @click-left="onClickLeft" />
    <router-view ref="child" @onChangeTitle="changeTitle" @onDoCMDSend="doCMDSend" class="" />
  </div>
  <div>
    <!--@change="onChange" -->
    <van-tabbar v-model="active" :border="false" safe-area-inset-bottom="true" inactive-color="#FFF" class="custabbar">
      <van-tabbar-item icon="home-o" name="/main/t1" to="/main/t1" replace="true" :dot="tabshow.t1">智能井口</van-tabbar-item>
      <van-tabbar-item icon="hotel-o" name="/main/t2" to="/main/t2" replace="true"
        :dot="tabshow.t2">绿色展区主屏幕</van-tabbar-item>
      <!-- <van-tabbar-item icon="home-o" name="/main/t3" to="/main/t3" replace="true" badge="5">环保工艺</van-tabbar-item> -->
      <!-- <van-tabbar-item icon="home-o" name="/main/t4" to="/main/t4" replace="true" badge="5">巷道新材料</van-tabbar-item> -->
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
          { key: 'page2_btn2', cmd: ['cmd$PC$YUIDX43_Video_single','cmd$PC$YUIDX43_Video_PV02',] }, //工业互联网平台·关键技术
          { key: 'page2_btn3', cmd: ['cmd$PC$YUIDX43_Video_single','cmd$PC$YUIDX43_Video_PV03',] }, //工业互联网平台·选煤厂
          { key: 'page2_btn4', cmd: ['cmd$PC$YUIDX43_Video_single','cmd$PC$YUIDX43_Video_PV04',] }, //工业互联网平台·低代码
          { key: 'page2_btn5', cmd: ['cmd$PC$YUIDX43_Video_single','cmd$PC$YUIDX43_Video_PV05',] }, //工业互联网平台·曹家滩选煤厂
          { key: 'page2_btn6', cmd: ['cmd$PC$YUIDX43_Video_single','cmd$PC$YUIDX43_Video_PV06', "cmd$Model$m1_close01", "cmd$Model$m1_close02"] }, //智慧园区·1+2+4+N
          { key: 'page2_btn7', cmd: ['cmd$PC$YUIDX43_Video_single','cmd$PC$YUIDX43_Video_PV07', "cmd$Model$m1_close01", "cmd$Model$m1_close02"] }, //智慧园区·智慧安防
          { key: 'page2_btn8', cmd: ['cmd$PC$YUIDX43_Video_single','cmd$PC$YUIDX43_Video_PV08', "cmd$Model$m1_close01", "cmd$Model$m1_close02"] }, //智慧园区·智慧后勤
          { key: 'page2_btn9', cmd: ['cmd$PC$YUIDX43_Video_single','cmd$PC$YUIDX43_Video_PV09', "cmd$Model$m1_close01", "cmd$Model$m1_close02"] }, //智慧园区·碳计量一体盒
          { key: 'page2_btn10', cmd: ['cmd$PC$YUIDX43_Video_single','cmd$PC$YUIDX43_Video_PV10', "cmd$Model$m1_close01", "cmd$Model$m1_close02"] }, //智慧园区·综合能效
          { key: 'page2_btn11', cmd: ['cmd$PC$YUIDX43_Video_single','cmd$PC$YUIDX43_Video_PV11', "cmd$Model$m1_close02", "cmd$Model$m1_open01"] }, //智慧园区·热循环
          { key: 'page2_btn12', cmd: ['cmd$PC$YUIDX43_Video_single','cmd$PC$YUIDX43_Video_PV12', "cmd$Model$m1_close01","cmd$Model$m1_open02",] }, //智慧园区·微电网
          { key: 'page2_btn13', cmd: ['cmd$PC$YUIDX43_Video_single','cmd$PC$YUIDX43_Video_PV13', "cmd$Model$m1_close02", "cmd$Model$m1_close01",] }, //武汉院·管道技术
          { key: 'page2_reset', cmd: ["cmd$PC$YUIDX43_video_list",  "cmd$Model$m1_open09", "cmd$Model$m1_close01", "cmd$Model$m1_close02",] },

          { key: 'page1_btn1', cmd: ['cmd$PC$QAZW23_Video_single', "cmd$PC$QAZW23_Video_PV01",] },
          { key: 'page1_btn2', cmd: ['cmd$PC$QAZW23_Video_single', "cmd$PC$QAZW23_Video_PV02",] },
          { key: 'page1_btn3', cmd: ['cmd$PC$QAZW23_Video_single', "cmd$PC$QAZW23_Video_PV03",] },
          { key: 'page1_btn4', cmd: ['cmd$PC$QAZW23_Video_single', "cmd$PC$QAZW23_Video_PV04",] }, 
          { key: 'page1_reset', cmd: ["cmd$PC$QAZW23_Video_list"] },

          { key: 'page3_btn1', cmd: ["cmd$PC$YUIDX43_Video_pause",  ] },
          { key: 'page3_btn2', cmd: ["cmd$PC$YUIDX43_Video_start",  ] },
          { key: 'page3_btn3', cmd: ["cmd$PC$QAZW23_Video_pause",] },
          { key: 'page3_btn4', cmd: ["cmd$PC$QAZW23_Video_pause",] },
          { key: 'page4_reset', cmd: ["cmd$PC$UJDH55_video_list"] },
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
