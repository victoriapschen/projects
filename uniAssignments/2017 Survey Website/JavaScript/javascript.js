/*Lots of code here from code examples from lectures*/


function formIsValid()
{
	/* If all functions return true return true, if any one function
	returns false, return false*/
	if(validatePassportPhoto()&&validateLName()&&validateFName()&&validateDOB()&&validateCityOB()&&validateCountryOB()&&validatepassportnum()&&validatepassportcountry()&&validatepassportexp()&&validateaddress()&&validatetelephone()&&validateresidentialemail()&&validatecontactname()&&validateotheraddress()&&validateothertelephone()&&validateotheremail()&&validateamount()&&validateChequePhoto()&&validateswitch()&&validatecardname()&&validatecardnumber()&&validatecvcId()&&validatecardexp())
		return true;
	else 
		return false;
}

/* Checks if a file was uploaded. 
https://stackoverflow.com/questions/46219/how-to-determine-if-user-selected-a-file-for-file-upload
*/
function validateChequePhoto()
{
	if(document.getElementById("chequephotoId").value == "") 
	{
		 var msg = "Please upload an image.";
		 //the cheque photo element clashes with the function so I just made it so it only makes the border red.
		 document.form1.chequephotoId.style.border="solid 1px red";
		 return false; 
	}
	return true;
}


/* Checks if a file was uploaded. 
https://stackoverflow.com/questions/46219/how-to-determine-if-user-selected-a-file-for-file-upload
*/
function validatePassportPhoto()
{
	if(document.getElementById("passportphotoId").value == "") 
	{
		 var msg = "Please upload an image.";
		 //call the display error function
		 displayError(document.form1.passportphotoId, msg);
		 return false; 
	}
	return true;
}

function validateFName()
{
	if(document.form1.fnameId.value=="")
	{
		 var msg = "Please enter your first name.";
		 //call the display error function
		 displayError(document.form1.fname, msg);
		 return false; 
	}
	return true;
}
function validateLName()
{
   
	 if(document.form1.lname.value=="")
	 {
	   
		 //create an error message
		 var msg = "Please enter your last name.";
		 //call the display error function
		 displayError(document.form1.lname, msg);
		 return false; 

	 }
	return true;
}


function validateDOB()
{
   
	 if(document.form1.dobId.value=="")
	 {
	   
		 //create an error message
		 var msg = "Please enter your date of birth.";
		 //call the display error function
		 displayError(document.form1.dobId, msg);
		 return false; 

	 }
	return true;
}

function validateCityOB()
{
   
	 if(document.form1.cityofbirthId.value=="")
	 {
	   
		 //create an error message
		 var msg = "Please enter your City of Birth.";
		 //call the display error function
		 displayError(document.form1.cityofbirthId, msg);
		 return false; 

	 }
	return true;
}

function validateCountryOB()
{
   
	 if(document.form1.countryofbirthId.value=="")
	 {
	   
		 //create an error message
		 var msg = "Please enter your Country of Birth.";
		 //call the display error function
		 displayError(document.form1.countryofbirthId, msg);
		 return false; 

	 }
	return true;
}

function validatepassportnum()
{
   
	 if(document.form1.passportnumId.value=="")
	 {
	   
		 //create an error message
		 var msg = "Please enter your passport number.";
		 //call the display error function
		 displayError(document.form1.passportnumId, msg);
		 return false; 

	 }
	return true;
}

function validatepassportcountry()
{
   
	 if(document.form1.passportcountryId.value=="")
	 {
	   
		 //create an error message
		 var msg = "Please enter your passport country.";
		 //call the display error function
		 displayError(document.form1.passportcountryId, msg);
		 return false; 

	 }
	return true;
}

function validatepassportexp()
{
   
	 if(document.form1.passportexpId.value=="")
	 {
	   
		 //create an error message
		 var msg = "Please enter your passport expiry date.";
		 //call the display error function
		 displayError(document.form1.passportexpId, msg);
		 return false; 

	 }
	return true;
}

function validateaddress()
{
   
	 if(document.form1.residentaddressId1.value=="")
	 {
	   
		 //create an error message
		 var msg = "Please enter your residential address.";
		 //call the display error function
		 displayError(document.form1.residentaddressId1, msg);
		 return false; 

	 }
	return true;
}

