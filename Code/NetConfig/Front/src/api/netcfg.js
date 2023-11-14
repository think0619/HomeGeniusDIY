import axios from 'axios'
import { serverUrl } from '@/api/config'


export async function getConfigList() {
    var result = {
        "Status": 0,
        "Total": 0,
        "Msg": "Success",
        "Data": null
    }
    await axios({
        method: 'post',
        timeout: 3000,
        url: serverUrl + '/api/netcfg/query',
        async: false,
        headers: { 'Content-Type': 'application/json', },
        params: {},
        dataType: "JSON"
    }).then(function(response) {
        if (response != null && response.data != null) {
            if (response.data) {
                result = response.data;
            }
        }
    }).catch(function(error) {
        console.log(error);
    });
    return result;
}