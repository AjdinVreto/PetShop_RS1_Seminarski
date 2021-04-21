// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var connection = new signalR.HubConnectionBuilder().withUrl("/myhub").build();

connection.on("ReceiveMessage", function (user, message) {
    swal("Poštovani " + user + ", novi proizvod je u našoj ponudi ! " + message);
});

connection.start().then(function () {
    console.info("SignalR started");
}).catch(function (err) {
    return console.error(err.toString());
});
