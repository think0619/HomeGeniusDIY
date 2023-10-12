var host = 'http://localhost:34172';

import axios from 'axios'


export async function getMQTTInfo(token) {
    var result = {
        "username": "",
        "password": "",
        "wsurl": "",
    }
    var host = "http://localhost:34172";
    await axios({
        method: 'post',
        timeout: 3000,
        url: host + '/api/apiconfig/get',
        async: false,
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token,
        },
        params: { "flag": "mqtt" },
        dataType: "JSON"
    }).then(function(response) {
        if (response != null && response.data != null) {
            var data = response.data;
            if (data) {
                result = data;
            }
        }
    }).catch(function(error) {
        console.log(error);
    });
    return result;
}