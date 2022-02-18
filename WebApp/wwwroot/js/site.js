// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const apiUrl = 'https://localhost:7261/api';

function postForm(form,appendUrl) {
   
    const formData = new FormData(form)

    $.ajax({
        url: apiUrl + appendUrl,
        method: 'POST',
        data: JSON.stringify(Object.fromEntries(formData)),
        contentType: 'application/json',

    }).done(function (data) {
        alert("Success");
        return data;

    }).fail(function (data) {
        alert('Error : (' + data.responseJSON.message + '). Please try later.');
    });
}


    //    fetch(url, {
    //        method: "POST",
    //        headers: { "Content-Type": "application/json" },
    //        body: JSON.stringify({ name: 'shubham' })
    //    }).then(() => {
    //        alert("New Blog Added");
    //    })
    //}


    //const form = document.querySelector('form')
    //form.addEventListener('submit', (e) => {
    //    e.preventDefault()
    //    url = apiUrl + `/StatesApi`;
    //    const formData = new FormData(e.target)
    //    const data = JSON.stringify(Object.fromEntries(formData));
    //    console.log(Object.fromEntries(formData));
    //    console.log(data);
    //    console.log(JSON.stringify({ "name": "pawan582" }))
    //    fetch(url, {
    //        method: "POST",
    //        headers: { "Content-Type": "application/json" },

    //        //body: JSON.stringify({ "name": "pawan582" })
    //        body: data
    //        //body: Object.fromEntries(formData)
    //    }).then(() => {
    //        alert("New Blog Added");
    //    })

    //})


