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