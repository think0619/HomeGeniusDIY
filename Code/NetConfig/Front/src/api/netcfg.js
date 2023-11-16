import axios from 'axios'
import { serverUrl } from '@/api/config'


export async function getConfigList() {
    var result = {
        "Status": 0,
        "Total": 0,
        "Msg": "Fail.",
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

//create
export async function addConfigInfo(updateobj) {
    var result = {
        "Status": 0,
        "Msg": "Fail.",
    }
    await axios({
        method: 'post',
        timeout: 3000,
        url: serverUrl + '/api/netcfg/add',
        async: false,
        headers: { 'Content-Type': 'application/json', },
        params: {},
        data: JSON.stringify(updateobj),
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

//update
export async function updateConfigInfo(updateobj) {
    var result = {
        "Status": 0,
        "Msg": "Fail.",
    }
    await axios({
        method: 'post',
        timeout: 3000,
        url: serverUrl + '/api/netcfg/update',
        async: false,
        headers: { 'Content-Type': 'application/json', },
        params: {},
        data: JSON.stringify(updateobj),
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

//delete
export async function deleteConfigInfos(IdArray) {
    var result = {
        "Status": 0,
        "Msg": "Fail.",
    }
    await axios({
        method: 'post',
        timeout: 3000,
        url: serverUrl + '/api/netcfg/bulkdel',
        async: false,
        headers: { 'Content-Type': 'application/json', },
        params: {},
        data: IdArray,
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