<template>
    <div class="title">
        <van-nav-bar :title="contentTitle"></van-nav-bar>
        <span>{{ currentdate }}</span>
    </div>
    <div>
        <h3>系统设置</h3>
        <div style="margin-top: 15px;">
            <span>时钟</span>
            <van-space size="1rem">
                <van-button type="primary" @click="clickCmd('on');">打开时钟</van-button>
                <van-button type="primary" @click="clickCmd('off');">关闭时钟</van-button>
                <van-button type="primary" @click="clickCmd('synctime');">同步时间</van-button>
            </van-space>
        </div>

        <div style="margin-top: 15px;">
            <span>时钟</span>
            <van-space size="1rem">
                <van-button type="primary" @click="clickCmd('on');">打开电源</van-button>
                <van-button type="primary" @click="clickCmd('off');">关闭电源</van-button>
                <van-button type="primary" @click="clickCmd('synctime');">启动硬盘</van-button>
            </van-space>
        </div>

        <div style="margin-top: 150px;">
            <span>OPS</span>
            <button id="on_button" type="button" onclick="clickOpsCMD('poweron');">打开电源</button>
            <button id="on_button" type="button" onclick="clickOpsCMD('poweroff');">关闭电源</button>
            <button id="on_button" type="button" onclick="clickOpsCMD('diskpower');">启动硬盘</button>
        </div>
        <div style="margin-top: 50px;">
            <button id="on_button" type="button" onclick="clickCmd('synctime');">同步时间</button>
        </div>
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
export default {
    components: {
    },
    data() {
        return {
            contentTitle: '', // 

            currentdate: '', //  
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
            //this.contentTitle = this.getDate() + " 运动数据";
            this.contentTitle = "新增运动数据";
        },
        clickCmd(a) {
            alert(a)
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