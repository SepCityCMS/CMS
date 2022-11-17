<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="stocks.aspx.cs" inherits="wwwroot.stocks" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <span ID="StockQuotes" runat="server"></span>

    <div class="ModFormDiv">

        <h4 id="ModifyLegend" runat="server">Add Stock Quote</h4>
        <input type="hidden" runat="server" id="EventID" />

        <div class="mb-3">
            <label id="StockSymbolsLabel" clientidmode="Static" runat="server" for="StockSymbols">Stock Symbols (Add multiple quotes by seperating them by comma's):</label>
            <input type="text" id="StockSymbols" runat="server" class="form-control" clientidmode="Static" />
            <asp:CustomValidator ID="StockSymbolsRequired" runat="server" ControlToValidate="StockSymbols"
                ClientValidationFunction="customFormValidator" ErrorMessage="Stock Symbols is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>