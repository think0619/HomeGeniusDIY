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

    <van-pull-refresh class="h1" v-model="refreshing" @refresh="onRefresh">
        <van-list class="dataview" v-model="loading" :finished="finished" finished-text="没有更多了" @load="onLoad">
            <van-swipe-cell v-for="(item, index) in list" :key="index" :name="item.Id" @click="swipeClick"
                @open="swipeOpen" :before-close="beforeClose">
                <van-card class="goods-card" :centered="false" :tag="item.TypeName" :price="item.Id" currency=""
                    :title="item._RecordDate" :desc="item.DescInfo"
                    :thumb="item.ImgSrc" />
                <template #right>
                    <van-button square type="danger" text="删除" class="swipe-button" @click="delClick" />
                    <van-button square type="primary" text="修改" class="swipe-button" @click="editClick" />
                </template>
            </van-swipe-cell>
        </van-list>
    </van-pull-refresh>
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

    import boxing from "@/assets/img/sport/icons8-boxing-96.png"
    import pushups from "@/assets/img/sport/icons8-pushups-80.png"
    import riding from "@/assets/img/sport/icons8-riding-64.png"
    import run from "@/assets/img/sport/icons8-run-96.png"
    import skipping from "@/assets/img/sport/icons8-skipping-rope-96.png"

    export default {
        components: {
        },
        data() {
            return {
                contentTitle: '', //   
                refreshing: false,
                loading: false, 		// 是否处在加载状态
                finished: false, 		// 是否已加载完成
                error: false, 		// 是否加载失败
                list: [],				// 列表
                page: 1,				// 分页
                page_size: 10,		// 每页条数
                total: 0,
                selectedItem: '',

                showCalendarForm: false,
                startDateQuery: '',
                endDateQuery: '',  
                selectedDatePicker: ''
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
                this.contentTitle = "运动记录";
            },
            // 获取列表数据方法
            async getList() {
                let _this = this;
                let { data: res } = await querykeeprecord({
                    PageIndex: _this.page,
                    PageCount: _this.page_size,
                    DateStart: _this.startDateQuery,
                    DateEnd: _this.endDateQuery,
                })
                if (res.Data.length === 0) {
                    _this.finished = true;
                }
                _this.total = res.Total;		// 
                res.Data.forEach(element => {
                    if(element.TypeId==1){
                        element.ImgSrc=run
                    }else if(element.TypeId==2){
                        element.ImgSrc=skipping
                    }else if(element.TypeId==3){
                        element.ImgSrc=boxing
                    }else if(element.TypeId==4){
                        element.ImgSrc=pushups
                    }else if(element.TypeId==5){
                        element.ImgSrc=riding
                    }
                }); 
                _this.list.push(...res.Data)	//  

                let index = 1;
                _this.list.forEach((e) => {
                    e.index = index;
                    index++;
                });
                _this.loading = false;			// 

                if (_this.list.length >= res.Total) {
                    _this.finished = true;		// 
                }
            },
            onLoad() {
                if (!this.finished) {
                    this.getList();
                    this.refreshing = false;
                    this.page++;
                }
            },
            onRefresh() {
                this.page = 1;				//  
                this.list = [];				//  
                this.finished = false; 		// 
                this.loading = true; 		// 将 loading 设置为 true，表示处于加载状态
                this.onLoad(); 				// 重新加载数据
            },
            delClick() {
                showConfirmDialog({
                    title: '提示',
                    message: '确认删除吗？',
                }).then(() => {
                    // on confirm
                    let _this = this;
                    deletekeeprecord({ Id: _this.selectedItem }).then(response => {
                        let res = response.data;
                        if (res.Status == 1) {
                            showSuccessToast({
                                message: res.Msg,
                                wordBreak: 'break-word'
                            });
                            _this.onRefresh();
                        } else {
                            showFailToast({
                                message: res.Msg,
                                wordBreak: 'break-word'
                            });
                        }
                    }).catch(() => {
                    });
                }).catch(() => {
                    showFailToast("xx");
                });
            },
            editClick() {
                this.$router.push({ path: '/keep/add', query: { Id: this.selectedItem } });
            },
            swipeClick(position) {
                // console.log('swiopeclick',position);
            },
            swipeOpen(item) {
                let _this = this;
                _this.selectedItem = item.name;

                console.log('swipeopen name', _this.selectedItem);
                // console.log('swipeopen position',item.position,);
            },
            beforeClose(position) {
                return true;
                console.log(position)
            },
            datepickerClick(datepickername) {
                this.selectedDatePicker = datepickername;
                this.showCalendarForm = true;
            },
            onConfirmDate(dates) {
                let that = this;
                that.showCalendarForm = false;
                const [start, end] = dates;  
                that.startDateQuery = `${start.getFullYear()}/${start.getMonth() + 1}/${start.getDate()}`; 
                that.endDateQuery =  `${end.getFullYear()}/${end.getMonth() + 1}/${end.getDate()}`;
                this.onRefresh() 
            },
            datepickerCustom(tag) {
                let currentDate = new Date();
                if (tag === 'today') {
                    this.startDateQuery = `${currentDate.getFullYear()}/${currentDate.getMonth() + 1}/${currentDate.getDate()}`; 
                    this.endDateQuery =   this.startDateQuery;
                } else if (tag === 'seven') { 
                    let startDate=new Date(currentDate.getTime());; 
                     startDate.setDate(currentDate.getDate() -7);
                     this.startDateQuery = `${startDate.getFullYear()}/${startDate.getMonth() + 1}/${startDate.getDate()}`; 
                     this.endDateQuery=`${currentDate.getFullYear()}/${currentDate.getMonth() + 1}/${currentDate.getDate()}`;  
                } else if (tag === 'thirty') {
                    let startDate=new Date(currentDate.getTime());; 
                    startDate.setDate(currentDate.getDate() -30);
                    this.startDateQuery = `${startDate.getFullYear()}/${startDate.getMonth() + 1}/${startDate.getDate()}`; 
                    this.endDateQuery=`${currentDate.getFullYear()}/${currentDate.getMonth() + 1}/${currentDate.getDate()}`;  
                } 
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