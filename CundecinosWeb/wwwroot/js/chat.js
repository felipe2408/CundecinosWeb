"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/Chat/ChatUser").build();

//Disable the send button until connection is established.
var userId = document.getElementById("userInput").value;

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    console.log(user, message);
    var name = $('#name').val()
    var avatar = $('#avatar').val()
    var messageHtml = `
        <div class="d-flex justify-content-start mb-10">
            <div class="d-flex flex-column align-items-start">
                <div class="d-flex align-items-center mb-2">
                    <div class="symbol symbol-35px symbol-circle">
                        <img alt="Pic" src="${avatar}" />
                    </div>
                    <div class="ms-3">
                        <a href="#" class="fs-5 fw-bolder text-gray-900 text-hover-primary me-1">${name}</a>
                    </div>
                </div>
                <div class="p-5 rounded bg-light-info text-dark fw-bold mw-lg-400px text-start" data-kt-element="message-text">${message}</div>
            </div>
        </div>
    `;
    $('#messagesDiv').append(messageHtml);
    // Mueve el scroll al final del div
    var messagesDiv = $("#messagesDiv");
    messagesDiv.scrollTop(messagesDiv.prop("scrollHeight"));

});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    console.log('enviando');
    //var user = document.getElementById("userInput").value;
    //var message = document.getElementById("textMessage").value;

    //});
    event.preventDefault();
    var message = {
        SenderID: $('#senderID').val(),
        AddresseeID: $('#userInput').val(),
        SentAt: new Date(),
        Text: $('#textMessage').val()
    };
    $.ajax({
        url: '/APIChat/SaveMessage',
        type: 'POST',
        data: JSON.stringify(message),
        contentType: 'application/json',
        success: function (response) {
            if (response.success) {
                $('#textMessage').val('')
                connection.invoke("SendMessage", message.AddresseeID, message.Text)
                    .catch(function (err) {
                        return console.error(err.toString())
                    });
                var avatar = $('#avatarSender').val();
                var messageHtml = `
                        <div class="d-flex justify-content-end mb-10">
                            <div class="d-flex flex-column align-items-end">
                                <div class="d-flex align-items-center mb-2">
                                    <div class="me-3">
                                        <a href="#" class="fs-5 fw-bolder text-gray-900 text-hover-primary ms-1">T\u00FA</a>
                                    </div>
                                    <div class="symbol symbol-35px symbol-circle">
                                        <img alt="Pic" src="${avatar}" />
                                    </div>
                                </div>
                                <div class="p-5 rounded bg-light-primary text-dark fw-bold mw-lg-400px text-end" data-kt-element="message-text">${message.Text}</div>
                            </div>
                        </div>
                    `;
                $('#messagesDiv').append(messageHtml);
                // Mueve el scroll al final del div
                var messagesDiv = $("#messagesDiv");
                messagesDiv.scrollTop(messagesDiv.prop("scrollHeight"));
            }
        }
    });
});
//class Message {
//    constructor(Text, SentAt, SenderID, AddresseeID) {
//        this.Text = Text;
//        this.SentAt = SentAt;
//        this.SenderID = SenderID;
//        this.AddresseeID = AddresseeID;
//    }
//}
//const senderID = document.getElementById('senderID').value;
//const text = document.getElementById('messageInput');
//function clearInput() {
//    text.value = "";
//}
//function sendMessage() {
//    if (text.value.trim() === "") {
//        return;
//    }
//    var user = document.getElementById("userInput").value;
//    var message = document.getElementById("messageInput").value;
//    var sendAt = new Date()
//    var message = new Message(text.value, sendAt, senderID, user)
//    sendMessageToHub(message)

//}
//function clearInputField() {
//    console.log('limpiar');
//}
//function addMessageToChat() {
//    console.log('agregar');
//}
