// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const apiUrl = 'https://localhost:7261/api';

function createState(form) {
    $.ajax({
        url: apiUrl + `/StatesApi?name=asdsf`,
        method: 'POST',
        //data: JSON.stringify({ name: 'jayesh' }),
        data: "{ name='jayesh' }",
        contentType: 'application/json',

    }).done(function (data) {
        if (callback)
            alert("Success");
        else {
            return data;
        }
    }).fail(function (data) {
        alert('Error : (' + data.responseJSON.message + '). Please try later.');
    });
}