<%@ page language="C#" viewstatemode="Enabled" masterpagefile="install.master"
    codebehind="runinstall.aspx.cs" inherits="wwwroot.runinstall" %>
<%@ Import Namespace="SepCommon" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        <%
            this.Context.Response.Write("var params = new Object();");
            this.Context.Response.Write("params.UserID = \"" + SepFunctions.Generate_GUID() + "\";");
            this.Context.Response.Write("params.DBAddress = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["DBAddress"])) + "\");" + Environment.NewLine);
            this.Context.Response.Write("params.DBName = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["DBName"])) + "\");");
            this.Context.Response.Write("params.DBUser = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["DBUser"])) + "\");");
            this.Context.Response.Write("params.DBPass = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["DBPass"])) + "\");");
            this.Context.Response.Write("params.InstallCategories = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["DBCategories"])) + "\");");
            this.Context.Response.Write("params.InstallSampleData = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["DBSampleData"])) + "\");");
            this.Context.Response.Write("params.SMTPServer = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["SMTPServer"])) + "\");" + Environment.NewLine);
            this.Context.Response.Write("params.SMTPUser = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["SMTPUser"])) + "\");" + Environment.NewLine);
            this.Context.Response.Write("params.SMTPPass = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["SMTPPass"])) + "\");" + Environment.NewLine);
            if (SepCommon.SepCore.Request.Item("DoAction") != "Upgrade")
            {
                this.Context.Response.Write("params.UserName = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["PUserName"])) + "\");" + Environment.NewLine);
                this.Context.Response.Write("params.Password = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["PPassword"])) + "\");" + Environment.NewLine);
                this.Context.Response.Write("params.EmailAddress = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["PEmailAddress"])) + "\");");
                this.Context.Response.Write("params.FirstName = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["PFirstName"])) + "\");" + Environment.NewLine);
                this.Context.Response.Write("params.LastName = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["PLastName"])) + "\");" + Environment.NewLine);
                this.Context.Response.Write("params.Male = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["PGender"])) + "\");" + Environment.NewLine);
                this.Context.Response.Write("params.BirthDate = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["PBirthDate"])) + "\");" + Environment.NewLine);
                this.Context.Response.Write("params.StreetAddress = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["PStreetAddress"])) + "\");");
                this.Context.Response.Write("params.City = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["PCity"])) + "\");");
                this.Context.Response.Write("params.State = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["PState"])) + "\");" + Environment.NewLine);
                this.Context.Response.Write("params.ZipCode = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["PPostalCode"])) + "\");" + Environment.NewLine);
                this.Context.Response.Write("params.PhoneNumber = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["PPhoneNumber"])) + "\");");
                this.Context.Response.Write("params.Country = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["PCountry"])) + "\");" + Environment.NewLine);
                this.Context.Response.Write("params.Secret_Answer = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["PSecretAnswer"])) + "\");");
                this.Context.Response.Write("params.Secret_Question = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["PSecretQuestion"])) + "\");");
            }
            this.Context.Response.Write("params.LicenseUser = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["LicenseUser"])) + "\");" + Environment.NewLine);
            this.Context.Response.Write("params.LicensePass = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["LicensePass"])) + "\");" + Environment.NewLine);
            this.Context.Response.Write("params.LicenseKey = unescape(\"" + SepFunctions.EscQuotes(Convert.ToString(HttpContext.Current.Session["LicenseKey"])) + "\");" + Environment.NewLine);
        %>

        $(document)
            .ready(function () {
                beginUpload();

                $.ajax({
                    type: "POST",
                    data: JSON.stringify(params),
                    <% if (SepCommon.SepCore.Request.Item("DoAction") == "Upgrade")
                       { %>
                    url: config.imageBase + "install/InstallService.asmx/RunUpgrade",
                    <% }
                       else
                       { %>
                    url: config.imageBase + "install/InstallService.asmx/RunInstall",
                    <% } %>
                    dataType: "json",
                    contentType: "application/json",
                    error: function (xhr) {
                        alert("There has been an error running the install." + debugMsg("\n\n" + xhr.responseText));
                    },
                    success: function (response) {
                        if (response.d != 'Done') {
                            alert(response.d);
                        } else {
                            $("#InstallCompleteDiv").show();
                            $("#Para1").html('');
                            $("#Para2").html('');
                        }
                    }
                });
            });

            function beginUpload() {
                var i = setInterval(function () {
                    $.ajax({
                        type: "POST",
                        url: config.imageBase + "install/InstallService.asmx/GetPercentage",
                        dataType: "json",
                        contentType: "application/json",
                        error: function () {
                            $(".progress-bar").css('width', '100%');
                            $(".progress-bar").attr('aria-valuenow', '100%');
                            $(".progress-bar").html('100%');
                            clearInterval(i);
                        },
                        success: function (response) {
                            var objResponse = $.parseJSON(response.d);
                            $(".progress-bar").css('width', objResponse[0].Percentage + '%');
                            $(".progress-bar").attr('aria-valuenow', objResponse[0].Percentage + '%');
                            $(".progress-bar").html(objResponse[0].Percentage + '%');
                        }
                    });
                }, 1500);
            }
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <div id="contentinstall" runat="server">
        <h1>SepCity is installing on your web server.</h1>
        <p id="Para1">Do not hit your browser refresh otherwise your database might get corrupted.</p>
        <p id="Para2">This may take a few minutes to complete... Please Wait ...</p>
        <div class="progress" style="height: 25px;">
            <div class="progress-bar" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100">0%</div>
        </div>
        <div id="InstallCompleteDiv" style="display: none">
            <p>Install has completed successfully.</p>
            <p id="hideNotes" runat="server">
                <b>NOTE: YOU MUST DELETE THE INSTALL DIRECTORY FROM YOUR WEB SERVER.</b>
            </p>
            <p>
                <a href="../default.aspx">Continue to your Web Site</a>
            </p>
        </div>
    </div>
</asp:content>