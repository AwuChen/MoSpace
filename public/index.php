<?php include ('includes/db.php')?>
<?php include ('includes/config.php')?>

<html lang="en">
<head>
  <meta charset="utf-8">
  <meta name="description" content="Making LDRs easier.">

  <title>MoSpace</title>

  <link href="styles.css" rel="stylesheet">
  <link href="https://fonts.googleapis.com/css2?family=Inter:wght@100;200;300;400;500;600;700;800;900&display=swap" rel="stylesheet">
  <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
</head>

<body>
  <header>
    <div id="nav">
      <a href="index.html"><img id="logo" src="MoSpace_logo.png"></a>
      <ul>
        <li><a href="#">Team</a></li>
        <li><a href="demo.html">Progress</a></li>
      </ul>
      <!-- <a class="call" href="#">Hear from us!</a> -->
    </div>
  </header>

  <main>
    <div id="wrapper">
      <h1>We are making <br>LDRs <span id="highlight">easier.</span></h1>
      <h3>Look, we get it. Long distance relationships are super tough. We know
        because we're in them too. We're making an app unlike any other to help
        you close the distance.</h3>
      <form>
        <input type="text" placeholder="Email address" name="email" required>
        <input type="submit" value="Hear from us!">
      </form>
    </div>
  </main>

<?php
  echo "php running";
  //$email=$_POST["email"];
  //echo "$email";
  // $sql = "INSERT INTO users(email) VALUES ('test');";
  // $stmt = $pdo->prepare($sql);
  // $stmt->execute();
  
  header("signup=success");
?>

</body>

</html>