<template>
    <div class="title">
        <van-nav-bar :title="contentTitle" left-text="" left-arrow @click-left="onClickLeft"></van-nav-bar>
    </div>
    <div class="control">

        <div style="margin-top: 20px;" class="content">
            <div class="sysname"><span>Air Condition</span></div>
            <van-space size="1rem" wrap>
                <van-button type="danger" @click="sendmsg('aircondition', 'off');">Power Off</van-button> 
            </van-space>
            <van-divider :style="{ color: '#1989fa', borderColor: '#1989fa', padding: '0' ,margin:'2px 0' }" > Cold </van-divider>
            <van-space size="1rem" wrap fill="true" >
                <van-button type="primary"  :icon="coldimg" @click="sendmsg('aircondition', 'cold24');">24℃</van-button>
                <van-button type="primary"  :icon="coldimg" @click="sendmsg('aircondition', 'cold25');">25℃</van-button>
                <van-button type="primary"  :icon="coldimg" @click="sendmsg('aircondition', 'cold25T1h');">25℃ 1h</van-button>
                <van-button type="primary"  :icon="coldimg" @click="sendmsg('aircondition', 'cold25T2h');">25℃ 2h</van-button> 
            </van-space> 
            <van-divider :style="{ color: '	#F4A460', borderColor: '	#F4A460', padding: '0' ,margin:'2px 0' }" > Warm </van-divider>
             <van-space size="1rem" wrap>
                <van-button type="warning"  :icon="hotimg" color="linear-gradient(to right, #1989fa, #ee0a24)" @click="sendmsg('aircondition', 'warm25');">25℃</van-button> 
                <van-button type="warning"  :icon="hotimg" color="linear-gradient(to right, #1989fa, #ee0a24)" @click="sendmsg('aircondition', 'warm26');">26℃</van-button> 
                <van-button type="warning" :icon="hotimg" color="linear-gradient(to right, #1989fa, #ee0a24)" @click="sendmsg('aircondition', 'warm27');">27℃</van-button> 
                <van-button type="warning" :icon="hotimg" color="linear-gradient(to right, #1989fa, #ee0a24)" @click="sendmsg('aircondition', 'warmon');">28℃</van-button> 
                <van-button type="warning" :icon="hotimg" color="linear-gradient(to right, #1989fa, #ee0a24)" @click="sendmsg('aircondition', 'warm26T1h');">26℃ 1h</van-button> 
                <van-button type="warning" :icon="hotimg" color="linear-gradient(to right, #1989fa, #ee0a24)" @click="sendmsg('aircondition', 'warm26T2h');">26℃ 2h</van-button> 
            </van-space>
        </div>
        <div style="margin-top: 20px;" class="content2">
            <div class="sysname"><span>Polk Bluetooth speaker</span></div>
            <van-row gutter="5">
                <van-col span="6">
                    <van-button class="bluebtn" type="primary" @click="sendmsg('bluetoothaudio', 'power');">Power</van-button>
                </van-col>
                <van-col span="6">
                    <van-button class="bluebtn" type="primary" @click="sendmsg('bluetoothaudio', 'mute');">Mute</van-button>
                </van-col>
            </van-row>

            <van-row class="rowmagrin" gutter="5">
                <van-col span="6">
                    <van-button class="bluebtn" type="primary" @click="sendmsg('bluetoothaudio', 'fiberoptic');">FiberOptic</van-button>
                </van-col>
                <van-col span="6">
                    <van-button class="bluebtn" type="primary" @click="sendmsg('bluetoothaudio', 'aux');">AUX</van-button>
                </van-col>
                <van-col span="6">
                    <van-button  class="bluebtn"  type="primary" @click="sendmsg('bluetoothaudio', 'bluetooth');">Bluetooth</van-button>
                </van-col>
              
            </van-row> 

                <van-row class="rowmagrin" gutter="5">
                    <van-col span="6">
                         <van-button type="primary" @click="sendmsg('bluetoothaudio', 'bassplus');">Bass +</van-button>
                    </van-col>
                    <van-col span="6">
                        <van-button type="primary" @click="sendmsg('bluetoothaudio', 'bassminus');">Bass -</van-button>
                    </van-col>
                    <van-col span="6">
                        <van-button type="primary" @click="sendmsg('bluetoothaudio', 'volplus');">VOL +</van-button>
                    </van-col>
                    <van-col span="">
                        <van-button type="primary" @click="sendmsg('bluetoothaudio', 'volminus');">VOL -</van-button>
                    </van-col>
                </van-row> 

                <van-row class="rowmagrin" gutter="5">
                <van-col span="6">
                    <van-button class="bluebtn" type="primary" @click="sendmsg('bluetoothaudio', 'modemovie');">Movie</van-button>
                </van-col>
                <van-col span="6">
                    <van-button class="bluebtn" type="primary" @click="sendmsg('bluetoothaudio', 'modenight');">Night</van-button>
                </van-col>
                <van-col span="6">
                    <van-button  class="bluebtn"  type="primary" @click="sendmsg('bluetoothaudio', 'modemusic');">Music</van-button>
                </van-col>
            </van-row> 
          
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
import { showSuccessToast, showFailToast, showToast, } from 'vant';
import { login } from "@/api/config";
import { getMQTTInfo } from "@/api/mqtthelper";
import * as mqtt from "mqtt/dist/mqtt.min"; 
import coldimg from "@/assets/img/制冷1.svg";
import hotimg from "@/assets/img/制热.svg";
export default {
    components: {
    },
    data() {
        return {
            contentTitle: '', //  
            currentdate: '', //  
            mqttclient: null,
            hotimg:hotimg,
            coldimg:coldimg,
        };
    },
    computed: {
    },
    setup() {
    },
    mounted() {
        this.setTitle();
        let token = this.$store.state.token;
        if (token) {
            getMQTTInfo(token).then((mqttr) => {
                if (mqttr != null && mqttr.Status == 1) {
                    let mqttdata = mqttr.Data;
                    let mqtt_wsurl = mqttdata.wsurl;
                    let mqtt_username = mqttdata.username;
                    let mqtt_password = mqttdata.password;
                    this.connectMQTT(mqtt_wsurl, mqtt_username, mqtt_password)
                } else {
                    showFailToast({
                        "wordBreak": "break-word",
                        "message": "Identity expired." + result.Msg,
                        "duration": 800
                    });
                }
            });
        } else {
            showFailToast('need to login')
        }
    },
    methods: {
        setTitle() {
            this.contentTitle = "Air condition";
        },
        onClickLeft() {
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
                that.mqttclient.subscribe('Aircondition', function (err) {
                    if (!err) { } else {
                    }
                })
            })
            that.mqttclient.on('message', function (topic, message) {
                //   console.log(topic.toString())
                //  console.log(message.toString())
            })
        },
        sendmsg(topicflag, msg) {
            var topic = ""
            switch (topicflag) {
                case "aircondition": topic = "Aircondition"; break;
                case "bluetoothaudio": topic = "BLAudio"; break;
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
        },
    }
}; 
</script>

<style scoped>
.control {
    margin: 0 20px;
}

.sysname {
    margin: 5px 0;
}

.title>span {
    display: block;
    font-size: 24px;
}

.bluebtn{
    display: block;
    width:80px !important;
}
.rowmagrin{
    margin: 18px 0 0 0;
}
.content button{
    width: 100px;
}
.content2 button{
    width: 80px;
}
</style>