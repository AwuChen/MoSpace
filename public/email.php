<?php include ('includes/db.php')?>
<?php include ('includes/config.php')?>

<?php
  
  $email = $_POST["email"];
  
  $sql = "INSERT INTO users(email) VALUES ('$email');";
  $stmt = $pdo->prepare($sql);
  $stmt->execute();
  
  header("Location: ../index.html?signup=success");
?>