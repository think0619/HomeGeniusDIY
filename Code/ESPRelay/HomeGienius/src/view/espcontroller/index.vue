<template>
    <div class="title">
        <van-nav-bar :title="contentTitle" left-text="" left-arrow  @click-left="onClickLeft"  ></van-nav-bar>
    </div>
    <div class="control"> 
        <div style="margin-top: 15px;">
            <div class="sysname"><span>ESP32 时钟</span></div>
            <van-space size="1rem">
                <van-button type="primary" @click="sendmsg('clock', 'on');">打开时钟</van-button>
                <van-button type="primary" @click="sendmsg('clock', 'off');">关闭时钟</van-button>
                <van-button type="primary" @click="sendmsg('clock', 'synctime');">同步时间</van-button> 
                <van-button type="primary" @click="sendmsg('clock', 'reset');">Reset</van-button> 
            </van-space>
        </div>

        <div style="margin-top: 30px;">
            <div class="sysname"><span>OPS</span></div> 
            <van-space size="1rem">
                <van-button type="primary" @click="sendmsg('ops','p');">打开电源</van-button>
                <van-button type="primary" @click="sendmsg('ops','r');">关闭电源</van-button>
                <van-button type="primary" @click="sendmsg('ops','diskpower');">启动硬盘</van-button>
            </van-space>
        </div>
        <div style="margin-top: 30px;">
            <div class="sysname"><span>Raspberry Pi Clock</span></div> 
            <van-space size="1rem">
                <van-button type="primary" @click="sendmsg('rasp','play');">开始</van-button>
                <van-button type="primary" @click="sendmsg('rasp','stop');">关闭</van-button>
                <van-button type="primary" @click="sendmsg('rasp','pause');">暂停</van-button>
            </van-space>
        </div>
        <div style="margin-top: 30px;">
            <div class="sysname"><span>锁屏</span></div> 
            <van-space size="1rem">
                <van-button type="primary" @click="sendmsg('lock','LOCKPC_PCLockEmma');">锁屏Emma</van-button>
                <van-button type="primary" @click="sendmsg('lock','LOCKPC_PCLockThink');">锁屏XS</van-button> 
            </van-space>
        </div> 
        <div style="margin-top: 10px;">
            <div class="sysname"><span>锁屏</span></div> 
            <van-space size="1rem" direction="vertical" fill>
            <van-cell-group inset><van-field v-model="msgcontent" label="内容" placeholder="请输入消息" clearable /></van-cell-group>
            <van-space size="1rem" >
            <van-button type="primary" @click="sendmsg('msg','Msg_PCLockEmma');" >to Emma</van-button>
            <van-button type="primary" @click="sendmsg('msg','Msg_PCLockThink');">to Think</van-button> </van-space>
        </van-space>
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
import { showSuccessToast, showFailToast, showToast } from 'vant';
import { login } from "@/api/config";
import { getMQTTInfo } from "@/api/mqtthelper";
import * as mqtt from "mqtt/dist/mqtt.min"; 

export default {
    components: {
    },
    data() {
        return {
            contentTitle: '', //  
            currentdate: '', //  
            mqttclient: null,
            msgcontent:''
        };
    },
    computed: {
    },
    setup() { 
    },
    mounted() {
        this.setTitle(); 
        let token = this.$store.state.token;
        if (token ) {
            getMQTTInfo(token).then((mqttr) => {
                if (mqttr != null && mqttr.Status == 1) {
                    let mqttdata = mqttr.Data;
                    let mqtt_wsurl = mqttdata.wsurl;
                    let mqtt_username = mqttdata.username;
                    let mqtt_password = mqttdata.password;
                    this.connectMQTT(mqtt_wsurl, mqtt_username, mqtt_password)
                }else{
                    showFailToast({
                            "wordBreak": "break-word",
                            "message": "Identity expired."+result.Msg,
                            "duration": 800
                        });
                }
            });
        } else {
            showFailToast('need login')
        }
    },
    methods: {
        setTitle() {
            this.contentTitle = "ESP32 Controller";
        },
        onClickLeft(){
            
            history.back();
        },
        connectMQTT(_url, _username, _password) {
            let that = this;
            that.mqttclient = mqtt.connect(_url, {
                keepalive: 60,
                reconnectPeriod: 3000,
                username: _username,
                password: _password
            })
            that.mqttclient.on('connect', function () {
                showSuccessToast({
                    "wordBreak": "break-word",
                    "message": "The mqtt client has connected.",
                    "duration": 800
                });
                that.mqttclient.subscribe('ShowClockTime', function (err) {
                    if (!err) { } else { }
                })
                that.mqttclient.subscribe('OPSRelayController', function (err) {
                    if (!err) { } else {
                    }
                })
            })
            that.mqttclient.on('message', function (topic, message) {
                // console.log(topic.toString())
                // console.log(message.toString())
            })
        },
        sendmsg(topicflag, msg) {
            var topic = ""
            switch (topicflag) {
                case "clock": topic = "ShowClockTime"; break;
                case "ops": topic = "OPSRelayController"; break;
                case "lock": topic = "LockPC"; break;
                case "msg": topic = "Msg"; break;
            } 
            if (topic != '') { 
                let that = this; 
                if(topic=="Msg"){
                    msg=`${msg}_${that.msgcontent}`
                    console.log(msg)
                }
                if (that.mqttclient != null) {
                    that.mqttclient.publish(topic, msg, {
                        qos: 0,
                        retain: false
                    }, function (err) {
                        if (err) {
                            showToast({
                                "wordBreak": "break-word",
                                "message": err,
                                "duration": 800,
                                'icon': 'fail'
                            })
                        } else {
                            showToast({
                                "wordBreak": "break-word",
                                "message": "Success.",
                                "duration": 800,
                                'icon': 'success'
                            })
                        }
                    })
                } else {
                    showToast({
                        "wordBreak": "break-word",
                        "message": "The mqtt client has disconnected.",
                        "duration": 800,
                        'icon': 'fail'
                    })
                }
            } else {
                showToast({
                    "wordBreak": "break-word",
                    "message": "The topic has error.",
                    "duration": 800,
                    'icon': 'fail'
                })
            }
        }
    }
}; 
</script>

<style scoped>
.control{
    margin: 0 20px;
}
.sysname{
    margin: 5px 0;
}

.title>span {
    display: block;
    font-size: 24px;
}
</style>