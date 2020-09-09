
let connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

let _connectionId = "";

connection.start()
    .then(function () {
        connection.invoke("getConnectionId")
            .then(function (connectionId) {
                _connectionId = connectionId;
                joinPopeyeClub('PopeyeClub');
            })
    })
    .catch(function (err) {
        console.log(err);
    });

function joinPopeyeClub(applicationName) {
    axios.post(`/Notification/OpenConnection/?connectionId=${_connectionId}&applicationName=${applicationName}`)
        .then(function (response) {
            console.log('connected');
        })
        .catch(function (error) {
            console.log(error);
        });
}

connection.on('RecieveNotification', function () {
    let notificationBtn = document.getElementById('notificationButton');
    notificationBtn.classList.remove('text-dark');
    notificationBtn.classList.add('text-danger');
});