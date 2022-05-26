const carregando = (exibir) => {
    let load = document.querySelector('.load');
    if (exibir) load.classList.add('load-ativo');
    else load.classList.remove('load-ativo');
};

const iniciarLoading = (load) => {
    if (!load) load = '#loadGenerico';
    document.querySelector(load).style.visibility = 'visible';
};
const pararLoading = (load) => {
    if (!load) load = '#loadGenerico';
    document.querySelector(load).style.visibility = 'hidden';
};

const carregarAjax = (link, id, funcao) => {
    carregando(true);
    $(`#${id}`).load(link, function (response, status, xhr) {
        carregando(false);
        if (status == "error") {
            var msg = "Desculpa, não conseguir carregar os dados";
            toastErro(msg);
        } else {
            iniciarComponentes();
            if(funcao) funcao();
        }
    });
};

const iniciarComponentes = () => {
    ativarLinkModal();

    $('.dataTable').each(function () {
        if (!$(this).attr('data-inicializada')) {
            $(this).DataTable({
                language: {
                    url: '../../lib/DataTables/pt_br.json'
                },
                order: [],
                lengthChange: false,
                pageLength: 10,
                dom: '<"row"<"col-6"f><"col-6">>rt<"row"<"col-6"i><"col-6"p>>'
            });
            $(this).attr('data-inicializada', true);
        }
    });
       

    $('.select2').select2({
        placeholder: {
            id: '-1', // the value of the option
            text: "Selecione uma opção"
        },
        allowClear: true
    });
};

