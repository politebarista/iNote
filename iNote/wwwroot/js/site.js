// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
back.onclick = function () {
    console.log("работает");
    if (document.getElementById("oldTitle").value != document.getElementById("newTitle").value || document.getElementById("oldDesc").value != document.getElementById("newDesc").value) {
        console.log("отличаются");
        document.getElementById("confirm").classList.toggle("d-none");
    } else {
        console.log("идентичны");
        window.location.href = '/Notes/View/' + document.getElementById("id").value;
    }
}
saveChanges.onclick = function () {
    document.getElementById("sendRequest").click();
}
discardChanges.onclick = function () {
    window.location.href = '/Notes/View/' + document.getElementById("id").value;
}