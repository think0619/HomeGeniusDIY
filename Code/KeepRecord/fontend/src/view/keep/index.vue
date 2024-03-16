<template>
    <div class="title">
        <van-nav-bar :title="contentTitle"></van-nav-bar>
        <span>{{ currentdate }}</span>
    </div>
    <van-form @submit="onAddSubmit">
        <van-cell-group inset>
            <van-field v-model="calendarPickDate" is-link readonly name="datePicker" label="日期" placeholder="点击选择日期"
                @click="showCalendarForm = true" />
            <van-calendar v-model:show="showCalendarForm" @confirm="onConfirmDate" />

            <van-field v-model="pickTypeResult" is-link readonly name="typePicker" label="运动种类" placeholder="点击选择种类"
                @click="showTypeShowPicker" />
            <van-field v-model="numberRec" name="countInput" label="数值" placeholder="数值" type="number"
                :rules="[{ required: true, message: '请填写数值' }]" />
            <van-field v-model="pickUnitsResult" is-link readonly name="unitsPicker" label="单位" placeholder="点击选择单位"
                @click="showUnitsShowPicker('main')" />

            <van-field v-model="numberRecSub" name="countInputSub" label="数值" placeholder="数值" type="number"
                :rules="[{ required: true, message: '请填写数值' }]" />
            <van-field v-model="pickUnitsResultSub" is-link readonly name="unitsPickerSub" label="单位" placeholder="点击选择单位"
                @click="showUnitsShowPicker('sub')" />

            <van-popup v-model:show="showTypePicker" position="bottom">
                <van-picker :columns="typeColumns" @confirm="onConfirmTypePicker" @cancel="hideTypeShowPicker" />
            </van-popup>
            <van-popup v-model:show="showUnitsPicker" position="bottom">
                <van-picker :columns="unitsColumns" @confirm="onConfirmUnitsPicker" @cancel="hideUnitsShowPicker" />
            </van-popup>
        </van-cell-group>
        <div style="margin: 16px;">
            <van-button round block type="primary" native-type="submit">提交</van-button>
        </div> 
           <van-floating-bubble icon="list-switch"  axis="xy" @click="onViewPathClick" />
    </van-form>
</template>    


<script setup  lang="jsx">
import { ref } from 'vue';
import { useRouter, useRoute } from 'vue-router';
const router = useRouter();
const route = useRoute();
const active = ref(route.path);  
</script>

<script lang="jsx">
import { showSuccessToast, showFailToast } from 'vant';
import { addkeeprecord } from "@/api/keep";
import { isFunction } from 'vant/lib/utils';
export default {
    components: {
    },
    data() {
        //  const date = ref('');
        return {
            contentTitle: '', // 

            currentdate: '', // 

            numberRec: '',
            numberRecSub: '',

            showCalendarForm: false,
            calendarPickDate: '',

            showTypePicker: false,
            pickTypeResult: '',
            typeColumns: [
                { text: '跑步', value: 1 },
                { text: '跳绳', value: 2 },
                { text: '拳击', value: 3 },
                { text: '俯卧撑', value: 4 },
                { text: '骑行', value: 5 },],

            showUnitsPicker: false,
            focusUnitsOption: '',
            pickUnitsResult: '',
            pickUnitsResultSub: '',
            unitsColumns: [
                { text: '分钟', value: 1 },
                { text: '次', value: 2 },
                { text: '个', value: 3 },
                { text: '公里', value: 4 },],
        };
    },
    computed: {
    },
    setup() {

    },
    mounted() {
        this.setTitle();
        this.calendarPickDate = this.getDate();
        var thisquery = this.$route.query;
        if (thisquery.recid) {
            console.log("edit");
        } else {
            console.log("add")
        }

    },
    methods: {
        showTypeShowPicker() {
            this.showTypePicker = true;
        },
        hideTypeShowPicker() {
            this.showTypePicker = false;
        },
        onConfirmTypePicker(selectedOption) {
            var _this = this;
            var selectObj = selectedOption.selectedOptions[0];
            var selectText = selectObj?.text;
            _this.pickTypeResult = selectText;
            _this.showTypePicker = false;
        },

        showUnitsShowPicker(option) {
            this.showUnitsPicker = true;
            this.focusUnitsOption = option;
        },
        hideUnitsShowPicker() {
            this.showUnitsPicker = false;
        },
        onConfirmUnitsPicker(selectedOption) {
            var _this = this;
            var selectObj = selectedOption.selectedOptions[0];
            var selectText = selectObj?.text;

            if (_this.focusUnitsOption === 'main') {
                _this.pickUnitsResult = selectText;
            } else if (_this.focusUnitsOption === 'sub') {
                _this.pickUnitsResultSub = selectText;
            }
            _this.focusUnitsOption = '';

            this.showUnitsPicker = false;
        },
        onConfirmDate(date) {
            this.calendarPickDate = `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`;
            this.showCalendarForm = false;
        },
        getDate() {
            var _this = this;
            let yy = new Date().getFullYear();
            let mm = new Date().getMonth() + 1;
            let dd = new Date().getDate();
            let todayDate = yy + "-" + mm + "-" + dd;
            return todayDate;
        },
        setTitle() {
            //this.contentTitle = this.getDate() + " 运动数据";
            this.contentTitle = "新增运动数据";
        },
        onAddSubmit(inputValue) {
            var _this = this;
            var newObj = {
                "RecordDate": inputValue.datePicker,
                "TypeId": -1,
                "Count": Number(inputValue.countInput),
                "UnitsId": -1,
                "SubCount": Number(inputValue.countInputSub),
                "SubUnitsId": -1,
            };

            //type id
            _this.typeColumns.forEach((t) => {
                if (inputValue.typePicker == t.text) {
                    newObj.TypeId = t.value;
                    return false;
                }
                return true;
            });

            //units id
            _this.unitsColumns.forEach((u) => {
                if (inputValue.unitsPicker == u.text) {
                    newObj.UnitsId = u.value;
                }
                if (inputValue.unitsPickerSub == u.text) {
                    newObj.SubUnitsId = u.value;
                }
            });

            addkeeprecord(newObj).then(response => {
                let res = response.data;
                if (res.Status == 1) {
                    showSuccessToast({
                        message: res.Msg,
                        wordBreak: 'break-word'
                    });
                    _this.onRefresh();
                } else {
                    showFailToast({
                        message: res.Msg,
                        wordBreak: 'break-word'
                    });
                }
            }).catch(e => { });
        },
        onViewPathClick() {
            this.$router.push('/keep/view')
        }

    }
}; 
</script>

<style scoped>
 .title>span {
     display: block;
     font-size: 24px;
     ;
 }</style>