// Website html stuff
const { Client } = require('pg'); 

const client = new Client({
  connectionString: process.env.DATABASE_URL,
  ssl: {
    rejectUnauthorized: false
  }
});

client.connect();

// Unity server stuff 
var express  = require('express');//import express NodeJS framework module
var app      = express();// create an object of the express module
var http     = require('http').Server(app);// create a http web server using the http library
var io       = require('socket.io')(http);// import socketio communication module
var shortId 		= require('shortid');//import shortid module

app.use("/public/TemplateData",express.static(__dirname + "/public/TemplateData"));
app.use("/public/Build",express.static(__dirname + "/public/Build"));
app.use(express.static(__dirname+'/public'));

var clients			= [];// to storage clients
var clientLookup = {};// clients search engine
var sockets = {};//// to storage sockets
// have the array here 
var currentUserPIC = "";

//open a connection with the specific client
io.on('connection', function(socket){

   //print a log in node.js command prompt
  console.log('A user ready for connection!');
  
  //to store current client connection
  var currentUser;
  var maze;
	
  	socket.on('EMAIL', function(_data) {
  		console.log("email submitted " + _data)
  		client.query('INSERT INTO testusers(email) VALUES (\''+_data+'\');');
  	});


  	socket.on('NAME', function(_pack) {

  		var pack = JSON.parse(_pack);	

	    console.log('login from user# '+socket.id+"name: "+pack.name);

  		client.query('INSERT INTO testusers(name) VALUES (\''+pack.name+'\');');
  	});

  	socket.on('WRITE', function(_pack) {

  		var pack = JSON.parse(_pack);	

	    console.log('writing from '+currentUser.name+": "+pack.writing);

  		client.query('INSERT INTO testusers(writing) VALUES (\''+pack.writing+'\');');
  		//socket.broadcast.emit('UPDATE_WRITING', currentUser.name, pack.writing);
  	});	

  	socket.on('INBOX', function(_pack) {

  		var pack = JSON.parse(_pack);	

  		var allWriting = client.query('SELECT * FROM testusers(writing)');
  		console.log('INBOX: '+allWriting);
  		socket.emit('UPDATE_WRITING', allWriting);
  	});	



	//create a callback fuction to listening EmitPing() method in NetworkMannager.cs unity script
	socket.on('PING', function (_pack)
	{
	  //console.log('_pack# '+_pack);
	    var pack = JSON.parse(_pack);	

	    console.log('message from user# '+socket.id+": "+pack.msg);
        
		 //emit back to NetworkManager in Unity by client.js script
		socket.emit('PONG', socket.id,pack.msg);
     
	});


	//create a callback fuction to listening EmitJoin() method in NetworkMannager.cs unity script
	socket.on('LOGIN', function (_data)
	{
	
	    console.log('[INFO] JOIN received !!! ');
		
		var data = JSON.parse(_data);

         // fills out with the information emitted by the player in the unity
        currentUser = {
			       name:data.name,
                   position:data.position,
				   rotation:'0,0,0,0',
			       id:shortId.generate(),//alternatively we could use socket.id
				   socketID:socket.id,//fills out with the id of the socket that was open
				   animation:"",
				   health:100,
			       maxHealth:100,
			       kills:0,
				   timeOut:0,
				   isDead:false,
				   moji:'0',
				   interact:'0',
				   msg:"",
				   multiplier:'0',
				   };//new user  in clients list
		
				
		console.log('[INFO] player '+currentUser.name+': logged!');
		console.log('[INFO] currentUser.position '+currentUser.position);	

		 //add currentUser in clients list
		 clients.push(currentUser);

		 //add client in search engine
		 clientLookup[currentUser.id] = currentUser;
		 
		 console.log('[INFO] Total players: ' + clients.length);

		 
		 /*********************************************************************************************/		
		
		//send to the client.js script
		socket.emit("LOGIN_SUCCESS",currentUser.id,currentUser.name,currentUser.position,currentUser.rotation);//emite para o metodo NetworkController.OnLoginSuccess(SocketIOEvent _myPlayer)
		
         //spawn all connected clients for currentUser client 
         clients.forEach( function(i) {
		    if(i.id!=currentUser.id)
			{
		     // console.log('[INFO] generate ' + i.name+ ' connected!');
				 
		      //send to the client.js script
		      socket.emit('SPAWN_PLAYER',i.id,i.name,i.position,i.rotation);
		    }//END_IF
	   
	     });//end_forEach
		
		 // spawn currentUser client on clients in broadcast
		socket.broadcast.emit('SPAWN_PLAYER',currentUser.id,currentUser.name,currentUser.position,currentUser.rotation);
  
	});//END_SOCKET_ON
	
	
	
    //create a callback fuction to listening method in NetworkMannager.cs unity script
	socket.on('RESPAW', function (_info) {
	
	    var info = JSON.parse(_info);	  
		
	    if(currentUser)
		{
	
			currentUser.isDead = false;
		
		    currentUser.health = currentUser.maxHealth;
			 
		    socket.emit('RESPAW_PLAYER',currentUser.id,currentUser.name,currentUser.position,currentUser.rotation);
			 
		    socket.broadcast.emit('SPAWN_PLAYER',currentUser.id,currentUser.name,currentUser.position,currentUser.rotation);
			 
	        console.log('[INFO] User ' + currentUser.name + ' respawned!');
			
		}
       
    });//END_SOCKET_ON
	
		
	//create a callback fuction to listening EmitMoveAndRotate() method in NetworkMannager.cs unity script
	socket.on('MOVE_AND_ROTATE', function (_data)
	{
     

	  var data = JSON.parse(_data);	
	  
	  if(currentUser)
	  {
	
       currentUser.position = data.position;
	   
	   currentUser.rotation = data.rotation;

	   currentUser.moji = data.moji;

	   currentUser.interact = data.interact;
	  
	   // send current user position and  rotation in broadcast to all clients in game
       socket.broadcast.emit('UPDATE_MOVE_AND_ROTATE', currentUser.id,currentUser.position,currentUser.rotation,currentUser.moji,currentUser.interact);
      console.log('[INFO] currentUser.position '+currentUser.position);
      console.log('[INFO] currentUser.moji '+currentUser.moji);
       }
	});//END_SOCKET_ON

	//create a callback fuction to listening EmitMoveAndRotate() method in NetworkMannager.cs unity script
	socket.on('ROTATE_MAZE', function (_data)
	{
	  var data = JSON.parse(_data);	
	  
	  if(currentUser)
	  {
	   currentUser.multiplier = data.multiplier;
	   // send current maze rotation in broadcast to all clients in game
       socket.broadcast.emit('UPDATE_MAZE_ROTATE', currentUser.multiplier);
       console.log('[INFO] maze multiplier '+currentUser.multiplier);
       }
	});//END_SOCKET_ON

	//create a callback fuction to listening EmitMoveAndRotate() method in NetworkMannager.cs unity script
	socket.on('SUBJECT', function (_data)
	{
	  var data = JSON.parse(_data);	
	  
	  if(currentUser)
	  {
	   currentUser.multiplier = data.subject;
	   // send current maze rotation in broadcast to all clients in game
       socket.broadcast.emit('UPDATE_SUBJECT', currentUser.multiplier);
       console.log('[INFO] Subject Reveal '+currentUser.multiplier);
       }
	});//END_SOCKET_ON

	//create a callback fuction to listening EmitMoveAndRotate() method in NetworkMannager.cs unity script
	socket.on('INITIAL', function (_data)
	{
	  var data = JSON.parse(_data);	
	  
	  if(currentUser)
	  {
	   currentUser.multiplier = data.initial;
	   // send current maze rotation in broadcast to all clients in game
       socket.broadcast.emit('UPDATE_INITIAL', currentUser.multiplier);
       console.log('[INFO] Initial Vote '+currentUser.multiplier);
       }
	});//END_SOCKET_ON

	//create a callback fuction to listening EmitMoveAndRotate() method in NetworkMannager.cs unity script
	socket.on('FINAL', function (_data)
	{
	  var data = JSON.parse(_data);	
	  
	  if(currentUser)
	  {
	   currentUser.multiplier = data.final;
	   // send current maze rotation in broadcast to all clients in game
       socket.broadcast.emit('UPDATE_FINAL', currentUser.name, currentUser.multiplier);
       console.log('[INFO] Final Vote '+currentUser.multiplier);
       }
	});//END_SOCKET_ON

	
	//create a callback fuction to listening GetHistory() method in NetworkMannager.cs unity script
	socket.on('GET_HISTORY', function (_pack)
	{
	  //console.log('Room# '+_pack);
	    var pack = JSON.parse(_pack);	

		// send history string to all clients 
       socket.broadcast.emit('REPLAY_HISTORY', currentUser.name, pack.RoomNum);
      console.log('[INFO] history '+ pack.RoomNum);
	});
	
	//create a callback fuction to listening SaveChat() method in NetworkMannager.cs unity script
	socket.on('SAVE_PIC', function (_data)
	{
     
	   var data = JSON.parse(_data);

	   
	   console.log("SAVED into pic" + " -pic: " + data.pic);
	   if(currentUserPIC != data.pic)
	   {
	   		currentUserPIC = data.pic;
	   		socket.broadcast.emit('SEND_PIC', currentUserPIC);
	   }
	});//END_SOCKET_ON

	//create a callback fuction to listening SaveChat() method in NetworkMannager.cs unity script
	socket.on('CHECK_PIC', function ()
	{
		if(currentUserPIC != "")
		{
	   		socket.emit('SEND_PIC', currentUserPIC);
		}
	});//END_SOCKET_ON

	//create a callback fuction to listening EmitAnimation() method in NetworkMannager.cs unity script
	socket.on('ANIMATION', function (_data)
	{
	  var data = JSON.parse(_data);	
	  if(currentUser)
	  {
	   
	   currentUser.timeOut = 0;
	   
	    //send to the client.js script
	   //updates the animation of the player for the other game clients
       socket.broadcast.emit('UPDATE_PLAYER_ANIMATOR', currentUser.id,data.animation);
	   
      }//END_IF
	  
	});//END_SOCKET_ON
	

	//create a callback fuction to listening EmitAnimation() method in NetworkMannager.cs unity script
	socket.on('ATACK', function (_data)
	{
	  var data = JSON.parse(_data);	
	  
	  if(currentUser)
	  {
	   
        socket.broadcast.emit('UPDATE_ATACK', currentUser.id);
      }
	  
	});//END_SOCKET_ON
	

	//create a callback fuction to listening EmitPhisicstDamage method in NetworkMannager.cs unity script
	socket.on('PHISICS_DAMAGE', function (_data)
	{
	  var data = JSON.parse(_data);	
      if(currentUser)
	  {
      
	  
	   var target = clientLookup[data.targetId];
	 
	   var _damage = 1;
	   
	   // if health target is not empty
	   if(target.health - _damage > 0)
	    {
		   console.log("player: "+target.name+"receive damage from : "+currentUser.name);
		   
		   console.log(target.name+"health: "+ target.health);
		   
		   target.health -=_damage;//decrease target health
		}
	  
	   else{
	  
	  
        if(!target.isDead)
        {				
			   
		 target.isDead = true;//target is dead
		 
		 target.kills = 0;
		 
		 //console.log("currentuser"+currentUser.name+" kills: "+currentUser.kills);
		 
		 currentUser.kills +=1;
		
		 jo_pack = {
				shooterId:currentUser.id,
		        targetId:data.targetId
		 };
	 
	     //emit only for the currentUser
		 socket.emit('DEATH',jo_pack.targetId);
		 
		 //emit to all connected clients in broadcast
		 socket.broadcast.emit('DEATH',jo_pack.targetId);
		
	    }//END_ if    
    }//END_ELSE
		  
	damage_pack = {
		 
		       name:target.name,
				shooterId:currentUser.id,
				targetId:data.targetId,
				targetHealth:target.health
		 }
	
	  
	   socket.broadcast.emit("UPDATE_PHISICS_DAMAGE",damage_pack.shooterId,damage_pack.targetId,damage_pack.targetHealth);
			   
	}//END_IF
	  
	});//END_SOCKET_ON
	

    // called when the user desconnect
	socket.on('disconnect', function ()
	{
     
	    if(currentUser)
		{
		 currentUser.isDead = true;
		 
		 //send to the client.js script
		 //updates the currentUser disconnection for all players in game
		 socket.broadcast.emit('USER_DISCONNECTED', currentUser.id);
		
		
		 for (var i = 0; i < clients.length; i++)
		 {
			if (clients[i].name == currentUser.name && clients[i].id == currentUser.id) 
			{

				console.log("User "+clients[i].name+" has disconnected");
				clients.splice(i,1);

			};
		};
		
		}
    });//END_SOCKET_ON
		
});//END_IO.ON


http.listen(process.env.PORT ||3000, function(){
	console.log('app running on server');
});
console.log("------- server is running -------");