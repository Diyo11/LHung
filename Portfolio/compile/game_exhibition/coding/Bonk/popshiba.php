<!doctype html>
<html lang="zh">
<head>
    <meta charset="utf-8" />
    <title>BONK柴犬</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />

    <style type="text/css">
        body{
            align-content: center;
            text-align: center;
            user-select: none;
            background: #CEE0DC;
        }
        *{
            font-family: 'SweiGothic','標楷體','Times New Roman';
        }
        .name_in{
            background: #B9CFD4;
            color: white;
            border: 3px dotted #FFFFFF;
            border-radius: 20px;
            padding-top: 10px;
            padding-bottom: 10px;
            font-size: 20px;
            z-index: 2;

        }
        input{
            text-align: center;
            max-width: 200px;
            min-width: 100px;
            
        }
        @font-face{
            font-family: 'SweiGothic';
            src: local("SweiGothicCJKjp-Bold"),
                 url('SweiGothicCJKjp-Bold.woff') format("woff"),
                 url('SweiGothicCJKjp-Bold.ttf') format("truetype"),
                 url('SweiGothicCJKjp-Bold.eot') format("embedded-opentype");
        }
        input::placeholder {
            color: #B48291;
            opacity: 50%;
        }
        .img_sit{
            text-align: center;
            
            width: 100%;
            background: #CEE0DC;
            z-index: 0;
        }
        
        .bonk_{

        }

        .ranking{
            position: fixed;
            float: left;
            bottom: 0px;
            width: 100%;
            margin-left: -10px;
            margin-right: -5px;
            background: #000000;
            color: white;
            text-align: center;
            align-content: center;
        }
        .ranking p{
            text-align: center;
            display: inline-block;
            width: 30%;
            font-size: 20px;
            
        }
        
        
        h1{
            font-size: 50px;
        }
        .bonk_:active{
            animation: img_change 0.1s ease 0s normal 1;
        }
        @keyframes img_change{
            from{
                content: url(1.png);
            }
            to{
                content: url(2.png);
            }
        }
        
    </style>
    
