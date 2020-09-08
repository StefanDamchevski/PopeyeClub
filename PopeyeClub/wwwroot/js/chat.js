function joinRoom(i) {
    let chatroomName = document.getElementById("chatroomName-" + i).value;

    let chatRoom = document.getElementById("chatRoom");

    if (chatRoom.childElementCount > 0) {
        while (chatRoom.firstChild) {
            chatRoom.removeChild(chatRoom.lastChild);
        }
    }

    axios.post(`/Chat/JoinRoom/?connectionId=${_connectionId}&chatroomName=${chatroomName}`)
        .then(function (response) {
            renderRoom(response.data);
            disableButton();
            updateScroll();
        })
        .catch(err => {
            console.log(err);
        })
}

function sendMessage(event) {
    event.preventDefault();
    let data = new FormData(event.target);

    axios.post(`/Chat/SendMessage/`, data)
        .then(function (response) {
            document.getElementById('chatMessageInput').value = "";
            updateScroll();
            disableButton();
        })
        .catch(err => {
            console.log(err);
        })
}

connection.on('RecieveMessage', function (data) {
    let chatMessages = document.getElementById("chatMessages");

    let message = document.createElement("div");
    message.classList.add("message");
    chatMessages.appendChild(message);

    let userMessage = document.createElement("div");
    userMessage.classList.add("user-info");
    message.appendChild(userMessage);

    let userImage = document.createElement("img");
    userImage.src = "data:image/jpeg;base64," + data.userImage;
    userImage.classList.add('chat-user-image');
    userMessage.appendChild(userImage);

    let messageUserName = document.createElement("span");
    messageUserName.innerHTML = data.userName;
    userMessage.appendChild(messageUserName);

    let messageText = document.createElement('span');
    messageText.innerHTML = data.text;
    messageText.classList.add('message-text');
    message.appendChild(messageText);

    let messageDate = document.createElement("span");
    messageDate.innerHTML = data.dateCreated;
    message.appendChild(messageDate);

    updateScroll();
    disableButton();
});

function updateScroll() {
    let element = document.getElementById("chatMessages");
    element.scrollTop = element.scrollHeight;
}

function disableButton() {
    let submitBtn = document.getElementById("submitMessageBtn");

    if (submitBtn != null) {
        submitBtn.disabled = true;
    }
}

function showUsers() {

    let searchInput = document.getElementById("searhUserInput").value.toLowerCase().trim();

    let tableRows = document.getElementsByClassName("table-rows");

    for (let i = 0; i < tableRows.length; i++) {
        let td = tableRows[i].getElementsByTagName("td")[1];

        let tdTextValue = td.innerText.toLowerCase().trim();

        if (tdTextValue.indexOf(searchInput) == -1 || searchInput == "") {
            tableRows[i].style.display = "none";
        } else {
            tableRows[i].style.display = "";
        }
    }
}

function enableBtn() {
    let submitBtn = document.getElementById("submitMessageBtn");
    let chatMessageInput = document.getElementById("chatMessageInput").value.trim();

    if (chatMessageInput != "") {
        submitBtn.disabled = false;
    } else {
        submitBtn.disabled = true;
    }
}

function renderRoom(data) {
    let chatRoom = document.getElementById("chatRoom");

    let chatUser = document.createElement("div");
    chatUser.classList.add("chat-user");
    chatRoom.appendChild(chatUser);

    let chatUserImage = document.createElement("img");
    chatUserImage.src = "data:image/jpeg;base64," + data.chatRoomPicture;
    chatUserImage.classList.add('chat-user-image');
    chatUser.appendChild(chatUserImage);

    let userName = document.createElement("span");
    userName.classList.add('user-name');
    userName.innerHTML = data.chatUserName;
    chatUser.appendChild(userName);

    let hr = document.createElement("hr");
    chatUser.appendChild(hr);

    let chatMessages = document.createElement("div");
    chatMessages.classList.add('chat-messages');
    chatMessages.id = "chatMessages";
    chatRoom.appendChild(chatMessages);

    if (data.messages != null) {
        for (let i = 0; i < data.messages.length; i++) {
            let message = document.createElement("div");
            message.classList.add("message");
            chatMessages.appendChild(message);

            let userMessage = document.createElement("div");
            userMessage.classList.add("user-info");
            message.appendChild(userMessage);

            let userImage = document.createElement("img");
            userImage.src = "data:image/jpeg;base64," + data.messages[i].userImage;
            userImage.classList.add('chat-user-image');
            userMessage.appendChild(userImage);

            let messageUserName = document.createElement("span");
            messageUserName.innerHTML = data.messages[i].userName;
            userMessage.appendChild(messageUserName);

            let messageText = document.createElement('span');
            messageText.innerHTML = data.messages[i].text;
            messageText.classList.add('message-text');
            message.appendChild(messageText);

            let messageDate = document.createElement("span");
            messageDate.innerHTML = data.messages[i].dateCreated;
            message.appendChild(messageDate);
        }
    }

    let sendMessageForm = document.createElement("div");
    sendMessageForm.classList.add('send-message-form');
    chatRoom.appendChild(sendMessageForm);

    let messageForm = document.createElement('form');
    messageForm.setAttribute('method', 'post');
    messageForm.setAttribute('onsubmit', 'sendMessage(event)')
    messageForm.classList.add('form-inline');
    sendMessageForm.appendChild(messageForm);

    let chatRoomNameInput = document.createElement('input');
    chatRoomNameInput.setAttribute('type', 'hidden');
    chatRoomNameInput.setAttribute('name', 'chatroomName');
    chatRoomNameInput.setAttribute('value', `${data.chatRoomName}`);
    messageForm.appendChild(chatRoomNameInput);

    let chatRoomIdInput = document.createElement('input');
    chatRoomIdInput.setAttribute('type', 'hidden');
    chatRoomIdInput.setAttribute('name', 'chatroomid');
    chatRoomIdInput.setAttribute('value', `${data.chatRoomId}`);
    messageForm.appendChild(chatRoomIdInput);

    let chatInput = document.createElement('div');
    chatInput.classList.add('form-group');
    chatInput.classList.add('chat-input');
    messageForm.appendChild(chatInput);

    let messageInput = document.createElement('input');
    messageInput.setAttribute('type', 'text');
    messageInput.setAttribute('name', 'text');
    messageInput.setAttribute('onkeyup', "enableBtn()");
    messageInput.id = "chatMessageInput";
    messageInput.classList.add('form-control');
    chatInput.appendChild(messageInput);

    let submitMessage = document.createElement('input');
    submitMessage.setAttribute('type', 'submit');
    submitMessage.setAttribute('value', 'Send');
    submitMessage.placeholder = "Write something to..." + data.chatRoomName;
    submitMessage.classList.add('btn');
    submitMessage.classList.add('btn-outline-success');
    submitMessage.id = "submitMessageBtn";
    messageForm.appendChild(submitMessage);
}