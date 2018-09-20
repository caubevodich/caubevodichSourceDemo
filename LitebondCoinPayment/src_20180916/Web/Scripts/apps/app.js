$(document).ready(function () {

    getLtcPriceUSD();
    $('#btnCreateLtcAddressWallet').on('click', function () {
        createLtcAddressWallet();
    });
    $('#btnGetBalanceLTC').on('click', function () {
        getBalance();
    });
    $('#btnWidthdraw').on('click', function () {
        widthdraw();
    });

    createQrCodeAddress();

    $('#btnPurchase').on('click', function () {
        InsertDeposit();
    });

    getBalanceOfUser();
    getReferralLinkOfUser();



});

function getLtcPriceUSD() {
    $.get('https://chain.so/api/v2/get_price/LTC/USD', function (response) {
        const priceLtc = response.data.prices[0].price;
        $('#ltcPrice').text('$ ' + priceLtc);
        $('#ltcPrice2').text('$ ' + priceLtc);
        $('#ltcPrice3').text('$ ' + priceLtc);
    });
}

function getConfirm(id) {
    $.ajax({
        type: "POST",
        url: '/wallet/GetTxtConfirmed',
        data: { transactionId: id },
        success: function (response) {
            swal({
                title: 'Successfully',
                type: 'success',
                confirmButtonText: 'Ok'
            }).then(() => {
                window.location.reload();
            });
        },
        error: function (message) {
            swal({
                title: 'Successfully',
                type: 'success',
                confirmButtonText: 'Ok'
            }).then(() => {
                window.location.reload();
            });
        }
    });
}

function getBalance() {
    $.ajax({
        type: "POST",
        url: '/wallet/getBalance',
        success: function (response) {
            swal({
                title: 'Successfully',
                type: 'success',
                confirmButtonText: 'Ok'
            }).then(() => {
                window.location.reload();
            });
        },
        error: function (message) {
            swal({
                title: 'Successfully',
                type: 'error',
                confirmButtonText: 'Ok'
            }).then(() => {
                window.location.reload();
            });
        }
    });
}

function widthdraw() {
    var two_factor = $('#TwoFactor').val();
    var amount = $('#Amount').val();
    var address = $('#LtcAddress').val();
    console.log(amount);
    $.ajax({
        type: "POST",
        url: '/wallet/widthdraw',
        data: { address: address, amount: amount, twoFactor: two_factor },
        success: function (response) {
            if (response.IsError == true) {
                swal({
                    title: response.message,
                    type: 'success',
                    confirmButtonText: 'Ok'
                }).then(() => {
                    window.location.reload();
                });
            } else {
                swal("Fail !", response.error, "error");
                swal({
                    type: "error",
                    title: "Fail !",
                    text: response.error,
                    footer: '<a href>Please try again !</a>'
                })
            }
        },
        error: function (message) {
        }
    });
}

function createLtcAddressWallet() {
    $.ajax({
        type: "POST",
        url: '/wallet/getNewAddress',
        success: function (response) {
            swal({
                title: 'Successfully',
                type: 'success',
                confirmButtonText: 'Ok'
            }).then(() => {
                window.location.reload();
            });
        },
        error: function (message) {
        }
    });
}

function UpdateStatusNetworkIncome(id, amount) {

    swal({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, receive it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: '/comission/updateStatusNetworkIncomeAndGetIncome',
                data: { recordId: id, Status: true, amount: amount },
                success: function (response) {
                    swal({
                        title: 'Successfully',
                        type: 'success',
                        confirmButtonText: 'Ok'
                    }).then(() => {
                        window.location.reload();
                    });
                },
                error: function (message) {
                }
            });
        }
    })
    
}

function UpdateStatusReferralIncome(id, amount) {

    swal({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, receive it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: '/comission/updateStatusReferralAndGetIncome',
                data: { recordId: id, Status: true, amount: amount },
                success: function (response) {
                    swal({
                        title: 'Successfully',
                        type: 'success',
                        confirmButtonText: 'Ok'
                    }).then(() => {
                        window.location.reload();
                    });
                },
                error: function (message) {
                }
            });
        }
    })


}

