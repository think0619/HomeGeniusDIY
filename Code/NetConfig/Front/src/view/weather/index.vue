<template>
    <div class="common-layout">
        <el-container>
            <el-header>   </el-header>
            <el-container>
                <el-aside width="200px" style="">Aside</el-aside>
                <el-main>
                    <div class="container">
                        <div class="menubar">
                            <el-button type="primary" :icon="Delete">Delete</el-button>
                            <el-button type="primary" :icon="Document">Excel</el-button>
                        </div>
                        <el-table :data="configList" stripe border height="calc(100% - 30px)">
                            <el-table-column type="selection" width="55" />
                            <el-table-column fixed type="index" :index="indexMethod" />
                            <el-table-column fixed prop="ServerName" label="Server Name" width="120" />
                            <el-table-column prop="MachineName" label="MachineName" width="150" />
                            <el-table-column label="IP Info">
                                <el-table-column prop="InnerIP" label="Inner IP" width="130" />
                                <el-table-column prop="OuterIP" label="Outer IP" width="130" />
                            </el-table-column>

                            <el-table-column label="Count Info">
                                <el-table-column prop="Username" label="User Name" width="100" />
                                <el-table-column prop="Userpassword" label="User Password" width="130" />
                            </el-table-column>

                            <el-table-column label="Misc.">
                                <el-table-column prop="TextRecord" label="Text Record" width="150" />
                                <el-table-column prop="Remark" label="Remark" width="150" />
                                <el-table-column prop="Token" label="Token" width="120" />
                            </el-table-column>

                            <el-table-column prop="Remark2" label="Remark 2" width="120" />
                            <el-table-column prop="Remark3" label="Remark 3" width="120" />

                            <!-- <el-table-column fixed="right" label="Operations" width="240">
                                <template #default="scope">
                                    <el-button type="info" @click="handleEdit(scope.$index, scope.row)">Copy</el-button>
                                    <el-button type="primary" @click="handleEdit(scope.$index, scope.row)">Edit</el-button>
                                    <el-button type="danger" @click="handleEdit(scope.$index, scope.row)">Del</el-button>
                                </template>
                            </el-table-column> -->
                         
                                <el-table-column label="Operations" width="240">
                                    <template #default="scope">
                                        <el-button-group class="ml-4">
                                            
                                            <el-button type="primary" :icon="DocumentCopy" @click="copyclip(scope.$index, scope.row)"/>
                                            <el-button type="primary" :icon="Edit"  @click="handleEdit(scope.$index, scope.row)"/>
                                            <el-button type="primary" :icon="Share" />
                                        </el-button-group>
                                    </template>
                                </el-table-column>

                        </el-table>
                    </div>
                </el-main>
            </el-container>
        </el-container>
    </div>
</template>
    

<script setup  lang="jsx">
import { ref } from 'vue';
import { useRouter, useRoute } from 'vue-router';
const router = useRouter();
const route = useRoute();
const active = ref(route.path);

import { Check, Delete, Edit, Message, Search, Star, Share, Upload,DocumentCopy } from '@element-plus/icons-vue' 
</script>  

<script lang="jsx">
import * as echarts from 'echarts';
import { login } from "@/api/config";
import { getConfigList } from "@/api/netcfg"

export default {
    components: {
    },
    data() {
        return {
            contentTitle: '', //   
            configList: [],
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
        function indexMethod(index) {
            return index * 2
        }
    },
    mounted() {
        this.setTitle();
        this.getAllConfigList()

    },
    methods: {
        setTitle() {
            this.contentTitle = "Weather Info";
        },
        getAllConfigList() {
            let that = this;
            getConfigList().then((data) => {
                that.configList = data.Data
            });
        },
        handleEdit(index, row) {
            console.log(index, row.date)
        },
        copyclip(index, row){
            navigator.clipboard.writeText(JSON.stringify(row));
        }
    }
}; 
</script>


<style scoped lang="less" >
 .header {
     width: 100%;
     height: 64px;
 }

 .sidebar {
     width: 200px;
     height: 100vh;
     position: fixed;
 }

 .container {
     margin: 00px;
     background-color: #efefef;
     position: absolute;
     left: 200px;
     top: 60px;
     bottom: 0;
     right: 0;
     overflow: auto; 

     .menubar{
        padding: 5px 0 5px 5px;
     }
 }
</style>


  
 