    //const serverUrl = 'http://localhost:34172';
    const serverUrl = 'http://192.168.2.100:47081';

    export {
        serverUrl
    }

    import axios from 'axios'
    export function login(username, password) {
        var url = serverUrl + "/api/login/login";
        return axios({
            url: url,
            method: "post",
            headers: {
                'Content-Type': 'application/json'
            },
            data: JSON.stringify({
                "Username": username,
                "Password": password,
            })
        })
    }