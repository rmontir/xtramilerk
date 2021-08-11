// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 5000
});

let swalSuccess = (text) => Toast.fire({
    icon: 'success',
    html: text
});

let swalError = (text) => Toast.fire({
    icon: 'error',
    html: text
});