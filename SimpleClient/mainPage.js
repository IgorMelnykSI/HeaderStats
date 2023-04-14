
function fetchQ1() {

    document.getElementById("ServerResponseID").innerHTML =
        "<iframe src=\"http://localhost:2000/api/Q1\"></iframe>";
}

function fetchQ2() {
    document.getElementById("ServerResponseID").innerHTML =
        "<iframe src=\"http://localhost:2000/api/Q2\"></iframe>";
}

function fetchQ3() {
    document.getElementById("ServerResponseID").innerHTML =
        "<iframe src=\"http://localhost:2000/api/Q3\"></iframe>";
}

function clearFrame(){
    let parent = document.getElementById("ServerResponseID");
    while (parent.firstChild) {
        parent.removeChild(parent.firstChild);
    }
}