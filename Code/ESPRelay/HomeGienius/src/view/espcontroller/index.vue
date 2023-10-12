<template>
    <div class="title">
        <van-nav-bar :title="contentTitle"></van-nav-bar> 
    </div>
    <div class="control"> 
        <div style="margin-top: 15px;">
            <div class="sysname"><span>ESP32 时钟</span></div>
            <van-space size="1rem">
                <van-button type="primary" @click="sendmsg('clock', 'on');">打开时钟</van-button>
                <van-button type="primary" @click="sendmsg('clock', 'off');">关闭时钟</van-button>
                <van-button type="primary" @click="sendmsg('clock', 'synctime');">同步时间</van-button> 
            </van-space>
        </div>

        <div style="margin-top: 30px;">
            <div class="sysname"><span>OPS</span></div> 
            <van-space size="1rem">
                <van-button type="primary" @click="sendmsg('ops','poweron');">打开电源</van-button>
                <van-button type="primary" @click="sendmsg('ops','poweroff');">关闭电源</van-button>
                <van-button type="primary" @click="sendmsg('ops','diskpower');">启动硬盘</van-button>
            </van-space>
        </div>
        <div style="margin-top: 50px;">
        
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
                            getMQTTInfo(token).then((mqttr) => {
                                if (mqttr != null && mqttr.Status == 1) {
                                    let mqttdata = mqttr.Data;
                                    let mqtt_wsurl = mqttdata.wsurl;
                                    let mqtt_username = mqttdata.username;
                                    let mqtt_password = mqttdata.password;
                                    this.connectMQTT(mqtt_wsurl, mqtt_username, mqtt_password)
                                }
                            });
                        }
                    } else {

                    }
                } else {
                    showFailToast('login error')
                }
            })
        } else {
            showFailToast('url error')
        }
    },
    methods: {
        setTitle() {
            this.contentTitle = "ESP32 Controller";
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
                case "ops1": topic = "OPSRelayController"; break;
            }
            if (topic != '') {
                let that = this;
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