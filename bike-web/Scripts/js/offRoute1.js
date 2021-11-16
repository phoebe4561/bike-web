
//鐵馬路線內文

let inputData = document.getElementsByClassName('inputData');
let outputData = document.getElementsByClassName('outputData');
for (let i = 0, len = inputData.length; i < len; i++) {
    outputData[i].innerHTML = inputData[i].textContent;   
}



// 星星評分
let spans = document.querySelectorAll("#commentStar span");
for (let span of spans) {
    span.addEventListener("mouseover", mouseOver);
    span.addEventListener("mouseout", mouseOut);
    span.addEventListener("click", Click);
    span.addEventListener("dblclick", dblClick);
}
let starCount = document.getElementById('starCount');
let starNum = document.getElementById('starNum');
let commentTxt = document.getElementById("btnCommentTxt");
let userComment = document.getElementById("userComment");

var startruehere;
starCount.innerHTML = '目前尚未評分';
starNum.innerHTML = 0;

function mouseOver() {
    let starhere = this.id.charAt(4);
    for (let j = 0; j < 5; j++) {
        spans[j].style.color = "#000";
    }
    for (let i = 0; i < starhere; i++) {
        spans[i].style.color = "#F4D19B";
    }
    starCount.innerHTML = '確定評分為' + starhere + '星級嗎?';

}

function mouseOut() {


    for (let j = 0; j < 5; j++) {
        spans[j].style.color = "#000";
    }
    for (let i = 0; i < startruehere; i++) {
        spans[i].style.color = "#F4D19B";
    }
    if (startruehere > 0 && startruehere < 6) {
        starCount.innerHTML = startruehere + "星級";
        starNum.value = startruehere;
    }
    else {
        starCount.innerHTML = '目前尚未評分';
        starNum.value = 0;
    }

}


function Click() {
    startruehere = this.id.charAt(4);


    for (let j = 0; j < 5; j++) {
        spans[j].style.color = "#000";
    }
    for (let i = 0; i < startruehere; i++) {
        spans[i].style.color = "#F4D19B";
    }

    starCount.innerHTML = startruehere + "星級";
    starNum.value = startruehere;
    console.log("星星click");

    
    if (document.getElementById("userComment").value.length > -1) { commentTxt.innerHTML = "評論最少五個字" }
    if (document.getElementById("userComment").value.length > 4) {
        $("#commentBtn").prop("disabled", false); commentTxt.innerHTML = "";
    }

}

function dblClick() {
    for (let j = 0; j < 5; j++) {
        spans[j].style.color = "#F4D19B";
    }
    startruehere = 0;
    starCount.innerHTML = '目前尚未評分';
    starNum.value = 0;
}

//---------------------------------------星星評分 END





//留言評論

$(function () {
    //0.監聽內容輸入
    $("body").delegate("#userComment", "propertychange input", function () {
        let starValue = document.getElementById('starNum').value;
        //判斷是否輸入內容        
        if ($(this).val().length > -1) {
            $("#commentBtn").prop("disabled", true);
            commentTxt.innerHTML = "評論最少五個字"
        }
        if ($(this).val().length > 5) {
            $("#commentBtn").prop("disabled", false);//按鈕可以用
            commentTxt.innerHTML = ""

            if (starValue == 0) {
                $("#commentBtn").prop("disabled", true);
                commentTxt.innerHTML = "星星尚未評分";
            }
        }

        //if (starValue > 0) { commentTxt.innerHTML = ""; console.log("星星數為0"); }

    })

})

console.log('what');

//鐵馬內文

//    console.log('weeeeeeeeeeeeee');
if (document.getElementById("userComment").value.length > -1) { commentTxt.innerHTML = "評論最少五個字" }
if (document.getElementById("userComment").value.length > 4) {
    $("#commentBtn").prop("disabled", false); commentTxt.innerHTML = "";
}