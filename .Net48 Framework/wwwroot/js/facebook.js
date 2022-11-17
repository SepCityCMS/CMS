function fbSignupLogin() {
    FB.login(function(response) {
        if (response.authResponse) {
            fbSignupUserInfo();
        } else {
            console.log("User cancelled login or did not fully authorize.");
        }
    },
    {
        scope: "email,user_photos,user_videos"
    });
}

function fbLogin() {
    FB.login(function(response) {
        if (response.authResponse) {
            FB.api("/me",
                function(response) {
                    $("#Facebook_Token").val(FB.getAuthResponse()["accessToken"]);
                    $("#Facebook_Id").val(response.id);
                    $("#Facebook_User").val(response.username);
                    $("#Facebook_Email").val(response.email);
                    $("#Facebook_FName").val(response.first_name);
                    $("#Facebook_LName").val(response.last_name);
                    document.forms[0].submit();
                });
        } else {
            console.log("User cancelled login or did not fully authorize.");
        }
    },
    {
        scope: "email,user_photos,user_videos"
    });
}

function fbAccountLogin() {
    FB.login(function(response) {
        if (response.authResponse) {
            FB.api("/me",
                function(response) {
                    $("#Facebook_Token2").val(FB.getAuthResponse()["accessToken"]);
                    $("#Facebook_Id2").val(response.id);
                    $("#Facebook_User2").val(response.username);
                    $("#Facebook_Email2").val(response.email);
                    $("#Facebook_FName2").val(response.first_name);
                    $("#Facebook_LName2").val(response.last_name);
                    document.frmAccForm.submit();
                });
        } else {
            console.log("User cancelled login or did not fully authorize.");
        }
    },
    {
        scope: "email,user_photos,user_videos"
    });
}

function fbSignupUserInfo() {
    FB.api("/me",
        function(response) {
            $("#UserName").val(response.username);
            $("#FirstName").val(response.first_name);
            $("#LastName").val(response.last_name);
            $("#EmailAddress").val(response.email);
            if (response.gender === "male") {
                $("#Gender").val("1");
            } else {
                $("#Gender").val("0");
            }
            $("#FacebookToken").val(FB.getAuthResponse()["accessToken"]);
            $("#FacebookId").val(response.id);
            $("#FacebookUser").val(response.username);
            $("#FacebookRow").hide();
        });
}

function fbAssociate() {
    FB.login(function(response) {
        if (response.authResponse) {
            FB.api("/me",
                function(response) {
                    $("#Facebook_Token").val(FB.getAuthResponse()["accessToken"]);
                    $("#Facebook_Id").val(response.id);
                    $("#Facebook_User").val(response.username);
                    document.forms[0].submit();
                });
        } else {
            console.log("User cancelled login or did not fully authorize.");
        }
    },
    {
        scope: "email,user_photos,user_videos"
    });
}