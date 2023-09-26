const login = async function(authstring) {
    var result = {
        "Status": -1,
        "Msg": "",
        "Token": "",
    }
    var host = "http://localhost:34172";
    await axios({
        method: 'post',
        timeout: 3000,
        url: host + '/api/login',
        async: false,
        data: JSON.stringify({
            "UserIDCode": authstring
        }),
        headers: {
            'Content-Type': 'application/json'
        },
        // params: { "UserIDCode": "8b5bc3f54c17498d9621f77d1107f71d" },
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

const getMQTTInfo = async function(token) {
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