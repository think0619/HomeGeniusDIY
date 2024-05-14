<template>
    <div class="title">
        <van-nav-bar :title="contentTitle" left-text="" left-arrow @click-left="onClickLeft"></van-nav-bar>
    </div>
    <div class="control">
        <div style="margin-top: 20px;">
            <div>
                <van-cell center title="Is running">
                    <template #right-icon>
                        <van-switch v-model="vlcrunning" @change="onRunningChanged" />
                    </template>
                </van-cell>
            </div>
            <!-- <div style="margin-top:10px">
                <van-space size="1rem">
                    <van-button type="primary" @click="sendmsg('rasp', 'play');" block
                        style="width: 80px;">Start</van-button>
                    <van-button type="primary" @click="sendmsg('rasp', 'stop');" block
                        style="width: 80px;">Stop</van-button>
                    <van-button type="primary" @click="sendmsg('rasp', 'pause');" block
                        style="width: 80px;">Pause</van-button> 
                </van-space>
            </div> -->
            <div style="margin-top:15px;padding-bottom: 20px;" class="oparea">
                <van-space direction="vertical" fill size="1.5rem">
                    <span>Volume</span>
                    <div style="display: flex; justify-content: center;">
                        <van-slider v-model="volumeValue" @change="onVolumeChange" style="width: 90%;" />
                    </div>
                </van-space>
                <!-- <van-button type="primary" @click="volumectrl('up');">Volume +</van-button>
                    <van-button type="primary" @click="volumectrl('down');">Volume -</van-button>
                    <van-button type="primary" @click="volumectrl('half');">Volume 50%</van-button> -->
            </div>
            <div style="margin-top: 15px;" class="oparea">
                <van-space>
                    <div>
                        <van-field v-model="vlcresult" is-link readonly name="picker" label="Source" placeholder="Select source" @click="colShowPicker = true" />
                        <van-popup v-model:show="colShowPicker" position="bottom">
                            <van-picker :columns="vlccolumns" @confirm="onconfirmvlc" @cancel="colShowPicker = false" />
                        </van-popup>  
                    </div>
                    <van-button type="primary" @click="changevlcsrc();">Change</van-button>
                </van-space>
                <van-space style="margin-top: 15px;">
                    <van-field v-model="vlcinputsrc" clearable label="Source" placeholder="Input source" />
                    <van-button type="primary" @click="changevlcsrcmanual();">Change</van-button>
                </van-space>
            </div>

            <div style="margin-top: 15px;" class="oparea">
                <span style="font-size: var(--van-cell-font-size)">Countdown(min):</span>
                <van-space>
                    <van-radio-group v-model="scheduleCheck" direction="horizontal">
                        <van-radio name="1">30</van-radio>
                        <van-radio name="2">60</van-radio>
                        <van-radio name="3">90</van-radio>
                        <!-- <van-radio name="4">120</van-radio> -->
                        <van-radio name="5">6s</van-radio>
                    </van-radio-group>
                    <van-button type="primary" @click="setScheduleTask();">Confirm</van-button>
                </van-space>
            </div>


            <!-- clock source -->
            <div style="margin-top: 15px;" class="oparea">
                <van-space>
                    <div>
                        <van-field v-model="clocksrcresult" is-link readonly name="picker2" label="Clock Source" placeholder="Select source" @click="colShowPicker1 = true" />
                        <van-popup v-model:show="colShowPicker1" position="bottom">
                            <van-picker :columns="vlccolumns" @confirm="onconfirmClockSrc" @cancel="colShowPicker1 = false" />
                        </van-popup>
                    </div> 
                    <van-button type="primary" @click="changeclocksrc();">Change</van-button>
                </van-space>
                <van-cell-group inset>
                    <van-field autosize v-model="clocksrcinput" label=" " label-width="0" type="textarea" rows="1" />
                </van-cell-group>
            </div>
            <!-- clock source -->

            <div style="margin-top:10px" class="oparea">
                <span>Temp ops</span>
                <van-space size="1rem">
                    <van-button type="primary" @click="sendmsg('rasp', 'play');" block
                        style="width: 80px;">Start</van-button>
                    <van-button type="primary" @click="sendmsg('rasp', 'stop');" block
                        style="width: 80px;">Stop</van-button>
                    <van-button type="primary" @click="sendmsg('rasp', 'pause');" block
                        style="width: 80px;">Pause</van-button>
                </van-space>
            </div>
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
import { getMQTTInfo } from "@/api/mqtthelper";
import * as mqtt from "mqtt/dist/mqtt.min";
import { getvlcsrc,setclocksrc,getdefaultclocksrc } from "@/api/rasp"

