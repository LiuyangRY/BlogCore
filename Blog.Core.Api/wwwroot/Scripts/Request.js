$(document).ready(function() {
    $("#jsonp").click(function(){
        $.getJSON("http://localhost:5000/Login/GetJsonp?callBack=?", function (data) {
            $("#data-jsonp").html("数据: " + data.value);
        });
    });

    $("#cors").click(function () {
        $.get("http://localhost:5000/Login/GetToken", function (data, status) {
            console.log(data);
            $("#status-cors").html("状态: " + status);
            $("#data-cors").html("数据: " + data? JSON.stringify(data):"失败");
        });
    });

    $("#cors-post").click(function () {
        let postdata = {
            "bID": 10,
            "bsubmitter": "222",
            "btitle": "33333",
            "bcategory": "4444",
            "bcontent": "5555",
            "btraffic": 0,
            "bcommentNum": 0,
            "bUpdateTime": "2018-11-08T02:36:26.557Z",
            "bCreateTime": "2018-11-08T02:36:26.557Z",
            "bRemark": "string"
        };

        $.ajax({
            type: 'post',
            url: 'http://localhost:5000/Login/PostToken',
            contentType: 'application/json',
            data: JSON.stringify(postdata),
            success: function (data, status) {
                console.log(data);
                $("#status-cors-post").html("状态: " + status);
                $("#data-cors-post").html("数据: " + JSON.stringify(data));
            }
        });
    });
});