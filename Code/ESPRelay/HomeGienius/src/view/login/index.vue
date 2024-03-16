<template>
    <div class="title">
        <van-nav-bar :title="contentTitle"></van-nav-bar>
    </div>
    <div class="control">
        <van-space direction="vertical" fill size="15px">
            <van-button type="primary" block @click="change('weather');">天气</van-button>
            <van-button type="primary" block @click="change('control');">控制</van-button>
            <van-button type="primary" block @click="change('raspclock');">闹钟&收音机</van-button>
            <van-button type="primary" block @click="change('aircondition');">红外控制【空调、音响】</van-button>
            <van-button type="primary" block @click="change('misc');">杂项</van-button>
        </van-space>
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
import { showSuccessToast, showFailToast, showToast } from 'vant';
import { login } from "@/api/config"; 
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
        const idcode = this.$route.query.idcode;
        if (idcode != null) {
            login(idcode).then((reqresult) => {
                if (reqresult.data) {
                    let result = reqresult.data; //Msg Token 
                    if (result.Status == 1) {
                        var token = result.Token;
                        //get mqtt info 
                        if (token != null) {
                            this.$store.commit('setToken',token)
                            showSuccessToast({
                                "wordBreak": "break-word",
                                "message": "Authentication successful.."  ,
                                "duration": 800
                            });
                        }
                    } else {
                        showFailToast({
                            "wordBreak": "break-word",
                            "message": "Identity expired."+result.Msg,
                            "duration": 800
                        });
                    }
                } else {
                    showFailToast('login error')
                }
            })
        } else {
            showFailToast('The url is error.')
        }
    },
    methods: {
        setTitle() {
            this.contentTitle = "Home Genius";
        },  
        change(flag) {
            let token = this.$store.state.token;
            if(token){
                let route='';
            switch(flag)
            {
                case 'control':route='/esp';break;
                case 'weather':route='/weather';break;
                case 'raspclock':route='/raspclock';break;
                case 'aircondition':route='/ir';break;
                case 'misc':route='/miscctrl';break;

                
            }
            this.$router.push({  
                path: route,   
                query: {   }
            })
          //  this.$router.replace(route)

            }else{
                showFailToast({
                            "wordBreak": "break-word",
                            "message": "Missing authentication information.",
                            "duration": 800
                        }); 
            } 
        }
    }
}; 
</script>

<style scoped>
.control{
    margin: 15px 20px 0 20px;
}
.sysname{
    margin: 5px 0;
}

.title>span {
    display: block;
    font-size: 24px;
}
</style>