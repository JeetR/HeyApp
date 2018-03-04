<?php 

$fileName = "myFile.txt";
$fp = fopen($fileName,"rb");
$data = fread($fp, 4096);

fclose($fp);
  echo $data;


?>