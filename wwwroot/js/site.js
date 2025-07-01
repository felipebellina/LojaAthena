// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code


//const mp = new MercadoPago('TEST-63456d9b-ebca-46e1-a590-1d1da378d629');

//const cardNumberElement = mp.fields.create('cardNumber', {
//    placeholder: "Número do cartão"
//}).mount('form-checkout__cardNumber');
//const expirationDateElement = mp.fields.create('expirationDate', {
//    placeholder: "MM/YY",
//}).mount('form-checkout__expirationDate');
//const securityCodeElement = mp.fields.create('securityCode', {
//    placeholder: "Código de segurança"
//}).mount('form-checkout__securityCode');

//(async function getIdentificationTypes() {
//    try {
//        const identificationTypes = await mp.getIdentificationTypes();
//        const identificationTypeElement = document.getElementById('form-checkout__identificationType');

//        createSelectOptions(identificationTypeElement, identificationTypes);
//    } catch (e) {
//        return console.error('Error getting identificationTypes: ', e);
//    }
//})();

//function createSelectOptions(elem, options, labelsAndKeys = { label: "name", value: "id" }) {
//    const { label, value } = labelsAndKeys;

//    elem.options.length = 0;

//    const tempOptions = document.createDocumentFragment();

//    options.forEach(option => {
//        const optValue = option[value];
//        const optLabel = option[label];

//        const opt = document.createElement('option');
//        opt.value = optValue;
//        opt.textContent = optLabel;

//        tempOptions.appendChild(opt);
//    });

//    elem.appendChild(tempOptions);
//}

//const paymentMethodElement = document.getElementById('paymentMethodId');
//const issuerElement = document.getElementById('form-checkout__issuer');
//const installmentsElement = document.getElementById('form-checkout__installments');

//const issuerPlaceholder = "Banco emissor";
//const installmentsPlaceholder = "Parcelas";

//let currentBin;
//cardNumberElement.on('binChange', async (data) => {
//    const { bin } = data;
//    try {
//        if (!bin && paymentMethodElement.value) {
//            clearSelectsAndSetPlaceholders();
//            paymentMethodElement.value = "";
//        }

//        if (bin && bin !== currentBin) {
//            const { results } = await mp.getPaymentMethods({ bin });
//            const paymentMethod = results[0];

//            paymentMethodElement.value = paymentMethod.id;
//            updatePCIFieldsSettings(paymentMethod);
//            updateIssuer(paymentMethod, bin);
//            updateInstallments(paymentMethod, bin);
//        }

//        currentBin = bin;
//    } catch (e) {
//        console.error('error getting payment methods: ', e)
//    }
//});

//function clearSelectsAndSetPlaceholders() {
//    clearHTMLSelectChildrenFrom(issuerElement);
//    createSelectElementPlaceholder(issuerElement, issuerPlaceholder);

//    clearHTMLSelectChildrenFrom(installmentsElement);
//    createSelectElementPlaceholder(installmentsElement, installmentsPlaceholder);
//}

//function clearHTMLSelectChildrenFrom(element) {
//    const currOptions = [...element.children];
//    currOptions.forEach(child => child.remove());
//}

//function createSelectElementPlaceholder(element, placeholder) {
//    const optionElement = document.createElement('option');
//    optionElement.textContent = placeholder;
//    optionElement.setAttribute('selected', "");
//    optionElement.setAttribute('disabled', "");

//    element.appendChild(optionElement);
//}

//// Esta etapa melhora as validações cardNumber e securityCode
//function updatePCIFieldsSettings(paymentMethod) {
//    const { settings } = paymentMethod;

//    const cardNumberSettings = settings[0].card_number;
//    cardNumberElement.update({
//        settings: cardNumberSettings
//    });

//    const securityCodeSettings = settings[0].security_code;
//    securityCodeElement.update({
//        settings: securityCodeSettings
//    });
//}

//async function updateIssuer(paymentMethod, bin) {
//    const { additional_info_needed, issuer } = paymentMethod;
//    let issuerOptions = [issuer];

