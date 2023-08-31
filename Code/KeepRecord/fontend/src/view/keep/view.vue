 <template>
    <div class="title"> </div>
    <van-nav-bar :title="contentTitle"></van-nav-bar>
    <van-list class="dataview" v-model="loading" :finished="finished" finished-text="没有更多了" @load="onLoad"> 
        <van-swipe-cell v-for="(item, index) in list" :key="index">
            <van-card :num="item.RecId" :price="item.RecId" desc="描述信息" title="商品标题" class="goods-card"
                thumb="https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg" />
            <template #right> <van-button square text="删除" type="danger" class="delete-button" /></template>
        </van-swipe-cell> 
    </van-list> 
</template>   
 

<script setup  lang="jsx">
import { ref } from 'vue';
import { useRouter, useRoute } from 'vue-router';

const router = useRouter();
const route = useRoute();
const active = ref(route.path);   
</script>



<script lang="jsx"> 
import { querykeeprecord } from "@/api/keep"; 

export default {
    components: {

    },
    data() {

        return {
            contentTitle: '', //  

            loading: false, 		// 是否处在加载状态
            finished: false, 		// 是否已加载完成
            error: false, 		// 是否加载失败
            list: [],				// 列表
            page: 1,				// 分页
            page_size: 10,		// 每页条数
            total: 0,
            testlist: [
                { num: 1, name: 'aa', price: 1234 },
                { num: 2, name: 'bb', price: 333 },
                { num: 3, name: 'vv', price: 444 },
                { num: 3, name: 'vv', price: 444 },
                { num: 3, name: 'vv', price: 444 }, 
            ]
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
        }, // 获取列表数据方法
        async getList() { 
            let { data: res } =await querykeeprecord({
                PageIndex: this.page,
                PageCount: this.page_size,
            })
            if (res.Data.length === 0) {  		// 判断获取数据条数若等于0
               // this.list = [];				// 清空数组
                this.finished = true;		// 停止加载
            }
            // 若数据条数不等于0
            this.total = res.Total;		// 给数据条数赋值
        
            this.list.push(...res.Data)	// 将数据放入list中
            this.loading = false;			// 加载状态结束 
            // 如果list长度大于等于总数据条数,数据全部加载完成
            if (this.list.length >= res.Total) {
                this.finished = true;		// 结束加载状态
            } 
        //     console.log("this.list.length :", this.list.length) ;
        //     console.log("this.Total :", this.Total) ;
        //    console.log(" this.finished :", this.finished ) ;
        },
        // 被 @load调用的方法
        onLoad() { // 若加载条到了底部
            if(!this.finished){

             
            this.getList();	   this.page++;		
            }			
            // let timer = setTimeout(() => {	// 定时器仅针对本地数据渲染动画效果,项目中axios请求不需要定时器
            //     this.getList();					// 调用上面方法,请求数据
            //     this.page++;					// 分页数加一
            //     this.finished && clearTimeout(timer);//清除计时器
            // }, 100);
        },
        // 加载失败调用方法
        onRefresh() {
            this.finished = false; 		// 清空列表数据
            this.loading = true; 			// 将 loading 设置为 true，表示处于加载状态
            this.page = 1;				// 分页数赋值为1
            this.list = [];				// 清空数组
            this.onLoad(); 				// 重新加载数据
        },
        informList() {
            return {
                data: {
                    total: 10,
                    list: [{ num: 1, name: 'aa', price: 1234 },
                    { num: 2, name: 'bb', price: 333 },
                    { num: 3, name: 'vv', price: 444 },
                    { num: 3, name: 'vv', price: 444 },
                    { num: 3, name: 'vv', price: 444 },
                    { num: 3, name: 'vv', price: 4442 }, 

                    ]
                }
            }
        }



    }
}; 
</script>

<style scoped> .title>span {
     display: block;
     font-size: 24px;
     ;
 }

 .goods-card {
     margin: 0;

 }

 .delete-button {
     height: 100%;
 }

 .dataview {
      height:calc(100% - 46px);
      margin-bottom: 150px;
     width: 100%;
     overflow-y: auto;
 }
</style>