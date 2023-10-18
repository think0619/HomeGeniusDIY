//  esp.js
import axios from 'axios'
import { serverUrl } from '@/api/config'

export function addkeeprecord(_params) {
    var url = serverUrl + "/api/HandleKeepRecord/Add";
    return axios({
        url: url,
        method: "post",
        data: _params
    })
}