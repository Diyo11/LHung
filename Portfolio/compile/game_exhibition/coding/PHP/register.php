<?php
// 連接到server
$servername = "web.csie2.nptu.edu.tw"; //localhost
$username = "cbb110115";         //root
$password = "An126874726";     //password
$dbname = "cbb110115_foto_sl";           //myDB

$conn = new mysqli($servername, $username, $password, $dbname);
if ($conn->connect_error) {
    die("Successs: " . $conn->connect_error);
}

// 處理表單提交
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    // 獲取表單數據
    //資料庫 = html
    $student_id = $_POST["StudNumber"];
    $name = $_POST["name"];

    // 驗證表單數據
    if (empty($student_id) || empty($name)) {
        echo "輸入東西喔！";
    } else {
        // 將數據插入數server
        $sql = "INSERT INTO member (student_id, name) VALUES ('$student_id', '$name')";
        if ($conn->query($sql) === TRUE) {
            echo "Success！";
            echo "<a href='../index.html'>未成功跳轉頁面請點擊此</a>";
            header("refresh:32;Location:../index.html");
            
        } else {
            echo "E04：" . $conn->error;
        }
    }
}

$conn->close();
?>
