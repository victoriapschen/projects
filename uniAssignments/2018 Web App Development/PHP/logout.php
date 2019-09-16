<?php
	$username = $_POST["username"];
	$con = mysqli_connect("mysql.cms.waikato.ac.nz", "vpsc1", "my10819578sql", "vpsc1");
	//updates the user to be offline
	$query="UPDATE `logindetails` SET `online`=0 WHERE `username`='".$username."'";
	$con->query($query);
	$con->close();
?>
