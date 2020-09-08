let connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

let _connectionId = "";

connection.start()
    .then(function () {
        connection.invoke("getConnectionId")
            .then(function (connectionId) {
                _connectionId = connectionId;
            })
    })
    .catch(function (err) {
        console.log(err);
    });