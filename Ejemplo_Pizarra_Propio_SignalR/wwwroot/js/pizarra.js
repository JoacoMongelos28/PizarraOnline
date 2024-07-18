var connection = new signalR.HubConnectionBuilder().withUrl("/pizarraHub").build();
var canvas = document.getElementById("pizarra");
var btnGuardar = document.getElementById("guardarBtn");
var btnCargar = document.getElementById("cargarBtn");
var context = canvas.getContext("2d");
var drawing = false;
var prevX = 0, prevY = 0;
var salaActual = "";
let divPizarra = document.getElementById("divPizarra");
let divChat = document.getElementById("divChat");
let divNombreSala = document.getElementById("divNombreSala");
let divSalasCreadas = document.getElementById("divSalasCreadas");
let divSalir = document.getElementById("salir");
let btnSalir = document.createElement('a');
function unirseASala() {
    divPizarra.removeAttribute("hidden");
    divChat.removeAttribute("hidden");
    divNombreSala.setAttribute("hidden", true);
    divSalasCreadas.setAttribute("hidden", true);
    btnSalir.textContent = "Salir de la sala";
    btnSalir.className = "btn btn-secondary";
    divSalir.appendChild(btnSalir);
    divSalir.removeAttribute("hidden");
    document.getElementById("h1-fuera-de-sala").setAttribute("hidden", true);
    document.getElementById("h1-en-sala").removeAttribute("hidden");
    document.getElementById("h1-en-sala").textContent = 'Te uniste a la sala - ' + salaActual;
    document.getElementById("container-prepizarra").classList.remove("container-prepizarra");
    document.getElementById("container-prepizarra").classList.add("class-titulo-en-sala");
    btnSalir.onclick = function () {
        connection.invoke("SalirDeSala", salaActual).catch(function (err) {
            return console.error(err.toString());
        });
        window.location.reload();
        divPizarra.setAttribute("hidden", true);
        divChat.setAttribute("hidden", true);
        divNombreSala.removeAttribute("hidden");
        divSalasCreadas.removeAttribute("hidden");
        btnSalir.remove;
        divSalir.setAttribute("hidden", true);
        document.getElementById("salaInput").value = "";
        salaActual = "";
    };

}
// Método para dibujar en la pizarra
function dibujarEnPizarra(data) {
    var dibujo = JSON.parse(data);
    context.beginPath();
    context.moveTo(dibujo.prevX, dibujo.prevY);
    context.lineTo(dibujo.currX, dibujo.currY);
    context.strokeStyle = dibujo.color;
    context.lineWidth = dibujo.size;
    context.stroke();
    context.closePath();
}

function getMousePos(canvas, evt) {
    var rect = canvas.getBoundingClientRect();
    return {
        x: evt.clientX - rect.left,
        y: evt.clientY - rect.top
    };
}

