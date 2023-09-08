<?php
$servername = "web.csie2.nptu.edu.tw"; 
$username = "cbb110115";         
$password = "An126874726";     
$dbname = "cbb110115_foto_sl";    

$conn = new mysqli($servername, $username, $password, $dbname);
if ($conn->connect_error) {
    die("Successs: " . $conn->connect_error);
}

if ($_SERVER["REQUEST_METHOD"] == "POST") {
  $student_id = $_POST["StudNumber"];

  // Check if the student ID exists in the database
  // 輸入是否存在資料庫 
  $sql = "SELECT * FROM member WHERE student_id='$student_id'";
  $result = mysqli_query($conn, $sql);

  if (mysqli_num_rows($result) > 0) {
    // 登入成功後將使用者學號存儲在session中
    session_start();
    $_SESSION['student_id'] = $student_id;

    // Redirect to the main page
    header("Location:../main.php");
    exit;
  } else {
    // echo "<script>alert('The student ID does not exist');</script>";
    // header("Location:../index.html");
    
    echo "The student ID does not exist";
    echo "<a href='../index.html'> --> 再來一次~~</a>";
    exit;
  }
}

mysqli_close($conn);
?>
