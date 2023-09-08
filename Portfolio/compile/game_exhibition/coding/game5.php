<!-- -----------資料讀取---------------------------------------------- -->
<?php
$servername = "web.csie2.nptu.edu.tw"; 
$username = "cbb110115";         
$password = "An126874726";     
$dbname = "cbb110115_foto_sl"; 
$conn = new mysqli($servername, $username, $password, $dbname);
if ($conn->connect_error) {
    die("連線失敗: " . $conn->connect_error);
}

session_start();
$student_id = $_SESSION['student_id'];
// echo "<p>session: ". $student_id . "</p>";
// 從資料庫中讀取使用者的學號和名稱
$sql = "SELECT student_id, name FROM member WHERE student_id = '$student_id'";
$result = $conn->query($sql);
$student_info = array();
if ($result->num_rows > 0) {
    while($row = $result->fetch_assoc()) {
        // $student_id = $row["student_id"];
        $name = $row["name"];
        $student_info[$student_id] = $name;
    }
} else {
    echo "0 results";
}
$conn->close();
?>

<!DOCTYPE html>
<html>
  <head>
    <meta charset="UTF-8">
    <title>IIG</title>
    <style>
      body {
        background-color: #f5f5f5;
        font-family: Arial, sans-serif;
        color: #666;
        margin: 0;
        padding: 0;
        display: flex;
        flex-direction: column;
        align-items: center;
      }

      h1 {
        font-size: 48px;
        font-weight: bold;
        margin-top: 50px;
        margin-bottom: 0;
      }

      .container {
        margin-top: 30px;
        display: flex;
        flex-direction: column;
        align-items: center;
      }

      .container .content {
        background-color: #fff;
        color: #333;
        padding: 30px;
        border-radius: 10px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.5);
        min-width: 300px; /* 最小寬度 */
      }

      p {
        font-size: 20px;
        line-height: 1.5;
        margin-bottom: 30px;
        text-align: justify;
      }

      input[type="text"] {
        padding: 10px;
        border-radius: 5px;
        border: none;
        margin-right: 10px;
        font-size: 16px;
        width: 300px;
      }

      input[type="submit"] {
        background-color: #4CAF50;
        color: #fff;
        padding: 10px 20px;
        border-radius: 5px;
        border: none;
        font-size: 16px;
        cursor: pointer;
      }

      .result {
        font-size: 24px;
        font-weight: bold;
        margin-top: 20px;
      }
    </style>
  </head>
  <body>
    <h1>I G</h1>
    <div class="container">
      <div class="content">
        <p>快 去 打 卡!!!</p>
        <p>IG 限動 @foto_sirlong</p>
        <form method="post">
            <input type="submit" name="submit" value="完成(?">
            <button type="button" onclick="goToMain()">回到主頁面</button>
        </form>
        <?php
          if(isset($_POST["button"])) {
            require_once "update.php";
            $result = update(LV5,$student_id, 1);
            echo '<div class="result">' . $result . '</div>';
            // echo "<script>window.replace('https://www.instagram.com/foto.sirlang/', '_blank');</script>";    
            echo "<script>alert('????!!!!~~~')</script>";
            echo "<script>window.location.replace('main.php');</script>";
            exit;
        }
        ?>
        </div>
        </div>  
    </div>
    </div>

</body>
</html>

<script>
  function goToMain() {
    window.location.replace("main.php");
  }
</script>