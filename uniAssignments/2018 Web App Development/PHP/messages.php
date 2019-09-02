<?php

	$con = mysqli_connect("mysql.cms.waikato.ac.nz", "vpsc1", "my10819578sql", "vpsc1");
	$query = "select * from messages";
	$result = $con->query($query);
	while($row=$result->fetch_array()){
		echo $row[username].': '.$row[message].'<br>';
	}
?>
