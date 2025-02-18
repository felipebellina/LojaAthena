// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code

$(document).ready(function () {
    function limparCampos() {
        $('#Logradouro').val('');
        $('#Bairro').val('');
        $('#Localidade').val('');
        $('#Estado').val('');
    }

    $('#Cep').on('blur', function () {
        var cep = $(this).val();
        console.log("CEP digitado: " + cep);
        if (cep.length == 8) { 
            $.ajax({
                url: '/Endereco/GetEndereco',
                type: 'GET',
                data: { cep: cep },
                success: function (data) {
                    console.log("Dados recebidos: ", data);
                    if (data.logradouro) {
                        $('#Logradouro').val(data.logradouro);
                        console.log("Logradouro atualizado: " + data.logradouro);
                    } else {
                        limparCampos();
                        console.log("Logradouro não encontrado no retorno");
                    }
                    $('#Bairro').val(data.bairro || '');
                    $('#Localidade').val(data.localidade || '');
                    $('#Estado').val(data.uf || '');
                },
                error: function (xhr, status, error) {
                    console.error("Erro: ", error);
                    limparCampos();
                    alert('Erro ao buscar endereço');
                }
            });
        } else {
            limparCampos();
        }
    });
});
