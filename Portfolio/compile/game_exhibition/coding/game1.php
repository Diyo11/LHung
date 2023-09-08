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

<!-- ------------顯示--------------------------------------------- -->
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>GAME1</title>

    <link rel="stylesheet" type="text/css" href="static\css\game2.css">
    
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Noto+Sans+HK:wght@300&family=Noto+Sans+TC:wght@500&display=swap" rel="stylesheet">

</head>
<body>
    <!-- <div class="header"> -->
    <div class="player-info">
			  <p>ID：</p>
        <label id="player-id" name="player-id"><?php echo $student_id; ?>       __</label>
        <p>TAG：</p>
        <label id="player-name" name="player-name"><?php echo $student_info[$student_id];?></label>
		    </div>
        <h1>\/汪\/</h1>
	<!-- </div> -->

    <div class="container">
      <div class="content">
        <p>不知道齁</p>
        <p>回去重睡!!</p>
        <p></p>
        <p>給我認真點喔</p>
        <form method="post">
          <input type="text" name="input" placeholder="輸入關鍵字">
          <input type="submit" value="提交">
        </form>
        <?php
          if(isset($_POST["input"])) {
            if($_POST["input"] == "huang.cpp") {
              require_once "update.php";
              $result = update(LV1,$student_id, 1);
              echo '<div class="result">' . $result . '</div>';            

              echo "<script>
              const message = '   應當細心地觀察，為的是理解;\\n應當努力地理解，為的是行動。\\n--法國作家 羅曼·羅蘭';   
              alert(message);window.location.replace('main.php');
              </script>";
              
              exit;  
            } else {
              echo '<div class="result">錯誤</div>';
            }
          }
        ?>
      </div>
    </div>
    
</body>
</html>