var counter = 0;

setInterval(highlight, 1500);

function highlight() {
	if (counter == 0) {
		document.getElementById("highlight").innerHTML = "shelter";
	} else if (counter == 1) {
		document.getElementById("highlight").innerHTML = "playground";
	} else if (counter == 2) {
		document.getElementById("highlight").innerHTML = "hideaway";
	} else {
		document.getElementById("highlight").innerHTML = "home";
	}

	if (counter < 3) {
		counter++;
	} else {
		counter = 0;
	}
}

// src="/socket.io/socket.io.js"

// var socket = io("wss://ancient-beyond-09960.herokuapp.com");

function submit() {
	email = document.getElementById("email").value;
	console.log(email);

	err = socket.emit("EMAIL", email);
	console.log(err);
}

function confirmation() {
	alert("Thank you for subscribing to our newsletter!");
}