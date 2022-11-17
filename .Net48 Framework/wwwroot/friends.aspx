﻿<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="friends.aspx.cs" inherits="wwwroot.friends" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script src="<%= this.GetInstallFolder(true) %>js/gridview.js" type="text/javascript"></script>
    <script src="<%= this.GetInstallFolder(true) %>js/management.js" type="text/javascript"></script>
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function ExecuteApproveAction(sID) {

            try {
                if (document.getElementById("ApproveDoAction").value == "") {
                    alert("You must select an action from the dropdown.");
                    return false;
                }
            } catch (ex) {
            }

            var allInputs = document.getElementsByTagName("input");
            var sUnqiueIDs = "";
            for (var i = 0; i < allInputs.length; i++) {
                if (allInputs[i].type == "checkbox" && Left(allInputs[i].id, sID.length) == sID) {
                    if (allInputs[i].checked) {
                        sUnqiueIDs += "," + allInputs[i].value;
                        allInputs[i].checked = false;
                    }
                }
            }
            sUniqueIDs = Right(sUnqiueIDs, sUnqiueIDs.length - 1);
            document.getElementById("UniqueIDs").value = sUniqueIDs;
            return true;
        }

        $(document)
            .ready(function () {
                restyleGridView("#ApproveFriendsGrid");
            });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <h1>My Friends</h1>

    <span class="successNotification" id="saveNotification">
        <span ID="SaveResult" runat="server"></span>
    </span>

    <p>
        <label>Add a New Friend:</label>
        <input type="text" id="FriendUserName" runat="server" class="ignore" />
        <asp:Button ID="SaveFriend" runat="server" Text="Save" />
    </p>

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
                            <option value="DeleteFriends">Delete Friend(s)</option>
                        </select>
                        <span class="input-group-btn">
                            <button class="btn btn-light" id="RunAction" runat="server" onserverclick="RunAction_Click" onclick="if(ExecuteAction(this, 'FriendID') == false) {return false} else">Go!</button>
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

        <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
            CssClass="GridViewStyle" AllowPaging="true" OnPageIndexChanging="ManageGridView_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast"
            OnSorting="ManageGridView_Sorting" EnableViewState="True">
            <Columns>
                <asp:TemplateField ItemStyle-Width="20px">
                    <HeaderTemplate>
                        <input type="checkbox" id="checkAll" onclick="gridviewCheckAll(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <input type="checkbox" id="FriendID<%#
                this.Eval("FriendID") %>"
                            value="<%#
                this.Eval("FriendID") %>"
                            onclick="gridviewSelectRow(this);" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="User Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="FromUsername">
                    <ItemTemplate>
                        <a href="<%= this.GetInstallFolder() %>userinfo.aspx?UserID=<%#
                this.Eval("AddedUserID") %>"><%#
                this.Eval("Username") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="BirthDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="BirthDate">
                    <ItemTemplate>
                        <%#
                this.Format_Date(this.Eval("BirthDate").ToString()) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Gender" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Male">
                    <ItemTemplate>
                        <%#
                this.Eval("Gender") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Approved" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Approved">
                    <ItemTemplate>
                        <%#
                this.Eval("Approved") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Online" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                Convert.ToString(Convert.ToBoolean(this.Eval("isOnline")) ? "Online" : "Offline") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pagination-ys" />
        </asp:GridView>
    </div>

    <div class="GridViewStyle" id="ApproveGrid" runat="server">
        <h2>Friends Waiting for Approval</h2>
        <div class="GridViewFilter">
            <div class="GridViewFilterLeft">
                <select id="ApproveDoAction" runat="server" class="GridViewAction" clientidmode="Static">
                    <option value="">Select an Action</option>
                    <option value="ApproveFriends">Approve Friend(s)</option>
                </select>
                <asp:Button ID="ApproveButton" runat="server" Text="GO" OnClientClick="return ExecuteApproveAction('ApproveFriendID')" />
            </div>
            <div class="GridViewFilterRight">
            </div>
        </div>

        <input type="hidden" id="ApproveFriendID" runat="server" clientidmode="Static" value="" />

        <asp:GridView ID="ApproveFriendsGrid" runat="server" AutoGenerateColumns="False" AllowSorting="false" ClientIDMode="Static"
            CssClass="GridViewStyle" AllowPaging="false">
            <Columns>
                <asp:TemplateField ItemStyle-Width="20px">
                    <HeaderTemplate>
                        <input type="checkbox" id="checkAll" onclick="gridviewCheckAll(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <input type="checkbox" id="ApproveFriendID<%#
                this.Eval("FriendID") %>"
                            value="<%#
                this.Eval("FriendID") %>"
                            onclick="
    gridviewSelectRow(this);" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="User Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="FromUsername">
                    <ItemTemplate>
                        <a href="<%= this.GetInstallFolder() %>userinfo.aspx?UserID=<%#
                this.Eval("AddedUserID") %>"><%#
                this.Eval("Username") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="BirthDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="BirthDate">
                    <ItemTemplate>
                        <%#
                this.Format_Date(this.Eval("BirthDate").ToString()) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Gender" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Male">
                    <ItemTemplate>
                        <%#
                this.Eval("Gender") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Online" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Approved">
                    <ItemTemplate>
                        <%#
                Convert.ToString(Convert.ToBoolean(this.Eval("isOnline")) ? "Online" : "Offline") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:content>