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
                                    <div class="grid-content " /><el-button type="Info" :icon="Delete" @click="deleteMutipleRecord()">Delete</el-button>
                                </el-col>
                                <el-col :span="1.5" class=" ">
                                    <div class="grid-content" /><el-button type="success" :icon="Delete">Delete</el-button>
                                </el-col>
                            </el-row>
                        </div>
                        <el-table ref="tableref" :data="configList" stripe border height="calc(100% - 52px)"  header-cell-class-name="tableheader">
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
                                        <el-tooltip content="Copy" placement="top-start">
                                            <el-button type="primary" :icon="DocumentCopy"  @click="copyclip(scope.$index, scope.row)" />
                                        </el-tooltip>

                                        <el-tooltip content="Update" placement="top-start">
                                            <el-button type="primary" :icon="Edit" @click="handleEdit(scope.$index, scope.row)" />
                                        </el-tooltip>  

                                        <el-popconfirm confirm-button-text="Yes" cancel-button-text="No" :icon="InfoFilled" icon-color="#626AEF" title="Are you sure to delete this?"
                                                @confirm="handleDeleteOne(scope.$index, scope.row)">
                                                <template #reference> 
                                                        <div style="float: left;">
                                                            <el-tooltip content="Delete" placement="top-start">    
                                                                <el-button type="primary" :icon="Delete"> </el-button>
                                                            </el-tooltip>                     
                                                        </div>
                                                    </template>
                                        </el-popconfirm>     
                                  
                                    </el-button-group>
                                </template>
                            </el-table-column>

                        </el-table>
                    </div>
                </el-main>

                <!-- the form of updating-->
                <el-dialog v-model="dialogFormVisible" :title="editdialogname" :show-close="true" @close="editformscoll2top"> 
                    <el-scrollbar height="400px" ref="editFormScollbar"> 
                     <el-form :model="editform" label-width="120px" style="">
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
                            <el-button type="primary" @click="saveEditForm()">Save</el-button>
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
 
import { ElMessage, ElMessageBox  } from 'element-plus' 
import {Plus,Check, Delete, Edit,InfoFilled, Message, Search, Star, Share, Upload, DocumentCopy,CircleCloseFilled } from '@element-plus/icons-vue'  
function indexMethod(index) {
    return index * 1
}
</script>

