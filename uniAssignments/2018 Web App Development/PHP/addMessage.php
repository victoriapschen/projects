<?php
	$username = $_POST["username"];
	$message = $_POST["message"];
	$con = mysqli_connect("mysql.cms.waikato.ac.nz", "vpsc1", "my10819578sql", "vpsc1");
	$query = "INSERT INTO `messages`(`id`, `username`, `message`) VALUES (null,'".$username."','".$message."')";
	if ($con->query($query)===TRUE){
		echo "Message successfully added.";
	}
	else{    
	echo "Error: " . $query . "<br>" . $con->error;
	}
	$con->close();
?>
