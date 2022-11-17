<%@ page title="Twilio Control Panel" viewstatemode="Enabled" language="C#" masterpagefile="Site.Master"
    codebehind="softphone.aspx.cs" inherits="wwwroot.twilio.softphone" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link href="softphone.css" rel="stylesheet">
    <script type="text/javascript" src="//media.twiliocdn.com/sdk/js/client/v1.4/twilio.min.js"></script>
    <link href="<%= this.GetInstallFolder(true) %>skins/public/styles/intlTelInput.css" rel="stylesheet">
    <script type="text/javascript">
        $(document).ready(function () {
            $('.num').click(function () {
                var num = $(this);
                var text = $.trim(num.find('.txt').clone().children().remove().end().text());
                var telNumber = $('#phone-number');
                $(telNumber).val(telNumber.val() + text);
            });
        });
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <div class="container softphone-container">
            <div class="mb-3">
                <div class="col-md-4 col-md-offset-4 phone">
                    <div class="row1">
                        <div class="col-md-12">
                            <asp:DropDownList ID="CallFromNumber" runat="server" CssClass="form-control" AutoPostBack="True" clientidmode="Static" EnableViewState="True" OnSelectedIndexChanged="CallFromNumber_SelectedIndex">
                            </asp:DropDownList>
                            <input type="tel" name="name" id="phone-number" class="form-control tel"<%= Convert.ToString(!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("Number")) ? " value=\"" + this.FormatNumber(SepCommon.SepCore.Request.Item("Number")) + "\"" : "") %> />
                            <div id="PhoneStatus"></div>
                            <div class="num-pad">
                            <div class="span4">
                                <div class="num">
                                    <div class="txt">1</div>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="num">
                                    <div class="txt">2 <span class="small"><p>ABC</p></span></div>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="num">
                                    <div class="txt">3 <span class="small"><p>DEF</p></span></div>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="num">
                                    <div class="txt">4 <span class="small"><p>GHI</p></span></div>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="num">
                                    <div class="txt">5 <span class="small"><p>JKL</p></span></div>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="num">
                                    <div class="txt">6 <span class="small"><p>MNO</p></span></div>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="num">
                                    <div class="txt">7 <span class="small"><p>PQRS</p></span></div>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="num">
                                    <div class="txt">8 <span class="small"><p>TUV</p></span></div>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="num">
                                    <div class="txt">9 <span class="small"><p>WXYZ</p></span></div>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="num">
                                    <div class="txt">*</div>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="num">
                                    <div class="txt">0 <span class="small"><p>+</p></span></div>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="num">
                                    <div class="txt">#</div>
                                </div>
                            </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <input type="button" id="button-call" style="margin-bottom:10px;" class="btn btn-success btn-block flatbtn" value="Call" />
                            <input type="button" id="button-hangup" style="margin-bottom:10px;" class="btn btn-danger btn-block flatbtn" value="Hangup" />
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
            </div>
        </div>

        <label>Ringtone Devices</label>
        <select id="ringtone-devices" multiple></select>
        <label>Speaker Devices</label>
        <select id="speaker-devices" multiple></select><br />

        <br /><br />

        <label>Mic Volume</label>
        <div id="input-volume"></div><br /><br />
        <label>Speaker Volume</label>
        <div id="output-volume"></div>

        <div id="output-selection"></div>
        <div id="volume-indicators"></div>

        <script src="<%= this.GetInstallFolder(true) %>js/intlTelInput.js" type="text/javascript"></script>
        <script src="<%= this.GetInstallFolder(true) %>js/softphone.js" type="text/javascript"></script>

        <script type="text/javascript">
            $("#phone-number").intlTelInput({
                formatOnDisplay: true,
                nationalMode: true,
                utilsScript: "<%= this.GetInstallFolder(true) %>js/intlTelInput-utils.js"
            });
        </script>
    </asp:Panel>
</asp:content>