export default {
    components: {
    },
    data() {
        return {
            contentTitle: '', //  
            currentdate: '', //  
            mqttclient: null,
            msgcontent: '',
            vlcresult: '',
            vlcinputsrc: '',
            clocksrcresult: '',
            clocksrcinput: '',
            vlcSrcResultValue: '',
            colShowPicker: false,
            colShowPicker1: false,
            volumeValue: 0,
            vlcrunning: false,
            vlccolumns: [
                { text: '中国之声', value: 'http://ngcdn001.cnr.cn/live/zgzs/index.m3u8' },
                // { text: '江苏音乐台', value: 'http://satellitepull.cnr.cn/live/wx32jsjdlxyy/playlist.m3u8' },
                // { text: '天籁之音 Hi-Fi Radio', value: 'http://play-radio-stream3.hndt.com/now/WUBA5hW2/playlist.m3u8' },
                // { text: '亚洲音乐', value: 'http://asiafm.hk:8000/asiafm' },
                // { text: '亚洲经典', value: 'http://goldfm.cn:8000/goldfm' },

                // { text: '江苏交通广播', value: 'http://satellitepull.cnr.cn/live/wx32jsjtgb/playlist.m3u8' },
                // { text: 'CNR经典音乐广播', value: 'http://liveop.cctv.cn/hls/jdyygb192/playlist.m3u8' },
                // { text: '南京音乐广播', value: 'http://live.njgb.com:10588/show.mp3' },
            ],
            scheduleCheck: '0',
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
                    getvlcsrc().then((result) => {
                        if (result != null && result.status == 200) {
                            let resultdata = result.data;
                            if (resultdata.Status == 1) {
                                let videolist = resultdata.Data;
                                if (videolist != null && videolist.length > 0) {
                                    let that = this;
                                    that.vlccolumns = [];
                                    videolist.forEach(element => {
                                        that.vlccolumns.push({
                                            text: element.Name,
                                            value: element.Value
                                        })
                                    });
                                }
                            } else {
                                showFailToast({
                                    "wordBreak": "break-word",
                                    "message": "Identity expired." + resultdata.Msg,
                                    "duration": 800
                                });
                            }
                        }
                        this.getDefaultClockUrl();
                    })
                  
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
            showFailToast('need login')
        }
    },
    methods: {
        setTitle() {
            this.contentTitle = "Raspberry Pi Clock";
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
                //status msg of Raspberry
                that.mqttclient.subscribe("RaspStatus", (err) => {
                    if (!err) {
                    }
                });
                that.sendmsg('rasp', 'getstatus');
            })
            that.mqttclient.on('message', function (topic, message) { 
                if (topic == "RaspStatus") {
                    try {
                        var resp = JSON.parse(message.toString());
                        if (resp.Status == 1) {
                            console.log(resp.Data.streamurl)
                            that.vlcinputsrc = resp.Data.streamurl
                            that.volumeValue = resp.Data.volume
                            if (resp.Data.state == 1) {
                                that.vlcrunning = true
                            } else {
                                that.vlcrunning = false
                            }
                            if(resp.Data.streamurl!=''){
                                that.vlccolumns.forEach(col => {
                                    if(col.value==resp.Data.streamurl){
                                        that.vlcresult=col.text;
                                        return;
                                    }
                                });
                            }  
                        }
                    } catch (err) {
                    }
                }
            })
        },
        sendmsg(topicflag, msg) {
            var topic = ""
            switch (topicflag) {
                case "rasp": topic = "RaspController"; break;
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
        changevlcsrc() {
            let that = this;
            if (that.vlcSrcResultValue) {
                that.sendmsg('rasp', `changesrc|${that.vlcSrcResultValue}`);
                setTimeout(() => {
                    that.sendmsg('rasp', 'getstatus');
                }, 3000); 
            }
        },
        changevlcsrcmanual() {
            let that = this;
            if (that.vlcinputsrc) {
                this.sendmsg('rasp', `changesrc|${that.vlcinputsrc}`);
                setTimeout(() => {
                    that.sendmsg('rasp', 'getstatus');
                }, 3000); 
            }
        },
        volumectrl(ctrl) { 
            if (ctrl) {
                this.sendmsg('rasp', `volume|${ctrl}`);
            }
        },
        onconfirmvlc(result) {
            var selectOption = result.selectedOptions[0];
            this.vlcresult = selectOption?.text;
            this.vlcSrcResultValue = selectOption?.value;
            this.colShowPicker = false;  
        },
        onconfirmClockSrc(result) {
            var selectOption = result.selectedOptions[0];
            this.clocksrcresult  = selectOption?.text;
            // this.vlcSrcResultValue = selectOption?.value;
            this.clocksrcinput= selectOption?.value;
            this.colShowPicker1 = false;  
        },
        changeclocksrc(){
            let that=this;
            setclocksrc(that.clocksrcinput).then((result) => {
                        if (result != null && result.status == 200) {
                            let resultdata = result.data;
                            if (resultdata.Status == 1) {
                                showSuccessToast({
                                    "wordBreak": "break-word",
                                    "message": "The clock source has been changed.",
                                    "duration": 800
                                });
                            } else {
                                showFailToast({
                                    "wordBreak": "break-word",
                                    "message": "Identity expired." + resultdata.Msg,
                                    "duration": 800
                                });
                            }
                        }
                    }) 
        },
        setScheduleTask() {
            // scheduleCheck: '0',
            let scheduleSelect = this.scheduleCheck
            let scheduleTime = 0 //mins
            switch (scheduleSelect) {
                case "1": scheduleTime = 30; break;
                case "2": scheduleTime = 60; break;
                case "3": scheduleTime = 90; break;
                case "4": scheduleTime = 120; break;
                case "5": scheduleTime = 0.1; break;
            }
            if (scheduleTime > 0) {
                this.sendmsg('rasp', `schedule|${scheduleTime}`);
            }
        },
        onVolumeChange(value) {
            console.log("change")
            if (value != null && value >= 0 && value <= 100) {
                this.sendmsg('rasp', `volume|${value}`);
            }
        },
        onRunningChanged(value){
            if(value){
                this.sendmsg('rasp', 'play')
            }else{
                this.sendmsg('rasp', 'stop')
            }

        },
        getDefaultClockUrl() {
            getdefaultclocksrc().then((result) => {
                if (result != null && result.status == 200) {
                    let resultdata = result.data;
                    if (resultdata.Status == 1) {
                        this.clocksrcinput=resultdata.Data
                        this.vlccolumns.forEach(col => {
                                    if(col.value==resultdata.Data){
                                        this.clocksrcresult=col.text;
                                        return;
                                    }
                                });
                    } else {
                        showFailToast({
                            "wordBreak": "break-word",
                            "message": "Identity expired." + resultdata.Msg,
                            "duration": 800
                        });
                    }
                }
            })
        }
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

.oparea {
    background-color: #FFF;
    padding: 5px 0 10px 10px;
    border-radius: 5px;
}
</style>