function validatetelephone()
{
   
	 if(document.form1.landlineId.value=="" && document.form1.mobileId.value=="")
	 {
	   
		 //create an error message
		 var msg = "Please enter at least one telephone number.";
		 //call the display error function
		 displayError(document.form1.mobileId, msg);
		 return false; 

	 }
	return true;
}



function validateresidentialemail()
{
   
	 if(document.form1.residentialemailId.value=="")
	 {
	   
		 //create an error message
		 var msg = "Please enter your email address.";
		 //call the display error function
		 displayError(document.form1.residentialemailId, msg);
		 return false; 

	 }
	return true;
}

function validatecontactname()
{
   
	 if(document.form1.contactnameId.disabled == false && document.form1.contactnameId.value=="")
	 {
	   
		 //create an error message
		 var msg = "Please enter a name.";
		 //call the display error function
		 displayError(document.form1.contactnameId, msg);
		 return false; 

	 }
	return true;
}

function validateotheraddress()
{
   
	 if(document.form1.contactnameId.disabled == false && document.form1.contactaddressId1.value=="")
	 {
	   
		 //create an error message
		 var msg = "Please enter the address.";
		 //call the display error function
		 displayError(document.form1.contactaddressId1, msg);
		 return false; 

	 }
	return true;
}

function validateothertelephone()
{
   
	 if(document.form1.contactnameId.disabled == false && document.form1.daytimeteleId.value=="" && document.form1.eveningId.value=="")
	 {
	   
		 //create an error message
		 var msg = "Please enter at least one telephone number.";
		 //call the display error function
		 displayError(document.form1.eveningId, msg);
		 return false; 

	 }
	return true;
}

function validateotheremail()
{
   
	 if(document.form1.contactnameId.disabled == false && document.form1.contactemailId.value=="")
	 {
	   
		 //create an error message
		 var msg = "Please enter an email address.";
		 //call the display error function
		 displayError(document.form1.contactemailId, msg);
		 return false; 

	 }
	return true;
}

function validateamount()
{
   
	 if(document.form1.amountId.value=="")
	 {
	   
		 //create an error message
		 var msg = "Please enter a value.";
		 //call the display error function
		 displayError(document.form1.amountId, msg);
		 return false; 

	 }
	return true;
}

function validateswitch()
{
   
	 if(document.form1.switchnumberid.disabled == false && document.form1.switchnumberid.value=="")
	 {
	   
		 //create an error message
		 var msg = "Please enter a value.";
		 //call the display error function
		 displayError(document.form1.switchnumberid, msg);
		 return false; 

	 }
	return true;
}



function validatecardname()
{
   
	 if(document.form1.cardNameId.disabled == false && document.form1.cardNameId.value=="")
	 {
	   
		 //create an error message
		 var msg = "Please enter the name on the card..";
		 //call the display error function
		 displayError(document.form1.cardNameId, msg);
		 return false; 

	 }
	return true;
}


function validatecardnumber()
{
   
	 if(document.form1.cardNumberId.disabled == false && document.form1.cardNumberId.value=="")
	 {
	   
		 //create an error message
		 var msg = "Please enter the card number.";
		 //call the display error function
		 displayError(document.form1.cardNumberId, msg);
		 return false; 

	 }
	return true;
}


function validatecvcId()
{
   
	 if(document.form1.cvcId.disabled == false && document.form1.cvcId.value=="")
	 {
	   
		 //create an error message
		 var msg = "Please enter the CVC/CVV number.";
		 //call the display error function
		 displayError(document.form1.cvcId, msg);
		 return false; 

	 }
	return true;
}


function validatecardexp()
{
   
	 if(document.form1.cardexpId.disabled == false && document.form1.cardexpId.value=="")
	 {
	   
		 //create an error message
		 var msg = "Please enter the expiry date";
		 //call the display error function
		 displayError(document.form1.cardexpId, msg);
		 return false; 

	 }
	return true;
}






