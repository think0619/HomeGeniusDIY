<template>
    <div class="my-login-page">
        <div style="display: flex; justify-content: center;">
            <img src="@/assets/img/icons8-technology-100.png" width="120" />
        </div>
        <el-form :label-position="labelPosition" label-width="100px" :model="inputform" style="max-width: 460px;margin-top: 30px;">
            <el-space fill>
                <el-form-item label=" ">
                    <el-input v-model="inputform.username" placeholder="Username" />
                </el-form-item>
                <el-form-item label=" ">
                    <el-input v-model="inputform.password" placeholder="Password" type="password" show-password  @keyup.enter.native="clicklogin"/>
                </el-form-item>
                <el-button type="primary" @click="clicklogin()">Login</el-button>
            </el-space>
        </el-form>
    </div>
</template>


<script setup  lang="jsx">
import { ref } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import { login as authlogin } from "@/api/config"
import { ElMessage } from 'element-plus'
const router = useRouter();
const route = useRoute();
const active = ref(route.path);

</script>

<script lang="jsx">

export default {
    components: {
    },
    data() {
        return {
            labelPosition: 'top',
            contentTitle: '', // 
            inputform: {
                username: '',
                password: '',
            }
        };
    },
    computed: {
    },
    setup() {

    },
    mounted() {
        this.setTitle();
        localStorage.removeItem('token');
    },
    methods: {
        setTitle() {
            this.contentTitle = "Weather Info";
        },
        clicklogin() {
            // inputform: {
            //     username: '',
            //     password: '', 
            // } 
            let that = this;
            if (that.inputform.username != '' && that.inputform.password != '') {
                authlogin(that.inputform.username, that.inputform.password).then((response) => {
                    if (response != null) {
                        if (response.status == 200) {
                            let result = response.data;
                            if (result.Status == 1) {
                                localStorage.setItem('token', result.Token);
                                ElMessage({
                                    showClose: true,
                                    message: 'Login successfully.',
                                    type: 'success',
                                })
                                this.$router.replace({ path: "/netconfig" }) ;
                            } else if(result.Status == -1){
                                this.$router.replace({ path: "/login" }) ;
                            } else{
                                ElMessage({
                                    showClose: true,
                                    message: result.Msg,
                                    type: 'error',
                                })
                            }
                        }else if(response.status == 401){
                            this.$router.replace({ path: "/login" }) ;
                        } 
                    }
                    else {
                        ElMessage({
                            showClose: true,
                            message: 'Network error.',
                            type: 'error',
                        })
                    }
                });
            } else {
                ElMessage({
                    showClose: true,
                    message: 'Please input username and password.',
                    type: 'error',
                })
            }
        }
    }
};
</script> 
 
<style scoped>
@import "css/my-login.css";

.user-content {
    background-color: #3982e5;
}
</style>
