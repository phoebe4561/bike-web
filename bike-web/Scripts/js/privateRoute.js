$(document).ready(function () {
    let thearticleId;
    let btnfav = document.querySelectorAll(".btnFav");
    let ifav = document.querySelectorAll(".iFav");

    for (let item of btnfav) {
        item.addEventListener('click', function (e) {
            e.preventDefault();
            
           
           /* var _this = $(this);*/

            thearticleId = e.target.value;
            $(this).attr("id", "targetfav");
            
            
            //alert(thearticleId);
            addToMyFavBtn(thearticleId);
        })
    };

    for (let item of ifav) {
        item.addEventListener('click', function (e) {
            
            e.preventDefault();
            $(this).attr("id", "targetfav");

           
            
            

            /* var _this = $(this);*/

            thearticleId = e.target.parentNode.value;
            
           /* alert(thearticleId);*/
            addToMyFavI(thearticleId);
            e.stopImmediatePropagation();
        })
    };




    function addToMyFavBtn(myarticleid)
    {
        let addToMyFavData = { articleid: myarticleid };
        $.ajax({
            type: 'post',
            contentType: 'application/json;charset=utf-8',
            url: '/PrivateRoute/addToMyFav',
            data: JSON.stringify(addToMyFavData),
            success: function (result) {
                if (result.toString() == "add") {
                    console.log($('#targetfav'));
                    //可以增添
                    $('#targetfav').removeClass("btn-outline-danger");
                    $('#targetfav').addClass("btn-danger");
                    //好像沒辦法處理
                    //$('#targetfav').find("i").removeClass("bi-heart");
                    //$('#targetfav').find("i").addClass("bi-heart-fill");

                    $('#targetfav').removeAttr('id');

                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: '已添加入我的最愛',
                        showConfirmButton: false,
                        timer: 1500
                    })
                  /*  alert("已添加入我的最愛按鈕")*/
                    
                    ;
                }
                else if (result.toString() == "delete") {
                    console.log($('#targetfav'));
                    //可以刪除
                    $('#targetfav').removeClass("btn-danger");
                    $('#targetfav').addClass("btn-outline-danger");
                    //可以刪掉
                    //$('#targetfav').find("i").removeClass("bi-heart-fill");
                    //$('#targetfav').find("i").addClass("bi-heart");

                    $('#targetfav').removeAttr('id');
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: '已從我的最愛中刪除',
                        showConfirmButton: false,
                        timer: 1500
                    })

                } else {
                    $('#targetfav').removeAttr('id');
                  /*  alert("失敗");*/
                }


            },
            error: function (result) {
                $('#targetfav').removeAttr('id');
              /*  alert("請再試一次")*/
            }

        })
       
    }

    function addToMyFavI(myarticleid) {
        let addToMyFavData = { articleid: myarticleid };
        $.ajax({
            type: 'post',
            contentType: 'application/json;charset=utf-8',
            url: '/PrivateRoute/addToMyFav',
            data: JSON.stringify(addToMyFavData),
            success: function (result) {
                if (result.toString() == "add") {
                    console.log($('#targetfav'));
                    //好像沒加到
                    $('#targetfav').closest('button').removeClass("btn-outline-danger");
                    $('#targetfav').closest('button').addClass("btn-danger");
                    //加到裡面
                    //$('#targetfav').removeClass("bi-heart");
                    //$('#targetfav').addClass("bi-heart-fill");

                    $('#targetfav').removeAttr('id');
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: '已添加入我的最愛',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    console.log($('#targetfav'));
                        ;
                }
                else if (result.toString() == "delete") {
                    console.log($('#targetfav'));
                      //不知道
                    $('#targetfav').closest('button').removeClass("btn-danger");
                    $('#targetfav').closest('button').addClass("btn-outline-danger");
                    //可以刪除 //增加
                    //$('#targetfav').removeClass("bi-heart-fill");
                    //$('#targetfav').addClass("bi-heart");

                    $('#targetfav').removeAttr('id');
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: '已從我的最愛中刪除',
                        showConfirmButton: false,
                        timer: 1500
                    })

                } else {
                    $('#targetfav').removeAttr('id');
                  /*  alert("失敗");*/
                }


            },
            error: function (result) {
                $('#targetfav').removeAttr('id');
               /* alert("請再試一次")*/
            }

        })

    }



})




