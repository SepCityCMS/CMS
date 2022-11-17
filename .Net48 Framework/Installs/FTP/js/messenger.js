// This method will persist a http request and wait for messages.
function Get_Messages() {

    $.ajax({
        type: "GET",
        url: config.imageBase + "api/messenger",
        dataType: "json",
        contentType: "application/json",
        complete: function() {
            // access the params 
        },

        error: function() {
            //alert("There has been an error loading data." + debugMsg("\n\n" + xhr.responseText));
        },

        success: function(response) {
            if (response !== "") {
                return displayMessage(response);
            }
        }
    });
    return false;
}

function waitEvent() {
    self.setInterval(function () {
        Get_Messages();
    }, 1000);
}

// Append a message content to the result panel.
function displayMessage(message) {
    if (message.Message !== null) {
        var panel = document.getElementById("lbMessages");
        panel.setAttribute('title', message.Subject);
        panel.innerHTML += message.Message;
        openDialog("lbMessages", 350, 150);
    }
}