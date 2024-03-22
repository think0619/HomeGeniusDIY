<template>
    <div class="common-layout">
        <el-container>
            <el-header>
                <div class="headbtn"> <el-button plain type="warning" :icon="TurnOff" @click="logout()">Logout</el-button>
                </div>
            </el-header>
            <el-container>
                <el-aside width="200px" style="">Aside</el-aside>
                <el-main>
                    <div class="container">
                        <!--  -->
                        <div class="menubar">
                            <el-row :gutter="0">
                                <el-col :span="1.5" class=" ">
                                    <div class="grid-content" /><el-button :icon="Plus" plain
                                        @click="showdialog('add')">Create</el-button>
                                </el-col>
                                <el-col :span="1.5" class=" ">
                                    <div class="grid-content " /><el-button plain :icon="Delete"
                                        @click="deleteMutipleRecord()">Delete</el-button>
                                </el-col>
                                <el-col :span="1.5" class=" ">
                                    <div class="grid-content" /><el-button plain :icon="Refresh"
                                        @click="refreshpage()">Refresh</el-button>
                                </el-col>
                                <el-col :span="1.5" class=" ">
                                    <div class="grid-content" /><el-button plain :icon="Memo"
                                        @click="exportdata()">Export</el-button>
                                </el-col>
                            </el-row>
                        </div>
                        <el-table ref="tableref" v-loading="loadingdata" :data="configList" stripe border
                            height="calc(100% - 52px)" element-loading-background="rgba(122, 122, 122, 0.8)"
                            header-cell-class-name="tableheader">

                            <el-table-column type="selection" width="55" />
                            <el-table-column fixed type="index" label="NO." :index="indexMethod" width="60" />
                            <el-table-column fixed prop="ServerName" label="Server Name" width="150" />
                            <el-table-column fixed label="Operations" width="170">
                                <template #default="scope">
                                    <el-button-group class="ml-4">
                                        <el-tooltip content="Copy" placement="top-start">
                                            <el-button type="primary" :icon="DocumentCopy"
                                                @click="copyclip(scope.$index, scope.row)" />
                                        </el-tooltip>

                                        <el-tooltip content="Update" placement="top-start">
                                            <el-button type="primary" :icon="Edit" class="el-btn-ml"
                                                @click="handleEdit(scope.$index, scope.row)" />
                                        </el-tooltip>

                                        <el-popconfirm confirm-button-text="Yes" cancel-button-text="No"
                                            :icon="InfoFilled" icon-color="#626AEF" title="Are you sure to delete this?"
                                            @confirm="handleDeleteOne(scope.$index, scope.row)">
                                            <template #reference>
                                                <div style="float: left;" class="el-btn-ml">
                                                    <el-tooltip content="Delete" placement="top-start">
                                                        <el-button type="primary" :icon="Delete"> </el-button>
                                                    </el-tooltip>
                                                </div>
                                            </template>
                                        </el-popconfirm>
                                    </el-button-group>
                                </template>
                            </el-table-column>

                            <el-table-column prop="ConfigType" label="Config Type" width="150" />
                            <el-table-column prop="MachineName" label="MachineName" width="150" />
                            <el-table-column label="IP Info">
                                <el-table-column prop="InnerIP" label="Inner IP" width="150" />
                                <el-table-column prop="OuterIP" label="Outer IP" width="200" />
                            </el-table-column>

                            <el-table-column label="Web Info">
                                <el-table-column prop="WebUrl" label="Url" width="200" />
                                <el-table-column prop="WebBindMail" label="Bind Mail" width="200" />
                            </el-table-column>

                            <el-table-column label="Count Info">
                                <el-table-column prop="Username" label="User Name" width="150" />
                                <el-table-column prop="Userpassword" label="User Password" width="130" />
                            </el-table-column>

                            <el-table-column label="Misc.">
                                <el-table-column prop="TextRecord" label="Text Record" width="150" />
                                <el-table-column prop="Remark" label="Remark" width="240" />
                                <el-table-column prop="Token" label="Token" width="200" />
                            </el-table-column>

                            <el-table-column prop="Remark2" label="Remark 2" width="200" />
                            <el-table-column prop="Remark3" label="Remark 3" width="120" />

                            <!-- <el-table-column fixed="right" label="Operations" width="240">
                                <template #default="scope">
                                    <el-button type="info" @click="handleEdit(scope.$index, scope.row)">Copy</el-button>
                                    <el-button type="primary" @click="handleEdit(scope.$index, scope.row)">Edit</el-button>
                                    <el-button type="danger" @click="handleEdit(scope.$index, scope.row)">Del</el-button>
                                </template>
                            </el-table-column> -->
                        </el-table>
                    </div>
                </el-main>

                <!-- the form of updating-->
                <el-dialog v-model="dialogFormVisible" :title="editdialogname" :show-close="true"
                    @close="editformscoll2top">
                    <el-scrollbar height="400px" ref="editFormScollbar">
                        <el-form :model="editform" label-width="120px" style="">
                            <el-form-item label="Server Name"><el-input v-model="editform.ServerName" /> </el-form-item>
                            <!-- <el-form-item label="Config Type"><el-input v-model="editform.ConfigType"/> </el-form-item>  -->
                            <el-form-item label="Config Type">
                                <el-select v-model="editform.ConfigType" placeholder="Select" clearable
                                    style="width:100%">
                                    <el-option v-for="item in cfgTypeOps" :key="item.value" :label="item.label"
                                        :value="item.value" />
                                </el-select>
                            </el-form-item>
                            <el-form-item label="Machine Name"><el-input v-model="editform.MachineName" />
                            </el-form-item>
                            <el-form-item label="Inner IP/Port"><el-input v-model="editform.InnerIP" /> </el-form-item>
                            <el-form-item label="Outer IP/Port"><el-input v-model="editform.OuterIP" /> </el-form-item>
                            <el-form-item label="Web Url"><el-input v-model="editform.WebUrl" /> </el-form-item>
                            <el-form-item label="Web Bind Mail"><el-input v-model="editform.WebBindMail" />
                            </el-form-item>
                            <el-form-item label="User Name"><el-input v-model="editform.Username" /> </el-form-item>
                            <el-form-item label="User Password"><el-input v-model="editform.Userpassword" />
                            </el-form-item>
                            <el-form-item label="Text Record"><el-input v-model="editform.TextRecord" /> </el-form-item>
                            <el-form-item label="Token"><el-input v-model="editform.Token" /> </el-form-item>
                            <el-form-item label="Remark"><el-input v-model="editform.Remark" /> </el-form-item>
                            <el-form-item label="Remark 2"><el-input v-model="editform.Remark2" /> </el-form-item>
                            <el-form-item label="Remark 3"><el-input v-model="editform.Remark3" /> </el-form-item>
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