//    if (additional_info_needed.includes('issuer_id')) {
//        issuerOptions = await getIssuers(paymentMethod, bin);
//    }

//    createSelectOptions(issuerElement, issuerOptions);
//}

//async function getIssuers(paymentMethod, bin) {
//    try {
//        const { id: paymentMethodId } = paymentMethod;
//        return await mp.getIssuers({ paymentMethodId, bin });
//    } catch (e) {
//        console.error('error getting issuers: ', e)
//    }
//};

//async function updateInstallments(paymentMethod, bin) {
//    try {
//        const installments = await mp.getInstallments({
//            amount: document.getElementById('transactionAmount').value,
//            bin,
//            paymentTypeId: 'credit_card'
//        });
//        const installmentOptions = installments[0].payer_costs;
//        const installmentOptionsKeys = { label: 'recommended_message', value: 'installments' };
//        createSelectOptions(installmentsElement, installmentOptions, installmentOptionsKeys);
//    } catch (error) {
//        console.error('error getting installments: ', e)
//    }
//}

//const formElement = document.getElementById('form-checkout');
//formElement.addEventListener('submit', createCardToken);

//async function createCardToken(event) {
//    try {
//        const tokenElement = document.getElementById('token');
//        if (!tokenElement.value) {
//            event.preventDefault();
//            const token = await mp.fields.createCardToken({
//                cardholderName: document.getElementById('form-checkout__cardholderName').value,
//                identificationType: document.getElementById('form-checkout__identificationType').value,
//                identificationNumber: document.getElementById('form-checkout__identificationNumber').value,
//            });
//            tokenElement.value = token.id;
//            formElement.requestSubmit();
//        }
//    } catch (e) {
//        console.error('error creating card token: ', e)
//    }
//}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


/*
const mp = new MercadoPago('TEST-63456d9b-ebca-46e1-a590-1d1da378d629');

const cardForm = mp.cardForm({
    amount: "100.5",
    iframe: true,
    form: {
        id: "form-checkout",
        cardNumber: {
            id: "form-checkout__cardNumber",
            placeholder: "Número do cartão",
        },
        expirationDate: {
            id: "form-checkout__expirationDate",
            placeholder: "MM/YY",
        },
        securityCode: {
            id: "form-checkout__securityCode",
            placeholder: "Código de segurança",
        },
        cardholderName: {
            id: "form-checkout__cardholderName",
            placeholder: "Titular do cartão",
        },
        issuer: {
            id: "form-checkout__issuer",
            placeholder: "Banco emissor",
        },
        installments: {
            id: "form-checkout__installments",
            placeholder: "Parcelas",
        },
        identificationType: {
            id: "form-checkout__identificationType",
            placeholder: "Tipo de documento",
        },
        identificationNumber: {
            id: "form-checkout__identificationNumber",
            placeholder: "Número do documento",
        },
        cardholderEmail: {
            id: "form-checkout__cardholderEmail",
            placeholder: "E-mail",
        },
    },
    callbacks: {
        onFormMounted: error => {
            if (error) return console.warn("Form Mounted handling error: ", error);
            console.log("Form mounted");
        },
        onSubmit: event => {
            event.preventDefault();

            const {
                paymentMethodId: payment_method_id,
                issuerId: issuer_id,
                cardholderEmail: email,
                amount,
                token,
                installments,
                identificationNumber,
                identificationType,
            } = cardForm.getCardFormData();

            fetch("/Pagamento/CreatePayment", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    token,
                    paymentMethodId: payment_method_id,
                    issuerId: issuer_id,
                    transactionAmount: Number(amount),
                    installments: Number(installments),
                    description: "Descrição do produto",
                    payer: {
                        email,
                        identification: {
                            type: identificationType,
                            number: identificationNumber,
                        },
                    },
                }),
            });
        },
        onFetching: (resource) => {
            console.log("Fetching resource: ", resource);

            // Animate progress bar
            const progressBar = document.querySelector(".progress-bar");
            progressBar.removeAttribute("value");

            return () => {
                progressBar.setAttribute("value", "0");
            };
        }
    },
});*/

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
