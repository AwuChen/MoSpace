<?php

$host = "ec2-34-225-162-157.compute-1.amazonaws.com";
$user = "tykfhmklcoamxo";
$password = "57ccdc0ec8ed93d9a208ebd1770d5ed296202bd832d6d6017911eeab9661e7ad";
$dbname = "d520dium8gqbaf";
$port = "5432";

try{
  //Set DSN data source name
    $dsn = "pgsql:host=" . $host . ";port=" . $port .";dbname=" . $dbname . ";user=" . $user . ";password=" . $password . ";";


  //create a pdo instance
  $pdo = new PDO($dsn, $user, $password);
  $pdo->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE,PDO::FETCH_OBJ);
  $pdo->setAttribute(PDO::ATTR_EMULATE_PREPARES,false);
  $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
}
catch (PDOException $e) {
echo 'Connection failed: ' . $e->getMessage();
}
  ?>
