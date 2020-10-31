<?php include ('config/db.php')?>
<?php include ('config/config.php')?>

<html>
<body>

Welcome <br>
Your email address is: <?php echo $_GET["email"]; ?>

<?php
  echo 'This is Index Page';
  $sql = 'INSERT INTO users(name) VALUES (\'tesname4\');';
  $stmt = $pdo->prepare($sql);
  $stmt->execute();
?>
</body>
</html>