
<?php
$var = $_GET['select'];
echo $var;
$fileName = "myFile.txt";
$fp = fopen($fileName,"w");
fwrite( $fp, $var );
//echo '<script>window.location.assign("https://autolligentbeta.000webhostapp.com/");</script>';
?>