// Conexión con el hub de SignalR
connection.start().then(function () {
    document.getElementById("crearSala").addEventListener("click", function (event) {
    salaActual = document.getElementById("salaInput").value;
    errorMessage = document.getElementById("error-sala");

    if (salaActual === "") {
        errorMessage.style.display = "block";
        event.preventDefault();
    } else {
        errorMessage.style.display = "none";


    connection.invoke("UnirseASala", salaActual).catch(function (err) {
        return console.error(err.toString());
    });
        unirseASala();
    }
});
    const buttons = document.querySelectorAll('#divSalasCreadas button.buttom-style-custom');


    buttons.forEach(button => {
        button.addEventListener('click', function (event) {

            event.preventDefault();

            let input = button.previousElementSibling;
            salaActual = input.value.trim();
            errorMessage = document.getElementById("error-sala");
            if (salaActual === "") {
                errorMessage.style.display = "block";
                return
            } else {
                errorMessage.style.display = "none";
                console.log(salaActual);
                connection.invoke("UnirseASala", salaActual).catch(function (err) {
                    return console.error(err.toString());
                });
                unirseASala();
            }
        });
    });

    document.getElementById("sendButton").addEventListener("click", function () {
        var message = document.getElementById("messageInput").value;
        var errorMessage = document.getElementById("error-message");

        if (message.trim() === "") {
            errorMessage.style.display = "inline";
        } else {
            errorMessage.style.display = "none";
            connection.invoke("EnviarMensaje", salaActual, message).catch(function (err) {
                return console.error(err.toString());
            });
            document.getElementById("messageInput").value = "";
        }
    });

    canvas.addEventListener("mousedown", function (e) {
        var pos = getMousePos(canvas, e);
        drawing = true;
        prevX = pos.x;
        prevY = pos.y;
    });

    //canvas.addEventListener("mousemove", function (e) {
    //    if (drawing && salaActual) {
    //        var pos = getMousePos(canvas, e);
    //        var currX = pos.x;
    //        var currY = pos.y;
    //        var color = $("#color").val();
    //        var size = $("#size").val();
    //        var dibujo = {
    //            prevX: prevX,
    //            prevY: prevY,
    //            currX: currX,
    //            currY: currY,
    //            color: color,
    //            size: size
    //        };
    //        dibujarEnPizarra(JSON.stringify(dibujo));

    //        connection.invoke("Dibujar", salaActual, JSON.stringify(dibujo)).catch(function (err) {
    //            return console.error(err.toString());
    //        });
    //        prevX = currX;
    //        prevY = currY;
    //    }
    //});

    canvas.addEventListener("mousemove", function (e) {

        if (drawing && salaActual) {
            var pos = getMousePos(canvas, e);
            var currX = pos.x;
            var currY = pos.y;
            var color = $("#color").val();
            var size = $("#size").val();
            var dibujo = {
                prevX: prevX,
                prevY: prevY,
                currX: currX,
                currY: currY,
                color: color,
                size: size
            };
            dibujarEnPizarra(JSON.stringify(dibujo));
            connection.invoke("Dibujar", salaActual, JSON.stringify(dibujo)).catch(function (err) {
                return console.error(err.toString());
            });
            prevX = currX;
            prevY = currY;
        }
    });

    //connection.invoke("ObtenerDibujos", parseInt(salaActual)).then(function (dibujos) {
    //    dibujos.forEach(function (dibujo) {
    //        dibujarEnPizarra(dibujo);
    //    });
    //}).catch(function (err) {
    //    return console.error(err.toString());
    //});

    connection.invoke("ObtenerDibujos", salaActual).then(function (dibujos) {
        dibujos.forEach(function (dibujo) {
            dibujarEnPizarra(dibujo);
        });
    }).catch(function (err) {
        return console.error(err.toString());
    });

    canvas.addEventListener("mouseup", function () {
        drawing = false;
    });

    canvas.addEventListener("mouseleave", function () {
        drawing = false;
    });

    connection.on("LimpiarPizarra", function () {

        context.clearRect(0, 0, canvas.width, canvas.height);
    });

    document.getElementById("limpiar").addEventListener("click", function () {
        connection.invoke("BorrarDibujos", salaActual).catch(function (err) {
            return console.error(err.toString());
        });
    });

    connection.on("dibujarEnPizarra", function (data) {
        dibujarEnPizarra(data);
    });

    connection.on("RecibirMensaje", function (user, message) {
        var msg = document.createElement("div");
        msg.textContent = user + ": " + message;
        document.getElementById("messagesList").appendChild(msg);
    });

    connection.on("ActualizarUsuarios", function (usuarios) {
        var listaUsuarios = document.getElementById("usuariosConectados");
        listaUsuarios.innerHTML = "";
        usuarios.forEach(function (usuario) {
            var userItem = document.createElement("div");
            userItem.textContent = usuario;
            listaUsuarios.appendChild(userItem);
        });
    });
    connection.on("GuardarImagen", function () {
        guardarImagen();
    });

}).catch(function (err) {
    //return console.error(err.toString());
});