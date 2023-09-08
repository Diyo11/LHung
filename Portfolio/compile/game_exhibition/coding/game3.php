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
    <title>發現小角落</title>

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
        <h1>發現小角落!!!</h1>
	<!-- </div> -->

    <div class="container">
      <div class="content">
        <p>探索一波，你需要在場中尋找解答，完成任務即可通過遊戲。</p>
        <p>---------------------------</p>
        <p>荒謬的工作人員們</br>以生活為題</p>
        <p>。。。</br>。。</br></p>
        <p>---------------------------</p>
        <p>觀迎找到角落的你</p>
        <p><h2>攝</h2>影是獨自</p>
        <p><h2>廊</h2>展是相聚</p>
        <form method="post">
          <input type="text" name="input" placeholder="輸入關鍵字">
          <input type="submit" value="O<O">
        </form>
        <?php
          if(isset($_POST["input"])) {
            if($_POST["input"] == "sirlong") {
              require_once "update.php";
              $result = update(LV3,$student_id, 1);
              echo '<div class="result">' . $result . '</div>';
              
              // echo "<script>alert('你不需要很厲害才能開始，但你要開始才能很厲害。--美國作家 吉格·金克拉', 
              // function() {window.location.replace('main.php');
              // });</script>";
              // echo "<script>window.location.replace('main.php');</script>";

              echo "<script>
              const message = '   你不需要很厲害才能開始，\\n但你要開始才能很厲害。\\n--美國作家 吉格·金克拉';   
              alert(message);window.location.replace('main.php');
              </script>";
              exit;  
              // header("Location: main.php");
            } else {
              echo '<div class="result">錯誤</div>';
            }
          }
        ?>
      </div>
    </div>
</body>
</html>


<!-- -----------判斷答案--------------------------------------------------
<script>
  const startGameButton = document.getElementById('start-game-button');
  startGameButton.addEventListener('click', () => {
    const startGameInput = document.getElementById('start-game');
    const userInput = startGameInput.value;
 //----------啟動輸入-----------// 
	const game1 = "#RGB";
	const game2 = "ggg";
	const game3 = "ggg";
	const game4 = "ggg";
	const game5 = "ggg";
	const game6 = "ggg";
 //----------啟動輸入-----------// 
    if (userInput === game1) {
      // 進入遊戲1關卡
      window.location.href = 'game1.html';
    } else if (userInput === game2) {
      window.location.href = 'game2.html';
    } else if (userInput === game3) {
      window.location.href = 'game3.html';
    } else if (userInput === game4) {
      window.location.href = 'game4.html';
    } else if (userInput === game5) {
      window.location.href = 'game5.html';
    } else if (userInput === game6) {
      window.location.href = 'game6.html';
    } 
	else {
      alert('無效的輸入，請重新輸入！');
    }
  });
</script> -->