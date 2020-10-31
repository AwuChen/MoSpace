<?php include ('db.php')?>
<?php include ('config.php')?>

<?php
  
  $email = $_POST["email"];
  
  $sql = "INSERT INTO users(email) VALUES ('$email');";
  $stmt = $pdo->prepare($sql);
  $stmt->execute();
  
  header("Location: ../index.html?signup=success");
?>