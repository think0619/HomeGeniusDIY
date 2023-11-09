<template>
 

    <el-table :data="tableData"  border  style="width: 100%">
        <el-table-column fixed prop="date" label="Date" width="150" />
        <el-table-column prop="name" label="Name" width="120" />
        <el-table-column prop="state" label="State" width="120" />
        <el-table-column prop="city" label="City" width="120" />
        <el-table-column prop="address" label="Address" width="600" />
        <el-table-column prop="zip" label="Zip" width="120" /> 
        <el-table-column fixed="right" label="Operations" width="120">
            <template #default="scope">
                <el-button type="primary" @click="handleEdit(scope.$index, scope.row)" >Edit</el-button>
            </template>
        </el-table-column>
    </el-table>
</template>
    

<script setup  lang="jsx">
import { ref } from 'vue';
import { useRouter, useRoute } from 'vue-router';
const router = useRouter();
const route = useRoute();
const active = ref(route.path);

import { Check, Delete, Edit, Message, Search, Star, } from '@element-plus/icons-vue'
</script>  

<script lang="jsx">
import * as echarts from 'echarts';
import { login } from "@/api/config";
import { getWeather } from "@/api/weather"

export default {
    components: {
    },
    data() {
        return {
            contentTitle: '', //   
            tableData: [
                {
                    date: '2016-05-03',
                    name: 'Tom',
                    address: 'No. 189, Grove St, Los Angeles',
                },
                {
                    date: '2016-05-02',
                    name: 'Tom',
                    address: 'No. 189, Grove St, Los Angeles',
                },
                {
                    date: '2016-05-04',
                    name: 'Tom',
                    address: 'No. 189, Grove St, Los Angeles',
                },
                {
                    date: '2016-05-01',
                    name: 'Tom',
                    address: 'No. 189, Grove St, Los Angeles',
                },
            ]
        };
    },
    computed: {
    },
    setup() {

    },
    mounted() {
        this.setTitle();
    },
    methods: {
        setTitle() {
            this.contentTitle = "Weather Info";
        },
        handleEdit(index, row){
  console.log(index, row.date)
}
    }
}; 
</script>

  
 