function updateScroll() {
    let element = document.getElementById("chatMessages");
    element.scrollTop = element.scrollHeight;
}

function disableButton() {
    let submitBtn = document.getElementById("submitMessageBtn");

    if (submitBtn != null) {
        submitBtn.disabled = true;
    }
}

function enableBtn() {
    let submitBtn = document.getElementById("submitMessageBtn");
    let chatMessageInput = document.getElementById("chatMessageInput").value.trim();

    if (chatMessageInput != "") {
        submitBtn.disabled = false;
    } else {
        submitBtn.disabled = true;
    }
}

disableButton();
updateScroll();

function showUsers() {

    let searchInput = document.getElementById("searhUserInput").value.toLowerCase().trim();

    let tableRows = document.getElementsByClassName("table-rows");

    for (let i = 0; i < tableRows.length; i++) {
        let td = tableRows[i].getElementsByTagName("td")[1];

        let tdTextValue = td.innerText.toLowerCase().trim();

        if (tdTextValue.indexOf(searchInput) == -1 || searchInput == "") {
            tableRows[i].style.display = "none";
        } else {
            tableRows[i].style.display = "";
        }
    }
}