<template>
    <div class="title">
        <van-nav-bar :title="contentTitle" left-text="" left-arrow @click-left="onClickLeft"></van-nav-bar>
    </div>
    <div class="control">
        <van-cell title="单元格" is-link value="空调==关" @click="openActionsheet('airPowerShow');" />
        <van-cell title="单元格" is-link value="空调==制冷" @click="openActionsheet('airColdShow');" />
        <van-cell title="单元格" is-link value="空调==制热" @click="openActionsheet('airHotShow');" />
        <van-cell title="单元格" is-link value="蓝牙音箱" @click="openActionsheet('polkSpeakerShow');" />

        <van-action-sheet v-model:show="airPowerShow" :actions="actions"  />
        <van-action-sheet v-model:show="airColdShow" :actions="actions" />
        <van-action-sheet v-model:show="airHotShow" :actions="actions"/>
        <van-action-sheet v-model:show="polkSpeakerShow" :actions="actions"  />

        <div style="margin-top: 20px;" class="content custombtn">
            <div class="sysname"><span>Air Condition</span></div>
            <van-space size="1rem" wrap>
                <van-button type="danger" @click="sendmsg('aircondition', 'off');">Power Off</van-button>
            </van-space>
            <van-divider :style="{ color: '#1989fa', borderColor: '#1989fa', padding: '0', margin: '2px 0' }"> Cold
            </van-divider>
            <van-space size="1rem" wrap fill="true">
                <van-button type="primary" :icon="coldimg" @click="sendmsg('aircondition', 'cold24');">24℃</van-button>
                <van-button type="primary" :icon="coldimg" @click="sendmsg('aircondition', 'cold25');">25℃</van-button>
                <van-button type="primary" :icon="coldimg" @click="sendmsg('aircondition', 'cold25T1h');">25℃ 1h</van-button>
                <van-button type="primary" :icon="coldimg" @click="sendmsg('aircondition', 'cold25T2h');">25℃ 2h</van-button>
            </van-space>
            <van-divider :style="{ color: '	#F4A460', borderColor: '	#F4A460', padding: '0', margin: '2px 0' }"> Warm
            </van-divider>
            <van-space size="1rem" wrap>
                <van-button type="warning" :icon="hotimg" color="linear-gradient(to right, #1989fa, #ee0a24)"
                    @click="sendmsg('aircondition', 'warm25');">25℃</van-button>
                <van-button type="warning" :icon="hotimg" color="linear-gradient(to right, #1989fa, #ee0a24)"
                    @click="sendmsg('aircondition', 'warm26');">26℃</van-button>
                <van-button type="warning" :icon="hotimg" color="linear-gradient(to right, #1989fa, #ee0a24)"
                    @click="sendmsg('aircondition', 'warm27');">27℃</van-button>
                <van-button type="warning" :icon="hotimg" color="linear-gradient(to right, #1989fa, #ee0a24)"
                    @click="sendmsg('aircondition', 'warmon');">28℃</van-button>
                <van-button type="warning" :icon="hotimg" color="linear-gradient(to right, #1989fa, #ee0a24)"
                    @click="sendmsg('aircondition', 'warm26T1h');">26℃ 1h</van-button>
                <van-button type="warning" :icon="hotimg" color="linear-gradient(to right, #1989fa, #ee0a24)"
                    @click="sendmsg('aircondition', 'warm26T2h');">26℃ 2h</van-button>
            </van-space>
        </div>
        <div style="margin-top: 20px;" class="content2">
            <div class="sysname"><span>Polk Bluetooth speaker</span></div>
            <van-row gutter="5">
                <van-col span="6">
                    <van-button class="bluebtn" type="primary"
                        @click="sendmsg('bluetoothaudio', 'power');">Power</van-button>
                </van-col>
                <van-col span="6">
                    <van-button class="bluebtn" type="primary"
                        @click="sendmsg('bluetoothaudio', 'mute');">Mute</van-button>
                </van-col>
            </van-row>

            <van-row class="rowmagrin" gutter="5">
                <van-col span="6">
                    <van-button class="bluebtn" type="primary"
                        @click="sendmsg('bluetoothaudio', 'fiberoptic');">FiberOptic</van-button>
                </van-col>
                <van-col span="6">
                    <van-button class="bluebtn" type="primary"
                        @click="sendmsg('bluetoothaudio', 'aux');">AUX</van-button>
                </van-col>
                <van-col span="6">
                    <van-button class="bluebtn" type="primary"
                        @click="sendmsg('bluetoothaudio', 'bluetooth');">Bluetooth</van-button>
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
                    <van-button class="bluebtn" type="primary"
                        @click="sendmsg('bluetoothaudio', 'modemovie');">Movie</van-button>
                </van-col>
                <van-col span="6">
                    <van-button class="bluebtn" type="primary"
                        @click="sendmsg('bluetoothaudio', 'modenight');">Night</van-button>
                </van-col>
                <van-col span="6">
                    <van-button class="bluebtn" type="primary"
                        @click="sendmsg('bluetoothaudio', 'modemusic');">Music</van-button>
                </van-col>
            </van-row>

        </div>
    </div>
