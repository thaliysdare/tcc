const ativarLinkModal = () => {
    let linksModal = document.querySelectorAll('[data-abrir-modal]');
    for (let link of linksModal) {
        link.onclick = () => {
            carregarModalJquery(link.getAttribute('data-abrir-modal-url'));
        };
    }
};

const carregarModalJquery = (link) => {
    carregando(true);
    $("#modalGenerico").load(link, function (response, status, xhr) {
        carregando(false);
        if (status == "error") {
            var msg = "Sorry but there was an error: ";
            $("#error").html(msg + xhr.status + " " + xhr.statusText);
        } else {
            let myModalEl = document.getElementById('jqueryModal');
            myModalEl.style.zIndex = 1054;

            let modal = bootstrap.Modal.getOrCreateInstance(myModalEl);
            modal.show();

            myModalEl.addEventListener('hidden.bs.modal', function () {
                document.querySelector('#modalGenerico').innerHTML = '';
            });
        }
    });
};

const fecharModal = () => {
    let myModalEl = document.getElementById('jqueryModal');
    let modal = bootstrap.Modal.getOrCreateInstance(myModalEl);
    modal.hide();
};
