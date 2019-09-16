<?php
	$username = $_POST["username"];
	$password = $_POST["password"];
	$con = mysqli_connect("mysql.cms.waikato.ac.nz", "vpsc1", "my10819578sql", "vpsc1");
	//checks that the username does not already exist
	$query = "select * from logindetails where username='$username'";
    	$result = mysqli_query($con, $query);
	//If there is a row, then the username exists and cannot be used
	if(mysqli_num_rows($result)>0) 
	{
		//tells function that username exists
		echo "exists";
	}
	else
	{
		//adds the username and password to the database
		$query2 = "INSERT INTO `logindetails`(`username`, `password`, `online`) VALUES ('".$username."','".$password."',0)";
		if ($username!=null && $password !=null && $con->query($query2)===TRUE){
			echo "1";
		}
		else{    
			echo "Please fill both fields";
		}
	}
	$con->close();
?>
