<%@ Page Language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master" 
    CodeBehind="default.aspx.cs" Inherits="wwwroot.Video._default" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="manifest" href="manifest.json"/>
    <style>
      div#previewVideoDiv video {
        max-width:100%;
        max-height:100%;
      }
      
      div#userVideoDiv video {
        max-width:100%;
        max-height:100%;
      }
    </style>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">
    
    <input type="hidden" id="VideoID" name="VideoID" runat="server" clientidmode="Static" />

    <noscript>You need to enable JavaScript to run this app.</noscript>
    <div id="root" class="row"></div>
    
    <script>!function (e) { function r(r) { for (var n, p, i = r[0], l = r[1], a = r[2], c = 0, s = []; c < i.length; c++)p = i[c], Object.prototype.hasOwnProperty.call(o, p) && o[p] && s.push(o[p][0]), o[p] = 0; for (n in l) Object.prototype.hasOwnProperty.call(l, n) && (e[n] = l[n]); for (f && f(r); s.length;)s.shift()(); return u.push.apply(u, a || []), t() } function t() { for (var e, r = 0; r < u.length; r++) { for (var t = u[r], n = !0, i = 1; i < t.length; i++) { var l = t[i]; 0 !== o[l] && (n = !1) } n && (u.splice(r--, 1), e = p(p.s = t[0])) } return e } var n = {}, o = { 1: 0 }, u = []; function p(r) { if (n[r]) return n[r].exports; var t = n[r] = { i: r, l: !1, exports: {} }; return e[r].call(t.exports, t, t.exports, p), t.l = !0, t.exports } p.m = e, p.c = n, p.d = function (e, r, t) { p.o(e, r) || Object.defineProperty(e, r, { enumerable: !0, get: t }) }, p.r = function (e) { "undefined" != typeof Symbol && Symbol.toStringTag && Object.defineProperty(e, Symbol.toStringTag, { value: "Module" }), Object.defineProperty(e, "__esModule", { value: !0 }) }, p.t = function (e, r) { if (1 & r && (e = p(e)), 8 & r) return e; if (4 & r && "object" == typeof e && e && e.__esModule) return e; var t = Object.create(null); if (p.r(t), Object.defineProperty(t, "default", { enumerable: !0, value: e }), 2 & r && "string" != typeof e) for (var n in e) p.d(t, n, function (r) { return e[r] }.bind(null, n)); return t }, p.n = function (e) { var r = e && e.__esModule ? function () { return e.default } : function () { return e }; return p.d(r, "a", r), r }, p.o = function (e, r) { return Object.prototype.hasOwnProperty.call(e, r) }, p.p = "/"; var i = this.webpackJsonpvideoapp = this.webpackJsonpvideoapp || [], l = i.push.bind(i); i.push = r, i = i.slice(); for (var a = 0; a < i.length; a++)r(i[a]); var f = l; t() }([])</script>

    </script>
    <script src="js/2.95456b74.chunk.js">
    </script>
    <script src="js/main.cf49d147.chunk.js">
    </script>

</asp:content>