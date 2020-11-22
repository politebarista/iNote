console.log("JSX запущен;"); // по сути нужен только для отладки, но я оставил

const element = (
    <div id="confirm">
        <div className="confirm-window">
            Были внесены изменения. Сохранить их?
            <button type="button" className="btn btn-success" id="saveChanges">Да</button>
            <button type="button" className="btn btn-warning" id="discardChanges">Нет</button>
        </div>
    </div>
);

const oldTitle = document.getElementById("Title").value; // запрашиваем неизмененные данные для дальнейшего использования
const oldDesc = document.getElementById("Desc").value;


back.onclick = function () {
    if (oldTitle != document.getElementById("Title").value || oldDesc != document.getElementById("Desc").value) { // сравниваем старые данные с только что введенными 
        console.log("Данные отличаются;");

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
        console.log("Данные идентичны;");
        window.location.href = '/Notes/View/' + document.getElementById("id").value;
    }
}
