

$(document).ready(function () {
    let thearticleId;
    let btnfav2 = document.querySelectorAll(".btnFav2");
    let ifav2 = document.querySelectorAll(".iFav2");

    $(".btnLoginYet").click(function () {
        swal.fire({
            position: 'center',
            text: "請先登入會員!",
            icon: "info",
            timer: 2000,
            width: 300,
            showConfirmButton: false,
        })
    });
    for (let item of btnfav2) {
        item.addEventListener('click', function (e) {
            e.preventDefault();

            /* var _this = $(this);*/

            thearticleId = e.target.value;
            $(this).attr("id", "targetfav");


            //alert(thearticleId);
            addToMyFavBtn(thearticleId);
        })
    };

    for (let item of ifav2) {
        item.addEventListener('click', function (e) {

            e.preventDefault();
            $(this).attr("id", "targetfav");

            /* var _this = $(this);*/

            thearticleId = e.target.parentNode.value;

            //alert(thearticleId);
            addToMyFavI(thearticleId);
            e.stopImmediatePropagation();
        })
    };




    function addToMyFavBtn(myarticleid) {
        let addToMyFavData = { articleid: myarticleid };
        $.ajax({
            type: 'post',
            contentType: 'application/json;charset=utf-8',
            url: '/Home/UpdateFavorite',
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
                    swal.fire({
                        position: 'center',
                        text: "已添加入我的最愛按鈕",
                        icon: "success",
                        timer: 2000,
                        width: 300,
                        showConfirmButton: false,
                    })
                    //    alert("已添加入我的最愛按鈕");
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
                    swal.fire({
                        position: 'center',
                        text: "已從我的最愛中刪除按鈕",
                        icon: "warning",
                        timer: 2000,
                        width: 300,
                        showConfirmButton: false,
                    })
                    //alert("已從我的最愛中刪除按鈕");

                } else {
                    $('#targetfav').removeAttr('id');
                    alert("***我的最愛加入失敗");
                }


            },
            error: function (result) {
                $('#targetfav').removeAttr('id');
                alert("請再試一次")
            }

        })

    }

    function addToMyFavI(myarticleid) {
        let addToMyFavData = { articleid: myarticleid };
        $.ajax({
            type: 'post',
            contentType: 'application/json;charset=utf-8',
            url: '/Home/UpdateFavorite',
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
                    //alert("已添加入我的最愛符號");
                    console.log($('#targetfav'));
                    swal.fire({
                        position: 'center',
                        text: "已添加入我的最愛按鈕",
                        icon: "success",
                        timer: 2000,
                        width: 300,
                        showConfirmButton: false,
                    })
                    e.stopImmediatePropagation();
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
                    //alert("已從我的最愛中刪除符號");
                    swal.fire({
                        position: 'center',
                        text: "已從我的最愛中刪除按鈕",
                        icon: "warning",
                        timer: 2000,
                        width: 300,
                        showConfirmButton: false,
                    })
                    e.stopImmediatePropagation();
                } else {
                    $('#targetfav').removeAttr('id');
                    alert("失敗");
                }


            },
            error: function (result) {
                $('#targetfav').removeAttr('id');
                alert("請再試一次")
            }

        })

    }



})






//$(document).ready(function () {
//    let thearticleId;
//    let btnfav = document.querySelectorAll(".btnFav");
//    let ifav = document.querySelectorAll(".iFav");
    
//    $(".btnLoginYet").click(function () {
//        swal.fire({
//            position: 'center',
//            text: "請先登入會員!",
//            icon: "info",
//            timer: 2000,
//            width: 300,
//            showConfirmButton: false,
//        })
//    });
//    for (let item of btnfav) {
//        item.addEventListener('click', function (e) {
//            e.preventDefault();

//            /* var _this = $(this);*/

//            thearticleId = e.target.value; 
//            $(this).attr("id", "targetfav");


//            //alert(thearticleId);
//            addToMyFavBtn(thearticleId);
//        })
//    };

//    for (let item of ifav) {
//        item.addEventListener('click', function (e) {

//            e.preventDefault();
//            $(this).attr("id", "targetfav");

//            /* var _this = $(this);*/

//            thearticleId = e.target.parentNode.value;

//            //alert(thearticleId);
//            //addToMyFavI(thearticleId);
//            e.stopImmediatePropagation();
//        })
//    };




