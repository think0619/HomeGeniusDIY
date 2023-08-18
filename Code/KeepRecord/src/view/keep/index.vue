<template>

    <div class="title">
        <van-nav-bar  :title="contentTitle" ></van-nav-bar>
        <span>{{ currentdate }}</span>
    </div> 

<van-cell-group inset>
  <!-- 输入任意文本 -->
  <van-field v-model="text" label="文本" />
  <!-- 输入手机号，调起手机号键盘 -->
  <van-field v-model="tel" type="tel" label="手机号" />
  <!-- 允许输入正整数，调起纯数字键盘 -->
  <van-field v-model="digit" type="digit" label="整数" />
  <!-- 允许输入数字，调起带符号的纯数字键盘 -->
  <van-field v-model="number" type="number" label="数字" />
  <!-- 输入密码 -->
  <van-field v-model="password" type="password" label="密码" /> 
</van-cell-group>

<van-cell-group inset>
    <van-field  v-for="(item,index) in testlist"
    :key="index"
    :v-model="item.model"
    :type="item.type"
    :label="item.label"
    
    />
</van-cell-group>
  
    <!-- <div class="btnArea"> -->
        <!-- <van-button type="primary" @click="showCalendarDialog">选择单个日期</van-button> -->
        <!-- <van-cell title="选择单个日期" :value="date" @click="showcalendar" color="#ee0a24" /> -->
        <!-- <van-calendar v-model:show="showcalendar" @confirm="confirmCalendarPick" />

    </div> -->
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
export default {
    components: {
    },
    data() {
        const date = ref('');
        return {
            showcalendar: false,
            calendarDate: '',
            currentdate: '',
            timer: '',

            title: ' ',
            contentTitle: '运动数据',
            active: 0,

            testlist:[
                {"model":"1","type":"text","label":"label1"},
                {"model":"2","type":"text","label":"label2"},
                {"model":"3","type":"text","label":"label3"},
                {"model":"4","type":"text","label":"label4"},
            ],

            curBtnIndex: -1,
            // scrolltop:0,
            pageBtnArrayKey: ['page1_btn1', 'page1_btn2', 'page1_btn3', 'page1_btn4',],
            showlist: {
                page1_btn1: false,
                page1_btn2: false,
                page1_btn3: false,
                page1_btn4: false,
            }
        };
    },
    computed: {
    },
    setup() {
        const tel = ref('');
    const text = ref('');
    const digit = ref('');
    const number = ref('');
    const password = ref('');

    return { tel, text, digit, number, password };

        // const date = ref('');
        // const show1 = ref(false);
        // const formatDate = (date) => `${date.getMonth() + 1}/${date.getDate()}`;
        // const onConfirm = (value) => {
        //     show1.value = false;
        //     date.value = formatDate(value);
        // };

        // return {
        //     date,
        //     show1,
        //     onConfirm,
        // };
    },
    mounted() {
        this.setTitle(); 
    },
    methods: {
        showCalendarDialog() {
            this.showcalendar = true;
        },
        confirmCalendarPick(value) {
            console.log('xx')
            this.calendarDate = `${value.getMonth() + 1}/${value.getDate()}`
            this.showcalendar = false;
            showToast(this.calendarDate);
        },
        getDate(){
            var _this = this;
            let yy = new Date().getFullYear();
            let mm = new Date().getMonth() + 1;
            let dd = new Date().getDate();  
            let todayDate = yy + "-" + mm + "-" + dd; 
            return todayDate;
        },
        setTitle(){ 
            this.contentTitle = this.getDate()+" 运动数据";
        }, 

        getdateTime() {
            var _this = this;
            let yy = new Date().getFullYear();
            let mm = new Date().getMonth() + 1;
            let dd = new Date().getDate();
            let hh = new Date().getHours();
            let mf =
                new Date().getMinutes() < 10
                    ? "0" + new Date().getMinutes()
                    : new Date().getMinutes();
            let ss =
                new Date().getSeconds() < 10
                    ? "0" + new Date().getSeconds()
                    : new Date().getSeconds();
            let gettime = yy + "-" + mm + "-" + dd + " " + hh + ":" + mf + ":" + ss;
            this.currentdate = gettime;
            return gettime;
        }
    }
}; 
</script>

<style scoped> 
.title>span {
    display: block;
    font-size: 24px;;
 }
 </style>