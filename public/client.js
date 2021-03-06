var socket = io() || {};
socket.isReady = false;

window.addEventListener('load', function() {

	var execInUnity = function(method) {
		if (!socket.isReady) return;
		
		var args = Array.prototype.slice.call(arguments, 1);
		
		gameInstance.SendMessage("NetworkController", method, args.join(','));
	};
			      
	socket.on('LOGIN_SUCCESS', function(id,name,position,rotation) {
				      		
	  var currentUserAtr = id+','+name+','+position+','+rotation;
	  gameInstance.SendMessage ('NetworkManager', 'OnJoinGame', currentUserAtr);
	});
	
		
	socket.on('SPAWN_PLAYER', function(id,name,position,rotation) {
	    var currentUserAtr = id+','+name+','+position+','+rotation;
		gameInstance.SendMessage ('NetworkManager', 'OnSpawnPlayer', currentUserAtr);
		
	});
	
	socket.on('RESPAW_PLAYER', function(id,name,position,rotation) {
	    var currentUserAtr = id+','+name+','+position+','+rotation;
		gameInstance.SendMessage ('NetworkManager', 'OnRespawPlayer', currentUserAtr);
		
	});
	
    socket.on('REPLAY_HISTORY', function(name,history) {
    	var currentUserAtr = name+','+history;
		gameInstance.SendMessage ('NetworkManager', 'OnReplayHistory',currentUserAtr);
		//execInUnity('Update_messages', currentUser);
	});

	socket.on('UPDATE_WRITING', function(writing) {
	    var currentUserAtr = writing;
		gameInstance.SendMessage ('NetworkManager', 'OnUpdateWriting',currentUserAtr);
		//execInUnity('Update_messages', currentUser);
	});

	socket.on('UPDATE_ALBUM', function(pictures) {
	    var currentUserAtr = pictures;
		gameInstance.SendMessage ('NetworkManager', 'OnUpdateAlbum',currentUserAtr);
		//execInUnity('Update_messages', currentUser);
	});

	socket.on('UPDATE_MOVE_AND_ROTATE', function(id,position,rotation,moji,interact) {
	     var currentUserAtr = id+','+position+','+rotation+','+moji+','+interact;
		gameInstance.SendMessage ('NetworkManager', 'OnUpdateMoveAndRotate',currentUserAtr);
		//execInUnity('Update_messages', currentUser);
	});

	socket.on('UPDATE_MAZE_ROTATE', function(multiplier) {
	     //var currentUserAtr = multiplier;
		gameInstance.SendMessage ('NetworkManager', 'OnUpdateMazeRotate',multiplier);
		//execInUnity('Update_messages', currentUser);
	});

	socket.on('UPDATE_SUBJECT', function(multiplier) {
	     //var currentUserAtr = multiplier;
		gameInstance.SendMessage ('NetworkManager', 'OnUpdateSubject',multiplier);
		//execInUnity('Update_messages', currentUser);
	});

	socket.on('UPDATE_INITIAL', function(multiplier) {
	     //var currentUserAtr = multiplier;
		gameInstance.SendMessage ('NetworkManager', 'OnUpdateInitial',multiplier);
		//execInUnity('Update_messages', currentUser);
	});

	socket.on('UPDATE_FINAL', function(name,multiplier) {
	    var currentUserAtr = name+','+multiplier;
		gameInstance.SendMessage ('NetworkManager', 'OnUpdateFinal',currentUserAtr);
		//execInUnity('Update_messages', currentUser);
	});

	socket.on('SEND_PIC', function(message) {
	     var currentUserAtr = message;
		gameInstance.SendMessage ('NetworkManager', 'OnUpdatePic',currentUserAtr);
		//execInUnity('Update_messages', currentUser);
	});

	
	socket.on('REPLAY', function(id,position,rotation) {
	     var currentUserAtr = id+','+position+','+rotation;
		gameInstance.SendMessage ('NetworkManager', 'OnReplay',currentUserAtr);
		//execInUnity('Update_messages', currentUser);
	});
	
	 socket.on('UPDATE_PLAYER_ANIMATOR', function(id,animation) {
	     var currentUserAtr = id+','+animation;
		gameInstance.SendMessage ('NetworkManager', 'OnUpdateAnim',currentUserAtr);
		//execInUnity('Update_messages', currentUser);
	});

	socket.on('UPDATE_ATACK', function(targetId) {
	    var currentUserAtr = targetId;
		gameInstance.SendMessage ('NetworkManager', 'OnUpdateAtack',currentUserAtr);
		//execInUnity('Update_messages', currentUser);
	});
	
	
	socket.on('DEATH', function(targetId) {
	    var currentUserAtr = targetId;
		gameInstance.SendMessage ('NetworkManager', 'OnPlayerDeath',currentUserAtr);
		//execInUnity('Update_messages', currentUser);
	});
	
    socket.on('UPDATE_PHISICS_DAMAGE', function(shooterId,targetId,targetHealth) {
	     var currentUserAtr = shooterId+','+targetId+','+targetHealth;
		gameInstance.SendMessage ('NetworkManager', 'OnUpdatePlayerPhisicsDamage',currentUserAtr);
		//execInUnity('Update_messages', currentUser);
	});		
	
	
		        
	socket.on('USER_DISCONNECTED', function(id) {
	     var currentUserAtr = id;
	     gameInstance.SendMessage ('NetworkManager', 'OnUserDisconnected', currentUserAtr);
		//execInUnity('User_disconected', score);
	});
	

});

