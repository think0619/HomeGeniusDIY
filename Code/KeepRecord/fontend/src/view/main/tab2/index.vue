<template>
    <div class="contentTitle">
        <span>{{ contentTitle }}</span>
    </div>
    <div class="btnArea" id="btnArea">
        <van-space direction="vertical" fill size="2vh">
            <div id="page2_btn1" class="cmdCustomBtn1 cmdbtn1-1" @click="onClickCmdBtn('page2_btn1')">
                <span class="btn_name">工业互联网平台·介绍</span>
                <van-image round class="crtIcon" fit="cover" :src="correcticon" v-show="showlist.page2_btn1" />
            </div>
            <div id="page2_btn2" class="cmdCustomBtn1 cmdbtn1-2" @click="onClickCmdBtn('page2_btn2')">
                <span class="btn_name">工业互联网平台·关键技术</span>
                <van-image round class="crtIcon" fit="cover" :src="correcticon" v-show="showlist.page2_btn2" />
            </div>
            <div id="page2_btn3" class="cmdCustomBtn1 cmdbtn1-3" @click="onClickCmdBtn('page2_btn3')">
                <span class="btn_name">工业互联网平台·选煤厂</span>
                <van-image round class="crtIcon" fit="cover" :src="correcticon" v-show="showlist.page2_btn3" />
            </div>
            <div id="page2_btn4" class="cmdCustomBtn1 cmdbtn1-4" @click="onClickCmdBtn('page2_btn4')">
                <span class="btn_name">工业互联网平台·低代码</span>
                <van-image round class="crtIcon" fit="cover" :src="correcticon" v-show="showlist.page2_btn4" />
            </div>
            <div id="page2_btn5" class="cmdCustomBtn1 cmdbtn2-1" @click="onClickCmdBtn('page2_btn5')">
                <span class="btn_name">工业互联网平台·曹家滩选煤厂</span>
                <van-image round class="crtIcon" fit="cover" :src="correcticon" v-show="showlist.page2_btn5" />
            </div>
            <div id="page2_btn6" class="cmdCustomBtn1 cmdbtn2-2" @click="onClickCmdBtn('page2_btn6')">
                <span class="btn_name">智慧园区·1+2+4+N</span>
                <van-image round class="crtIcon" fit="cover" :src="correcticon" v-show="showlist.page2_btn6" />
            </div>
            <div id="page2_btn7" class="cmdCustomBtn1 cmdbtn2-3" @click="onClickCmdBtn('page2_btn7')">
                <span class="btn_name">智慧园区·智慧安防</span>
                <van-image round class="crtIcon" fit="cover" :src="correcticon" v-show="showlist.page2_btn7" />
            </div>
            <div id="page2_btn8" class="cmdCustomBtn1 cmdbtn2-4" @click="onClickCmdBtn('page2_btn8')">
                <span class="btn_name">智慧园区·智慧后勤</span>
                <van-image round class="crtIcon" fit="cover" :src="correcticon" v-show="showlist.page2_btn8" />
            </div>
            <div id="page2_btn9" class="cmdCustomBtn1 cmdbtn3-4" @click="onClickCmdBtn('page2_btn9')">
                <span class="btn_name">智慧园区·碳计量一体盒</span>
                <van-image round class="crtIcon" fit="cover" :src="correcticon" v-show="showlist.page2_btn9" />
            </div>
            <div id="page2_btn10" class="cmdCustomBtn1 cmdbtn3-2" @click="onClickCmdBtn('page2_btn10')">
                <span class="btn_name">智慧园区·综合能效</span>
                <van-image round class="crtIcon" fit="cover" :src="correcticon" v-show="showlist.page2_btn10" />
            </div>
            <div id="page2_btn11" class="cmdCustomBtn1 cmdbtn3-3" @click="onClickCmdBtn('page2_btn11')">
                <span class="btn_name">智慧园区·热循环</span>
                <van-image round class="crtIcon" fit="cover" :src="correcticon" v-show="showlist.page2_btn11" />
            </div>
            <div id="page2_btn12" class="cmdCustomBtn1 cmdbtn3-4" @click="onClickCmdBtn('page2_btn12')">
                <span class="btn_name">智慧园区·微电网</span>
                <van-image round class="crtIcon" fit="cover" :src="correcticon" v-show="showlist.page2_btn12" />
            </div>
            <div id="page2_btn13" class="cmdCustomBtn1 cmdbtn1-1" @click="onClickCmdBtn('page2_btn13')">
                <span class="btn_name">武汉院·管道技术</span>
                <van-image round class="crtIcon" fit="cover" :src="correcticon" v-show="showlist.page2_btn13" />
            </div>
        </van-space>
        <van-image round class="cmdResetBtn" fit="cover" @click="onclickReset('page2_reset')" :src="resetBtnImg" />
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
            contentTitle: '绿色展区主屏幕',
            active: 0,
            curBtnIndex: -1,
            // scrolltop:0,
            pageBtnArrayKey: 
            [   'page2_btn1', 'page2_btn2', 'page2_btn3', 'page2_btn4',
                'page2_btn5', 'page2_btn6', 'page2_btn7', 'page2_btn8',
                'page2_btn9', 'page2_btn10', 'page2_btn11', 'page2_btn12', 'page2_btn13',
            ],
            showlist: {
                page2_btn1: false,
                page2_btn2: false,
                page2_btn3: false,
                page2_btn4: false,
                page2_btn5: false,
                page2_btn6: false,
                page2_btn7: false,
                page2_btn8: false,
                page2_btn9: false,
                page2_btn10: false,
                page2_btn11: false,
                page2_btn12: false,
                page2_btn13: false,
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
            } 
        },
        onclickReset(btnkey) {
            this.curBtnIndex=-1; 
            // this.curBtnIndex = 0;
            // for (let i = 0; i < this.showlist.length; i++) {
            //     if (i == 0) {
            //         this.showlist[i] = true;
            //     } else {
            //         this.showlist[i] = false;
            //     } 
            // }

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

 