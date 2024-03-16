<template>
    <div class="title"> </div>
    <van-nav-bar :title="contentTitle"></van-nav-bar>

    <van-field v-model="dataquery" is-link readonly name="datePicker" label="开始日期" placeholder="点击选择日期"
        @click="datepickerClick " />
    <van-calendar v-model:show="showCalendarForm" type="range" @confirm="onConfirmDate" :min-date="minDate"
        :max-date="maxDate" />
    <van-grid direction="horizontal" :column-num="3">
        <van-grid-item icon="photo-o" text="今日" @click="datepickerCustom('today') "/>
        <van-grid-item icon="calendar-o" text="7天" @click="datepickerCustom('seven') "/>
        <van-grid-item icon="photo-o" text="30天" @click="datepickerCustom('thirty') "/>
    </van-grid> 
 
    <van-floating-bubble icon="plus" axis="xy" @click="onAddPathClick" />
</template>

<script setup lang="jsx">
    import { ref } from 'vue';
    import { useRouter, useRoute } from 'vue-router';

    const router = useRouter();
    const route = useRoute();
    const active = ref(route.path);   
</script>

<script lang="jsx">
    import { showConfirmDialog, showSuccessToast, showFailToast } from 'vant';
    import { querykeeprecord, deletekeeprecord } from "@/api/keep";

    export default {
        components: {
        },
        data() {
            return {
                contentTitle: '', //    
            };
        },
        computed: {
            dataquery: function () {
                return this.startDateQuery + ' 至 ' + this.endDateQuery
            },
            minDate: function () {
                let currentDate = new Date();
                currentDate.setFullYear(currentDate.getFullYear() - 1);
                return currentDate; 
            },
            maxDate: function () {
                let currentDate = new Date();
                currentDate.setMonth(currentDate.getMonth() + 1);
                return currentDate; 
            },
        },
        setup() {
        },
        beforeCreate() {

        },
        created() {
            const today = new Date();
            const todayfmt = `${today.getFullYear()}/${today.getMonth() + 1}/${today.getDate()}`;
            this.startDateQuery = todayfmt;
            this.endDateQuery = todayfmt;
        },
        mounted() {
            this.setTitle();
        },
        methods: {
            setTitle() {
                this.contentTitle = "x";
            },  
            onConfirmDate(dates) {
                let that = this;
                that.showCalendarForm = false;
                const [start, end] = dates;  
                that.startDateQuery = `${start.getFullYear()}/${start.getMonth() + 1}/${start.getDate()}`; 
                that.endDateQuery =  `${end.getFullYear()}/${end.getMonth() + 1}/${end.getDate()}`;
                this.onRefresh() 
            },
             
            onAddPathClick(){
                this.$router.push('/keep/add')
            }
        }
    }; 
</script>
  
<style scoped>  
    .title>span {
        display: block;
        font-size: 24px;
    }

    .goods-card {
        margin: 0;

    }

    .swipe-button {
        height: 100%;
    }

    .h1 {
        height: calc(100% - var(--van-nav-bar-height));
    }

    .dataview { 
        height: 100%;
        margin-bottom: 150px;
        width: 100%;
        overflow-y: auto;
    }
</style>