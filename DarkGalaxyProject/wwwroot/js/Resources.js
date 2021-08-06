var connection = new signalR.HubConnectionBuilder()
    .withUrl("/resourcehub")
    .build();

connection.on("GetUpdatedResources", function (quantity) {
        $("#resources").append(quantity);
});

window.onload = function () {
    startConnection;
    connection.invoke("GetUpdatedResources", "013d18a2-0a81-4320-a0b0-2d4196f29a37");
}

var startConnection = function () {
    connection.start().catch(function (err) {
        return console.error(err.toString());
    });
}