// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
back.onclick = function () {
    console.log("работает");
    if (document.getElementById("oldTitle").value != document.getElementById("newTitle").value || document.getElementById("oldDesc").value != document.getElementById("newDesc").value) {
        console.log("отличаются");
    } else {
        console.log("идентичны");
    }
}