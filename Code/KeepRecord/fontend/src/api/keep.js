//  index.js
import axios from 'axios'

export function addkeeprecord(_params) {
    var host = 'http://localhost:34172';
    var url = host + "/api/HandleKeepRecord/Add";
    return axios({
        url: url,
        method: "post",
        data: _params
    })
}

export function querykeeprecord(_params) {
    var host = 'http://localhost:34172';
    var url = host + "/api/HandleKeepRecord/Query";
    return axios({
        url: url,
        method: "post",
        data: _params
    })
}