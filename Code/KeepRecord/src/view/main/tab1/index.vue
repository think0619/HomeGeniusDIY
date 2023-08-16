<template>
    <div class="contentTitle">
        <span>{{ contentTitle }}</span>
    </div>
    <div class="btnArea">
        <van-space direction="vertical" fill size="2vh">
            <div  id="page1_btn1" class="cmdCustomBtn1 cmdbtn1-1" @click="onClickCmdBtn('page1_btn1')">
                <span class="btn_name">现状及解决方案</span>
                <van-image round class="crtIcon" fit="cover" :src="correcticon" v-show="showlist.page1_btn1" />
            </div>
            <div  id="page1_btn2" class="cmdCustomBtn1 cmdbtn1-2" @click="onClickCmdBtn('page1_btn2')">
                <span class="btn_name">人员升入井流线</span>
                <van-image round class="crtIcon" fit="cover" :src="correcticon" v-show="showlist.page1_btn2" />
            </div>
            <div  id="page1_btn3" class="cmdCustomBtn1 cmdbtn1-3" @click="onClickCmdBtn('page1_btn3')">
                <span class="btn_name">洗靴、衣服分拣流线</span>
                <van-image round class="crtIcon" fit="cover" :src="correcticon" v-show="showlist.page1_btn3" />
            </div>
            <div  id="page1_btn4" class="cmdCustomBtn1 cmdbtn1-4" @click="onClickCmdBtn('page1_btn4')">
                <span class="btn_name">智能井口设备</span>
                <van-image round class="crtIcon" fit="cover" :src="correcticon" v-show="showlist.page1_btn4" />
            </div>
        </van-space>
        <van-image round class="cmdResetBtn" fit="cover" @click="onclickReset('page1_reset')" :src="resetBtnImg" />
   
        <div class="arrowdiv">
            <van-row justify="space-between" align="center" gutter="0">

                <van-col span="8" class="arrowCol"> <van-image round class=" " fit="cover" @click="changeBtn('pre')"
                        :src="leftarrow" /></van-col>
                <van-col span="8" class="arrowCol"></van-col>
                <van-col span="8" class="arrowCol"> <van-image round class=" " fit="cover" @click="changeBtn('next')"
                        :src="rightarrow" /></van-col>
            </van-row>
        </div>
   </div>
</template>   

<script setup lang="jsx">
import resetBtnImg from '@/assets/img/resetbtn.png';
import correcticon from '@/assets/img/icons8-checkmark.svg';
import p4r1 from '@/assets/img/p4/e1.png';

import leftarrow from '@/assets/img/btn/icons8-left-arrow-64.png';
import rightarrow from '@/assets/img/btn/icons8-right-arrow-64.png';
</script> 

<script lang="jsx">
export default {
    components: {
    },
    data() {
        return {
            title: '绿色矿山展区',
            contentTitle: '智能井口',
            active: 0,
            show: false, 
            curBtnIndex: -1,
            // scrolltop:0,
            pageBtnArrayKey:   [   'page1_btn1', 'page1_btn2', 'page1_btn3', 'page1_btn4',    ],
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
    },
    mounted() {
        this.$emit("onChangeTitle", this.title);
    },
    methods: {
        onClickCmdBtn(btnkey) {
            this.$emit('onDoCMDSend', btnkey);
            for (let key in this.showlist) {
                if (key == btnkey) {
                    this.showlist[key] = true;
                }else{
                    this.showlist[key] = false;
                }
            }

            for(let i=0;i<this.pageBtnArrayKey.length;i++){
                if (this.pageBtnArrayKey[i] == btnkey) {
                    this.curBtnIndex = i;
                }
            } S
        },
        onclickReset(btnkey) {
            this.curBtnIndex=-1; 
            document.getElementById(this.pageBtnArrayKey[0]).scrollIntoView();
            this.$emit('onDoCMDSend', btnkey);
            for (let key in this.showlist) {
                this.showlist[key] = false;
            }
        },
        
        changeBtn(direction) {
            if (direction == 'pre') {
                if (this.curBtnIndex > 0) {
                    this.curBtnIndex = this.curBtnIndex - 1;
                }

            } else if (direction == 'next') {
                if (this.curBtnIndex < (this.pageBtnArrayKey.length - 1)) {
                    this.curBtnIndex = this.curBtnIndex + 1;
                }
            }
            for (let key in this.showlist) {
                if (key == this.pageBtnArrayKey[this.curBtnIndex]) {
                    this.showlist[key] = true;
                }
            }
            document.getElementById(this.pageBtnArrayKey[this.curBtnIndex]).scrollIntoView();
            this.onClickCmdBtn(this.pageBtnArrayKey[this.curBtnIndex]);
        }
    }
}; 
</script>