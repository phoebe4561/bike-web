fetch('https://opendata.cwb.gov.tw/api/v1/rest/datastore/F-D0047-091?Authorization=CWB-6DB1B8BA-C3F5-49F7-8443-999865A34532&locationName=%E9%AB%98%E9%9B%84%E5%B8%82')
    .then((res) => {
        return res.json();
    })
    .then((data) => {
        weather_forcast = data.records.locations[0].location[0].weatherElement;
        get_weather_data(weather_forcast);
        //console.log(weather_forcast);
    });

let weather_tool = document.getElementById('weather_tool');

function get_weather_data(data) {
    weather_tool.innerHTML = '';
    let raining_percent = data[0].time;
    let themometer_high = data[12].time;
    let themometer_low = data[8].time;
    let weather_total_des = data[10].time;
    //console.log(weather_total_des);
    weather_total_des.forEach((item) => {
        weather_block = document.createElement('div');
        start_time = document.createElement('h3');
        start_time.classList.add('weather_title');
        des = document.createElement('div');
        let regex = /\D*陰+\D*雲+/g;
        let regex2 = /\D*晴+\D*雲+/g;
        let regex3 = /\D*雲+/g;
        let regex4 = /\D*陰+\D*/g;
        if (item.startTime.split(' ')[1] === '18:00:00') {
            start_time.innerHTML = item.startTime.split(' ')[0] + ' 晚上';
        } else if (item.startTime.split(' ')[1] === '06:00:00') {
            start_time.innerHTML = item.startTime.split(' ')[0] + ' 白天';
        } else {
            start_time.innerHTML = item.startTime.split(' ')[0] + ' 白天';
        }
        if (item.elementValue[0].value.split('。')[0].includes('雨')) {
            des.innerHTML = '<img src="../images/weather/rain.svg" alt="雨">';
        } else if (item.elementValue[0].value.split('。')[0].match(regex)) {
            des.innerHTML = '<img src="../images/weather/cloudy.svg" alt="陰天">';
        } else if (item.elementValue[0].value.split('。')[0].match(regex2)) {
            des.innerHTML = '<img src="../images/weather/sun.svg" alt="晴">';
        } else if (item.elementValue[0].value.split('。')[0].match(regex3)) {
            des.innerHTML = '<img src="../images/weather/cloudy.svg" alt="陰天">';
        } else if (item.elementValue[0].value.split('。')[0].match(regex4)) {
            des.innerHTML = '<img src="../images/weather/cloudy_more.svg" alt="陰天">';
        } else if (item.startTime.split(' ')[1] !== '18:00:00') {
            des.innerHTML = '<img src="../images/weather/night.svg" alt="夜晚">';
        } else {
            des.innerHTML = '<img src="../images/weather/night.svg" alt="夜晚">';
        }
        des.classList.add('weather_icon');
        //加入hover
        let weather_description = document.createElement('p');
        weather_description.classList.add('weather_description');
        weather_description.innerHTML = `${item.elementValue[0].value.split('。')[3]}</br>
      ${item.elementValue[0].value.split('。')[4]}</br>
      ${item.elementValue[0].value.split('。')[5]}`;
        des.appendChild(weather_description);

        weather_block.appendChild(start_time);

        let des_word = document.createElement('div');
        des_word.innerHTML = item.elementValue[0].value.split('。')[0];
        des_word.classList.add('des_word');
        des.appendChild(des_word);

        weather_block.appendChild(des);

        weather_block.classList.add('weather_block');
        weather_tool.appendChild(weather_block);
    });

    let weather_block_div = document.getElementsByClassName('weather_block');
    for (let i = 0; i < weather_block_div.length; i++) {
        let val = raining_percent[i].elementValue[0].value;
        let rain_block = document.createElement('div');
        rain_block.classList.add('raning_block');
        rain_block.innerHTML = `<div class='raining'><i class="fas fa-umbrella"></i></div> <div class="weather_word">${val !== ' ' ? val + '%' : '尚無資料'}</div>`;
        weather_block_div[i].appendChild(rain_block);

        let high_temp = themometer_high[i].elementValue[0].value;
        let low_temp = themometer_low[i].elementValue[0].value;
        let temp_block = document.createElement('div');
        temp_block.classList.add('raning_block');
        temp_block.classList.add('raining_move');
        temp_block.innerHTML = `<div class='raining'><i class="fas fa-thermometer-half"></i></div> <div class="weather_word">${low_temp + '°' + ' ~ ' + high_temp + '°C'}</div>`;
        weather_block_div[i].appendChild(temp_block);
    }
}
