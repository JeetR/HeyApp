<?php 
date_default_timezone_set('Asia/Kolkata');
echo date("d-m-y H:i:s" );
 $name = $_GET['name'];
 $emailid = $_GET['email'];
 $message = $_GET['message'];
 

 
 $to ="developer@email.com" ;
 $subject = "Regarding heyApp" ;
 $headers = "MIME-Version: 1.0" . "\r\n";
$headers .= "Content-type:text/html;charset=UTF-8" . "\r\n";

// More headers
$headers .= 'From:'.$emailid . "\r\n";

 
 mail($to,$subject,$message."<br><br> <u>".date("d-m-y H:i:s")."</u>",$headers);
 $replyheader="MIME-Version: 1.0" . "\r\n";
 $replyheader .= "Content-type:text/html;charset=UTF-8" . "\r\n";
 mail($emailid,$subject,"Thanks for Your response we will do the need full asap! <br><br>This is an auto generated mail please dont reply",$replyheader);
  
//echo $name,$emailid,$message;

?>
