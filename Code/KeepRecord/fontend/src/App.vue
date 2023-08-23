<template>
  <div id="app"> 
    <router-view />
  </div>
</template>

<script setup>
import './assets/css/index.css'
import './assets/css/font.css'
</script>
<script>

(function() {
           if (typeof WeixinJSBridge == "object" && typeof WeixinJSBridge.invoke == "function") {
               handleFontSize();
           } else {
               if (document.addEventListener) {
                   document.addEventListener("WeixinJSBridgeReady", handleFontSize, false);
               } else if (document.attachEvent) {
                   document.attachEvent("WeixinJSBridgeReady", handleFontSize);
                   document.attachEvent("onWeixinJSBridgeReady", handleFontSize);  }
           }
           function handleFontSize() {
               // 设置网页字体为默认大小
               WeixinJSBridge.invoke('setFontSizeCallback', { 'fontSize' : 0 });
               // 重写设置网页字体大小的事件
               WeixinJSBridge.on('menu:setfont', function() {
                   WeixinJSBridge.invoke('setFontSizeCallback', { 'fontSize' : 0 });
               });
           }
       })();
</script>


<style>
body {
  font-size: 16px; 
  background-size: cover;
  -webkit-font-smoothing: antialiased;
  margin: 0;
  padding: 0;
  border: 0
}
</style>
