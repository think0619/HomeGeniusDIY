<template>
    <div class="title">
        <van-nav-bar :title="contentTitle" left-text="" left-arrow  @click-left="onClickLeft"  ></van-nav-bar>
    </div>
    <div class="control" title="Location">
        <van-cell-group>
            <van-cell title="南京·浦口" :value="weatherDatetime" :label="weatherlonglat" />
        </van-cell-group>
        <van-cell-group title="Realtime">
            <van-cell v-for="(item, index) in realtimeInfoItems" :key="index" :title="item.title"
                :value="item.value"></van-cell>
        </van-cell-group>
        <van-cell-group title="Daily">
            <van-cell v-for="(item, index) in dailyInfoItems" :key="index" :title="item.title"
                :value="item.value"></van-cell>
        </van-cell-group>
        <van-cell-group title="Hourly">
            <van-cell title="温度变化趋势" :value="hourlyInfo" />
            <div id="tempcharts" style="width: 100%;height: 300px;"></div>
            <van-cell title="降水概率趋势" />
            <div id="precipitationcharts" style="width: 100%;height: 300px;"></div>
        </van-cell-group>
        <van-cell-group title="Hourly Sky Condition">
            <van-cell v-for="(item, index) in hoursForecast" :key="index" :title="item.time" :label="item.value"
                :value="item.alias"></van-cell>
        </van-cell-group>
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
import * as echarts from 'echarts';

import { login } from "@/api/config";
import { getWeather,getWeatherHefeng } from "@/api/weather"

