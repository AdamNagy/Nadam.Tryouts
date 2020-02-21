function deleteManifest(btn, fileTitle) {
    
    fileTitle = fileTitle.split('.')[0];
    var url = "api/dispatch";
    var xhr = new XMLHttpRequest();
    xhr.open("POST", url, true);
    xhr.setRequestHeader("Content-Type", "application/json");

    // console.log(fileTitle);

    xhr.onload = function () {
        if (xhr.readyState === 4 && xhr.status === "200") {
            console.table("good");
        } else {
            console.error("shit");
        }
    };

    xhr.send(JSON.stringify({
        action: "delete",
        payload: fileTitle
    }));
}