//    function addToMyFavBtn(myarticleid) {
//        let addToMyFavData = { articleid: myarticleid };
//        $.ajax({
//            type: 'post',
//            contentType: 'application/json;charset=utf-8',
//            url: '/Home/UpdateFavorite',
//            data: JSON.stringify(addToMyFavData),
//            success: function (result) {
//                if (result.toString() == "add") {
//                    console.log($('#targetfav'));
//                    //可以增添
//                    $('#targetfav').removeClass("btn-outline-danger");
//                    $('#targetfav').addClass("btn-danger");
//                    //好像沒辦法處理
//                    //$('#targetfav').find("i").removeClass("bi-heart");
//                    //$('#targetfav').find("i").addClass("bi-heart-fill");

//                    $('#targetfav').removeAttr('id');
//                    swal.fire({
//                        position: 'center',
//                        text: "已添加入我的最愛按鈕",
//                        icon: "success",
//                        timer: 2000,
//                        width: 300,
//                        showConfirmButton: false,
//                    })
//                //    alert("已添加入我的最愛按鈕");
//                }
//                else if (result.toString() == "delete") {
//                    console.log($('#targetfav'));
//                    //可以刪除
//                    $('#targetfav').removeClass("btn-danger");
//                    $('#targetfav').addClass("btn-outline-danger");
//                    //可以刪掉
//                    //$('#targetfav').find("i").removeClass("bi-heart-fill");
//                    //$('#targetfav').find("i").addClass("bi-heart");

//                    $('#targetfav').removeAttr('id');
//                    swal.fire({
//                        position: 'center',
//                        text: "已從我的最愛中刪除按鈕",
//                        icon: "warning",
//                        timer: 2000,
//                        width: 300,
//                        showConfirmButton: false,
//                    })
//                    //alert("已從我的最愛中刪除按鈕");

//                } else {
//                    $('#targetfav').removeAttr('id');
//                    alert("***我的最愛加入失敗");
//                }


//            },
//            error: function (result) {
//                $('#targetfav').removeAttr('id');
//                alert("請再試一次")
//            }

//        })

//    }

//    function addToMyFavI(myarticleid) {
//        let addToMyFavData = { articleid: myarticleid };
//        $.ajax({
//            type: 'post',
//            contentType: 'application/json;charset=utf-8',
//            url: '/Home/UpdateFavorite',
//            data: JSON.stringify(addToMyFavData),
//            success: function (result) {
//                if (result.toString() == "add") {
//                    console.log($('#targetfav'));
//                    //好像沒加到
//                    $('#targetfav').closest('button').removeClass("btn-outline-danger");
//                    $('#targetfav').closest('button').addClass("btn-danger");
//                    //加到裡面
//                    //$('#targetfav').removeClass("bi-heart");
//                    //$('#targetfav').addClass("bi-heart-fill");

//                    $('#targetfav').removeAttr('id');
//                    //alert("已添加入我的最愛符號");
//                    //console.log($('#targetfav'));
//                    swal.fire({
//                        position: 'center',
//                        text: "已添加入我的最愛按鈕",
//                        icon: "success",
//                        timer: 2000,
//                        width: 300,
//                        showConfirmButton: false,
//                    })
                    
//                }
//                else if (result.toString() == "delete") {
//                    console.log($('#targetfav'));
//                    //不知道
//                    $('#targetfav').closest('button').removeClass("btn-danger");
//                    $('#targetfav').closest('button').addClass("btn-outline-danger");
//                    //可以刪除 //增加
//                    //$('#targetfav').removeClass("bi-heart-fill");
//                    //$('#targetfav').addClass("bi-heart");

//                    $('#targetfav').removeAttr('id');
//                    //alert("已從我的最愛中刪除符號");
//                    swal.fire({
//                        position: 'center',
//                        text: "已從我的最愛中刪除按鈕",
//                        icon: "warning",
//                        timer: 2000,
//                        width: 300,
//                        showConfirmButton: false,
//                    })
//                } else {
//                    $('#targetfav').removeAttr('id');
//                    alert("失敗");
//                }


//            },
//            error: function (result) {
//                $('#targetfav').removeAttr('id');
//                alert("請再試一次")
//            }

//        })

//    }



//})