function displayError(element, msg)
{
		 if(element.nextSibling.tagName=="SPAN" && element.nextSibling.textContent.trim==msg.trim)
		 {
			return;
		 }
		 var msgElement=document.createElement("span");
		 msgElement.textContent=msg;
		 msgElement.style.color="red";
		 element.parentNode.insertBefore(msgElement, element.nextSibling);
		 element.style.border="solid 1px red";
}


//Implements fade in fade out using javascript
var fadeNode;
var repeater;
var opacity = 0;
var increments = 100;
var increment_value = 1/increments;
function radioSelectionTitle()
{
    var thisRadio = event.target;
    var div=document.getElementById("othertitlebox");
    if(thisRadio.id=="othertitleId")
    {
      
        div.style.opacity=0;
        opacity = 0;
        fadeNode = div;
        fadeIn(500);
        div.style.display="inline";
    }
    else
    {
        opacity = 1;
        fadeNode = div;
        fadeOut(500);


    }
}

function radioSelectionComm()
{
    var thisRadio = event.target;
    var div=document.getElementById("otheraddressbox");
    if(thisRadio.id=="asbelowId")
    {
      
        div.style.opacity=0;
        opacity = 0;
        fadeNode = div;
        fadeIn(500);
        div.style.display="block";
        document.form1.contactnameId.disabled = false;
        document.form1.contactaddressId1.disabled = false;
        document.form1.contactaddressId2.disabled = false;
        document.form1.daytimeteleId.disabled = false;
        document.form1.eveningId.disabled = false;
        document.form1.contactemailId.disabled = false;
    }
    else
    {
        opacity = 1;
        fadeNode = div;
        fadeOut(500);
        document.form1.contactnameId.disabled = true;
        document.form1.contactaddressId1.disabled = true;
        document.form1.contactaddressId2.disabled = true;
        document.form1.daytimeteleId.disabled = true;
        document.form1.eveningId.disabled = true;
        document.form1.contactemailId.disabled = true;


    }
}


function radioSelectionPayment()
{
    var thisRadio = event.target;
    var chequediv=document.getElementById("chequebox");
    var switchdiv=document.getElementById("switchbox");
    var carddiv=document.getElementById("creditcardbox");
    if(thisRadio.id=="chequeId")
    {
        chequediv.style.display="block";
        switchdiv.style.display="none";
        carddiv.style.display="none";
        document.form1.cvcId.disabled = true;
        document.form1.cardexpId.disabled = true;
        document.form1.cardNameId.disabled = true;
        document.form1.cardNumberId.disabled = true;
        document.form1.switchnumberid.disabled = true;
        document.form1.chequephotoId.disabled = false;
    }
	else if(thisRadio.id=="switchId")
	{
        chequediv.style.display="none";
        switchdiv.style.display="inline";
        carddiv.style.display="block";
        document.form1.cvcId.disabled = false;
        document.form1.cardexpId.disabled = false;
        document.form1.cardNameId.disabled = false;
        document.form1.cardNumberId.disabled = false;
        document.form1.switchnumberid.disabled = false;
        document.form1.chequephotoId.disabled = true;
	}
    else
    {
        chequediv.style.display="none";
        switchdiv.style.display="none";
        carddiv.style.display="block";
        document.form1.cvcId.disabled = false;
        document.form1.cardexpId.disabled = false;
        document.form1.cardNameId.disabled = false;
        document.form1.cardNumberId.disabled = false;
        document.form1.switchnumberid.disabled = true;
        document.form1.chequephotoId.disabled = true;
    }
}

function fadeIn(duration)
{

     var interval = duration/increments;
     repeater = setInterval(increaseOpacity, interval);
    
}
function increaseOpacity() 
{
    
    if (opacity < 1.0) 
    {
        opacity = opacity + increment_value;
        fadeNode.style.opacity = opacity;
    } 
    else 
    {
        clearInterval(repeater);
    }
}
function fadeOut(duration)
{

     var interval = duration/increments;
     repeater = setInterval(decreaseOpacity, interval);
    
}
function decreaseOpacity() 
{
    
    if (opacity > 0) 
    {
        opacity = opacity - increment_value;
        fadeNode.style.opacity = opacity;
    } 
    else 
    {
        fadeNode.style.display="none";
        clearInterval(repeater);
    }
}


