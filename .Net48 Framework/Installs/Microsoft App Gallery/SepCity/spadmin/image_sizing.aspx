<%@ page title="Image Sizing" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="image_sizing.aspx.cs" inherits="wwwroot.image_sizing" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <h2>
            <span ID="PageHeader" runat="server" Text="Imaging Resizing"></span>
        </h2>

        <div id="ImgSizeMainContent">

            <p id="ArticlesRow" runat="server">
                <label ID="ArticlesLabel" clientidmode="Static" runat="server" for="Enable35"><strong>Articles:</strong></label>
                Enabled: <asp:CheckBox ID="Enable35" runat="server" Checked="true" />
                Height: <input type="text" id="Height35" runat="server" class="form-control inline-block" style="width:70px;" />
                Width: <input type="text" id="Width35" runat="server" class="form-control inline-block" style="width:70px;" />
            </p>

            <p id="AuctionRow" runat="server">
                <label ID="AuctionLabel" clientidmode="Static" runat="server" for="Enable31"><strong>Auction:</strong></label>
                Enabled: <asp:CheckBox ID="Enable31" runat="server" Checked="true" />
                Height: <input type="text" id="Height31" runat="server" class="form-control inline-block" style="width:70px;" />
                Width: <input type="text" id="Width31" runat="server" class="form-control inline-block" style="width:70px;" />
            </p>

            <p id="BusinessesRow" runat="server">
                <label ID="BusinessLabel" clientidmode="Static" runat="server" for="Enable20"><strong>Business Directory:</strong></label>
                Enabled: <asp:CheckBox ID="Enable20" runat="server" Checked="true" />
                Height: <input type="text" id="Height20" runat="server" class="form-control inline-block" style="width:70px;" />
                Width: <input type="text" id="Width20" runat="server" class="form-control inline-block" style="width:70px;" />
            </p>

            <p id="ClassifiedsRow" runat="server">
                <label ID="ClassifiedLabel" clientidmode="Static" runat="server" for="Enable44"><strong>Classified Ads:</strong></label>
                Enabled: <asp:CheckBox ID="Enable44" runat="server" Checked="true" />
                Height: <input type="text" id="Height44" runat="server" class="form-control inline-block" style="width:70px;" />
                Width: <input type="text" id="Width44" runat="server" class="form-control inline-block" style="width:70px;" />
            </p>

            <p id="DownloadsRow" runat="server">
                <label ID="DownloadsLabel" clientidmode="Static" runat="server" for="Enable10"><strong>Downloads:</strong></label>
                Enabled: <asp:CheckBox ID="Enable10" runat="server" Checked="true" />
                Height: <input type="text" id="Height10" runat="server" class="form-control inline-block" style="width:70px;" />
                Width: <input type="text" id="Width10" runat="server" class="form-control inline-block" style="width:70px;" />
            </p>

            <p id="EventCalendarRow" runat="server">
                <label ID="EventsLabel" clientidmode="Static" runat="server" for="Enable46"><strong>Event Calendar:</strong></label>
                Enabled: <asp:CheckBox ID="Enable46" runat="server" Checked="true" />
                Height: <input type="text" id="Height46" runat="server" class="form-control inline-block" style="width:70px;" />
                Width: <input type="text" id="Width46" runat="server" class="form-control inline-block" style="width:70px;" />
            </p>

            <p id="MatchMakerRow" runat="server">
                <label ID="MatchLabel" clientidmode="Static" runat="server" for="Enable18"><strong>Match Maker:</strong></label>
                Enabled: <asp:CheckBox ID="Enable18" runat="server" Checked="true" />
                Height: <input type="text" id="Height18" runat="server" class="form-control inline-block" style="width:70px;" />
                Width: <input type="text" id="Width18" runat="server" class="form-control inline-block" style="width:70px;" />
            </p>

            <p id="NewsRow" runat="server">
                <label ID="NewsLabel" clientidmode="Static" runat="server" for="Enable23"><strong>News:</strong></label>
                Enabled: <asp:CheckBox ID="Enable23" runat="server" Checked="true" />
                Height: <input type="text" id="Height23" runat="server" class="form-control inline-block" style="width:70px;" />
                Width: <input type="text" id="Width23" runat="server" class="form-control inline-block" style="width:70px;" />
            </p>

            <p id="AlbumsRow" runat="server">
                <label ID="PhotoAlbumsLabel" clientidmode="Static" runat="server" for="Enable28"><strong>Photo Albums:</strong></label>
                Enabled: <asp:CheckBox ID="Enable28" runat="server" Checked="true" />
                Height: <input type="text" id="Height28" runat="server" class="form-control inline-block" style="width:70px;" />
                Width: <input type="text" id="Width28" runat="server" class="form-control inline-block" style="width:70px;" />
            </p>

            <p id="RealEstateRow" runat="server">
                <label ID="RealEstateLabel" clientidmode="Static" runat="server" for="Enable32"><strong>Real Estate:</strong></label>
                Enabled: <asp:CheckBox ID="Enable32" runat="server" Checked="true" />
                Height: <input type="text" id="Height32" runat="server" class="form-control inline-block" style="width:70px;" />
                Width: <input type="text" id="Width32" runat="server" class="form-control inline-block" style="width:70px;" />
            </p>

            <p id="ShoppingRow" runat="server">
                <label ID="ShopMallLabel" clientidmode="Static" runat="server" for="Enable41"><strong>Shopping Mall:</strong></label>
                Enabled: <asp:CheckBox ID="Enable41" runat="server" Checked="true" />
                Height: <input type="text" id="Height41" runat="server" class="form-control inline-block" style="width:70px;" />
                Width: <input type="text" id="Width41" runat="server" class="form-control inline-block" style="width:70px;" />
            </p>

            <p id="SpeakersRow" runat="server">
                <label ID="SpeakersLabel" clientidmode="Static" runat="server" for="Enable50"><strong>Speaker's Bureau:</strong></label>
                Enabled: <asp:CheckBox ID="Enable50" runat="server" Checked="true" />
                Height: <input type="text" id="Height50" runat="server" class="form-control inline-block" style="width:70px;" />
                Width: <input type="text" id="Width50" runat="server" class="form-control inline-block" style="width:70px;" />
            </p>

            <p id="UserProfilesRow" runat="server">
                <label ID="UserProfilesLabel" clientidmode="Static" runat="server" for="Enable63"><strong>User Profiles:</strong></label>
                Enabled: <asp:CheckBox ID="Enable63" runat="server" Checked="true" />
                Height: <input type="text" id="Height63" runat="server" class="form-control inline-block" style="width:70px;" />
                Width: <input type="text" id="Width63" runat="server" class="form-control inline-block" style="width:70px;" />
            </p>
        </div>
        
        <div class="button-to-bottom">
            <asp:Button ID="SetupSave" runat="server" cssclass="btn btn-primary" Text="Save Changes" onclick="SetupSave_Click" />
            <span><span ID="SaveMessage" runat="server"></span></span>
        </div>
    </asp:Panel>
</asp:content>