//timer for the messages to update, off by default
var messageTimer;
//timer for the users to update automatically, on by default
var userTimer=setInterval(uTimer,1000);
//Currently logged in user on this window
var loggedInUser;

function login(){
	var xhttp = new XMLHttpRequest();
	var url = "./PHP/login.php";
	var uname = document.forms["loginform"]["username"].value;
	var pword = document.forms["loginform"]["password"].value;
	var params = "username="+uname+"&password="+pword;
	xhttp.open("POST", url, true);
	xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
	xhttp.onreadystatechange = function() {
		if(xhttp.readyState==4 && xhttp.status == 200) {
			document.getElementById("logintext").innerHTML = xhttp.responseText;
			//checks if the response text was a "1" aka a success, so replaces the div with the messagebox
			if(document.getElementById("logintext").innerHTML == "1"){
				document.getElementById("center").innerHTML="Welcome "+uname+"<br><h1>Messages</h1><div id='messagebox'></div><form id='messageform' name='messageform'><input type='text' name='messagetext'></form><button type='button' onClick='sendMessage()'>Send Message</button><span id='messagenoti'></span><br><br><button type='button' onClick='logout()'>Logout</button>";
				//activates the automatic refresh timer for the messages
				messageTimer=setInterval(mTimer,1000);
				//sets the logged in user variable
				loggedInUser=uname;
			}
		}
	}
	xhttp.send(params);
}//end login function

//Replaces the center div with the login and register options.
function logout(){
	document.getElementById("center").innerHTML="<h1>Login</h1><form name='loginform' method='POST'>Username <input type='text' name='username'> <br>Password <input id='password' type='password' name='password'> <br><br></form><button type='button' onClick='login()'>Login</button> <br><button type='button' onClick='register()'>Register with these details.</button> <br><span id=logintext></span><!--for ajax result--><br>";
	//stops the message reloading timer
	clearTimeout(messageTimer);
	//updates database that user has logged out
	var xhttp = new XMLHttpRequest();
	var url = "./PHP/logout.php";
	var params="username="+loggedInUser;
	xhttp.open("POST", url, true);
	xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
	xhttp.onreadystatechange=function(){
		if(xhttp.readyState==4 && xhttp.status == 200) {
		document.getElementById("test").innerHTML=xhttp.responseText;		
		}
	}
	xhttp.send(params);
	//clears loggedinuser
	loggedInUser=null;
}

function mTimer() {
	var xhttp = new XMLHttpRequest();
	var url="./PHP/messages.php";
	xhttp.open("GET",url,true);
	xhttp.onreadystatechange=function(){
		if(xhttp.readyState==4 && xhttp.status == 200) {
		document.getElementById("messagebox").innerHTML=xhttp.responseText;		
		}
	}
	xhttp.send("");
}

function sendMessage(){
	//gets the text
	var message = document.forms["messageform"]["messagetext"].value;
	//if the message is empty, it won't send, shows a message and ends
	if (message.length==0){
		document.getElementById("messagenoti").innerHTML="Cannot send empty message.";
		return;
	}
	var xhttp = new XMLHttpRequest();
	var url = "./PHP/addMessage.php";
	var params = "username="+loggedInUser+"&message="+message;
	xhttp.open("POST", url, true);
	xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
	xhttp.onreadystatechange = function() {
		if(xhttp.readyState==4 && xhttp.status == 200) {
			document.getElementById("messagenoti").innerHTML=xhttp.responseText;
		}
	}
	xhttp.send(params);
	//clears the textbox
	document.getElementById("messageform").reset();
}


//Calls to update the online and offline users on a timer
function uTimer() {
	updateUser('online');
	updateUser('offline');
}

//function to update the users
function updateUser(status) {
	var xhttp = new XMLHttpRequest();
	var url="./PHP/users.php";
	//get online users first
	var params = "status="+status;
	xhttp.open("POST",url,true);
	xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
	xhttp.onreadystatechange=function(){
		if(xhttp.readyState==4 && xhttp.status == 200) {
		document.getElementById(status).innerHTML=xhttp.responseText;		
		}
	}
	xhttp.send(params);	
}

//register the user with the details they had input

function register(){
	var xhttp = new XMLHttpRequest();
	var url = "./PHP/register.php";
	var uname = document.forms["loginform"]["username"].value;
	var pword = document.forms["loginform"]["password"].value;
	var params = "username="+uname+"&password="+pword;
	xhttp.open("POST", url, true);
	xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
	xhttp.onreadystatechange = function() {
		if(xhttp.readyState==4 && xhttp.status == 200) {
			document.getElementById("logintext").innerHTML = xhttp.responseText;
			//checks if the response text was a "1" aka a success
			if(document.getElementById("logintext").innerHTML == "1"){
				document.getElementById("logintext").innerHTML="Register successful.";
			}
			//If the response was exists then username is taken
			else if(document.getElementById("logintext").innerHTML == "exists"){
				document.getElementById("logintext").innerHTML="Username is taken.";
			}
		}
	}
	xhttp.send(params);
}//end register function
