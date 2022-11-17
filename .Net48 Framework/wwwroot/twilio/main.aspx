<%@ page title="Twilio Control Panel" language="C#" masterpagefile="Site.Master"
    codebehind="main.aspx.cs" inherits="wwwroot.twilio.main" %>

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
        <h3>Messages (Inbox)</h3>

        <span class="successNotification" id="successNotification">
            <span ID="DeleteResult" runat="server"></span>
        </span>

        <div class="GridViewStyle">
            <div class="GridViewFilter">
                <div class="GridViewFilterLeft">
                    <select ID="FilterDoAction" runat="server" Class="GridViewAction" ClientIDMode="Static">
                        <option value="">Select an Action</option>
                        <option value="DeleteRecordings">Delete Recordings</option>
                    </select>
                    <button class="btn btn-light" ID="RunAction" runat="server" OnServerClick="RunAction_Click" onclick="if(ExecuteAction(this, 'RecordID') == false) {return false} else">GO</button>
                </div>
                <div class="GridViewFilterRight">
                </div>
            </div>

            <input type="hidden" id="UniqueIDs" runat="server" clientidmode="Static" value="" />

            <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="false" ClientIDMode="Static"
                CssClass="GridViewStyle" AllowPaging="false">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="20px">
                        <HeaderTemplate>
                            <input type="checkbox" id="checkAll" onclick="gridviewCheckAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type="checkbox" id="RecordID<%#
                this.Eval("Sid").ToString() %>"
                                value="<%#
                this.Eval("Sid").ToString() %>"
                                onclick="gridviewSelectRow(this);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date Created" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <%#
                this.Eval("DateCreated") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Duration" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <%#
                this.Eval("Duration") %> secs
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Play Recording" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <div style="width: 480px">
                                <audio controls>
                                    <source src="<%#
                this.Eval("Url") %>" type="audio/mpeg">
                                    Your browser does not support the audio element.
                                </audio>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>
</asp:content>