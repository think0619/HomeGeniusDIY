<template>
    <div class="title">
        <van-nav-bar :title="contentTitle" left-text="" left-arrow @click-left="onClickLeft"></van-nav-bar>
    </div>
    <div class="control">
        <van-cell-group inset title="Air Condition"> 
            <van-cell is-link title="Air Condition Hot" :icon="hotimg" icon-prefix="cellicon"
                @click="airhotShow = true"  size="large"/>
            <van-cell is-link title="Air Condition Cold" :icon="coldimg" icon-prefix="cellicon"
                @click="aircoldShow = true"  size="large"/>
            <van-cell is-link title="Air Condition Power" :icon="powerimg" icon-prefix="cellicon"
                @click="airpowerShow = true"  size="large"/>
            </van-cell-group>
                <van-cell-group inset title="Polk Speaker"> 
            <van-cell is-link title="Polk Speaker" :icon="speakerimg" icon-prefix="cellicon" @click="polkShow = true"  size="large"/>

            <van-action-sheet v-model:show="airhotShow" :actions="airhotActions" @select="onSelect" cancel-text="取消"
                close-on-click-action />
            <van-action-sheet v-model:show="aircoldShow" :actions="aircoldActions" @select="onSelect" cancel-text="取消"
                close-on-click-action />
            <van-action-sheet v-model:show="airpowerShow" :actions="airpowerActions" @select="onSelect" cancel-text="取消"
                close-on-click-action />
            <van-action-sheet v-model:show="polkShow" :actions="polkActions" @select="onSelect" cancel-text="取消"
                close-on-click-action />
        </van-cell-group>
    </div>
</template>



<script lang="jsx">
import { showSuccessToast, showFailToast, showToast, } from 'vant';
import { login } from "@/api/config";
import { getMQTTInfo } from "@/api/mqtthelper";
import * as mqtt from "mqtt/dist/mqtt.min"; 
import coldimg from "@/assets/img/cold.svg";
import hotimg from "@/assets/img/制热.svg";
import powerimg from "@/assets/img/power.svg";
import speakerimg from "@/assets/img/speaker.svg";
import muteimg from "@/assets/img/speaker/mute.svg";  
import fiberopticimg from "@/assets/img/speaker/网络光纤.svg"; 
import auximg from "@/assets/img/speaker/Aux-Cabel.svg"; 
import bluetoothimg from "@/assets/img/speaker/bluetooth.svg"; 
import subwooferimg from "@/assets/img/speaker/subwoofer.svg"; 

import vulumnmiusimg from "@/assets/img/speaker/volumnQuiet_1.svg"; 
import vulumnupimg from "@/assets/img/speaker/volumnLouder_1.svg"; 
import movieimg from "@/assets/img/speaker/movie.svg"; 
import nightimg from "@/assets/img/speaker/night-mode-fill.svg"; 
import musicimg from "@/assets/img/speaker/music_fill.svg"; 

import { ref } from 'vue';
import { useRouter, useRoute } from 'vue-router'; 

