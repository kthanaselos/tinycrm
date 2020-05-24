// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function addProductToServer() {
    actionMethod = "POST"
    actionUrl = "/product/AddProduct"
    sendData = {
        "ProductId": $('#ProductId').val(),
        "Name": $('#Name').val(),
        "Description": $('Description').val(),
        //"Price": $('#Price').val()
        Price: parseFloat($('#Price').val())
    }
    alert(JSON.stringify(sendData))

    $.ajax({
        url: actionUrl,
        dataType: 'json',
        type: actionMethod,
        data: JSON.stringify(sendData),

        contentType: 'application/json',
        processData: false,
        success: function (data, textStatus, jQxhr) {
            $('#responseDiv').html(JSON.stringify(data));
        },
        error: function (jqxhr, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    });
}

function addCustomer() {
    actionMethod = "POST"
    actionUrl = "/api/createCustomer"
    sendData = {
        "FirstName": $('#FirstName').val(),
        "LastName": $('#LastName').val(),
        "Vatnumber": $('#Vatnumber').val()
    }

    $.ajax({
        url: actionUrl,
        dataType: 'json',
        type: actionMethod,
        data: JSON.stringify(sendData),

        contentType: 'application/json',
        processData: false,
        success: function (data, textStatus, jQxhr) {
            $('#responseDiv').html(JSON.stringify(data));
        },
        error: function (jqXhr, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    });

}
