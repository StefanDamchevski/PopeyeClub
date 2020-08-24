
window.onscroll = function () { stickyNavbar() };

let navbar = document.getElementById("navbar");

let sticky = navbar.offsetTop;

function stickyNavbar() {
    if (window.pageYOffset > sticky) {
        navbar.classList.add("sticky")
    } else {
        navbar.classList.remove("sticky");
    }
}

function searchUsers() {
    let input = document.getElementById("searchInput").value.toLowerCase().trim();

    let userRows = document.getElementsByClassName("user-row");

    if (input == "") {
        document.getElementById("closeBtn").click();
    } else {
        document.getElementById("toggleBtn").click();
    }

    for (let i = 0; i < userRows.length; i++) {
        let td = userRows[i].getElementsByTagName("td")[1];

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


let postBtn = document.getElementById("postBtn");
let fileBtn = document.getElementById("fileBtn");
let submitPost = document.getElementById("submitPost");

postBtn.addEventListener('click', function () {
    fileBtn.click();
});

fileBtn.addEventListener('change', function () {
    submitPost.click();
});