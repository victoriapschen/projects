<?php
	$status = $_POST["status"];
	$con = mysqli_connect("mysql.cms.waikato.ac.nz", "vpsc1", "my10819578sql", "vpsc1");
	if ($status=='online')
	{
		$status=1;
	}
	else{
		$status=0;
	}
	$query= "SELECT `username` FROM `logindetails` WHERE `online`=".$status;
	$result = $con->query($query);
	if(mysqli_num_rows($result)>0) 
	{
		while($row=$result->fetch_array()){
			echo $row[username].'<br>';
		}
	}
	else
	{
		echo "A faint breeze blows... it's empty as can be.";
	}
?>
