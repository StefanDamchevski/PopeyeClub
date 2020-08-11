function searchUsers() {
    let input = document.getElementById("searchInput").value.toLowerCase().trim();

    let userRows = document.getElementsByClassName("user-row");

    if (input == "") {
        document.getElementById("closeBtn").click();
    } else {
        document.getElementById("toggleBtn").click();
    }

    for (let i = 0; i < userRows.length; i++) {
        let td = userRows[i].getElementsByTagName("td")[0];
        let textValue = td.innerText.toLowerCase().trim();

        if (textValue.indexOf(input) == -1 || input == "") {
            userRows[i].style.display = "none"
        }
        else {
            document.getElementById("listOfUsers").style.display = "block";
            userRows[i].style.display = "";
        }
    }
}