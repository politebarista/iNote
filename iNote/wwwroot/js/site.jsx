console.log("JSX работает");
const element = (
    <div id="confirm">
        <div className="confirm-window">
            Были внесены изменения. Сохранить их?
            <button type="button" className="btn btn-success" id="saveChanges">Да</button>
            <button type="button" className="btn btn-warning" id="discardChanges">Нет</button>
        </div>
    </div>
);

back.onclick = function () {
    if (document.getElementById("oldTitle").value != document.getElementById("newTitle").value || document.getElementById("oldDesc").value != document.getElementById("newDesc").value) {
        console.log("Данные отличаются");
        ReactDOM.render(
            element,
            document.getElementById('content')
        );

        saveChanges.onclick = function () {
            document.getElementById("sendRequest").click();
        }

        discardChanges.onclick = function () {
            window.location.href = '/Notes/View/' + document.getElementById("id").value;
        }
    } else {
        console.log("Данные идентичны");
        window.location.href = '/Notes/View/' + document.getElementById("id").value;
    }
}
