function getGridData(roomId) {
    var _gridData;
    $.ajax({
        url: '/api/HandleVoiceCfgData/GetInfo?RoomId=' + roomId,
        async: false,
        data: {},
        dataType: "JSON",
        success: function (resultdata) {
            console.log(resultdata)
            if (resultdata.length > 0) {
                _gridData = resultdata;
            }
        }
    });
    return _gridData;
}

///图表类
function getChartGridData(pageType) {
    var _gridData;
    $.ajax({
        url: '/GetChartMgmtDataHandler.ashx?pageType=' + pageType,
        async: false,
        data: {},
        dataType: "JSON",
        success: function (resultdata) {
            if (resultdata.length > 0) {
                _gridData = resultdata;
            }
        }
    });
    return _gridData;
} 