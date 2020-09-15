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
            document.getElementById("listOfUsers").style.paddingRight = 0;
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

function getNotifications() {
    let notificationBtn = document.getElementById('notificationButton');

    if (notificationBtn.classList.contains('text-danger')) {
        notificationBtn.classList.add('text-dark');
        notificationBtn.classList.remove('text-danger');
    }

        axios.get('/Notification/Overview')
            .then(function (response) {
                renderNotifications(response.data);
            })
            .catch(function (error) {
                console.log(error);
            });
}

function renderNotifications(data) {

    let modalBody = document.getElementById("notificationList");

    if (data.length > 0) {

            let table = document.createElement('table');
            table.id = "notificationTable";
            table.classList.add('table');
            modalBody.appendChild(table);

            for (let i = 0; i < data.length; i++) {
                let tr = document.createElement('tr');
                table.appendChild(tr);

                let imageCell = document.createElement('td');
                tr.appendChild(imageCell);

                let profileLink = document.createElement('a');
                profileLink.href = "/User/Profile?userId=" + data[i].userFromId
                imageCell.appendChild(profileLink);

                let image = document.createElement('img');
                image.src = "data:image/jpeg;base64," + data[i].userFromImage;
                image.style.width = "18px";
                image.style.height = "18px";
                image.style.cursor = "pointer";
                image.style.borderRadius = "100%";
                profileLink.appendChild(image);

                let notMessageCell = document.createElement('td');
                notMessageCell.innerText = data[i].notificationMessage;
                tr.appendChild(notMessageCell);

                let dateSentCell = document.createElement('td');
                dateSentCell.innerText = data[i].dateSent;
                tr.appendChild(dateSentCell);

                if (data[i].type == 1) {
                    let statusCell = document.createElement('td');
                    tr.appendChild(statusCell);

                    if (data[i].status == 0) {
                        let acceptBtn = document.createElement('a');
                        acceptBtn.href = '/Follow/Accept?userId=' + data[i].userFromId;
                        acceptBtn.classList.add('btn');
                        acceptBtn.classList.add('btn-success');
                        acceptBtn.classList.add('btn-sm');
                        acceptBtn.innerHTML = 'Accept';
                        statusCell.appendChild(acceptBtn);

                        let splitter = document.createElement('span');
                        splitter.innerHTML = ' | ';
                        statusCell.appendChild(splitter);

                        let declineBtn = document.createElement('a');
                        declineBtn.href = '/Follow/Decline?userId=' + data[i].userFromId;
                        declineBtn.classList.add('btn');
                        declineBtn.classList.add('btn-danger');
                        declineBtn.classList.add('btn-sm');
                        declineBtn.innerHTML = 'Decline';
                        statusCell.appendChild(declineBtn);

                    } else if (data[i].status == 1) {

                        let followBackBtn = document.createElement('a');
                        followBackBtn.href = '/Follow/SendFollowRequset?userId=' + data[i].userFromId;
                        followBackBtn.classList.add('btn');
                        followBackBtn.classList.add('btn-primary');
                        followBackBtn.classList.add('btn-sm');
                        followBackBtn.innerHTML = 'Follow Back';
                        statusCell.appendChild(followBackBtn);

                    } else {

                        let checkedIcon = document.createElement('i');
                        checkedIcon.classList.add('fas');
                        checkedIcon.classList.add('fa-check');
                        checkedIcon.classList.add('text-success');
                        checkedIcon.style.fontSize = "1em";
                        statusCell.appendChild(checkedIcon);

                    }
                }
            }

    } else {
        let noNots = document.createElement('div');
        noNots.classList.add('not-available');
        modalBody.appendChild(noNots);

        let defaultMessage = document.createElement('p');
        defaultMessage.innerHTML = "No notifications to show";
        noNots.appendChild(defaultMessage);
    }
}

function removeAllChildElements() {
    let notificationList = document.getElementById("notificationList");

    if (notificationList.childElementCount > 0) {
        while (notificationList.firstChild) {
            notificationList.removeChild(notificationList.lastChild);
        }
    }
}