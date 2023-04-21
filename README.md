# HomeGeniusDIY
一些关于智能家居方面 diy

### 一点想法
之前在鄂尔多斯时候找了家福建的物联网公司，购买了些开机卡，对某展厅内大部分PC机进行改造，结合二次开发实现了用平板电脑一键给这些电脑进行开机。（为什么不用wol？其实我也想用的，但是那些机器总是没成功。另外，关机用了程序调用"shutdown -f"的方式进行强制关机，舍弃开机卡的原因是因为开机卡其实是模拟物理按键，并没有实现真正的开关机，只用于开机比较合适点。）

最近在想着做点智能家居的东西，就是放在自己家里，谁叫咱家是毛坯房，瞎折腾也没啥事呢

会作为长期项目，希望能坚持久点

### 实例
1. **MQTT Broker** MQTT服务器
2. **网络收音机** 早晨到点给我放放新闻什么，
3. ...


### 技术 
1. EMQX [https://github.com/emqx/emqx](https://github.com/emqx/emqx "")。
2. MQTTNet [https://github.com/emqx/emqx](https://github.com/dotnet/MQTTnet)
3. libvlc [https://www.videolan.org/vlc/libvlc.html](https://www.videolan.org/vlc/libvlc.html)
4. ...
