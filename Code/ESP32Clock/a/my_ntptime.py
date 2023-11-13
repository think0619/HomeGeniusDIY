# Adapted from official ntptime by Peter Hinch July 2022
# The main aim is portability:
# Detects host device's epoch and returns time relative to that.
# Basic approach to local time: add offset in hours relative to UTC.
# Timeouts return a time of 0. These happen: caller should check for this.
# Replace socket timeout with select.poll as per docs:
# http://docs.micropython.org/en/latest/library/socket.html#socket.socket.settimeout

#  modified D. Festing 03 Feb 2023 to just return val

import socket
import struct
import select
from time import gmtime


# The NTP host can be configured at runtime by doing: ntptime.host = 'myhost.org'
host = "ntp.aliyun.com"


def time():
    NTP_QUERY = bytearray(48)
    NTP_QUERY[0] = 0x1B
    try:
        addr = socket.getaddrinfo(host, 123)[0][-1]
    except OSError:
        return 0
    s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    poller = select.poll()
    poller.register(s, select.POLLIN)
    try:
        s.sendto(NTP_QUERY, addr)
        if poller.poll(1000):  #  time in milliseconds
            msg = s.recv(48)
            val = struct.unpack("!I", msg[40:44])[0]  # can return 0

            return val
    except OSError:
        pass  #  LAN error
    finally:
        s.close()