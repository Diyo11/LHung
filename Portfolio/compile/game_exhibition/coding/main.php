<!-- main.php + info.php -->

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
// echo $sql;
// echo "</br>";
// echo $student_id;
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

<!-- ----------------------////---------------------------------------->
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- <meta name="viewport" content="width=device-width, initial-scale=1.0"> -->
    <title>START</title>

    <link rel="stylesheet" type="text/css" href="static\css\mainStyles.css">
	<!-- <script src="static\js\mainGame.js"></script>  -->

    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Noto+Sans+HK:wght@300&family=Noto+Sans+TC:wght@500&display=swap" rel="stylesheet">

</head>
<body>	
	<div class="header">
		<h1>GAMEMAIN</h1>
        <div class="player-info">
			<p>學號：</p>
			<label id="player-id" name="player-id"><?php echo $student_id; ?></label>
			<p>名稱：</p>
			<label id="player-name" name="player-name"><?php echo $student_info[$student_id];?></label>
		</div>
	</div>
<!-- ------------------------------------------------------------------------------------------- -->
    <div class="container">
		<div class="start-game">
			<label for="start-game">輸入啟動遊戲：</label>
			<input type="text" id="start-game" name="start-game">
  			<button type="submit" id="start-game-button" class="strgamebutton">開始遊戲</button>
		</div>
		<!-- <php console.log(); ?> -->
		
<!-- ------------------------------------------------------------------------------------------- -->

		<div class="game-progress">
			<h2>關卡進度</h2>
			<div class="grid-container">
				<?php
				$servername = "web.csie2.nptu.edu.tw"; 
				$username = "cbb110115";         
				$password = "An126874726";     
				$dbname = "cbb110115_foto_sl"; 
				$conn = new mysqli($servername, $username, $password, $dbname);
				if ($conn->connect_error) {
					die("連線失敗: " . $conn->connect_error);
				}

				// 從資料庫中讀取使用者通關的關卡
				$sql = "SELECT LV1, LV2, LV3, LV4, LV5, LV6 FROM member WHERE student_id = '$student_id'";
				// echo "SQL query: $sql\n";
				$result = $conn->query($sql);
				$level_completed = array();
				if ($result->num_rows > 0) {
					while($row = $result->fetch_assoc()) {				
						for ($i = 1; $i <= 6; $i++) {
							$level_completed[$i] = $row['LV'.$i] == 1 ? '已通關' : '未通關';
						}
					}
				}
				else {
					echo "0 results";
				}

				// 顯示關卡進度
				for ($i = 1; $i <= 6; $i++) {
					$class = "";
					if ($level_completed[$i] == '已通關') {
						$class = "completed";
					}
					echo "<div class='grid-item $class'>$i</div>";
				}
				?>

			</div>
			<!-- <div class="game-status">過關：<php echo $current_level; ?></div> -->
		</div>	
	</div>

	<footer>
        <div class="footer-container">
            <div class="footer-item">
              <a href="https://forms.gle/g5mL93qAEX7MoYkdA">BUG報報</a>
            </div>
            <div class="footer-item">
              <a href="https://web.csie2.nptu.edu.tw/~cbb110115/">WEB</a>
            </div>
            <div class="footer-item">
              <a href="https://forms.gle/FdWgRcZHnnxTw2n86">回饋單單</a>
            </div>
        </div>
    </footer>

</body>
</html>

<!-- -------------------------------------------------------------------- -->
<script>
  const startGameButton = document.getElementById('start-game-button');
  startGameButton.addEventListener('click', () => {
    const startGameInput = document.getElementById('start-game');
    const userInput = startGameInput.value;
 //----------啟動輸入-----------// 
	const game1 = "f8ecc9";
	const game2 = "FFFFFF";
	const game3 = "RGB";	//發現小角落 //介紹
	const game4 = "0000FF";
	const game5 = "FOTO";
	const game6 = "cc5800";
	const game6_1 = "passSIX";
	const game7 = "e09641"; //Bonk
	const game8 = "0XF101"; //哈
	
 //----------啟動輸入-----------// 
    if (userInput === game1) {
      // 進入遊戲1關卡
      window.location.href = 'game1.php';
    } else if (userInput === game2) {
      window.location.href = 'game2.php';
    } else if (userInput === game3) {
      window.location.href = 'game3.php';
    } else if (userInput === game4) {
      window.location.href = 'game4.php';
    } else if (userInput === game5) {
      window.location.href = 'game5.php';
    } else if (userInput === game6) {
      window.location.href = 'game\\index.html';
    } else if (userInput === game6_1) {
      window.location.href = 'game6.php';
    } else if (userInput === game7) {
      window.location.href = 'Bonk\\popshiba.php';
    } else if (userInput === game8) {
      window.location.href = 'https://www.instagram.com/yi_lhung/?hl=zh-tw';
    } 
	else {
      alert('無效的輸入，請重新輸入！');
    }
  });
</script>