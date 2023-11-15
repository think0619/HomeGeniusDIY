<template>
    <div class="common-layout">
        <el-container>
            <el-header> </el-header>
            <el-container>
                <el-aside width="200px" style="">Aside</el-aside>
                <el-main>
                    <div class="container">
                        <!--  -->
                        <div class="menubar">
                            <el-row :gutter="0">
                                <el-col :span="1.5" class=" ">
                                    <div class="grid-content" /><el-button :icon="Plus" plain @click="showdialog('add')">Create</el-button>
                                </el-col>
                                <el-col :span="1.5" class=" ">
                                    <div class="grid-content " /><el-button type="success" :icon="Delete">Delete</el-button>
                                </el-col>
                                <el-col :span="1.5" class=" ">
                                    <div class="grid-content" /><el-button type="success" :icon="Delete">Delete</el-button>
                                </el-col>
                            </el-row>
                        </div>
                        <el-table :data="configList" stripe border height="calc(100% - 52px)"
                            header-cell-class-name="tableheader">
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
                                        <el-button type="primary" :icon="DocumentCopy"
                                            @click="copyclip(scope.$index, scope.row)" />
                                        <el-button type="primary" :icon="Edit"
                                            @click="handleEdit(scope.$index, scope.row)" />
                                        <el-button type="primary" :icon="Share" />
                                    </el-button-group>
                                </template>
                            </el-table-column>

                        </el-table>
                    </div>
                </el-main>

                <!-- the form of updating-->
                <el-dialog v-model="dialogFormVisible" title="Shipping address" :show-close="true">
                  
                    <el-scrollbar height="400px"> 
                     <el-form :model="form" label-width="120px" style="">
                        <el-form-item label="Server Name"><el-input v-model="editform.ServerName"/> </el-form-item> 
                        <el-form-item label="Machine Name"><el-input v-model="editform.MachineName"/> </el-form-item> 
                        <el-form-item label="Inner IP"><el-input v-model="editform.InnerIP"/> </el-form-item> 
                        <el-form-item label="Outer IP"><el-input v-model="editform.OuterIP"/> </el-form-item> 
                        <el-form-item label="User Name"><el-input v-model="editform.Username"/> </el-form-item> 
                        <el-form-item label="User Password"><el-input v-model="editform.Userpassword"/> </el-form-item> 
                        <el-form-item label="Text Record"><el-input v-model="editform.TextRecord"/> </el-form-item>  
                        <el-form-item label="Token"><el-input v-model="editform.Token"/> </el-form-item> 
                        <el-form-item label="Remark"><el-input v-model="editform.Remark"/> </el-form-item> 
                        <el-form-item label="Remark 2"><el-input v-model="editform.Remark2"/> </el-form-item> 
                        <el-form-item label="Remark 3"><el-input v-model="editform.Remark3 "/> </el-form-item> 
                     </el-form>
                    </el-scrollbar>

                    <template #footer>
                        <span class="dialog-footer">
                            <el-button @click="dialogFormVisible = false">Cancel</el-button>
                            <el-button type="primary" @click="dialogFormVisible = false">Save</el-button>
                        </span>
                    </template> 
                    
                </el-dialog>
                <!-- the form of updating-->
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
import {Plus, Check, Delete, Edit, Message, Search, Star, Share, Upload, DocumentCopy,CircleCloseFilled } from '@element-plus/icons-vue'  
function indexMethod(index) {
    return index * 1
}
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
            dialogFormVisible: false,
            configList: [],
            editform: {
                ServerName: '',
                InnerIP: '',
                OuterIP: '',
                Username: '',
                Userpassword: '',
                Token: '',
                Remark: '',
                Remark2: '',
                Remark3: '',
                MachineName: '',
                TextRecord: '',
                RecId: 0,
            }

        };
    },
    computed: {
    },
    setup() {

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
        copyclip(index, row) {
            navigator.clipboard.writeText(JSON.stringify(row));
        },
        showdialog(type) {
            let that = this;
            if(type=='add'){
                that.editform.ServerName="";
                that.editform.InnerIP="";
                that.editform.OuterIP="";
                that.editform.Username="";
                that.editform.Userpassword="";
                that.editform.Token="";
                that.editform.Remark="";
                that.editform.Remark2="";
                that.editform.Remark3="";
                that.editform.MachineName="";
                that.editform.TextRecord="";
                that.editform.RecId="";  
            }
            that.dialogFormVisible = true;
        }

    }
};
</script>

<style>
.tableheader {
    background-color: rgb(50, 83, 220) !important;
    color: #FFF;
}
</style>

<style lang="scss">
.el-dialog__header {
    padding: 10px 10px 0 20px
}
</style>


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
    bottom: 10px;
    right: 0;
    overflow: auto;

    .menubar {
        padding: 10px 0 10px 10px;
    }
}
.my-header {
  display: flex;
  flex-direction: row;
  justify-content: space-between; 
}
.el-row {
    margin-bottom: 20px;
}

.el-row:last-child {
    margin-bottom: 0;
}

.el-col {
    border-radius: 4px;
    margin: 0 6px;
}

.grid-content {
    border-radius: 4px;
}


</style>



