<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="articles_modify.aspx.cs" inherits="wwwroot.articles_modify1" %>
<%@ Import Namespace="SepCommon" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.min.css" integrity="sha256-DOS9W6NR+NFe1fUhEE0PGKY/fubbUCnOfTje2JMDw3Y=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.full.min.js" integrity="sha256-FEqEelWI3WouFOo2VWP/uJfs1y8KJ++FLh2Lbqc8SJk=" crossorigin="anonymous"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            <%= SepFunctions.Date_Picker(this.HeadlineDate.ClientID, "false", "true", "") %>;
            <%= SepFunctions.Date_Picker(this.StartDate.ClientID, "false", "true", "") %>;
            <%= SepFunctions.Date_Picker(this.EndDate.ClientID, "false", "true", "$('#StartDate.ClientID').val()") %>;
            $('#<%= this.StartDate.ClientID %>')
                .on("dp.change",
                    function (e) {
                        $('#<%= this.EndDate.ClientID %>').data("DateTimePicker").minDate(e.date);
                });
            $('#<%= this.EndDate.ClientID %>')
                .on("dp.change",
                    function (e) {
                        $('#<%= this.StartDate.ClientID %>').data("DateTimePicker").maxDate(e.date);
                });
        });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">Post an Article</h4>
        <input type="hidden" runat="server" id="ArticleID" />

        <div class="mb-3">
            <label id="CategoryLabel" clientidmode="Static" runat="server" for="Category">Select a Category in the box below:</label>
            <sep:CategoryDropdown ID="Category" runat="server" ModuleID="35" ClientIDMode="Static" />
            <asp:CustomValidator ID="CategoryRequired" runat="server" ControlToValidate="Category"
                ClientValidationFunction="customFormValidator" ErrorMessage="Category is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="HeadlineLabel" clientidmode="Static" runat="server" for="Headline">Headline:</label>
            <input type="text" id="Headline" runat="server" class="form-control" />
            <asp:CustomValidator ID="HeadlineRequired" runat="server" ControlToValidate="Headline"
                ClientValidationFunction="customFormValidator" ErrorMessage="Headline is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="HeadlineDateLabel" clientidmode="Static" runat="server" for="HeadlineDate">Headline Date:</label>
            <input type="text" id="HeadlineDate" runat="server" class="form-control" clientidmode="Static" />
            <asp:CustomValidator ID="HeadlineDateRequired" runat="server" ControlToValidate="HeadlineDate"
                ClientValidationFunction="customFormValidator" ErrorMessage="Headline Date is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="StartDateLabel" clientidmode="Static" runat="server" for="StartDate">Start Date:</label>
            <div class="form-group">
                <div class="input-group date" id="StartDateDiv">
                    <input type="text" id="StartDate" class="form-control" runat="server" />
                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                </div>
            </div>
        </div>
        <div class="mb-3">
            <label id="EndDateLabel" clientidmode="Static" runat="server" for="EndDate">End Date:</label>
            <div class="form-group">
                <div class="input-group date" id="EndDateDiv">
                    <input type="text" id="EndDate" class="form-control" runat="server" />
                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                </div>
            </div>
        </div>
        <div class="mb-3">
            <label id="AuthorLabel" clientidmode="Static" runat="server" for="Author">Author:</label>
            <input type="text" id="Author" runat="server" class="form-control" />
            <asp:CustomValidator ID="AuthorRequired" runat="server" ControlToValidate="Author"
                ClientValidationFunction="customFormValidator" ErrorMessage="Author is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="SummaryLabel" clientidmode="Static" runat="server" for="Summary">Summary:</label>
            <textarea id="Summary" runat="server" class="form-control"></textarea>
            <asp:CustomValidator ID="SummaryRequired" runat="server" ControlToValidate="Summary"
                ClientValidationFunction="customFormValidator" ErrorMessage="Summary is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3" id="SourceNameRow" runat="server">
            <label id="SourceLabel" clientidmode="Static" runat="server" for="Source">Article Source Name:</label>
            <input type="text" id="Source" runat="server" class="form-control" />
        </div>
        <div class="mb-3" id="SourceURLRow" runat="server">
            <label id="ArticleURLLabel" clientidmode="Static" runat="server" for="ArticleURL">URL to the Article Source:</label>
            <input type="text" id="ArticleURL" runat="server" class="form-control" />
        </div>
        <div class="mb-3">
            <sep:WYSIWYGEditor runat="server" ID="ArticleText" Width="99%" Height="450" />
        </div>
        <%
            var cCustomFields = new SepCityControls.CustomFields();
            cCustomFields.ModuleID = 35;
            cCustomFields.FieldUniqueID = this.ArticleID.Value;
            cCustomFields.UserID = SepFunctions.Session_User_ID();
            this.Response.Write(cCustomFields.Render());
        %>
        <div class="mb-3">
            <label id="PicturesLabel" clientidmode="Static" runat="server" for="Pictures">Pictures:</label>
            <sep:UploadFiles ID="Pictures" runat="server" Mode="MultipleFiles" FileType="Images" ModuleID="35" />
        </div>
        <div class="mb-3" id="MetaKeywordsRow" runat="server">
            <label id="MetaKeywordsLabel" clientidmode="Static" runat="server" for="SEOMetaKeywords">Meta Keywords:</label>
            <textarea id="SEOMetaKeywords" runat="server" class="form-control"></textarea>
        </div>
        <div class="mb-3" id="MetaDescriptionRow" runat="server">
            <label id="MetaDescriptionLabel" clientidmode="Static" runat="server" for="SEOMetaDescription">Meta Description:</label>
            <textarea id="SEOMetaDescription" runat="server" class="form-control"></textarea>
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>