<template>
    <div class="title">
        <van-nav-bar :title="contentTitle"></van-nav-bar> 
    </div>
    <div class="control"> 
        <van-cell-group>
  <van-cell title="Cell title1" value="Content1" />
  <van-cell title="Cell title2" value="Content2" label="Description" />
</van-cell-group>

<van-cell-group title="Group 1">
  <van-cell title="Cell title" value="Content" />
  <van-cell title="Cell title" value="Content" />
  <van-cell title="Cell title" value="Content" />
  <van-cell title="Cell title" value="Content" />
  <van-cell title="Cell title" value="Content" />
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
import { login } from "@/api/config"; 
export default {
    components: {
    },
    data() {
        return {
            contentTitle: '', //  
            currentdate: '', //  
            mqttclient: null,
        };
    },
    computed: {
    },
    setup() {

    },
    mounted() {
        this.setTitle();
        const idcode = this.$route.query.idcode;
        if (idcode != null) {
           
        } else {
            showFailToast('url error')
        }
    },
    methods: {
        setTitle() {
            this.contentTitle = "ESP32 Controller";
        },  
    }
}; 
</script>

<style scoped>
.control{
    margin: 0 20px;
}
.sysname{
    margin: 5px 0;
}

.title>span {
    display: block;
    font-size: 24px;
}
</style>