<script lang="jsx"> 
import { getConfigList,addConfigInfo,updateConfigInfo,deleteConfigInfos } from "@/api/netcfg"

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
            },
            editdialogname:'',
            eidtdialogType:''

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
            let that = this;
            that.editform.RecId = row.RecId;
            that.editform.ServerName = row.ServerName;
            that.editform.InnerIP = row.InnerIP;
            that.editform.OuterIP = row.OuterIP;;
            that.editform.Username = row.Username;
            that.editform.Userpassword = row.Userpassword;
            that.editform.Token = row.Token;
            that.editform.Remark = row.Remark;
            that.editform.Remark2 = row.Remark2;
            that.editform.Remark3 = row.Remark3;
            that.editform.MachineName = row.MachineName;
            that.editform.TextRecord = row.TextRecord;

            this.showdialog("update");
        },
        copyclip(index, row) {
            navigator.clipboard.writeText(JSON.stringify(row));
        },
        editformscoll2top(){
            this.$refs.editFormScollbar.scrollTo({ top: 0, behavior: 'smooth' });  
        },
        showdialog(type) {  
            let that = this;
            that.eidtdialogType=type;
            if(type=='add'){
                that.editdialogname="Create a new configuration record";  
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
                that.editform.RecId=0;  
            }else if(type=='update'){
                that.editdialogname="Update configuration information"; 
            }
            that.dialogFormVisible = true;
        },
        saveEditForm() {
            let that = this;
            if (that.eidtdialogType == "add") {
                addConfigInfo(that.editform).then((resultTip) => {
                    ElMessageBox.alert(resultTip.Msg, 'Tip', {
                        confirmButtonText: 'OK',
                        callback: (action) => {
                            if (resultTip.Status == 1) {
                                ElMessage({
                                    showClose: true,
                                    message: "New configuration record created successfully.",
                                    type: 'success',
                                });
                                this.getAllConfigList();
                                setTimeout(function () { that.dialogFormVisible = false; }, 1500);
                            } else {
                                ElMessage({
                                    showClose: true,
                                    message: 'Fail.' + resultTip.Msg,
                                    type: 'error',
                                });
                            }
                        },
                    })
                })
            }else if (that.eidtdialogType == "update") {
                updateConfigInfo(that.editform).then((resultTip) => {
                    ElMessageBox.alert(resultTip.Msg, 'Tip', {
                        confirmButtonText: 'OK',
                        callback: (action) => {
                            if (resultTip.Status == 1) {
                                //that.editform
                                //that.configList
                                that.configList.forEach((element) => {
                                    if (element.RecId == that.editform.RecId) {
                                        element.ServerName = that.editform.ServerName;
                                        element.InnerIP = that.editform.InnerIP;
                                        element.OuterIP = that.editform.OuterIP;;
                                        element.Username = that.editform.Username;
                                        element.Userpassword = that.editform.Userpassword;
                                        element.Token = that.editform.Token;
                                        element.Remark = that.editform.Remark;
                                        element.Remark2 = that.editform.Remark2;
                                        element.Remark3 = that.editform.Remark3;
                                        element.MachineName = that.editform.MachineName;
                                        element.TextRecord = that.editform.TextRecord; 
                                    }
                                }); 

                                ElMessage({
                                    showClose: true,
                                    message: "The configuration record has been successfully updated.",
                                    type: 'success',
                                });
                                setTimeout(function () { that.dialogFormVisible = false; }, 1500);
                            } else {
                                ElMessage({
                                    showClose: true,
                                    message: 'Fail.' + resultTip.Msg,
                                    type: 'error',
                                });
                            }
                        },
                    })
                })
            }
        },
        handleDeleteOne(index, row){
            let deleteId=row.RecId;
            let idarray=[deleteId]; 
            deleteConfigInfos(idarray).then((resultTip) => {
                ElMessageBox.alert(resultTip.Msg, 'Tip', {
                        confirmButtonText: 'OK',
                        callback: (action) => {
                            if (resultTip.Status == 1) {
                                ElMessage({
                                    showClose: true,
                                    message: "The configuration record has been successfully deleted.",
                                    type: 'success',
                                });
                                this.getAllConfigList();
                                setTimeout(function () { that.dialogFormVisible = false; }, 1500);
                            } else {
                                ElMessage({
                                    showClose: true,
                                    message: 'Fail.' + resultTip.Msg,
                                    type: 'error',
                                });
                            }
                        },
                    })
            });
        },
        deleteMutipleRecord(){
           var selectRows= this.$refs.tableref.getSelectionRows();
           console.log(selectRows);

            if (selectRows.length == 0) {
                ElMessage({
                    showClose: true,
                    message: 'Please select at least one record.',
                    type: 'error',
                });
            } else {
                ElMessageBox.confirm(
                    `${selectRows.length} record${selectRows.length>1?'s':''} will be deleted. Continue?`,
                    'Warning',
                    {
                        confirmButtonText: 'OK',
                        cancelButtonText: 'Cancel',
                        type: 'warning',
                    }
                ).then(() => {
                    let idArray = [];
                    for (let index = 0; index < selectRows.length; index++) {
                        idArray.push(selectRows[index].RecId);
                    }
                    deleteConfigInfos(idArray).then((resultTip) => {
                        ElMessageBox.alert(resultTip.Msg, 'Tip', {
                            confirmButtonText: 'OK',
                            callback: (action) => {
                                if (resultTip.Status == 1) {
                                    ElMessage({
                                        showClose: true,
                                        message: `The configuration ${idArray.length>1?'record has':'records have'}been successfully deleted.`,
                                        type: 'success', 
                                    });
                                    this.getAllConfigList();
                                    setTimeout(function () { that.dialogFormVisible = false; }, 1500);
                                } else {
                                    ElMessage({
                                        showClose: true,
                                        message: 'Fail.' + resultTip.Msg,
                                        type: 'error',
                                    });
                                }
                            },
                        })
                    }); 
                }).catch(() => {
                    ElMessage({
                        type: 'info',
                        message: 'Delete canceled',
                    })
                })
            } 
        } 
    }
};
</script> 
<style lang="scss">
.el-dialog__header {
    padding: 10px 10px 0 20px
}

.tableheader {
    background-color: rgb(50, 83, 220) !important;
    color: #FFF;
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