</head>
<body >
    
    <h1>BONK - <font class="time_count">10</font> - BONK</h1>

    <input type="text" name="name" class="name_in" placeholder="輸入暱稱" autocomplete="off" value="" maxlength="5">
    <h2 class="score">0</h2>
    <div class="img_sit" onclick="click_bonk()" to>
        <img class="bonk_" id="bonk" src="start.png" onclick="start_button()">
    </div>

    <div class="ranking">
        <p class="first">123:123</p>
        <p class="second">123:123</p>
        <p class="third">123:123</p>
    </div>
    
    

    <script src="jquery.js"></script>
    <script type="text/javascript">
        document.oncontextmenu = new Function("return false");
        oncontextmenu="return false;";

        var ww=window.innerWidth;
        var hh=window.innerHeight;
        //alert("Width:"+ww+" Height:"+hh);
        var start_count=0;
        var count = 10;

        $(".img_sit").css("height",hh*0.7);
        var mmg=document.getElementById('bonk');
        var img=["start.png","yi_bonk1.png","yi_bonk2.png","1.png","2.png"];
        var score=0;
        $(".score").html(score);
        mmg.src=img[0];
        var img_count=0;
        var timerId,timerID2;
        
        function start_button(){
            if(start_count==0){
                start_count=1;
                img_count=3;
                score=0;
                $(".score").html(score);
                mmg.src=img[img_count];
                timerId = setInterval(timer, 1000);
                count = 10;
                $(".time_count").html(count);
                //$(".bonk_:active").css("animation","img_change 0.1s ease 0s normal 1");
                RWD_img();

            }else if(start_count==1){

                //$(".bonk_:active").css("animation","img_change 0.1s ease 0s normal 1");
                score=score+1;
                $(".score").html(score);
            }
        }
        function RWD_img(){
            if(ww>hh){
                $(".bonk_").css("width","auto");
                $(".bonk_").css("height",hh*0.6);
            }else{
                $(".bonk_").css("width",ww*0.9);
                $(".bonk_").css("height","auto");
            }
            
        }
        
        /*$("body").addEventListener('touchstart',function(e){
            alert("123");
            if(start_count==1){
                score=score+1;
                $(".score").html(score);
                mmg.src=img[img_count+1];
            }
        });
        $("body").addEventListener('touchend',function(e){
            if(start_count==1){
                mmg.src=img[img_count];
            }
        });*/


        /*$("body").mouseup(function(){
            if(start_count==1){
                mmg.src=img[img_count];
            }
        }).mousedown(function(){
            if(start_count==1){
                score=score+1;
                $(".score").html(score);
                mmg.src=img[img_count+1];
            }
        });*/




        
        $(".ranking p").css("width",ww/4);
        
        let name = $('input[name=name]').val();
          if(name==""){
            name="無名氏";
        }

        
 
         function timer() {
            count--; // 每次執行timer就把count減1。
            $(".time_count").html(count);
            
            // 若已計數完畢，則停止計時。
            if (count == 0) {
                name = $('input[name=name]').val();
                  if(name==""){
                    name="無名氏";
                }
                clearInterval(timerId);
                start_count=2;
                img_count=0;
                mmg.src=img[0];
                $(".score").html(score);
                sent_score();
                alert(name+"："+score);
                start_count=0;
                ranking();
                RWD();
            }
         }
         function timer2(time) {
            time--; // 每次執行timer就把count減1。
            //$(".time_count").html(count);
            
            // 若已計數完畢，則停止計時。
            if (time == 0) {
                /*name = $('input[name=name]').val();
                  if(name==""){
                    name="無名氏";
                }*/
                clearInterval(timerId2);
            }
         }
         function sent_score() {
          
          let score_t = score;
          $.ajax({
            url: "https://script.google.com/macros/s/AKfycbxv7684ehbXDgG4gSLZsw8NHrxjfpFIG2Xmh_hq-0mL_Q0E6hoHnyBT5uBijJVHwS8iNA/exec",
            data: {
                "name": name,
                "score": score_t,
            },
            success: function(response) {
              /*if(response == "成功"){
                alert("成功");
              }*/
            },
          });
        };
        //排行榜
        var spreadsheet_id = "1k0qIm1UpXS-HaTLEYTRl980v0XfFPZmSwvTgyocZKtU", // 填入試算表 ID
        tab_name = "sheet2", // 填入工作表名稱
        api_key = "AIzaSyBqKfrW0NymoR4YvC-qDJgQqFLqfvRKoNw", // 填入 API 金鑰
        url = "https://sheets.googleapis.com/v4/spreadsheets/" + spreadsheet_id + "/values/" + tab_name + "?key=" + api_key;
        
        ranking();
        function ranking(){
            $.getJSON(url, function(json) {
                //alert("123");
                var excel = [];
                var values = json.values; // 所有試算表資料
                values.forEach(function(rows) {

                    rows.forEach(function(item) {
                        excel.push(item);
                    });
                });
                //alert(excel[2]+":"+excel[3]);
                $(".first").html(excel[2]+":"+excel[3]);
                $(".second").html(excel[4]+":"+excel[5]);
                $(".third").html(excel[6]+":"+excel[7]);
            });

        }
        RWD();
        window.onresize= function(){
            RWD();

        }
        function RWD(){
            ww=window.innerWidth;
            hh=window.innerHeight;
            if(ww<600){
                //alert(ww);
                $(".bonk_").css("width",ww*0.7);
                $(".bonk_").css("height","auto");
                $(".third").css("display","none");
                $(".ranking p").css("width","40%");
                $(".bonk_").css("margin-top","50px");
                $("h1").css("font-size","30px");
            }else{
                $(".bonk_").css("height",(hh/5)*2.5);
                $(".bonk_").css("width","auto");
                $(".bonk_").css("margin-top","0px");
                $(".third").css("display","inline-block");
                $(".ranking p").css("width","30%");
                $("h1").css("font-size","40px");
            }
            
        }
        
                


    </script>

</body>

</html>