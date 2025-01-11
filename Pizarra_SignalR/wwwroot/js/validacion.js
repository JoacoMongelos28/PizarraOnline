document.addEventListener('DOMContentLoaded', function () {
    var form = document.getElementById('validacion-usuario');

    form.addEventListener('submit', function (event) {
        var nombre = document.getElementById('nombre').value;
        var errorUsuario = document.getElementById('error-usuario');
        if (nombre.trim() === '') {
            errorUsuario.style.display = 'block';
            setTimeout(function () {
                errorUsuario.classList.add('show');
            }, 10);
            event.preventDefault();
        } else {
            errorUsuario.classList.remove('show');
            setTimeout(function () {
                errorUsuario.style.display = 'none';
            }, 500);
        }
    });
});