<template>
    <div>
      
    </div>
</template>


<script setup>
// import { useRouter, useRoute } from 'vue-router'
// const router = useRouter()
// const route = useRoute()
// console.log('路由', route.path)
// const goto = (path) => {
//     router.push(path)
// }
</script>

<style lang="scss" scoped></style>

<script>
export default {
    components: {
    },
    data() { 
         return {
            title:'智能喷涂',
            active: 0,
        };
    },
    computed: {
    },
    setup() {
    },
    mounted() {
         this.$emit("onChangeTitle", this.title);
    },
    methods: {
       
    }
}; 
</script>