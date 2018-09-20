$(document).ready(function () {
    createQrCodeAddress();

    $('#btnCreateLtcAddressWallet').on('click', function () {
        createLtcAddressWallet();
    });
    $('#btnGetBalanceLTC').on('click', function () {
        getBalance();
    });
    $('#btnWidthdraw').on('click', function () {
        widthdraw();
    });    
})

function getConfirm(id) {
    $.ajax({
        type: "POST",
        url: '/wallet/GetTxtConfirmed',
        data: { transactionId : id},
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
            swal({
                title: 'Successfully',
                type: 'success',
                confirmButtonText: 'Ok'
            }).then(() => {
             //   window.location.reload();
            });
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
        size: 250,

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