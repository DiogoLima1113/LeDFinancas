window.addEventListener("load", function () {
    
    $('#formLogin').on('submit', function(evt) {
        evt.preventDefault();
        login($(this));
    });

    alterarMensagemInputInvalido(document.querySelector('input[name="usuario"]'), "Informe seu usuário");
    alterarMensagemInputInvalido(document.querySelector('input[name="senha"]'), "Informe sua senha");
    document.querySelector('input[name="usuario"]').focus();
});

function alterarMensagemInputInvalido(input, mensagem) {
    input.addEventListener("invalid", function (e) {
        e.target.setCustomValidity(mensagem);
    });
    input.addEventListener("input", function (e) {
        e.target.setCustomValidity("");
    });
}

function login($form) {
    $.post($form.attr("action"), $form.serialize())
        .done(function(response) {
            location.href = getParameterByName("returnUrl") || "/";
        })
        .fail(function(response) {
            if(response.status == 0)
                return exibirMensagemErro("Não foi possível estabelecer conexão com o servidor do Helpdesk. <br>Por favor, tente novamente mais tarde.");
            
            exibirMensagemErro(response.responseJSON.mensagem);
        });
}

function exibirMensagemErro(msgErro) {
    var templateErro = document.createElement("div");
    templateErro.className = "alert alert-danger alert-dismissable center";
    templateErro.id = "msgErroLogin";
    templateErro.innerHTML = '<i class="fa fa-ban"></i>' +
                            '<button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>' + msgErro;

    document.body.insertBefore(templateErro, document.body.firstChild);
}

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}