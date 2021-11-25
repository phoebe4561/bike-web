

$(document).ready(function () {
    let thearticleId;
    let btnRemoveFav = document.getElementsByClassName("btnRemoveFav");
    let userFav = document.getElementsByClassName("userFav");
    let theRoute = document.getElementsByClassName("theRoute");

 
    for (let i = 0, len = btnRemoveFav.length; i < len; i++ ) {
        btnRemoveFav[i].addEventListener('click', function (e) {
            e.preventDefault();
            Swal.fire({
                title: '移除我的最愛',
                text: "確定要移除這條路線嗎?",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#F4D19B',
                cancelButtonText: '我再想想',
                confirmButtonText: '確認移除'
                }).then((result) => {
                if (result.isConfirmed) {
                    Swal.fire(
                        'Deleted!',
                        'Your file has been deleted.',
                        'success'
                    )
                    thearticleId = userFav[i].textContent;
                    theRoute[i].setAttribute("hidden", "true");
                    addToMyFavBtn(thearticleId);
                }
            })           
           
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

                if (result.toString() == "delete") {
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
                    alert("***我的最愛刪除失敗");
                }


            },
            error: function (result) {
                //$('#targetfav').removeAttr('id');
                alert("請再試一次")
            }

        })

    }   


})

