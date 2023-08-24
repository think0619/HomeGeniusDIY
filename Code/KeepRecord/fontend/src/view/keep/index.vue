<template>
    <div class="title">
        <van-nav-bar :title="contentTitle"></van-nav-bar>
        <span>{{ currentdate }}</span>
    </div>
    <van-form @submit="onSubmit">
        <van-cell-group inset>
            <!-- <van-cell title="日期" :value="calendarPickDate" name="datePicker" @click="showCalendarForm = true" /> -->
            <van-field v-model="calendarPickDate" is-link readonly name="datePicker" label="日期" placeholder="点击选择日期" @click="showCalendarForm = true"/>
            <van-calendar v-model:show="showCalendarForm" @confirm="onConfirmDate" />

            <van-field v-model="pickTypeResult" is-link readonly name="typePicker" label="运动种类" placeholder="点击选择种类" @click="showTypeShowPicker" />
            <van-field v-model="numberRec" name="countInput" label="数值" placeholder="数值"   type="number" :rules="[{ required: true, message: '请填写数值' }]" />
            <van-field v-model="pickUnitsResult" is-link readonly name="unitsPicker" label="单位" placeholder="点击选择单位"   @click="showUnitsShowPicker('main')" />

            
            <van-field v-model="numberRecSub" name="countInputSub" label="数值" placeholder="数值"   type="number" :rules="[{ required: true, message: '请填写数值' }]" />
            <van-field v-model="pickUnitsResultSub" is-link readonly name="unitsPickerSub" label="单位" placeholder="点击选择单位"   @click="showUnitsShowPicker('sub')" />

            <van-popup v-model:show="showTypePicker" position="bottom">
                <van-picker :columns="typeColumns" @confirm="onConfirmTypePicker" @cancel="hideTypeShowPicker" />
            </van-popup>
            <van-popup v-model:show="showUnitsPicker" position="bottom">
                <van-picker :columns="unitsColumns" @confirm="onConfirmUnitsPicker" @cancel="hideUnitsShowPicker" />
            </van-popup>


        </van-cell-group>
        <div style="margin: 16px;">
        <van-button round block type="primary" native-type="submit"> 提交   </van-button>
        </div>
        
    </van-form>











    <!-- <van-cell-group inset>
    <van-field  v-for="(item,index) in testlist"
    :key="index"
    :v-model="item.model"
    :type="item.type"
    :label="item.label"
    
    />
</van-cell-group> -->

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
      //  const date = ref('');
        return {
            currentdate: '', // 

            numberRec:'', 
            numberRecSub:'',
            
            showCalendarForm:false,
            calendarPickDate:'',

            showTypePicker: false,
            pickTypeResult: '',
            typeColumns: [
                { text: '跑步', value: 1 },
                { text: '跳绳', value: 2 },
                { text: '拳击', value: 3 },
                { text: '俯卧撑', value: 4 },
                { text: '骑行', value: 5 },],

            showUnitsPicker: false,
            focusUnitsOption:'',
            pickUnitsResult: '',
            pickUnitsResultSub: '',
            unitsColumns: [
                { text: '分钟', value: 1 },
                { text: '次', value: 2 },
                { text: '个', value: 3 },
                { text: '公里', value: 4 }, ],












            showcalendar: false,
            calendarDate: '',

            timer: '',
            showPicker1: false,

            title: ' ',
            contentTitle: '运动数据',
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
        const typeColumns1 = [
            { text: '跑步', value: 'run' },
            { text: '跳绳', value: 'ropeSkip' },
            { text: '拳击', value: 'boxing' },
            { text: '俯卧撑', value: 'pushup' },
            { text: '骑行', value: 'riding' },
        ]; 

       
        //     const tel = ref('');
        //  var text = ref('');
        // const digit = ref('');
        // const number = ref('');
        // const password = ref('');

        // const result = ref('');
        // const showPicker = ref(false);


        // const onConfirm = ({ selectedOptions }) => {
        //     result.value = selectedOptions[0]?.text;
        //     showPicker.value = false;
        // };

        return { typeColumns1  ,};

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
        this.calendarPickDate=this.getDate();
    },
    methods: {
        showTypeShowPicker() {
            this.showTypePicker = true;
        },
        hideTypeShowPicker() {
            this.showTypePicker = false;
        },
        onConfirmTypePicker(selectedOption) {
            var _this=this;
            var selectObj = selectedOption.selectedOptions[0];
            var selectText = selectObj?.text; 
            _this.pickTypeResult = selectText;
            _this.showTypePicker = false; 
        },

        showUnitsShowPicker(option) {
            this.showUnitsPicker = true;

            this.focusUnitsOption=option;
        },
        hideUnitsShowPicker() {
            this.showUnitsPicker = false;
        },
        onConfirmUnitsPicker(selectedOption) {
            var _this=this;
            var selectObj = selectedOption.selectedOptions[0];
            var selectText = selectObj?.text; 
             
            if(_this.focusUnitsOption==='main'){
                _this.pickUnitsResult = selectText;
            }else if(_this.focusUnitsOption==='sub'){
                _this.pickUnitsResultSub = selectText;
            } 
             _this.focusUnitsOption=''; 

            this.showUnitsPicker = false;
        },
        onSubmit(values){
            console.log('submit', values);
        },
        onConfirmDate(date){
            this.calendarPickDate=`${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`;
            this.showCalendarForm=false; 
        },














        showCalendarDialog() {
            this.showcalendar = true;
        },
        confirmCalendarPick(value) {
            console.log('xx')
            this.calendarDate = `${value.getMonth() + 1}/${value.getDate()}`
            this.showcalendar = false;
            showToast(this.calendarDate);
        },
        getDate() {
            var _this = this;
            let yy = new Date().getFullYear();
            let mm = new Date().getMonth() + 1;
            let dd = new Date().getDate();
            let todayDate = yy + "-" + mm + "-" + dd;
            return todayDate;
        },
        setTitle() {
            //this.contentTitle = this.getDate() + " 运动数据";
            this.contentTitle = "新增运动数据";
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

<style scoped> .title>span {
     display: block;
     font-size: 24px;
     ;
 }
</style>