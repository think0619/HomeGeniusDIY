<template>
    <div class="title"> </div>
    <van-nav-bar :title="contentTitle"></van-nav-bar>
    <van-pull-refresh class="h1" v-model="refreshing" @refresh="onRefresh">
            <van-list class="dataview" v-model="loading" :finished="finished" finished-text="没有更多了" @load="onLoad" >
                <van-swipe-cell v-for="(item, index) in list" :key="index" :name="item.RecID" @click="swipeClick" @open="swipeOpen"  :before-close="beforeClose">
                    <van-card   class="goods-card" 
                    :centered="false"
                    :tag="item.TypeName"
                    :price="item.index" 
                    currency=""
                    :title="item._RecordDate" 
                    :desc="item.DescInfo"  
                    thumb="https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg" />
                    <template #right>
                        <van-button square type="danger" text="删除" class="swipe-button"  @click="delClick"  />
                        <van-button square type="primary" text="修改"  class="swipe-button"  @click="editClick" />
                    </template>
                </van-swipe-cell>
            </van-list>
        </van-pull-refresh>
</template>    

<script setup  lang="jsx">
import { ref } from 'vue';
import { useRouter, useRoute } from 'vue-router';

const router = useRouter();
const route = useRoute();
const active = ref(route.path);   
</script> 

<script lang="jsx">
import { showConfirmDialog,showSuccessToast, showFailToast  } from 'vant';
import { querykeeprecord,deletekeeprecord } from "@/api/keep";

export default {
    components: {
    },
    data() {
        return {
            contentTitle: '', //   
            refreshing:false,
            loading: false, 		// 是否处在加载状态
            finished: false, 		// 是否已加载完成
            error: false, 		// 是否加载失败
            list: [],				// 列表
            page: 1,				// 分页
            page_size: 10,		// 每页条数
            total: 0, 
            selectedItem:'' 
        };
    },
    computed: {
    },
    setup() {
    },
    mounted() {
        this.setTitle();
    },
    methods: {
        setTitle() {
            this.contentTitle = "运动数据";
        },
        // 获取列表数据方法
        async getList() {
            let _this = this;
            let { data: res } = await querykeeprecord({
                PageIndex: _this.page,
                PageCount: _this.page_size,
            })
            if (res.Data.length === 0) {
                _this.finished = true;
            }
            _this.total = res.Total;		// 
            _this.list.push(...res.Data)	//  

            let index=1;
            _this.list.forEach((e)=>{
                e.index=index;
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
                 this.refreshing=false;
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
                let _this=this; 
                let res = deletekeeprecord({
                    RecID:_this.selectedItem
                  });
                  console.log(res)
                  if(res.Status==1){
                    showSuccessToast(res.Msg);
                  }else{
                    showFailToast(res.Msg);
                  } 
            }).catch(() => {
                showFailToast("xx");
            });
        },
        editClick(){
            this.$router.push('/keep/add')
        },
        swipeClick(position){
           // console.log('swiopeclick',position);
        }, 
        swipeOpen(item){
            let _this=this;
            _this.selectedItem=item.name;
            
            console.log('swipeopen name', _this.selectedItem);
           // console.log('swipeopen position',item.position,);
        },
        beforeClose(position ){
            return true;
            console.log(position)
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
 .h1{
    height: calc(100% - var(--van-nav-bar-height));
 }

 .dataview {
    
      height: 100% ;  
     margin-bottom: 150px;
     width: 100%;
     overflow-y: auto;
 }
</style>