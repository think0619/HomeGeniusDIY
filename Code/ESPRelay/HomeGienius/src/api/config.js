var serverUrl = 'http://localhost:34172';

var host = 'http://localhost:34172';

import axios from 'axios'


export function login(authcode) {
    var url = host + "/api/login";
    return axios({
        url: url,
        method: "post",
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify({
            "UserIDCode": authcode
        })
    })
}