 "use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/Chat/ChatUser").build();

//Disable the send button until connection is established.
var userId = document.getElementById("userInput").value;

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${userId} says ${message}`;
    $('')
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    sendMessage();
    //event.preventDefault();
});
class Message {
    constructor(Text, SentAt, SenderID, AddresseeID) {
        this.Text = Text;
        this.SentAt = SentAt;
        this.SenderID = SenderID;
        this.AddresseeID = AddresseeID;
    }
}
const senderID = senderID;
const text = document.getElementById('messageInput');
function clearInput() {
    text.value = "";
}
function sendMessage() {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    console.log('send');
}
//window.onload = function () {
//    // Obtener el elemento de entrada de texto
//    const inputBusqueda = document.getElementsByName("search")[1];
//    // Agregar un evento keyup al elemento de entrada de texto
//    inputBusqueda.addEventListener("keyup", function (event) {
//        const buscar = event.target.value;
//        if ((buscar.trim() !== '' && buscar.trim().length + 5 > buscar.length) || (buscar.trim() === '' && buscar.length < 3)) {
//            busqueda(buscar.trim());
//        }
//        else {
//            event.target.value = buscar.trim() !== '' ? buscar.trim() : '';
//        }
//    });
//}

//function busqueda(cadena) {
//    const emailUsuarioBuscado = cadena; // Valor de búsqueda, podría ser un valor ingresado por el usuario
//    const url = `/GetSearch?consulta=${encodeURIComponent(emailUsuarioBuscado)}`;

//    fetch(url)
//        .then(response => response.json())
//        .then(data => {
//            if (data.length > 0) {
//                console.log(data);
//                const contenedor = document.querySelector('#kt_chat_contacts_body > div'); //contenedor principal
//                contenedor.innerHTML = "";
//                data.forEach(x => {
//                    // Crear un div 
//                    const div = document.createElement("div");
//                    div.classList.add("d-flex", "flex-stack", "py-4");
//                    // Crear un div para la imagen
//                    const divImagen = document.createElement("div");
//                    divImagen.classList.add("symbol", "symbol-45px", "symbol-circle");
//                    // Crear una imagen
//                    const imagen = document.createElement("img");
//                    imagen.setAttribute("alt", "Pic");
//                    imagen.setAttribute("src", x.AvatarUrl);
//                    // Agregar la imagen al div de la imagen
//                    divImagen.appendChild(imagen);
//                    // Crear un div para el contenido de texto
//                    const divTexto = document.createElement("div");
//                    divTexto.classList.add("ms-5");
//                    // Crear un enlace
//                    const enlace = document.createElement("a");
//                    enlace.classList.add("fs-5", "fw-bolder", "text-gray-900", "text-hover-primary", "mb-2");
//                    enlace.setAttribute("href", "#");
//                    enlace.textContent = x.FullName;
//                    // Agregar el enlace al div de texto
//                    divTexto.appendChild(enlace);
//                    // Crear un div para el correo electrónico
//                    const divEmail = document.createElement("div");
//                    divEmail.classList.add("fw-bold", "text-gray-400");
//                    divEmail.textContent = x.Email;
//                    // Agregar el div del correo electrónico al div de texto
//                    divTexto.appendChild(divEmail);
//                    //div que agrupa todos los campos
//                    const divSecundario = document.createElement("div");
//                    divSecundario.classList.add("d-flex", "align-items-center");
//                    // Agregar el div de la imagen y el div de texto al div secundario
//                    divSecundario.appendChild(divImagen);
//                    divSecundario.appendChild(divTexto);
//                    //agregar al div principal
//                    div.appendChild(divSecundario);
//                    // Agregar un separador
//                    const separador = document.createElement("div");
//                    separador.classList.add("separator", "separator-dashed", "d-none");
//                    // Agregar el div principal y el separador al contenedor
//                    contenedor.appendChild(div);
//                    contenedor.appendChild(separador);
//                })
//            } else {
//                console.log("No se encontraron usuarios.");
//            }
//        })
//        .catch(error => console.error(error));
//}