<script setup lang="jsx">
    import { ref } from 'vue';
    import { useRouter, useRoute } from 'vue-router';
    import { ElMessage, ElMessageBox } from 'element-plus'
    import { Plus, Check, Delete, Edit, InfoFilled, Refresh, Memo, TurnOff, DocumentCopy, CircleCloseFilled } from '@element-plus/icons-vue'

  //   import logout from "@/assets/img/icons8-logout-48.png"
    import logout from "@/assets/img/logout-1.svg"


    const router = useRouter();
    const route = useRoute();
    const active = ref(route.path);
    function indexMethod(index) {
        return index + 1
    }


</script>

<script lang="jsx">
    import { getConfigList, addConfigInfo, updateConfigInfo, deleteConfigInfos, exportConfigInfos } from "@/api/netcfg"

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
                    WebUrl: '',
                    WebBindMail: '',
                    Username: '',
                    Userpassword: '',
                    Token: '',
                    Remark: '',
                    Remark2: '',
                    Remark3: '',
                    MachineName: '',
                    TextRecord: '',
                    ConfigType: '',
                    RecId: 0,
                },
                editdialogname: '',
                eidtdialogType: '',
                loadingdata: false,
                cfgTypeOps: [
                    { value: 'Config', label: 'Config', },
                    { value: 'Web', label: 'Web', },
                ]
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
            logout() {
                localStorage.clear()
                // this.$router.replace({ path: "/login" }) ;
            },
            getAllConfigList() {
                let that = this;
                that.loadingdata = true;
                getConfigList().then((data) => {
                    if (data.Status == -1) {
                        localStorage.clear()
                        this.$router.replace({ path: "/login" });
                    } else if (data.Status == 1) {
                        that.configList = data.Data;
                        that.loadingdata = false
                    }
                });
            },
            handleEdit(index, row) {
                let that = this;
                that.editform.RecId = row.RecId;
                that.editform.ServerName = row.ServerName;
                that.editform.InnerIP = row.InnerIP;
                that.editform.OuterIP = row.OuterIP;;
                that.editform.WebUrl = row.WebUrl;
                that.editform.WebBindMail = row.WebBindMail;;
                that.editform.Username = row.Username;
                that.editform.Userpassword = row.Userpassword;
                that.editform.Token = row.Token;
                that.editform.Remark = row.Remark;
                that.editform.Remark2 = row.Remark2;
                that.editform.Remark3 = row.Remark3;
                that.editform.MachineName = row.MachineName;
                that.editform.TextRecord = row.TextRecord;
                that.editform.ConfigType = row.ConfigType;

                this.showdialog("update");
            },
            copyclip(index, row) {
                navigator.clipboard.writeText(JSON.stringify(row));
            },
            editformscoll2top() {
                this.$refs.editFormScollbar.scrollTo({ top: 0, behavior: 'smooth' });
            },
            showdialog(type) {
                let that = this;
                that.eidtdialogType = type;
                if (type == 'add') {
                    that.editdialogname = "Create a new configuration record";
                    that.editform.ServerName = "";
                    that.editform.InnerIP = "";
                    that.editform.OuterIP = "";
                    that.editform.Username = "";
                    that.editform.Userpassword = "";
                    that.editform.Token = "";
                    that.editform.Remark = "";
                    that.editform.Remark2 = "";
                    that.editform.Remark3 = "";
                    that.editform.MachineName = "";
                    that.editform.TextRecord = "";
                    that.editform.RecId = 0;
                    that.editform.WebUrl = "";
                    that.editform.WebBindMail = "";
                    that.editform.ConfigType = "";
                } else if (type == 'update') {
                    that.editdialogname = "Update configuration information";
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
                                        message: "New configuration record has been created successfully.",
                                        type: 'success',
                                    });
                                    this.getAllConfigList();
                                    setTimeout(function () { that.dialogFormVisible = false; }, 1500);
                                } else if (resultTip.Status == -1) {
                                    ElMessage({
                                            showClose: true,
                                            message: 'Unauthorized.',
                                            type: 'error',
                                        });
                                    localStorage.clear()
                                    this.$router.replace({ path: "/login" });
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
                } else if (that.eidtdialogType == "update") {
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
                                            element.WebUrl = that.editform.WebUrl;
                                            element.WebBindMail = that.editform.WebBindMail;;
                                            element.Username = that.editform.Username;
                                            element.Userpassword = that.editform.Userpassword;
                                            element.Token = that.editform.Token;
                                            element.Remark = that.editform.Remark;
                                            element.Remark2 = that.editform.Remark2;
                                            element.Remark3 = that.editform.Remark3;
                                            element.MachineName = that.editform.MachineName;
                                            element.TextRecord = that.editform.TextRecord;
                                            element.ConfigType = that.editform.ConfigType;
                                        }
                                    });
                                    ElMessage({
                                        showClose: true,
                                        message: "The configuration record has been updated successfully.",
                                        type: 'success',
                                    });
                                    setTimeout(function () { that.dialogFormVisible = false; }, 1500);
                                } else if (resultTip.Status == -1) {
                                    localStorage.clear()
                                    this.$router.replace({ path: "/login" });
                                }
                                else {
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
            handleDeleteOne(index, row) {
                let deleteId = row.RecId;
                let idarray = [deleteId];
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
                            } else if (resultTip.Status == -1) {
                                ElMessage({
                                    showClose: true,
                                    message: 'Unauthorized.',
                                    type: 'error',
                                });
                                localStorage.clear()
                                this.$router.replace({ path: "/login" });
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
            deleteMutipleRecord() {
                var selectRows = this.$refs.tableref.getSelectionRows();
                // console.log(selectRows);

                if (selectRows.length == 0) {
                    ElMessage({
                        showClose: true,
                        message: 'Please select at least one record.',
                        type: 'error',
                    });
                } else {
                    ElMessageBox.confirm(
                        `${selectRows.length} record${selectRows.length > 1 ? 's' : ''} will be deleted. Continue?`,
                        'Warning',
                        {
                            confirmButtonText: 'OK',
                            cancelButtonText: 'Cancel',
                            type: 'warning',
                        }
                    ).then(() => {
                        let idArray = [];
                        for (let index = 0; index < selectRows.length; index++) {
                            idArray.push(selectRows[index].Id);
                        }
                        deleteConfigInfos(idArray).then((resultTip) => {
                            ElMessageBox.alert(resultTip.Msg, 'Tip', {
                                confirmButtonText: 'OK',
                                callback: (action) => {
                                    if (resultTip.Status == 1) {
                                        ElMessage({
                                            showClose: true,
                                            message: `The configuration ${idArray.length > 1 ? 'record has' : 'records have'}been successfully deleted.`,
                                            type: 'success',
                                        });
                                        this.getAllConfigList();
                                        setTimeout(function () { that.dialogFormVisible = false; }, 1500);
                                    } else if (resultTip.Status == -1) { 
                                        ElMessage({
                                            showClose: true,
                                            message: 'Unauthorized.',
                                            type: 'error',
                                        });
                                        localStorage.clear()
                                        this.$router.replace({ path: "/login" });
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
            },
            async refreshpage() {
                let that = this;
                this.loadingdata = true;
                await getConfigList().then((data) => {
                    if (data.Status == -1) {
                        ElMessage({
                            showClose: true,
                            message: 'Unauthorized.',
                            type: 'error',
                        });
                        localStorage.clear()
                        this.$router.replace({ path: "/login" });
                    } else if (data.Status == 1) {
                        that.configList = data.Data;
                        this.$refs.tableref.scrollTo({ top: 0, left: 0, })
                        that.$refs.tableref.clearSelection();
                        that.loadingdata = false
                    }
                });
            },
            exportdata() {
                var selectRows = this.$refs.tableref.getSelectionRows();
                if (selectRows.length == 0) {
                    ElMessage({
                        showClose: true,
                        message: 'Please select at least one record.',
                        type: 'error',
                    });
                } else {
                    let idArray = [];
                    for (let index = 0; index < selectRows.length; index++) {
                        idArray.push(selectRows[index].Id);
                    }
                    exportConfigInfos(idArray).then((result) => {
                        //接口请求成功之后完成以下操作
                        let blob = new Blob([result.data], { type: 'application/vnd.ms-excel' });
                        // 获取 获取响应头 heads 中的 filename 文件名          
                        let temp = result.headers["content-disposition"].split(";")[1].split("Filename=")[1];
                        // 把 %E9%AB%98%E6%84%8F%E5%90%91%E5%AD%A6%E5%91%98303.xlsx 转化成文字
                        var fileName = decodeURIComponent(temp.slice(1).slice(0, -1));
                        //创建一个 a 标签
                        const link = document.createElement('a')
                        //不显示a标签
                        link.style.display = 'none'
                        // 给a 标签的href属性赋值
                        link.href = URL.createObjectURL(blob);
                        link.setAttribute('download', fileName)
                        //把a标签插入页面中
                        document.body.appendChild(link)
                        link.click()
                        //点击之后移除a标签
                        document.body.removeChild(link)
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

    .el-icon-my-export {
        background: url('@/assets/img/reset.png') no-repeat;
        font-size: 16px;
        background-size: cover;
    }

    .el-icon-my-export:before {
        content: "替";
        font-size: 16px;
        visibility: hidden;
    }

    .el-btn-ml {
        margin-left: 3px !important;
    }


</style>


<style scoped lang="less">
    .header {
        width: 100%;
        height: 64px;
    }

    .headbtn {
        float: right;
        margin: 8px 0 0 0;
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
