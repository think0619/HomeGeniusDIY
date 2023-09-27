//  index.js
import axios from 'axios'

var host = 'http://localhost:34172';

export function addkeeprecord(_params) {
    var url = host + "/api/HandleKeepRecord/Add";
    return axios({
        url: url,
        method: "post",
        data: _params
    })
}

export function querykeeprecord(_params) {
    var url = host + "/api/HandleKeepRecord/Query";
    return axios({
        url: url,
        method: "post",
        data: _params
    })
}

export function deletekeeprecord(_params) {
    var url = host + "/api/HandleKeepRecord/Del";
    return axios({
        url: url,
        method: "post",
        data: _params
    })
}