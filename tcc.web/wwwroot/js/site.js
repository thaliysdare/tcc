

//https://select2.org/configuration/options-api
//<select class="form-control select2 select2-hidden-accessible" style="width: 100%;" tabindex="-1" aria-hidden="true">
$(document).ready(function () {
    $('.dataTable').DataTable({
        language: {
            url: '../../lib/DataTables/pt_br.json'
        }
    });

    $('.select2').select2({
        placeholder: {
            id: '-1', // the value of the option
            text: "Selecione uma opção"
        },
        allowClear: true
    });
});

