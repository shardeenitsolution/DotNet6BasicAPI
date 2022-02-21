// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const apiUrl = 'https://localhost:7261/api';

access_token = getCookie("token");

function postForm(form, passUrl) {
    const formData = new FormData(form)
    $.ajax({
        url: apiUrl + passUrl,
        method: 'POST',
        data: JSON.stringify(Object.fromEntries(formData)),
        contentType: 'application/json',

    }).done(function (data) {
        if (window.location.pathname == '/Authenticate/Login') {
            setCookie("token", data.token, data.expiration);
        }
        alert("Success");
        return data;

    }).fail(function (data) {
        alert('Error : (' + data.responseJSON.message + '). Please try later.');
    });
}

function authPostForm(form, passUrl) {
    const formData = new FormData(form)
    $.ajax({
        url: apiUrl + passUrl,
        beforeSend: function (jqXHR) {
            jqXHR.setRequestHeader("Authorization", "Bearer " + access_token);
        },
        method: 'POST',
        data: JSON.stringify(Object.fromEntries(formData)),
        contentType: 'application/json',

    }).done(function (data) {
        if (window.location.pathname == '/Authenticate/Login') {
            setCookie(`${$("#username").val()}_token`, data.token, data.expiration);
        }
        alert("Success");
        return data;

    }).fail(function (data) {
        alert('Error : (' + data.responseJSON.message + '). Please try later.');
    });
}

function onPut(obj, passUrl) {
    const formData = new FormData(obj)
    $.ajax({
        url: apiUrl + '/StatesApi/' + passUrl,
        method: 'PUT',
        data: JSON.stringify(Object.fromEntries(formData)),
        contentType: 'application/json',

    }).done(function (data) {
        alert("Success");

        return data;

    }).fail(function (data) {
        alert('Error : (' + data.responseJSON.message + '). Please try later.');
    });

}

function onDelete(passUrl) {
    $.ajax({
        url: apiUrl + '/StatesApi/' + passUrl,
        method: 'DELETE',
    }).done(function (data) {
        alert("Success");
        return data;

    }).fail(function (data) {
        alert('Error : (' + data.responseJSON.message + '). Please try later.');
    });

}

function setCookie(cname, cvalue, exdays) {
    const d = new Date(exdays);
    //d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            console.log(c.substring(name.length, c.length))
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function checkCookie() {
    let user = getCookie(`${$("#username").val()}_token`);
    if (user != "") {
        alert("Welcome again " + $("#username").val());
    } else {
        alert("Invalid Token");
    }
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