function UpdateStatusReceiveProfit(id, amount) {
    swal({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, receive it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: '/comission/updateStatusReceiveProfit',
                data: { recordId: id, Status: true, amount: amount },
                success: function (response) {
                    swal({
                        title: 'Successfully',
                        type: 'success',
                        confirmButtonText: 'Ok'
                    }).then(() => {
                        window.location.reload();
                    });
                },
                error: function (message) {
                }
            });
        }
    })
}

function InsertDeposit(amount) {
    var amount = $('#amountPurchase').val();
    $.ajax({
        type: "POST",
        url: '/comission/insertDepositToData',
        data: { amount: parseFloat(amount) },
        success: function (response) {
            if (response.IsError == true) {
                swal({
                    title: response.message,
                    type: 'success',
                    confirmButtonText: 'Ok'
                }).then(() => {
                    window.location.reload();
                });
            } else {
                swal("Fail !", response.message, "error");
                swal({
                    type: "error",
                    title: "Fail !",
                    text: response.message,
                    footer: '<a href>Please try again !</a>'
                }).then(() => {
                    window.location.reload();
                });
            }
        },
        error: function (message) {
            console.log(message);
        }
    });
}

function getIncomeToBalance() {
    $.ajax({
        type: "POST",
        url: '/comission/getToBalanceNetworkIncome',
        success: function (response) {
            if (response.IsError == true) {
                swal({
                    title: response.message,
                    type: 'success',
                    confirmButtonText: 'Ok'
                }).then(() => {
                    window.location.reload();
                });
            } else {
                swal("Fail !", response.message, "error");
                swal({
                    type: "error",
                    title: "Fail !",
                    text: response.message,
                    footer: '<a href>Please try again !</a>'
                })
            }
        },
        error: function (message) {
        }
    });
}

function getProfitToBalance() {
    $.ajax({
        type: "POST",
        url: '/comission/getToBalanceProfitIncome',
        success: function (response) {
            if (response.IsError == true) {
                swal({
                    title: response.message,
                    type: 'success',
                    confirmButtonText: 'Ok'
                }).then(() => {
                    window.location.reload();
                });
            } else {
                swal("Fail !", response.message, "error");
                swal({
                    type: "error",
                    title: "Fail !",
                    text: response.message,
                    footer: '<a href>Please try again !</a>'
                })
            }
        },
        error: function (message) {
        }
    });
}

function getBalanceOfUser() {

    $.ajax({
        type: "POST",
        url: '/dashboard/GetBalanceOfUser',
        success: function (response) {
            const litecoinBalance = response;
            $('#litecoinBalance').text(litecoinBalance);
            $('#litecoinBalance2').text(litecoinBalance);
        },
        error: function (message) {
        }
    });

}

function getReferralLinkOfUser() {

    $.ajax({
        type: "POST",
        url: '/dashboard/GetReferralLink',
        success: function (response) {
            const ReferralLinkUser = response;
            $('#ReferralLinkUser').text(ReferralLinkUser);
            $('#ReferralLinkUser2').text(ReferralLinkUser);
        },
        error: function (message) {
        }
    });

}



function createQrCodeAddress() {
    var __ltcAddress = $('#__ltcAddress').val();

    var options = {
        // render method: 'canvas' or 'image'
        render: 'canvas',

        // render pixel-perfect lines
        crisp: true,

        // minimum version: 1..40
        minVersion: 1,

        // error correction level: 'L', 'M', 'Q' or 'H'
        ecLevel: 'L',

        // size in pixel
        size: 150,

        // pixel-ratio, null for devicePixelRatio
        ratio: null,

        // code color
        fill: '#333',

        // background color
        back: '#fff',

        // content
        text: __ltcAddress,

        // roundend corners in pc: 0..100
        rounded: 0,

        // quiet zone in modules
        quiet: 0,

        // modes: 'plain', 'label' or 'image'
        mode: 'plain',

        // label/image size and pos in pc: 0..100
        mSize: 30,
        mPosX: 50,
        mPosY: 50,

        // label
        label: 'no label',
        fontname: 'sans',
        fontcolor: '#333',

        // image element
        image: null
    };

    var container = $('#qrcode');
    var qrcode = kjua(options);

    container.html(qrcode);
}