export default {
    components: {
    },
    data() {
        return { 
        };
    },
    computed: {
    },
    setup() {
        const router = useRouter();
        const route = useRoute();
        const active = ref(route.path);
        const contentTitle=ref('')
        const mqttclient=ref(null) 

        const setTitle = () => {contentTitle.value = "Air condition"; };
       // const onClickLeft = () => { history.back(); };
        const onClickLeft = () => {  router.go(-1);; };
        const connectMQTT =(_url, _username, _password) => { 
           mqttclient.value = mqtt.connect(_url, {
                keepalive: 60,
                reconnectPeriod: 3000,
                username: _username,
                password: _password
            })
            mqttclient.value.on('connect', function () {
                showSuccessToast({
                    "wordBreak": "break-word",
                    "message": "The mqtt client has connected.",
                    "duration": 800
                });
                mqttclient.value.subscribe('Aircondition', function (err) {
                    if (!err) { } else {
                    }
                })
            })
            mqttclient.value.on('message', function (topic, message) { 
            })
        };
        const sendmsg=(topicflag, msg,txt)=>{
            var topic = ""
            switch (topicflag) {
                case "aircondition": topic = "Aircondition"; break;
                case "bluetoothaudio": topic = "BLAudio"; break;
            }
            if (topic != '') { 
                if (mqttclient.value != null) {
                   mqttclient.value.publish(topic, msg, {
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
                                "message": txt,
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
        };   
         
        const onSelect = (item,index) => {
            if(item.type==='airhot'){
                airhotShow.value=false;
            }
            else if(item.type==='aircold'){
                aircoldShow.value=false;
            }
            else if(item.type==='airpower'){
                airpowerShow.value=false;
            }
            else if(item.type==='polk'){
                polkShow.value=false;
            }  
            sendmsg(item.flag,item.msg,item.txt)
        };

        const airhotShow=ref(false);
        const aircoldShow=ref(false);
        const airpowerShow=ref(false);
        const polkShow=ref(false); 
        const airhotActions = [
            {type:'airhot', icon: hotimg , color: '#ee0a24', name: '25℃',flag:'aircondition',msg:'warm25',txt:'制热25℃'},
            {type:'airhot',  icon: hotimg , color: '#ee0a24',name: '26℃',flag:'aircondition',msg:'warm26' ,txt:'制热26℃'},
            {type:'airhot',  icon: hotimg , color: '#ee0a24',name: '27℃',flag:'aircondition',msg:'warm27' ,txt:'制热27℃'},
            {type:'airhot',  icon: hotimg , color: '#ee0a24',name: '28℃',flag:'aircondition',msg:'warmon',txt:'制热28℃'},
            { type:'airhot', icon: hotimg , color: '#ee0a24',name: '26 1h℃',flag:'aircondition',msg:'warm26T1h' ,txt:'制热26℃，定时1小时'},
            { type:'airhot', icon: hotimg , color: '#ee0a24',name: '26 2h℃',flag:'aircondition',msg:'warm26T2h' ,txt:'制热26℃，定时2小时'},
        ]; 
        const aircoldActions = [
            {type:'aircold', icon: coldimg , color: '#1E90FF', name: '24℃',flag:'aircondition',msg:'cold24',txt:'制冷24℃'},
            {type:'aircold',  icon: coldimg , color: '#1E90FF',name: '25℃',flag:'aircondition',msg:'cold25' ,txt:'制冷25℃'},
            {type:'aircold',  icon: coldimg , color: '#1E90FF',name: '25℃ 1h',flag:'aircondition',msg:'cold25T1h' ,txt:'制冷25℃，定时1小时'},
            {type:'aircold',  icon: coldimg , color: '#1E90FF',name: '25℃ 2h',flag:'aircondition',msg:'cold25T2h' ,txt:'制冷25℃，定时1小时'},
        ]; 
        const airpowerActions = [
            {type:'airpower', icon: powerimg , color: '#f0280e', name: 'Power Off',flag:'aircondition',msg:'off',txt:'关闭空调'},
        ]; 
        const polkActions = [
            {type:'polk', icon:powerimg, name: 'Power',flag:'bluetoothaudio',msg:'power',txt:'音箱开关'},
            {type:'polk',icon:muteimg, name: 'Mute',flag:'bluetoothaudio',msg:'mute',txt:'静音'},
            {type:'polk', icon:fiberopticimg, name: 'FiberOptic',flag:'bluetoothaudio',txt:'光纤输入'},
            {type:'polk', icon:auximg, name: 'AUX',flag:'bluetoothaudio',msg:'aux',txt:'蓝牙输入'},
            {type:'polk', icon:bluetoothimg, name: 'Bluetooth',flag:'bluetoothaudio',msg:'bluetooth',txt:'AUX输入'},
            {type:'polk', icon:vulumnupimg, name: 'VOL +',flag:'bluetoothaudio',msg:'volplus',txt:'音量 +'},
            {type:'polk', icon:vulumnmiusimg, name: 'VOL -',flag:'bluetoothaudio',msg:'volminus',txt:'音量 -'}, 
            {type:'polk', icon:subwooferimg, name: 'Bass +',flag:'bluetoothaudio',msg:'bassplus',txt:'低音 +'},
            {type:'polk', icon:subwooferimg, name: 'Bass -',flag:'bluetoothaudio',msg:'bassminus',txt:'低音 -'}, 
            {type:'polk', icon:movieimg, name: 'Movie',flag:'bluetoothaudio',msg:'modemovie',txt:'电影模式'},
            {type:'polk', icon:nightimg, name: 'Night',flag:'bluetoothaudio',msg:'modenight',txt:'夜间模式'},
            {type:'polk', icon:musicimg, name: 'Music',flag:'bluetoothaudio',msg:'modemusic',txt:'音乐模式'},
        ];  

        return {
            hotimg,coldimg,powerimg,speakerimg,
            muteimg,fiberopticimg,auximg,bluetoothimg,subwooferimg,vulumnmiusimg,vulumnupimg,movieimg,nightimg,musicimg,
            contentTitle,mqttclient,
            setTitle,onClickLeft,connectMQTT,sendmsg,
            airhotShow,airhotActions,aircoldShow,aircoldActions,airpowerShow,airpowerActions,
            polkShow,polkActions, 
            onSelect, 
        };
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
        
    }
}; 
</script>

<style scoped>
.control {
    margin: 0 0px;
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
.custombtn {
        --van-button-normal-padding	:0 8px;
    }

</style>

<style >
.cellicon {
    display: flex;
    justify-content: center; /* Horizontal centering */
    align-items: center; /* Vertical centering */   
    font-size: 16px;
    text-rendering: auto;
    -webkit-font-smoothing: antialiased;
}
</style>
