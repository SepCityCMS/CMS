<%@ page language="C#" codebehind="search.aspx.cs" inherits="wwwroot.search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="site.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <div>
            <input type="text" id="SearchText" runat="server" ckass="form-control" />
            <asp:button id="SearchButton" runat="server" cssclass="btn btn-light" text="Go!" />
        </div>
        <br />
        <div>
            <asp:literal id="SearchResults" runat="server" />
        </div>
    </form>
</body>
</html>