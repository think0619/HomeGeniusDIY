import axios from 'axios'
import { serverUrl } from '@/api/config'


export async function getWeather(token) {
    var result = {
        "username": "",
        "password": "",
        "wsurl": "",
    }
    await axios({
        method: 'post',
        timeout: 3000,
        url: serverUrl + '/api/weather/getall/filter',
        async: false,
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token,
        },
        params: {},
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