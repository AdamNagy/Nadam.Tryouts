<?php

   $dbhost = 'localhost:3306';
   $dbuser = 'root';
   // $dbpass = 'guest123';
   $conn = mysqli_connect($dbhost, $dbuser);
   
   if(!$conn ) {
      die('Could not connect: ' . mysql_error());
   }
   
   $sql = 'SELECT * FROM todo';
   mysqli_select_db($conn, "qweasd");
   $retval = mysqli_query( $conn, $sql );
   
   if(! $retval ) {
      die('Could not get data: ' . mysql_error());
   }
   
   while($row = mysqli_fetch_array($retval)) {
      echo "ID :{$row['id']}  <br> ".
         "Status: {$row['status']} <br> ".
         "--------------------------------<br>";
   }
   
   echo 'Connected successfully';
   mysqli_close($conn);
?>
