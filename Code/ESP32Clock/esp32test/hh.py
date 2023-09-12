# matrixtest.py
#
import sys, os
from time import sleep_ms,sleep
from machine import Pin, SPI
from matrix8x8 import MATRIX

chip=sys.platform
if chip == 'esp8266':
    # Pintranslator fuer ESP8266-Boards
    # LUA-Pins     D0 D1 D2 D3 D4 D5 D6 D7 D8
    # ESP8266 Pins 16  5  4  0  2 14 12 13 15 
    #                 SC SD
    bus = 1
    MISOp = Pin(12)
    MOSIp = Pin(13)
    SCKp  = Pin(14)
    spi=SPI(1,baudrate=4000000)   #ESP8266
    # # alternativ virtuell mit bitbanging
    # spi=SPI(-1,baudrate=4000000,sck=SCK,mosi=MOSI,\
    #         miso=MISO,polarity=0,phase=0)  #ESP8266
    CSp = Pin(16, mode=Pin.OUT, value=1)
elif chip == 'esp32':
    bus = 1
    MISOp= Pin(15)
    MOSIp= Pin(2)
    SCKp = Pin(4)
    spi=SPI(1,baudrate=10000000,sck=Pin(4),mosi=Pin(2),\
            miso=Pin(15),polarity=0,phase=0)  # ESP32
    CSp = Pin(4, mode=Pin.OUT, value=1)
else:
    # blink(led,800,100,inverted=True,repeat=5)
    raise OSError ("Unbekannter Port")
    
print("Hardware-Bus {}: Pins fest vorgegeben".format(bus))
print("MISO {}, MOSI {}, SCK {}, CS{}\n".format(MISOp,MOSIp,SCKp,CSp))

d=MATRIX(spi,CSp,16)

heart=( 0b00000000,
        0b01101100,
        0b11111110,
        0b11111110,
        0b01111100,
        0b00111000,
        0b00010000,
        0b00000000,
      )

delay=50
# *************************************
d.shape(d.anzahl-1,heart)
d.setIntensity(0)
d.show()
sleep(0.5)
for i in range(5):
    d.setIntensity(15)
    sleep_ms(500)
    for b in range(15,0,-1):
        d.setIntensity(b)
        sleep_ms(30)
# *************************************
d.pixelShift((d.anzahl-1)*8-1,direction="left",delay=35)
sleep_ms(500)
for v in range(d.anzahl*8-4,10,-7):
    d.pixelShift(8,direction="right",delay=v)
d.clear()
# *************************************
d.roll("Hallo, Freunde, hola amigos, ahoi pratele, hello baratok...", cnt=1, delay=30)
# *************************************
d.text("SHIFT UP",0,0,1)
d.show()
sleep(0.5)
d.blink(200,400,5)
d.pixelShift(8,"UP")
# *************************************
d.rect(0,0,d.anzahl*8,8,1)
d.show()
sleep(1)
# *************************************
for i in range(d.anzahl):
    d.line(0+i*8,0,(i+1)*8-1,7,1)
    d.show()
    sleep(0.1)
    d.line(0+i*8,7,(i+1)*8-1,0,1)
    d.show()
    sleep(0.3)
d.rect(0,0,d.anzahl*8,8,0)
d.pixelShift(8,"down",500)
# *************************************
for j in range(d.anzahl):
    for i in range(4):
        d.rect(j*8+3-i,3-i,(i+1)*2,(i+1)*2,1)
        d.show()
        sleep(0.05)
    for i in range(4):
        d.rect(j*8+3-i,3-i,(i+1)*2,(i+1)*2,0)
        d.show()
        sleep(0.05)
# *************************************
mesg=("FESTE","feiern","PARTY",".PARTY.","..PARTY..")
for i in range(len(mesg)):
    d.clear()
    d.center(mesg[i])
    d.show()
    sleep(1)
# *************************************    
d.pixelShift(d.anzahl*8-4,"right")
d.text("ENDE",16,0,1)
d.show()
sleep(0.3)
d.clear()
d.show()
d.text("EN DE",12,0,1)
d.show()
sleep(0.3)
d.clear()
d.show()
d.text("E N DE",8,0,1)
d.show()
sleep(0.3)
d.clear()
d.show()
d.text("E N D E",4,0,1)
d.show()
sleep(2)
d.pixelShift(d.anzahl*8-4,"right",20)