<%@ page title="Members" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="members.aspx.cs" inherits="wwwroot.members" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <script src="../js/gridview.js" type="text/javascript"></script>
    <script src="../js/management.js" type="text/javascript"></script>
    <script type="text/javascript">
        function RunActionFunction() {

            if ($('#FilterDoAction').val() == "MoveClass") {
                openModal('MoveToClass',
                    300,
                    150,
                    '<button type="button" class="btn btn-primary" onclick="$(\'#ClassID\').val($(\'#MoveAccessClassDropdown\').val());<%= SepCommon.SepCore.Strings.Replace(Page.ClientScript.GetPostBackEventReference(RunAction, String.Empty), "'", "\\'")%>;">Move User(s)</button>');
                    return false;
                }

                if ($('#FilterDoAction').val() == "AddAccessKey") {
                    openModal('AddAccessKey',
                        300,
                        150,
                        '<button type="button" class="btn btn-primary" onclick="$(\'#KeyID\').val($(\'#AddAccessKeyDropdown\').val());<%= SepCommon.SepCore.Strings.Replace(Page.ClientScript.GetPostBackEventReference(RunAction, String.Empty), "'", "\\'")%>;">Add Key</button>');
                        return false;
                    }

                    if ($('#FilterDoAction').val() == "RemoveAccessKey") {
                        openModal('RemoveAccessKey',
                            300,
                            150,
                            '<button type="button" class="btn btn-primary" onclick="$(\'#KeyID\').val($(\'#RemoveAccessKeyDropdown\').val());<%= SepCommon.SepCore.Strings.Replace(Page.ClientScript.GetPostBackEventReference(RunAction, String.Empty), "'", "\\'")%>;">Remove Key</button>');
                            return false;
                        }

                        if ($('#FilterDoAction').val() == "AddGroup") {
                            openModal('AddToGroupList',
                                300,
                                150,
                                '<button type="button" class="btn btn-primary" onclick="$(\'#ListID\').val($(\'#GroupListDropdown\').val());<%= SepCommon.SepCore.Strings.Replace(Page.ClientScript.GetPostBackEventReference(RunAction, String.Empty), "'", "\\'")%>;">Add User(s)</button>');
                                return false;
                            }

            <%=Page.ClientScript.GetPostBackEventReference(RunAction, string.Empty)%>;
            return false;
        }
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 986;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontent">

        <h2>
            <span ID="PageHeader" runat="server" Text="Members"></span>
        </h2>

        <span class="successNotification" id="successNotification">
			<span ID="DeleteResult" runat="server"></span>
		</span>

        <div class="panel panel-default" id="PageManageGridView" runat="server">
			<div class="panel-heading">
				<div class="row">
					<div class="col-lg-6">
						<div class="input-group">
                            <select id="FilterDoAction" runat="server" Class="GridViewAction" ClientIDMode="Static">
                                <option value="">Select an Action</option>
                                <option value="DeleteMembers">Delete members</option>
                                <option value="MarkActive">Mark as active</option>
                                <option value="MarkNotActive">Mark as not active</option>
                                <option value="MoveClass">Move to a class</option>
                                <option value="AddAccessKey">Add access key</option>
                                <option value="RemoveAccessKey">Remove access key</option>
                                <option value="AddGroup">Add to group list</option>
                            </select>
							<span class="input-group-btn">
							    <button class="btn btn-light" ID="RunAction" runat="server" OnServerClick="RunAction_Click" onclick="if(ExecuteAction(this, 'UserID') == true){RunActionFunction()};return false;">Go!</button>
						    </span>
					    </div>
				    </div>
				    <div class="col-lg-6">
					    <div class="input-group">
						    <input type="text" ID="ModuleSearch" runat="server" placeholder="Search for..." onKeyPress="if(submitSearch(event) == false){document.getElementById('ModuleSearchButton').click();return submitSearch(event);}"  class="form-control" />
						    <span class="input-group-btn">
							    <button class="btn btn-light" ID="ModuleSearchButton" runat="server" clientidmode="static" OnServerClick="ModuleSearchButton_Click">Go!</button>
						    </span>
					    </div>
				    </div>
			    </div>
		    </div>

            <input type="hidden" ID="UniqueIDs" runat="server" ClientIDMode="Static" Value="" />
            <input type="hidden" ID="ClassID" runat="server" ClientIDMode="Static" Value="" />
            <input type="hidden" ID="KeyID" runat="server" ClientIDMode="Static" Value="" />
            <input type="hidden" ID="ListID" runat="server" ClientIDMode="Static" Value="" />

            <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                            CssClass="GridViewStyle" AllowPaging="true" OnPageIndexChanging="ManageGridView_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast"
                            OnSorting="ManageGridView_Sorting" EnableViewState="True">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="20px">
                        <HeaderTemplate>
                            <input type="checkbox" id="checkAll" onclick="gridviewCheckAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type="checkbox" id="UserID<%#
                Eval("UserID").ToString()%>" value="<%#
                Eval("UserID").ToString()%>" onclick="gridviewSelectRow(this);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:templatefield HeaderText="Action" ItemStyle-Width="20px">
                        <itemtemplate>
                            <div class="btn-group">
                                <button class="btn btn-secondary btn-sm dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Action</button>
                                <div class="dropdown-menu">
                                    <a class="dropdown-item" href="members_modify.aspx?UserID=<%#Eval("UserID").ToString()%>">Edit Member</a>
                                    <a class="dropdown-item" href="email_user.aspx?UserID=<%#Eval("UserID").ToString()%>">Email Member</a>
                                    <a class="dropdown-item" href="invoices_modify.aspx?UserID=<%#Eval("UserID").ToString()%>">Add Invoice</a>
                                    <a class="dropdown-item" href="activities_modify.aspx?UserID=<%#Eval("UserID").ToString()%>">New Activity</a>
                                    <a class="dropdown-item" href="zoom_meeting_create.aspx?UserID=<%#Eval("UserID").ToString()%>" attr-class="ZoomMeetingLink">Zoom Meeting</a>
                                </div>
                            </div>
                        </itemtemplate>
                    </asp:templatefield>
                    <asp:TemplateField HeaderText="User Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="UserName">
                        <ItemTemplate>
                            <%#
                Eval("UserName").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Full Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="FirstName">
                        <ItemTemplate>
                            <%#
                Eval("FirstName").ToString()%> <%#
                Eval("LastName").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email Address" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="EmailAddress">
                        <ItemTemplate>
                            <a href="email_user.aspx?UserID=<%#
                Eval("UserID").ToString()%>"><%#
                Eval("EmailAddress").ToString()%></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Active" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Status">
                        <ItemTemplate>
                            <%#  Convert.ToString(Eval("Status").ToString() == "0" ? "Not Active" : "Active")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Last Login" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="LastLogin">
                        <ItemTemplate>
                            <%#
                Eval("LastLogin").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Signup Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="CreateDate">
                        <ItemTemplate>
                            <%#
                Eval("CreateDate").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pagination-ys" />
            </asp:GridView>
        </div>
        </div>

        <div id="MoveToClass" title="Move to a Class" style="display: none">
            <sep:AccessClassDropdown ID="MoveAccessClassDropdown" runat="server" CssClass="form-control" ClientIDMode="Static" />
        </div>

        <div id="AddAccessKey" title="Add Access Key" style="display: none">
            <sep:AccessKeyDropdown ID="AddAccessKeyDropdown" runat="server" CssClass="form-control" ClientIDMode="Static" />
        </div>

        <div id="RemoveAccessKey" title="Remove Access Key" style="display: none">
            <sep:AccessKeyDropdown ID="RemoveAccessKeyDropdown" runat="server" CssClass="form-control" ClientIDMode="Static" />
        </div>

        <div id="AddToGroupList" title="Add to Group List" style="display: none">
            <sep:GroupListDropdown ID="GroupListDropdown" runat="server" CssClass="form-control" ClientIDMode="Static" />
        </div>

    </asp:Panel>
</asp:content>