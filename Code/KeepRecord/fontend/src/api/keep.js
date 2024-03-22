//  index.js
import axios from 'axios'

var host2 = 'http://localhost:34172';
var host = 'http://192.168.2.100:47081';

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

export function querySinglekeeprecord(_params) {
    var url = host + "/api/HandleKeepRecord/RecordInfo";
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