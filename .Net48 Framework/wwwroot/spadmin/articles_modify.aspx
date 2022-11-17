<%@ page title="Articles" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="articles_modify.aspx.cs" inherits="wwwroot.articles_modify" %>
<%@ import namespace="SepCommon" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.min.css" integrity="sha256-DOS9W6NR+NFe1fUhEE0PGKY/fubbUCnOfTje2JMDw3Y=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.full.min.js" integrity="sha256-FEqEelWI3WouFOo2VWP/uJfs1y8KJ++FLh2Lbqc8SJk=" crossorigin="anonymous"></script>
    <script type="text/javascript">
        skipRestyling = true;

        $(document).ready(function () {
            <%=SepFunctions.Date_Picker(HeadlineDate.ClientID, "false", "true", "")%>;
            <%=SepFunctions.Date_Picker(StartDate.ClientID, "false", "true", "")%>;
            <%=SepFunctions.Date_Picker(EndDate.ClientID, "false", "true", "$('#StartDate.ClientID').val()")%>;
            $('#<%=StartDate.ClientID%>')
                .on("dp.change",
                    function (e) {
                        $('#<%=EndDate.ClientID%>').data("DateTimePicker").minDate(e.date);
                });
            $('#<%=EndDate.ClientID%>')
                .on("dp.change",
                    function (e) {
                        $('#<%=StartDate.ClientID%>').data("DateTimePicker").maxDate(e.date);
                });
        });

        function addRelatedArticle(iRowOffset) {
            $('#iframeRelatedArticles').attr('src', 'articles_add_related.aspx?RowOffset=' + iRowOffset);
            openDialog('addArticle', 640, 510);
        }

        function openGeneralOptions() {
            $('#tabGeneral a').addClass('btn-info');
            $('#tabSEO a').removeClass('btn-info');
            $('#tabRelated a').removeClass('btn-info');
            $("#SEOOptions").hide();
            $("#RelatedOptions").hide();
            $("#GeneralOptions").show();
            restyleFormElements('#GeneralOptions');
        }

        function openSEO() {
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabSEO a').addClass('btn-info');
            $('#tabRelated a').removeClass('btn-info');
            $("#GeneralOptions").hide();
            $("#RelatedOptions").hide();
            $("#SEOOptions").show();
            restyleFormElements('#SEOOptions');
        }

        function openRelatedArticles() {
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabSEO a').removeClass('btn-info');
            $('#tabRelated a').addClass('btn-info');
            $("#GeneralOptions").hide();
            $("#SEOOptions").hide();
            $("#RelatedOptions").show();
            restyleFormElements('#RelatedOptions');
        }

        $(document).ready(function () {
            $('#tabGeneral a').addClass('btn-info');
            restyleFormElements('#GeneralOptions');
        });
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 35;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

        <div class="col-md-12 pagecontentsave">

            <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Article</h4>
                <input type="hidden" runat="server" ID="ArticleID" />

                <div class="panel panel-default" id="PageManageGridView" runat="server">
                    <div class="panel-body">
                        <ul class="nav nav-pills">
                            <li class="nav-item" id="tabGeneral">
                                <a class="nav-link" href="javascript:void(0)" onclick="openGeneralOptions();">General Options</a>
                            </li>
                            <li class="nav-item" id="tabSEO">
                                <a class="nav-link" href="javascript:void(0)" onclick="openSEO();">SEO</a>
                            </li>
                            <li class="nav-item" id="tabRelated">
                                <a class="nav-link" href="javascript:void(0)" onclick="openRelatedArticles();">Related Articles</a>
                            </li>
                        </ul>
                    </div>
                </div>

                <div class="panel-body">
                    <div id="GeneralOptions">
                        <sep:ChangeLogDropdown id="ChangeLog" runat="server" ModuleID="35" CssClass="form-control"></sep:ChangeLogDropdown>
                        <div class="mb-3">
                            <label ID="CategoryLabel" ClientIDMode="Static" runat="server" for="Category">Select a Category in the box below:</label>
                            <sep:CategoryDropdown ID="Category" runat="server" ModuleID="35" ClientIDMode="Static" />
                            <asp:CustomValidator ID="CategoryRequired" runat="server" ControlToValidate="Category"
                                                    ClientValidationFunction="customFormValidator" ErrorMessage="Category is required."
                                                    ValidateEmptyText="true" Display="Dynamic">
                            </asp:CustomValidator>
                        </div>
                        <div class="mb-3" runat="server" id="PortalsRow">
                            <label ID="PortalLabel" clientidmode="Static" runat="server" for="Portal">Portal:</label>
                            <sep:PortalDropdown ID="Portal" runat="server" ClientIDMode="Static" CssClass="form-control" />
                        </div>
                        <div class="mb-3">
                            <label ID="HeadlineLabel" clientidmode="Static" runat="server" for="Headline">Headline:</label>
                            <input type="text" ID="Headline" runat="server"  class="form-control" />

                            <asp:CustomValidator ID="HeadlineRequired" runat="server" ControlToValidate="Headline"
                                                    ClientValidationFunction="customFormValidator" ErrorMessage="Headline is required."
                                                    ValidateEmptyText="true" Display="Dynamic">
                            </asp:CustomValidator>
                        </div>
                        <div class="mb-3">
                            <label ID="HeadlineDateLabel" clientidmode="Static" runat="server" for="HeadlineDate">Headline Date:</label>
                            <div class="form-group">
                                <div class="input-group date" id="HeadlineDateDiv">
                                    <input type="text" id="HeadlineDate" class="form-control" runat="server" />
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                                </div>
                            </div>
                        </div>
                        <div class="mb-3">
                            <label ID="AuthorLabel" clientidmode="Static" runat="server" for="Author">Author:</label>
                            <input type="text" ID="Author" runat="server"  class="form-control" />
                            <asp:CustomValidator ID="AuthorRequired" runat="server" ControlToValidate="Author"
                                                    ClientValidationFunction="customFormValidator" ErrorMessage="Author is required."
                                                    ValidateEmptyText="true" Display="Dynamic">
                            </asp:CustomValidator>
                        </div>
                        <div class="mb-3">
                            <label ID="SummaryLabel" clientidmode="Static" runat="server" for="Summary">Summary:</label>
                            <textarea ID="Summary" runat="server"  class="form-control"></textarea>
                            <asp:CustomValidator ID="SummaryRequired" runat="server" ControlToValidate="Summary"
                                                    ClientValidationFunction="customFormValidator" ErrorMessage="Summary is required."
                                                    ValidateEmptyText="true" Display="Dynamic">
                            </asp:CustomValidator>
                        </div>
                        <div class="mb-3">
                            <label ID="StartDateLabel" clientidmode="Static" runat="server" for="StartDate">Start Date:</label>
                            <div class="form-group">
                                <div class="input-group date" id="StartDateDiv">
                                    <input type="text" id="StartDate" class="form-control" runat="server" />
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                                </div>
                            </div>
                        </div>
                        <div class="mb-3">
                            <label ID="EndDateLabel" clientidmode="Static" runat="server" for="EndDate">End Date:</label>
                            <div class="form-group">
                                <div class="input-group date" id="EndDateDiv">
                                    <input type="text" id="EndDate" class="form-control" runat="server" />
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                                </div>
                            </div>
                        </div>
                        <div class="mb-3">
                            <label ID="StatusLabel" clientidmode="Static" runat="server" for="Status">Status:</label>
                            <select ID="Status" runat="server" class="form-control">
                                <option value="1">Published</option>
                                <option value="0">Pending</option>
                                <option value="2">Archived</option>
                            </select>
                            <asp:CustomValidator ID="StatusRequired" runat="server" ControlToValidate="Status"
                                                    ClientValidationFunction="customFormValidator" ErrorMessage="Status is required."
                                                    ValidateEmptyText="true" Display="Dynamic">
                            </asp:CustomValidator>
                        </div>
                        <div class="mb-3">
                            <label ID="SourceLabel" clientidmode="Static" runat="server" for="Source">Article Source Name:</label>
                            <input type="text" ID="Source" runat="server"  class="form-control" />
                        </div>
                        <div class="mb-3">
                            <label ID="ArticleURLLabel" clientidmode="Static" runat="server" for="ArticleURL">URL to the Article Source:</label>
                            <input type="text" ID="ArticleURL" runat="server"  class="form-control" />
                        </div>
                        <div class="mb-3">
                            <sep:WYSIWYGEditor Runat="server" ID="ArticleText" Width="99%" Height="450" />
                        </div>
                        <div class="mb-3">
                            <label ID="PicturesLabel" clientidmode="Static" runat="server" for="Pictures">Pictures:</label>
                            <sep:UploadFiles ID="Pictures" runat="server" Mode="MultipleFiles" FileType="Images" ModuleID="35" />
                        </div>
                        <% 
                            var cCustomFields = new SepCityControls.CustomFields();
                            cCustomFields.ModuleID = 35;
                            cCustomFields.FieldUniqueID = ArticleID.Value;
                            if(sUserID != "") {
                                cCustomFields.UserID = sUserID;
                            } else {
                                cCustomFields.UserID = SepCommon.SepFunctions.Session_User_ID();
                            }
                            Response.Write(cCustomFields.Render()); 
                        %>
                    </div>
                    <div id="SEOOptions" style="display: none">
                        <div class="mb-3">
                            <label ID="MetaKeywordsLabel" clientidmode="Static" runat="server" for="SEOMetaKeywords">Meta Keywords:</label>
                            <textarea ID="SEOMetaKeywords" runat="server"  class="form-control"></textarea>
                        </div>
                        <div class="mb-3">
                            <label ID="MetaDescriptionLabel" clientidmode="Static" runat="server" for="SEOMetaDescription">Meta Description:</label>
                            <textarea ID="SEOMetaDescription" runat="server"  class="form-control"></textarea>
                        </div>
                    </div>
                    <div id="RelatedOptions" style="display: none">
                        <div class="mb-3">
                            <label ID="RelatedArticlesLabel" ClientIDMode="Static" runat="server">Related Articles:</label>

                            <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                                            CssClass="GridViewStyle" ShowHeaderWhenEmpty="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Headline" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <%#
            Eval("Headline")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Headline Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <%#
            Format_Date(Eval("Headline_Date").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <input type="hidden" ID="RelatedArticles" runat="server" ClientIDMode="Static" />
                            <div class="mb-3">
                                <asp:HyperLink ID="AddArticle2" runat="server" NavigateUrl="javascript:addRelatedArticle('0')" ClientIDMode="Static" Text="Add Article" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="button-to-bottom">
		        <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		        <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	        </div>
        </div>
        <div id="addArticle" title="Add Related Articles" style="display: none; height: 400px; width: 600px;">
            <iframe src="" id="iframeRelatedArticles" style="height: 400px; width: 470px;"></iframe>
        </div>
    </asp:Panel>
</asp:content>