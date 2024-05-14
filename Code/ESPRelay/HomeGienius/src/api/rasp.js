import { serverUrl } from '@/api/config'
import axios from 'axios'

export function getvlcsrc() {
    var url = serverUrl + "/api/raspsrc/getlist";
    return axios({
        url: url,
        method: "post",
        headers: {
            'Content-Type': 'application/json'
        },
        data: ''
    })
}

export function setclocksrc(_clocksrc) {
    var url = serverUrl + "/api/raspsrc/setclockurl";
    let fromData = new FormData()
    fromData.append('clocksrc', _clocksrc)
    return axios({
        url: url,
        method: "post",
        // headers: {
        //     'Content-Type': 'application/json'
        // },
        data: fromData
    })
}