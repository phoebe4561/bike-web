$(function () {
    //0.監聽內容輸入
    $("body").delegate(".comment", "propertychange input", function () {
        //判斷是否輸入內容
        if ($(this).val().length > 0) {
            //按鈕可以用
            $(".send").prop("disabled", false);

        } else {
            //按鈕不可用
            $(".send").prop("disabled", true);
        }

    })
    //取文章ID用
    let articleId = $(".btnFav").val();




   
    



    //這裡輸入文章
    $("#btnSave").click(function (e) {
        
        var articleid = e.target.value;
        console.log(articleid);
        var starnum = $("#starNum").val();
        console.log(starnum );
        var comment = $("#txtComment").val();
        console.log(comment);
        var data = JSON.stringify({ 'articleid': articleid, 'starnum': starnum, 'comment': comment });


        //留言板內容清空
        $("#starNum").attr("vaule", "");
        $("#txtComment").val("");
        starCount.innerHTML = '目前尚未評分';
        for (let j = 0; j < 5; j++) {
            spans[j].style.color = "#000";
        }
        startruehere = 0;



        //
        $.ajax({
            type: "post",
            url: 'saveComment',
            data: data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (vm) {
               /* var data = JSON.parse(vm)*/
               /* var cap = vm.responseJSON*/
                console.log(vm.留言資訊[0].id)
                console.log(vm.留言資訊[0].comment)

                //先清空留言板
                $("#messagebody1 div").remove();

                //清空頁數
                $("#commentPage ul li").remove();

                //清空統計的星星數


                    $("#starNumber span").remove();
         

                //開始循環留言板
                $.each(vm.留言資訊, function (i, item) {

                    /*var date = new Date(parseInt(item.datetime.substr(6)));*/
                    
                    console.log(item);
                    var starrow = "";
                    if (item.user_give_star_num == 1) {
                        starrow = '<p class="infoStar">★</p>';
                    }
                    else if (item.user_give_star_num == 2) {
                        starrow = '<p class="infoStar">★★</p>';
                    }
                    else if (item.user_give_star_num == 3) {
                        starrow = '<p class="infoStar">★★★</p>';
                    }
                    else if (item.user_give_star_num == 4) {
                        starrow = '<p class="infoStar">★★★★</p>';
                    }
                    else if (item.user_give_star_num == 5) {
                        starrow = '<p class="infoStar">★★★★★</p>';
                    }
                    else {
                        starrow = '<p class="infoStar"></p>';
                    }


                    var rows = "<div class ='infomessage'>" + "<p class='infoName'>" + item.comment_user_name + "</p>" + starrow + "<p class='infoText'>" + item.comment + "</p>" + "<p class='infoTime'>" + ConvertJsonDateString(item.datetime) + "</p>" + "</div>";
                    console.log(rows);

                    $("#messagebody1").append(rows);

                  



                })

                //開始循環頁數
                for (i = 1; i <= vm.pagenum; i++) {
                    if (i == 1) {
                        var rows = '<li class="page-item disabled">' + '<a class="page-link active" href="javascript:;">' +

                            i +
                            '</a >' + '</li >'

                    } else {
                        var rows = '<li class="page-item">' + '<a class="page-link" href="javascript:;">' + i + '</a>' + '</li>'

                    }

                    $("#commentPage ul").append(rows);

                }

                //加個星星

                var rows = '<span class="ms-2">' + vm.star + '</span>'

                $("#starNumber").append(rows);

               //插入事件註冊
                for (var i = 0; i < $(".page-item").length; i++) {
                    $(".page-item")[i].addEventListener('click', test);
                }

                   
               
            },
            error: function () {
                alert("error");


            }
           
        });
        return false;

    })

    


    var test = function (e) {
        e.preventDefault();
        let obj = event.target;

        let page = parseInt(obj.textContent);
        console.log(page);


        console.log(articleId);
        var data = JSON.stringify({ 'articleId': articleId, 'page': page });

        //AJAX
        $.ajax({
            type: "post",
            url: 'changePage',
            data: data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (vm) {

                //先清空留言板
                $("#messagebody1 div").remove();

                //清空頁數
                $("#commentPage ul li").remove();

                $.each(vm.留言資訊, function (i, item) {

                    var date = new Date(parseInt(item.datetime.substr(6)));
                    console.log(item);
                    var starrow = "";
                    if (item.user_give_star_num == 1) {
                        starrow = '<p class="infoStar">★</p>';
                    }
                    else if (item.user_give_star_num == 2) {
                        starrow = '<p class="infoStar">★★</p>';
                    }
                    else if (item.user_give_star_num == 3) {
                        starrow = '<p class="infoStar">★★★</p>';
                    }
                    else if (item.user_give_star_num == 4) {
                        starrow = '<p class="infoStar">★★★★</p>';
                    }
                    else if (item.user_give_star_num == 5) {
                        starrow = '<p class="infoStar">★★★★★</p>';
                    }
                    else {
                        starrow = '<p class="infoStar"></p>';
                    }


                    var rows = "<div class ='infomessage'>" + "<p class='infoName'>" + item.comment_user_name + "</p>" + starrow + "<p class='infoText'>" + item.comment + "</p>" + "<p class='infoTime'>" + ConvertJsonDateString(item.datetime) + "</p>" + "</div>";
                    console.log(rows);

                    $("#messagebody1").append(rows);



                })
                for (i = 1; i <= vm.pagenum; i++) {
                    if (i == vm.nowpage) {
                        var rows = '<li class="page-item disabled">' + '<a class="page-link active" href="javascript:;">' +

                            i +
                            '</a >' + '</li >'

                    } else {
                        var rows = '<li class="page-item">' + '<a class="page-link" href="javascript:;">' + i + '</a>' + '</li>'

                    }

                    $("#commentPage ul").append(rows);

                }
                //加這個
                for (var i = 0; i < $(".page-item").length; i++) {
                    $(".page-item")[i].addEventListener('click', test);
                    console.log("讚");
                }
            },
            error: function () {
                alert("error");
            }
        });
        return false;
    }

        //這裡結束
    for (var i = 0; i < $(".page-item").length; i++) {
        $(".page-item")[i].addEventListener('click', test);
        console.log("讚");
    }



    



})

function ConvertJsonDateString(jsonDate) {
    var shortDate = null;
    if (jsonDate) {
        var regex = /-?\d+/;
        var matches = regex.exec(jsonDate);
        console.log(matches);
        var dt = new Date(parseInt(matches[0]));
        var month = dt.getMonth() + 1;
        var monthString = month > 9 ? month : '0' + month;
        var day = dt.getDate();
        

        var dayString = day > 9 ? day : '0' + day;
        var year = dt.getFullYear();
        var hour = dt.getHours();

        var amorpm;
        if (hour  >= 12) {
            amorpm = '下午';
            hour = padStart((hour - 12),2,'0');


            
        }
        else {
            amorpm = '上午';
            hour = padStart(hour , 2, '0');
        }



        var minute = dt.getMinutes();
        var second = dt.getSeconds();
        shortDate = year + '/ ' + monthString + '/' + dayString + " " + amorpm +" " + hour + ':' + minute + ':' + second;
    }
    return shortDate;
};

function padStart(string, targetLength, padString = ' ') {
    return (Array(targetLength).join(padString) + string)
        .slice(-targetLength)
}
                    




