<%@ Page Language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master" 
    CodeBehind="video.aspx.cs" Inherits="wwwroot.video" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script src="//media.twiliocdn.com/sdk/js/video/releases/2.0.0/twilio-video.min.js"></script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <div id="local-media"></div>

    <script>
        const { createLocalVideoTrack } = require('twilio-video');

        createLocalVideoTrack().then(track => {
            const localMediaContainer = document.getElementById('local-media');
            localMediaContainer.appendChild(track.attach());
        });
    </script>

</asp:content>