const toastSucesso = (mensagem) => {
    var toastAlerta = document.getElementById('containerAlerta');
    var toastAlertaBody = document.getElementById('containerAlertaText');
    toastAlerta.classList.remove('bg-primary');
    toastAlerta.classList.remove('bg-danger');
    if (mensagem) {
        console.log(mensagem);
        toastAlerta.classList.add('bg-primary');
        toastAlertaBody.innerHTML = mensagem;
        var toast = new bootstrap.Toast(toastAlerta);
        toast.show();
    }
};
const toastErro = (mensagem) => {
    var toastAlerta = document.getElementById('containerAlerta');
    var toastAlertaBody = document.getElementById('containerAlertaText');
    toastAlerta.classList.remove('bg-primary');
    toastAlerta.classList.remove('bg-danger');
    if (mensagem) {
        console.log(mensagem);
        toastAlerta.classList.add('bg-danger');
        toastAlertaBody.innerHTML = mensagem;
        var toast = new bootstrap.Toast(toastAlerta);
        toast.show();
    }
};

const processarErros = (formId, erros) => {
    let div = document.querySelector(`#${formId} .form-validacao`);
    div.classList.remove('form-valido');
    div.classList.remove('form-erro');

    if (!div) return;
    if (!Array.isArray(erros)) return;
    if(erros.length == 0) return;

    erros.forEach((e, i , a) => {
        a[i] = `<li>${e}</li>`;
    });

    let ul = '<ul>';
    ul += erros.join('');
    ul += '</ul>';

    div.classList.add('form-erro');
    div.innerHTML = ul;

    setTimeout(() => {
        div.classList.remove('form-valido');
        div.classList.remove('form-erro');
    }, 5000);
};