<template>
    <div class="contentTitle"> 
        <span>{{ contentTitle }}</span>
    </div>
    <div class="btnArea" >
        <van-button type="primary" size="large" block class="cmdCustomBtn2" @click="onClickCmdBtn('page3_btn1')">园区暂停</van-button>
        <van-button type="primary" size="large" block class="cmdCustomBtn2" @click="onClickCmdBtn('page3_btn2')">园区开始</van-button>
        <van-button type="primary" size="large" block class="cmdCustomBtn2" @click="onClickCmdBtn('page3_btn3')">井口暂停</van-button>
        <van-button type="primary" size="large" block class="cmdCustomBtn2" @click="onClickCmdBtn('page3_btn4')">井口开始</van-button> 
    </div>
    <van-image round class="cmdResetBtn" fit="cover" 
         @click="onclickReset('page3_reset')"
        :src="resetBtnImg" />
</template> 
<script setup lang="jsx"> 
import resetBtnImg from '@/assets/img/setting.svg'
</script>
<script  lang="jsx">
export default {
    components: {
    },
    data() { 
         return {
            title:'绿色低碳智慧园区',
            contentTitle:'绿色·低碳·智慧园区',
            active: 0,
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
        onClickLeft2() {
            // this.$parent.onClickLeft();
             this.$emit('onClickLeftChild',title); 
        },
        onClickCmdBtn(btnkey) {
            this.$emit('onDoCMDSend', btnkey); 
        },
    }
}; 
</script>