export default {
    components: {
    },
    data() {
        return {
            contentTitle: '', //  
            currentdate: '', //   
            weatherDatetime: '', //   
            weatherlonglat: '', //   
            hourlyInfo: '', //   
            dailyInfoItems: [
                { property: 'SunriseSunsetDesc', title: '日出日落', value: '' },
                { property: 'PrecipitationDesc', title: '降水概率%', value: '' },
                { property: 'TemperatureDesc', title: '气温℃', value: '' },
                { property: 'UltravioletDesc', title: '紫外线', value: '' },
                { property: 'CarWashingDesc', title: '洗车', value: '' },
                { property: 'DressingDesc', title: '穿衣指数', value: '' },
                { property: 'ComfortDesc', title: '舒适度指数', value: '' },
                { property: 'ColdRiskDesc', title: '感冒指数', value: '' }, 
            ],
            realtimeInfoItems: [  
                ///======region begin ===caiyun
                { property: 'Temperature', title: '地表2米气温', value: '' },
                { property: 'ApparentTemperature', title: '体感气温', value: '' },
                { property: 'Humidity', title: '相对湿度(%)', value: '' },
                { property: 'CloudRate', title: '总云量(0.0-1.0)', value: '' },
                { property: '_Skycon', title: '天气现象', value: '' },
                { property: 'WindSpeed', title: '地表10米风速', value: '' },
                { property: 'Pressure', title: '地面气压', value: '' },
                { property: 'PrecipitationIntensityLocal', title: '	本地降水强度', value: '' },
                { property: 'PrecipitationDistanceNearest', title: '最近降水带与本地的距离', value: '' },
                { property: 'AirPM25', title: 'PM25浓度(μg/m³)', value: '' },
                { property: 'AirPM10', title: 'PM10浓度(μg/m³)', value: '' },
                { property: 'AirO3', title: '臭氧浓度(μg/m³)', value: '' },
                { property: 'AirSO2', title: '二氧化硫浓度(μg/m³)', value: '' },
                { property: 'AirNO2', title: '二氧化氮浓度(μg/m³)', value: '' },
                { property: 'AirCO', title: '一氧化碳浓度(μg/m³)', value: '' },
                { property: 'AirAQI_CHN', title: '空气质量（CN)', value: '' },
                { property: 'AirAQI_USA', title: '空气质量（US)', value: '' },
                { property: 'Life_Ultraviolet', title: '紫外线', value: '' },
                { property: 'Life_Comfort', title: '生活指数', value: '' },
                ///======region begin ===caiyun
            ],
            hoursForecast: []
        };
    },
    computed: {
    },
    setup() {

    },
    mounted() {
        this.setTitle();
        this.chartTemp();
        let token = this.$store.state.token; 
        if (token ) { 
            getWeather(token).then((weatherResult) => {
                if (weatherResult != null && weatherResult.Status == 1) {
                    let weatherdata = weatherResult.Data;

                    let that = this;

                    that.weatherDatetime = 'Server：' + weatherdata._ServerTime;
                    that.weatherlonglat = weatherdata.LatLong;

                    let dailyInfos = weatherdata.dailyInfo;
                    for (let key in dailyInfos) {
                        if (dailyInfos.hasOwnProperty(key)) {
                            const dailyValue = dailyInfos[key];
                            that.dailyInfoItems.forEach((ele) => {
                                if (ele.property === key) {
                                    ele.value = dailyValue;
                                    return;
                                }
                            });
                        }
                    }

                    let realtimeInfos = weatherdata.realtimeInfo;
                    for (let key in realtimeInfos) {
                        if (realtimeInfos.hasOwnProperty(key)) {
                            const realtimeValue = realtimeInfos[key];
                            that.realtimeInfoItems.forEach((ele) => {
                                if (ele.property === key) {
                                    ele.value = realtimeValue;
                                    return;
                                }
                            });
                        }
                    }

                    let hourlyInfos = weatherdata.hourlyInfo;
                    that.hourlyInfo = hourlyInfos.Description;
                    let timeArray = [];
                    let tempArray = [];
                    let apparentTemp = [];
                    hourlyInfos.Temperature.forEach((temp) => {
                        timeArray.push(temp.Time);
                        tempArray.push(temp.Value);
                    });
                    hourlyInfos.ApparentTemperature.forEach((temp) => {
                        apparentTemp.push(temp.Value);
                    });
                    that.chartTemp(timeArray, tempArray, apparentTemp);

                    let precipTimeArray = [];
                    let precipValueArray = [];
                    hourlyInfos.Precipitations.forEach((prec) => {
                        precipTimeArray.push(prec.Time);
                        precipValueArray.push(prec.Probability);
                    });
                    that.echartPrecipitation(precipTimeArray, precipValueArray);

                    that.hoursForecast = []
                    hourlyInfos.SkyconInfo.forEach((skycon) => {
                        that.hoursForecast.push({
                            "time": skycon.Time,
                            "value": skycon.Value,
                            "alias": skycon._Value
                        });
                    });
                } else {
                    showFailToast({
                        "wordBreak": "break-word",
                        "message": "Identity expired.",
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
            this.contentTitle = "Weather Info";
        },
        onClickLeft(){
            this.$router.back(-1)
           // history.back();
        },
        chartTemp(xData, y1Data, y2Data) {
            var chartDom = document.getElementById('tempcharts');
            var myChart = echarts.init(chartDom);
            var option;

            option = {
                xAxis: {
                    type: 'category',
                    data: xData,
                },
                yAxis: {
                    type: 'value'
                },
                legend: {
                    data: ['气温', '体感气温'],
                    right: 30
                },
                series: [
                    {
                        name: '气温',
                        type: 'line',
                        smooth: true,
                        data: y1Data
                    }, {
                        name: '体感气温',
                        type: 'line',
                        smooth: true,
                        data: y2Data
                    },
                ]
            };

            option && myChart.setOption(option);
        },
        echartPrecipitation(xData, y1Data) {
            // var chartDom = ;
            var myChart = echarts.init(document.getElementById('precipitationcharts'));
            var option;
            option = {
                xAxis: {
                    type: 'category',
                    data: xData
                },
                legend: {
                    data: ['降水概率'],
                    right: 30
                },
                yAxis: {
                    type: 'value'
                },
                series: [
                    {
                        name: '降水概率',
                        data: y1Data,
                        smooth: true,
                        type: 'line'
                    }
                ]
            };
            option && myChart.setOption(option);
        },
    }
}; 
</script>

<style>
:root {
    --van-cell-group-title-font-size: 20px;
    --van-cell-group-title-color: rgb(0, 102, 255);
    --van-cell-value-color: #000000;
}
</style>

<style scoped>
.control {
    margin: 0 10px;

}

.sysname {
    margin: 5px 0;
}

.title>span {
    display: block;
    font-size: 24px;
}
</style>
 