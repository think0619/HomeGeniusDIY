//  index.js
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

export function querykeeprecord(_params) {
    var url = serverUrl + "/api/HandleKeepRecord/Query";
    return axios({
        url: url,
        method: "post",
        data: _params
    })
}

export function deletekeeprecord(_params) {
    var url = serverUrl + "/api/HandleKeepRecord/Del";
    return axios({
        url: url,
        method: "post",
        data: _params
    })
}