<?php
	$username = $_POST["username"];
	$password = $_POST["password"];
	$con = mysqli_connect("mysql.cms.waikato.ac.nz", "vpsc1", "my10819578sql", "vpsc1");
	//checks if the username and password are correct
	$query = "select * from logindetails where username='$username' AND password='$password'";
    	$result = mysqli_query($con, $query);
	//If there is a row, then the username and pass are correct
	if(mysqli_num_rows($result)>0) 
	{
		//echos that logged in "1" as success message
		echo "1";
		//updates that the user is online
		$query2="UPDATE `logindetails` SET `online`=1 WHERE `username`='".$username."'";
		$con->query($query2);
	}
	else
	{
		echo "Incorrect login details.";
	}
	$con->close();
?>
