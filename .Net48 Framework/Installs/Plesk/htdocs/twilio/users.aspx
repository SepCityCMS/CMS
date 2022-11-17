﻿<%@ page title="Twilio Control Panel" language="C#" masterpagefile="Site.Master"
    codebehind="users.aspx.cs" inherits="wwwroot.twilio.users" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <script src="<%= this.GetInstallFolder(true) %>js/gridview.js" type="text/javascript"></script>
    <script src="<%= this.GetInstallFolder(true) %>js/management.js" type="text/javascript"></script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <nav class="navbar navbar-inverse" role="banner">
            <div class="collapse navbar-collapse navbar-left">
                <ul class="nav navbar-nav">
                    <li><a href="users.aspx">Users</a></li>
                    <li><a href="user_modify.aspx">Add User</a></li>
                    <li><a href="groups.aspx">Groups</a></li>
                    <li><a href="group_modify.aspx">Add Group</a></li>
                </ul>
            </div>
        </nav>

        <h3>Users</h3>

        <span class="successNotification" id="successNotification">
            <span ID="DeleteResult" runat="server"></span>
        </span>

        <div class="GridViewStyle">
            <div class="GridViewFilter">
                <div class="GridViewFilterLeft">
                    <select ID="FilterDoAction" runat="server" Class="GridViewAction" ClientIDMode="Static">
                        <option value="">Select an Action</option>
                        <option value="DeleteUsers">Delete Users</option>
                    </select>
                    <button class="btn btn-light" ID="RunAction" runat="server" OnServerClick="RunAction_Click" onclick="if(ExecuteAction(this, 'UserID') == false) {return false} else">GO</button>
                </div>
                <div class="GridViewFilterRight">
                </div>
            </div>

            <input type="hidden" id="UniqueIDs" runat="server" clientidmode="Static" value="" />

            <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="false" ClientIDMode="Static"
                CssClass="GridViewStyle" AllowPaging="false" EnableViewState="True">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="20px">
                        <HeaderTemplate>
                            <input type="checkbox" id="checkAll" onclick="gridviewCheckAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type="checkbox" id="UserID<%#
                this.Eval("UserID").ToString() %>"
                                value="<%#
                this.Eval("UserID").ToString() %>"
                                onclick="gridviewSelectRow(this);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="20px">
                        <ItemTemplate>
                            <a href="user_modify.aspx?UserID=<%#
                this.Eval("UserID").ToString() %>">
                                <img src="../images/public/edit.png" alt="Edit" />
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <%#
                this.Eval("UserName").ToString() %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Full Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <%#
                this.Eval("FullName").ToString() %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Phone Number" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <%#
                this.Eval("PhoneNumber").ToString() %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Groups" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <%#
                this.List_Groups(this.Eval("GroupIDs").ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>
</asp:content>