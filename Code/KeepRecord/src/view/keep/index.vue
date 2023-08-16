<template>
    <div class="btnArea">
        <van-button type="primary"  @click="showCalendarDialog">选择单个日期</van-button>
        <!-- <van-cell title="选择单个日期" :value="date" @click="showcalendar" color="#ee0a24" /> -->
        <van-calendar v-model:show="showcalendar" @confirm="confirmCalendarPick" />

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
export default {
    components: {
    },
    data() {
        return {
            showcalendar: false,
            calendarDate:'',

            title: '绿色矿山展区',
            contentTitle: '智能井口',
            active: 0,
          
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
        const date = ref('');
        const show1 = ref(false);
        const formatDate = (date) => `${date.getMonth() + 1}/${date.getDate()}`;
        const onConfirm = (value) => {
            show1.value = false;
            date.value = formatDate(value);
        };

        return {
            date,
            show1,
            onConfirm,
        };
    },
    mounted() {

    },
    methods: {
        showCalendarDialog() {
            this.showcalendar = true;
        },
        confirmCalendarPick(value) {
            console.log('xx') 
             this.calendarDate =  `${value.getMonth() + 1}/${value.getDate()}`
            this.showcalendar  = false;
            showToast(this.calendarDate);
        }
    }
}; 
</script>