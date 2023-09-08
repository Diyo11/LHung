window.onload = function () {
    alert('請用手機遊玩\n使用左右方向鍵控制小車左右移動\n\n通關條件:200分\n\n點擊確定按鈕開始遊戲')
    // 捕获背景（父元素）
    var bg = this.document.getElementById('bg');
    var background = this.document.getElementById('background');
    var bgImg = this.document.getElementById('bgImg');
    // 获取屏幕高度,赋予背景
    var h = this.document.documentElement.clientHeight / 1.25;
    var w = bg.clientWidth;
    var carBgs = [];
    bg.style.height = h + 'px';
    bgImg.style.top = -(bgImg.clientHeight - h + 10) + 'px';
    background.style.height = document.documentElement.clientHeight + 'px';
    var score = this.document.getElementById('score');
    var timeLast = this.document.getElementById('timeLast');

    // 生成box
    function createBox() {
        var box = this.document.createElement('div')
        box.setAttribute('class', 'box');
        // 生成的位置---> 22;88;172;238 四个赛道随机生成
        var randNum = Math.random();
        var deltai = (((w - 20) / 4) - boxw) / 2;
        var i = deltai + 10; // 起始横坐标
        // console.log(randNum);
        if (randNum < 0.25) {
            box.style.left = i + 'px';
        } else if (randNum >= 0.25 && randNum < 0.5) {
            box.style.left = i + boxw + (2 * deltai) + 'px';
        } else if (randNum >= 0.5 && randNum < 0.75) {
            box.style.left = i + (2 * boxw) + (4 * deltai) + 'px';
        } else {
            box.style.left = i + (3 * boxw) + (6 * deltai) + 'px';
        };
        box.style.background = carBgs[Math.floor(Math.random() * carBgs.length)];
        bg.appendChild(box);
        moveBox(box); // 为每个box绑定对象
        var carNum = document.querySelectorAll('.box').length;
        if (carNum < 6) {
            intervalTime -= 50;
            clearInterval(timer);
            if (intervalTime < 0) {
                intervalTime = 10;
            };
            if (intervalTime > 0) {
                timer = setInterval(add, intervalTime);
                // console.log(intervalTime);
            };
        };
    };
    // 统计游戏时间
    var timeLastNum = 0;
    setInterval(function () {
        timeLastNum++;
        timeLast.innerHTML = 'Time: ' + timeLastNum + 's';
    }, 1000)
    // 当屏幕数量的车辆小于3辆时生成车辆
    // 初始每隔1s判断数量，添加小车，每次最多添加3辆
    // 每当车辆总数小于3时，同时减少生成车辆的时间间隔
    var intervalTime = 2000;
    // 需要正确关闭！！！
    function add() {
        createBox(); // 生成第1辆
        createBox(); // 生成第2辆
        createBox(); // 生成第3辆
    };
    var timer = setInterval(add, intervalTime);
    // 设置移动背景
    movebg();
    function movebg() {
        var timerS = setInterval(moveStreet, time);
        function moveStreet() {
            var pos = bgImg.offsetTop;
            pos += deltaX;
            if (pos > 0) {
                bgImg.style.top = -(bgImg.clientHeight - h) + 'px';
            } else {
                clearInterval(timerS);
                bgImg.style.top = pos + 'px';
                timerS = setInterval(moveStreet, time);
            }
        }
    }
    //一般獲勝
    function wingame() {
        // alert('你贏了\n看來你很勇麻!!\n\n最終得分:' + scoreNum + '分' + '\n存活時間:' + timeLastNum + 's');
        // window.location.reload(true);
        // window.location.href = '../main.html';
        
        const message = '你的通關不是通關\n看來你很勇麻!!\n\n最終得分:' + scoreNum + '分' + '\n存活時間:' + timeLastNum + 's';   
        alert(message);
        window.location.replace('../main.php');
    }
    //彩蛋觸發成功
    function wingame2() {
        // alert('你贏了\n\n人生不一是只有前進,何不全力倒退一次!!');
        // window.location.reload(true);
        const message = '你贏了\n\n人生不一是只有前進,何不全力倒退一次!!';   
        alert(message);

        // session_start();
        // $student_id = $_SESSION['student_id'];
        // alert($student_id);

//----------------///--------------------------------------
    // 使用 jQuery 的 $.ajax() 方法來呼叫 update.php
    $.ajax({
      url: "../update.php",
      method: "POST",
      data: {
        LV: "LV6",
        student_id: "<?php echo $student_id; ?>",
        status: "1"
      },
      success: function(result) {
        // 更新成功，可以在這裡加上對應的處理程式碼
        // $result = update(LV4,$student_id, 1);
        console.log(result);
        const message1 = '$SystemCoding <?php echo  #passSIX  ;?>';   
        alert(message1);
        window.location.replace('../main.php');
        
      },
      error: function(xhr, status, error) {
        // 更新失敗，可以在這裡加上對應的處理程式碼
        console.log("Error: " + status + " - " + error);
        const message = 'errorrrrrrrrr';   
        alert(message);
    }
    });
  
    }

//-----------------///---------------------------------------
    function gameOver() {
        alert('遊戲結束\n\n3，2，1，GO\n相信你的直覺\n\n\n最終得分:' + scoreNum + '分' + '\n存活時間:' + timeLastNum + 's');
        window.location.reload(true);
        window.location.replace('../main.php');
    }

    // 使得每个box运动起来
    var deltaX = 1;
    var time = 5;
    var scoreNum = 0;
    function moveBox(obj) {
        // bgImg.style.top = -(bgImg.clientHeight - h) + 'px';
        deltaX += 0.01; // 加速度运动
        var pos = 0;
        var id = setInterval(move, time);

        function move() {
            if (scoreNum == 200) {
                clearInterval(id);
                clearInterval(moveBox);
                clearInterval(id);
                wingame();
            }
            if (pos > (h - boxh * 1.2)) {
                pos = h - boxh * 1.2;
                if (pos == (h - boxh * 1.2)) {
                    clearInterval(id);
                    bg.removeChild(obj); // 移除运动到底的box
                    scoreNum++;
                    score.innerHTML = 'Score: ' + scoreNum;
                }
            } else {
                pos += deltaX;
                obj.style.top = pos + 'px';


                if (
                    mybox.offsetTop < obj.offsetTop + boxh - 55 &&
                    mybox.offsetTop + boxh - 55 > obj.offsetTop &&
                    mybox.offsetLeft < obj.offsetLeft + boxw - 10 &&
                    mybox.offsetLeft + boxw - 10 > obj.offsetLeft
                ) {
                    clearInterval(id);
                    gameOver();
                    mybox.speed = 0;
                }
            };
        }
    };


    // 加入自定义小车
    var mybox = this.document.getElementById('mybox');
    var boxh = mybox.clientHeight;
    var boxw = mybox.clientWidth;
    // 指定初始位置
    mybox.style.top = h - boxh - 10 + 'px';
    mybox.style.left = w / 2 + 'px';
    var speed = 10;
    // 定义位移函数

    var speed = 5;
    var leftInterval;
    var rightInterval;
    var upInterval;
    var downInterval;
    var parent = document.getElementById('bg');
    var mybox = document.getElementById('mybox');
    var btnDownWinTimer; // 新增一个变量用于存储获胜计时器

    // 取得父元素的寬度
    var w = parent.clientWidth;

    // 按下上鍵時移動 mybox 元素的函數
    function moveUp() {
        var currentTop = parseInt(mybox.style.top) || 0;
        if (currentTop >= 25) {
            mybox.style.top = (currentTop - speed) + 'px';
        }
    }

    // 按下下鍵時移動 mybox 元素的函數
    function moveDown() {
        var currentTop = parseInt(mybox.style.top) || 0;
        if (currentTop <= h - mybox.offsetHeight - 20) {
            mybox.style.top = (currentTop + speed) + 'px';
        }
    }


    // 按下左鍵時移動 mybox 元素的函數
    function moveLeft() {
        // console.log('Left');
        var currentLeft = parseInt(mybox.style.left) || 0;
        if (currentLeft >= 25) {
            mybox.style.left = (currentLeft - speed) + 'px';
        }
    };

    // 按下右鍵時移動 mybox 元素的函數
    function moveRight() {
        // console.log('Right');
        var currentLeft = parseInt(mybox.style.left) || 0;
        if (currentLeft <= w - mybox.offsetWidth - 20) {
            mybox.style.left = (currentLeft + speed) + 'px';
        }
    };

    // 綁定按鈕點擊事件
    var btnLeft = document.getElementById('btnLeft');
    var btnRight = document.getElementById('btnRight');
    var btnUp = document.getElementById('btnUp');
    var btnDown = document.getElementById('btnDown');

    btnLeft.addEventListener("touchstart", function () {
        event.preventDefault();
        clearInterval(rightInterval);
        clearInterval(upInterval);
        clearInterval(downInterval);
        leftInterval = setInterval(moveLeft, 15);
    });

    btnRight.addEventListener("touchstart", function () {
        event.preventDefault();
        clearInterval(leftInterval);
        clearInterval(upInterval);
        clearInterval(downInterval);
        rightInterval = setInterval(moveRight, 15);
    });

    btnUp.addEventListener("touchstart", function () {
        event.preventDefault();
        clearInterval(downInterval);
        clearInterval(leftInterval);
        clearInterval(rightInterval);
        upInterval = setInterval(moveUp, 15);
    });

    btnDown.addEventListener("touchstart", function () {
        event.preventDefault();
        clearInterval(upInterval);
        clearInterval(leftInterval);
        clearInterval(rightInterval);
        downInterval = setInterval(moveDown, 15);

        // 开始计时，并在5秒后检查是否满足获胜条件
        btnDownWinTimer = setTimeout(function () {
            wingame2(); // 若满足条件则调用 wingame 函数
        }, 2500);
    });

    btnLeft.addEventListener("touchend", function () {
        clearInterval(leftInterval);
    });

    btnRight.addEventListener("touchend", function () {
        clearInterval(rightInterval);
    });

    btnUp.addEventListener("touchend", function () {
        clearInterval(upInterval);
    });

    btnDown.addEventListener("touchend", function () {
        clearInterval(downInterval);
        // 当 btnDown 按钮释放时，清除获胜计时器
        clearTimeout(btnDownWinTimer);
    });

}