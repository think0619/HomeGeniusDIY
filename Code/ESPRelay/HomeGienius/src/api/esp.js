//  esp.js
import axios from 'axios'



export function addkeeprecord(_params) {
    var url = host + "/api/HandleKeepRecord/Add";
    return axios({
        url: url,
        method: "post",
        data: _params
    })
}