</template>

<script setup lang="jsx">
    import { ref } from 'vue';
    import { useRouter, useRoute } from 'vue-router';
    import { showSuccessToast, showFailToast, showToast } from 'vant';
    import { login } from "@/api/config";
    import { getMQTTInfo } from "@/api/mqtthelper";
    import * as mqtt from "mqtt/dist/mqtt.min";
    import coldimg1 from "@/assets/img/制冷1.svg";
    import hotimg1 from "@/assets/img/制热.svg";

    const router = useRouter();
    const route = useRoute();

    const contentTitle = ref('');
    const currentdate = ref('');
    let mqttclient = null; // Not reactive
    const hotimg = ref(hotimg1); // Assuming hotimg1 is a reactive object
    const coldimg = ref(coldimg1); // Assuming coldimg1 is a reactive object
    const airPowerShow = ref(false);
    const airColdShow = ref(false);
    const airHotShow = ref(false);
    const polkSpeakerShow = ref(false);
    const actions = ref([
        { name: '选项一' },
        { name: '选项二' },
        { name: '选项三' },
    ]);

    const actionsheet=ref({
        
    )

    // Mounted hook
    const setTitle = () => {
        contentTitle.value = "Air condition";
    };

    const connectMQTT = (_url, _username, _password) => {
        let that = this;
        mqttclient = mqtt.connect(_url, {
            keepalive: 60,
            reconnectPeriod: 3000,
            username: _username,
            password: _password
        });

        mqttclient.on('connect', function () {
            showSuccessToast({
                "wordBreak": "break-word",
                "message": "The mqtt client has connected.",
                "duration": 800
            });
            mqttclient.subscribe('Aircondition', function (err) {
                if (!err) { } else {
                }
            });
        });

        mqttclient.on('message', function (topic, message) {
            // console.log(topic.toString());
            // console.log(message.toString());
        });
    };

    const sendmsg = (topicflag, msg) => {
        // Your sendmsg logic here
    };

  

    // const openActionsheet = (variableName) => {
    //     const refVariables = [airPowerShow, airColdShow, airHotShow, polkSpeakerShow];
    //     refVariables.forEach((variable) => {
    //         variable.value = false;
    //     });
    //     switch (variableName) {
    //         case 'airPowerShow':
    //             airPowerShow.value = true;
    //             break;
    //         case 'airColdShow':
    //             airColdShow.value = true;
    //             break;
    //         case 'airHotShow':
    //             airHotShow.value = true;
    //             break;
    //         case 'polkSpeakerShow':
    //             polkSpeakerShow.value = true;
    //             break;
    //     }

    // };

    setTitle(); // Call the setTitle function

    // Return the variables and functions that need to be accessed in the template
    // return {
    //     contentTitle,
    //     currentdate,
    //     mqttclient,
    //     hotimg,
    //     coldimg,
    //     airPowerShow,
    //     airColdShow,
    //     airHotShow,
    //     polkSpeakerShow,
    //     actions,
    //     sendmsg,
    //     openActionsheet
    // };
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

    .bluebtn {
        display: block;
        width: 80px !important;
    }

    .rowmagrin {
        margin: 18px 0 0 0;
    }

    .content button {
        width: 100px;
    }

    .content2 button {
        width: 80px;
    }
</style>

<style lang="less" scoped>
    .custombtn {
        --van-button-normal-padding: 0 8px;
    }
</style>