function postData(url, data) {
    var headers = new Headers();
    headers.append('Content-Type', 'application/json');
    headers.append('Accept', 'application/json');

    return fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        },
        body: JSON.stringify(data),
    }).then((response) => {
        return response.json();
    });
}

function getData(url) {
    return fetch(url).then(response => {
        return response.json();
    });
}

function createTimeQuery() {
    var tz = document.getElementById("timeZoneSelect").value;
    var data = { "TimeZone": tz };
    postData("http://localhost:53579/home/createtimequery", data).then((response) => {
        var currTime = document.getElementById("currentTime");
        currTime.innerHTML = moment(response.Time).format("MM/DD/YYYY hh:mm:ss A");
        currTime.parentNode.classList.remove("d-none");

        var timeQueryTable = document.getElementById("timeQueryTable");
        var row = document.getElementById("hiddenRow").cloneNode(true);
        row.getElementsByClassName("time")[0].innerHTML = moment(response.Time).format("MM/DD/YYYY hh:mm:ss A");
        row.getElementsByClassName("tz")[0].innerHTML = response.TimeZoneName;
        row.getElementsByClassName("ip")[0].innerHTML = response.ClientIp;
        row.getElementsByClassName("UTC")[0].innerHTML = moment(response.UTCTime).utc().format("MM/DD/YYYY hh:mm:ss A");
        row.removeAttribute("class");
        row.removeAttribute("id");

        timeQueryTable.appendChild(row);
    });
}

function formatDate(dateString) {
    return moment(dateString).format("MM/DD/YYYY hh:mm:ss A");
}