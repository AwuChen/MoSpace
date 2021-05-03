var counter = 0;

setInterval(highlight, 1500);

setInterval(LDR, 1500);

function highlight() {
	if (counter == 0) {
		document.getElementById("highlight").innerHTML = "memorial";
	} else if (counter == 1) {
		document.getElementById("highlight").innerHTML = "tribute";
	} else if (counter == 2) {
		document.getElementById("highlight").innerHTML = "family album";
	} else {
		document.getElementById("highlight").innerHTML = "legacy";
	}

	if (counter < 3) {
		counter++;
	} else {
		counter = 0;
	}
}

function LDR() {
	if (counter == 0) {
		document.getElementById("LDR").innerHTML = "shelter";
	} else if (counter == 1) {
		document.getElementById("LDR").innerHTML = "playground";
	} else if (counter == 2) {
		document.getElementById("LDR").innerHTML = "hideaway";
	} else {
		document.getElementById("LDR").innerHTML = "home";
	}

	if (counter < 3) {
		counter++;
	} else {
		counter = 0;
	}
}

// src="/socket.io/socket.io.js"

// var socket = io("wss://ancient-beyond-09960.herokuapp.com");

// function submit() {
// 	email = document.getElementById("email").value;
// 	console.log(email);

// 	err = socket.emit("EMAIL", email);
// 	console.log(err);
// }

function confirmation() {
	alert("Thank you for subscribing to our newsletter!");
}