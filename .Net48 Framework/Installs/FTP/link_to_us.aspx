<%@ page title="Link to Us" language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="link_to_us.aspx.cs" inherits="wwwroot.link_to_us" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        function selectCode(imageId) {
            $('#' + imageId).parent().find('textarea').select();
        }
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <div id="PageContent">
        <div class="mb-3">
            To get credit as an affiliate you can either copy and paste the following "Affiliate URL" or the HTML Code below each image to your web site to get credit.
        </div>

        <div class="mb-3">
            Your Affiliate URL: <span ID="AffiliateURL" runat="server"></span>
        </div>
    </div>

    <span class="successNotification" id="successNotification">
        <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <asp:GridView ID="AffiliateImagesView" runat="server" AutoGenerateColumns="False" AllowSorting="False"
                  ClientIDMode="Static" ShowHeader="false" BorderWidth="0px">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <img src="<%#
                Eval("ImageURL")%>" alt="Affiliate Image" />
                    <br />
                    <b>HTML Code:</b>
                    <br />
                    <textarea ID="HTMLCode" runat="server" class="form-control" readonly="readonly" style="width:450px"><%# Eval("HTMLCode")%></textarea>
                    <a href="javascript:void(0)" onclick="selectCode('img<%# Eval("ImageID")%>')" id='img<%# Eval("ImageID")%>'>Select HTML Code</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:content>