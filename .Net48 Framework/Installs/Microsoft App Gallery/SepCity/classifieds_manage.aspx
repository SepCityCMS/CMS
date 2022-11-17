<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="classifieds_manage.aspx.cs" inherits="wwwroot.classifieds_manage" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script src="<%= this.GetInstallFolder(true) %>js/gridview.js" type="text/javascript"></script>
    <script src="<%= this.GetInstallFolder(true) %>js/management.js" type="text/javascript"></script>
    <script type="text/javascript">
        function openFeedback(adId, feedbackId) {
            <%
                this.Context.Response.Write("var params = new Object();");
            %>
            params.AdID = adId;
            params.FeedbackID = feedbackId;

            // ajax call to fetch next set of rows
            $.ajax({
                type: "HttpPost",
                data: JSON.stringify(params),
                url: config.imageBase + "api/classifieds/feedback",
                dataType: "json",
                contentType: "application/json",
                error: function (xhr) {
                    alert("There has been an error loading data." + debugMsg("\n\n" + xhr.responseText));
                },

                success: function (response) {

                    switch (response.Rating) {
                        case 1:
                            $("#FeedbackRating").html("1 - Horrible");
                            break;
                        case 2:
                            $("#FeedbackRating").html("2 - Poor");
                            break;
                        case 3:
                            $("#FeedbackRating").html("3 - Fair");
                            break;
                        case 4:
                            $("#FeedbackRating").html("4 - Good");
                            break;
                        case 5:
                            $("#FeedbackRating").html("5 - Excellent");
                            break;
                        default:
                            $("#FeedbackRating").html("N/A");
                    }

                    $("#FeedbackDesc").html(response.Description);
                }
            });

            openDialog('ViewFeedback', 400, 400);
        }

        $(document).ready(function () {
            restyleGridView("#SellingContent");
            restyleGridView("#SoldContent");
            restyleGridView("#BoughtContent");
        });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <div id="ViewFeedback" title="Feedback from User" style="display: none;">
        <br />
        Rating: <span id="FeedbackRating"></span>
        <br />
        <div id="FeedbackDesc"></div>
    </div>

    Your Rating:
    <span ID="lblYourRating" runat="server"></span>
    <br />

    <span class="successNotification" id="successNotification">
        <span ID="DeleteResult" runat="server"></span>
    </span>

    <div class="panel panel-default" id="PageManageGridView" runat="server">
        <div class="panel-heading">
            <div class="row">
                <div class="col-lg-6">
                    <div class="input-group">
                        <select id="FilterDoAction" runat="server" class="form-control" clientidmode="Static">
                            <option value="">Select an Action</option>
                            <option value="DeleteAds">Delete Ads</option>
                        </select>
                        <span class="input-group-btn">
                            <button class="btn btn-light" id="RunAction" runat="server" onserverclick="RunAction_Click" onclick="if(ExecuteAction(this, 'AdID') == false) {return false} else">Go!</button>
                        </span>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="input-group">
                        <input type="text" id="ModuleSearch" runat="server" placeholder="Search for..." onkeypress="if(submitSearch(event) == false){document.getElementById('ModuleSearchButton').click();return submitSearch(event);}" class="form-control" />
                        <span class="input-group-btn">
                            <button class="btn btn-light" ID="ModuleSearchButton" runat="server" clientidmode="static" OnServerClick="ModuleSearchButton_Click">Go!</button>
                        </span>
                    </div>
                </div>
            </div>
        </div>

        <input type="hidden" id="UniqueIDs" runat="server" clientidmode="Static" value="" />

        <asp:GridView ID="SellingContent" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static"
            CssClass="GridViewStyle" OnSorting="ManageGridView_Sorting" EnableViewState="True" Caption="Items I'm Selling">
            <Columns>
                <asp:TemplateField ItemStyle-Width="20px">
                    <HeaderTemplate>
                        <input type="checkbox" id="checkAll" onclick="gridviewCheckAll(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <input type="checkbox" id="AdID<%#
                this.Eval("AdID") %>"
                            value="<%#
                this.Eval("AdID") %>"
                            onclick="gridviewSelectRow(this);" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="20px">
                    <ItemTemplate>
                        <a href="<%= this.GetInstallFolder() %>classifieds_modify.aspx?CatID=<%#
                this.Eval("CatID") %>&AdID=<%#
                this.Eval("AdID") %>">
                            <img src="<%= this.GetInstallFolder(true) + "images/" %>public/edit.png" alt="Edit" />
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                this.Eval("Title") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                this.Eval("Price") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date Posted" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                this.Format_Date(this.Eval("DatePosted").ToString()) %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:GridView ID="SoldContent" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static"
            CssClass="GridViewStyle" OnSorting="ManageGridView_Sorting" EnableViewState="True" Caption="Items I Sold">
            <Columns>
                <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                this.Eval("Title") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Buyer" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                this.Eval("SoldUserName") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                this.Eval("Price") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date Sold" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                this.Eval("SoldDate") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Leave Feedback" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                Convert.ToString(!string.IsNullOrWhiteSpace(Convert.ToString(DataBinder.Eval(Container.DataItem, "BuyerFeedbackID"))) ? "<a href=\"javascript:void(0)\" onclick=\"openFeedback('" + this.Eval("AdID") + "', '" + this.Eval("BuyerFeedbackID") + "');return false;\">View Feedback</a>" : "<a href='classifieds_feedback.aspx?AdID=" + this.Eval("AdID") + "&SoldUserID=" + this.Eval("SoldUserID") + "'>Leave Feedback</a>") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:GridView ID="BoughtContent" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static"
            CssClass="GridViewStyle" OnSorting="ManageGridView_Sorting" EnableViewState="True" Caption="Items I Bought">
            <Columns>
                <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                this.Eval("Title") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Seller" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                this.Eval("SellerUserName") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                this.Eval("Price") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date Sold" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                this.Eval("SoldDate") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Leave Feedback" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                Convert.ToString(!string.IsNullOrWhiteSpace(Convert.ToString(DataBinder.Eval(Container.DataItem, "SellerFeedbackID"))) ? "<a href=\"javascript:void(0)\" onclick=\"openFeedback('" + this.Eval("AdID") + "', '" + this.Eval("SellerFeedbackID") + "');return false;\">View Feedback</a>" : "<a href='classifieds_feedback.aspx?AdID=" + this.Eval("AdID") + "&SellerUserID=" + this.Eval("UserID") + "'>Leave Feedback</a>") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:content>