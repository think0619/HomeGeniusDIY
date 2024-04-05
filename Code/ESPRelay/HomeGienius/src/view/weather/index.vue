<template>
    <div class="title">
        <van-nav-bar :title="contentTitle" left-text="" left-arrow  @click-left="onClickLeft"  ></van-nav-bar>
    </div>
    <div class="control" title="Location">
       
        <van-cell-group>
            <van-cell title="南京·浦口" :value="weatherDatetime" :label="weatherlonglat" ><i :class="weatherIcon" style="font-size: 23px;"></i></van-cell>
       
        </van-cell-group>
        <van-cell-group title="Realtime">
            <van-cell v-for="(item, index) in realtimeInfoItems" :key="index" :title="item.title"
                :value="item.value"></van-cell>
        </van-cell-group>
        <!-- <van-cell-group title="Daily">
            <van-cell v-for="(item, index) in dailyInfoItems" :key="index" :title="item.title"
                :value="item.value"></van-cell>
        </van-cell-group> -->
        <van-cell-group title="Hourly">
            <van-cell title="温度、降水变化趋势" :value="hourlyInfo" />
            <div id="tempcharts" style="width: 100%;height: 300px;"></div>
            <!-- <van-cell title="降水概率趋势" />
            <div id="precipitationcharts" style="width: 100%;height: 300px;"></div> -->
        </van-cell-group>
        <van-cell-group title="Hourly Sky Condition">
            <van-cell v-for="(item, index) in hoursForecast" :key="index" :title="item.time" :label="item.value" label-class="labelcustom"
                :value="item.alias"></van-cell>
        </van-cell-group>
        <van-cell-group title="Daily Sky Condition">
            <van-cell v-for="(item, index) in dailyForecast" :key="index" :title="item.date" :label="item.value" label-class="labelcustom"
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
            weatherlonglat: '118.700859,32.112609', //   
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
                { property: 'obsTimeFmt', title: '数据观测时间', value: '' },
                { property: 'temp', title: '温度℃', value: '' },
                { property: 'feelsLike', title: '体感温度℃', value: '' },
                { property: 'text', title: '天气状况', value: '' },
                { property: 'wind360', title: '风向360角度', value: '' },
                { property: 'windDir', title: '风向', value: '' },
                { property: 'windScale', title: '风力等级', value: '' },
                { property: 'windSpeed', title: '风速km/h', value: '' },
                { property: 'humidity', title: '相对湿度%', value: '' },
                { property: 'precip', title: '当前小时累计降水量mm', value: '' },
                { property: 'pressure', title: '大气压强(百帕)', value: '' },
                { property: 'vis', title: '能见度(km)', value: '' },
                { property: 'cloud', title: '云量(%)', value: '' },
                { property: 'dew', title: '露点温度', value: '' }, 
            ],
            weatherIcon:'qi-307',
            hoursForecast: [],
            dailyForecast: []
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
            let that = this;
            getWeatherHefeng(token).then((weatherResult) => {
                if (weatherResult != null && weatherResult.Status == 1) {
                    let weatherdata = weatherResult.Data; 

                    let realtimeInfos = weatherdata.now;
                    that.weatherIcon=`qi-${realtimeInfos.icon}`
                    for (let key in realtimeInfos) {
                        if (realtimeInfos.hasOwnProperty(key)) {
                            const dailyValue = realtimeInfos[key];
                            that.realtimeInfoItems.forEach((ele) => {
                                if (ele.property === key) {
                                    ele.value = dailyValue;
                                    return;
                                }
                            });
                        }
                    }

                    let hourlyInfos = weatherdata.hourly;
                    let timeArray = [];
                    let tempArray = []; //温度
                    let popArray = []; //降水 
                    that.hoursForecast = []

                    hourlyInfos.forEach((item) => {
                        timeArray.push(item.fxTimeTimeFmt);
                        tempArray.push(item.temp);
                        popArray.push(item.pop);

                        that.hoursForecast.push({
                            "time":  `${item.fxTimeTimeFmt}:00:00`,
                            "value": `温度:${item.temp}℃，降水概率:${item.pop}%,  风力:${item.windDir} ${item.windScale}级别.`,
                            "alias": item.text
                        });
                    });
                    that.chartTemp(timeArray, tempArray, popArray);


                    let dailyInfos = weatherdata.daily; 
                    that.dailyForecast = []
                    dailyInfos.forEach((item) => {  
                        that.dailyForecast.push({
                            "date":  `${item.fxTimeTimeFmt} ${this.getDayofWeek(item.fxTimeTimeFmt)}`,
                            "value": `日间:${item.sunrise}-${item.sunset}, 月相:${item.moonPhase}, 紫外线:${item.uvIndex}, 能见度:${item.vis}.`,
                            "alias": `${item.tempMin}-${item.tempMax}℃，${item.textDay}-${item.textNight}`
                        });
                    });
                    
 
                  
                }});



            // getWeather(token).then((weatherResult) => {
            //     if (weatherResult != null && weatherResult.Status == 1) {
            //         let weatherdata = weatherResult.Data;

            //         let that = this;

            //         that.weatherDatetime = 'Server：' + weatherdata._ServerTime;
            //         that.weatherlonglat = weatherdata.LatLong;

            //         let dailyInfos = weatherdata.dailyInfo;
            //         for (let key in dailyInfos) {
            //             if (dailyInfos.hasOwnProperty(key)) {
            //                 const dailyValue = dailyInfos[key];
            //                 that.dailyInfoItems.forEach((ele) => {
            //                     if (ele.property === key) {
            //                         ele.value = dailyValue;
            //                         return;
            //                     }
            //                 });
            //             }
            //         }

            //         let realtimeInfos = weatherdata.realtimeInfo;
            //         for (let key in realtimeInfos) {
            //             if (realtimeInfos.hasOwnProperty(key)) {
            //                 const realtimeValue = realtimeInfos[key];
            //                 that.realtimeInfoItems.forEach((ele) => {
            //                     if (ele.property === key) {
            //                         ele.value = realtimeValue;
            //                         return;
            //                     }
            //                 });
            //             }
            //         }

            //         let hourlyInfos = weatherdata.hourlyInfo;
            //         that.hourlyInfo = hourlyInfos.Description;
            //         let timeArray = [];
            //         let tempArray = [];
            //         let apparentTemp = [];
            //         hourlyInfos.Temperature.forEach((temp) => {
            //             timeArray.push(temp.Time);
            //             tempArray.push(temp.Value);
            //         });
            //         hourlyInfos.ApparentTemperature.forEach((temp) => {
            //             apparentTemp.push(temp.Value);
            //         });
            //         that.chartTemp(timeArray, tempArray, apparentTemp);

            //         let precipTimeArray = [];
            //         let precipValueArray = [];
            //         hourlyInfos.Precipitations.forEach((prec) => {
            //             precipTimeArray.push(prec.Time);
            //             precipValueArray.push(prec.Probability);
            //         });
            //         that.echartPrecipitation(precipTimeArray, precipValueArray);

            //         that.hoursForecast = []
            //         hourlyInfos.SkyconInfo.forEach((skycon) => {
            //             that.hoursForecast.push({
            //                 "time": skycon.Time,
            //                 "value": skycon.Value,
            //                 "alias": skycon._Value
            //             });
            //         });
            //     } else {
            //         showFailToast({
            //             "wordBreak": "break-word",
            //             "message": "Identity expired.",
            //             "duration": 800
            //         });
            //     }
            // });

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
                yAxis: [{
                    type: 'value' 
                },{
                    type: 'value' 
                },],
                legend: {
                    data: ['温度', '降水概率'],
                    right: 30
                },
                series: [
                    {
                        name: '温度',
                        type: 'line',
                        smooth: true,yAxisIndex: '0',
                        data: y1Data
                    }, {
                        name: '降水概率',
                        type: 'line',
                        smooth: true,yAxisIndex: '1',
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

        getDayofWeek(datestr){
            const weeks=['Sun','Mon','Tue','Wed','Thu','Fri','Sat']
            var dayofweek=(new Date(datestr)).getDay();
            return weeks[dayofweek] 
        }
    }
}; 
</script>

<style>
:root {
    --van-cell-group-title-font-size: 20px;
    --van-cell-group-title-color: rgb(0, 102, 255);
    --van-cell-value-color: #000000;
}

.labelcustom{
    
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
 