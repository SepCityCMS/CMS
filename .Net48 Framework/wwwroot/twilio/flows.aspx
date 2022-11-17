<%@ page title="Twilio Control Panel" language="C#" masterpagefile="Site.Master"
    codebehind="flows.aspx.cs" inherits="wwwroot.twilio.flows" %>

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
                    <li><a href="flows.aspx">Flows</a></li>
                    <li><a href="flow_modify.aspx">Add Flow</a></li>
                </ul>
            </div>
        </nav>

        <h3>Flows</h3>

        <span class="successNotification" id="successNotification">
            <span ID="DeleteResult" runat="server"></span>
        </span>

        <div class="GridViewStyle">
            <div class="GridViewFilter">
                <div class="GridViewFilterLeft">
                    <select ID="FilterDoAction" runat="server" Class="GridViewAction" ClientIDMode="Static">
                        <option value="">Select an Action</option>
                        <option value="DeleteFlows">Delete Flows</option>
                    </select>
                    <button class="btn btn-light" ID="RunAction" runat="server" OnServerClick="RunAction_Click" onclick="if(ExecuteAction(this, 'FlowID') == false) {return false} else">GO</button>
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
                            <input type="checkbox" id="FlowID<%#
                this.Eval("FlowID").ToString() %>"
                                value="<%#
                this.Eval("FlowID").ToString() %>"
                                onclick="gridviewSelectRow(this);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="20px">
                        <ItemTemplate>
                            <a href="flow_modify.aspx?FlowID=<%#
                this.Eval("FlowID").ToString() %>">
                                <img src="../images/public/edit.png" alt="Edit" />
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Flow Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <%#
                this.Eval("FlowName").ToString() %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>
</asp:content>