 const serverUrl = 'https://hw.hellolinux.cn:8888';
 // const serverUrl = 'http://localhost:34172';

 export {
     serverUrl
 }

 import axios from 'axios'
 export function login(authcode) {
     var url = serverUrl + "/api/login";
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