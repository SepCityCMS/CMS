function OnLinkedInFrameworkLoad() {
    if (document.location.href.indexOf("login.aspx") < 0) {
        IN.Event.on(IN, "auth", OnLinkedInAuth);
    }
}

function OnLinkedInAuth() {
    IN.API.Profile("me").fields("id", "first-name", "last-name", "email-address").result(ShowProfileData);
}

function ShowProfileData(profiles) {
    var member = profiles.values[0];
    var id = member.id;
    var firstName = member.firstName;
    var lastName = member.lastName;
    var emailAddress = member.emailAddress;

    if ($("#LinkedInID").length > 0) { // Signup Form
        $("#LinkedInID").val(id);
        $("#FirstName").val(firstName);
        $("#LastName").val(lastName);
        $("#EmailAddress").val(emailAddress);
    } else {
        if ($("#LinkedInId").length > 0) { // Login Form
            $("#LinkedInId").val(id);
            $("#UserName").val(emailAddress);
            $("#Password").val(id);
            document.aspnetForm.submit();
        } else {
            if ($("#LinkedInId2").length > 0) { // Login Form on Site Template
                $("#LinkedInId2").val(id);
                $("#UserName2").val(emailAddress);
                $("#Password2").val(id);
                document.frmAccForm.submit();
            }
        }
    }

}