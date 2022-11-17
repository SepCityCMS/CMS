<%@ page language="C#" codebehind="default.aspx.cs" inherits="wwwroot._default2" %>

<!DOCTYPE html>
<html>
<head>
    <title>SepCity Help Manual</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="robots" content="noindex,follow" />
    <style type="text/css">
        html {
            overflow: hidden;
        }

        body {
            background: #F9F9F9 url('images/background.png') top center repeat-x;
            height: 100%;
            margin: 0;
            overflow: hidden;
            padding: 0;
        }

        #hmheadbox {
            height: 120px;
            left: 10px;
            position: absolute;
            right: 10px;
            top: 5px;
        }

        #navbar {
            font-size: 120%;
            margin: 115px 0 0 5px;
        }

            #navbar img {
                border: none;
                margin-right: 10px;
            }

        #hmnavbox {
            bottom: 10px;
            left: 10px;
            min-width: 50px;
            position: absolute;
            top: 156px;
            width: 245px;
        }

        #SepTitle {
            color: #555555;
            margin-top: 30px;
            position: absolute;
        }

        #seplogo {
            position: absolute;
            right: 0;
            top: 0;
        }

        #hmcontentbox {
            background: #FFF;
            bottom: 10px;
            box-shadow: 0 4px 12px #777;
            left: 260px;
            position: absolute;
            right: 10px;
            top: 130px;
        }

        iframe {
            border: none;
            height: 100%;
            left: 0;
            position: absolute;
            top: 0;
            width: 100%;
        }

        /* Start of EC Software main navigation menus */
    </style>
</head>
<body>
    <div id="hmheadbox">
        <h1 id="SepTitle">Help Manual</h1>
        <p id="seplogo"></p>
        <p id="navbar">
            <a class="selected" href="navmenu.aspx" title="Table of Contents" target="hmnavigation">
                <img src="images/hm_btn_toc.png" alt="Table of Contents"
                    onmouseover="this.src = 'images/hm_btn_toc_orange.png'"
                    onmouseout="this
    .src = 'images/hm_btn_toc.png'" />
            </a>
            <a href="keywords.aspx" title="Keyword Index" target="hmnavigation">
                <img src="images/hm_btn_index.png" alt="Keyword Index"
                    onmouseover="this.src = 'images/hm_btn_index_orange.png'"
                    onmouseout="
this.src = 'images/hm_btn_index.png'" />
            </a>
            <a href="search.aspx" title="Search" target="hmnavigation">
                <img src="images/hm_btn_search.png" alt="Search"
                    onmouseover="this.src = 'images/hm_btn_search_orange.png'"
                    onmouseout="this.src = 'images/hm_btn_search.png'" />
            </a>
        </p>
    </div>
    <div id="hmnavbox">
        <iframe name="hmnavigation" id="hmnavigation" src="navmenu.aspx" scrolling="auto" title="Navigation Pane" frameborder="0"></iframe>
    </div>

    <div id="hmcontentbox">
        <asp:literal id="IFrameContent" runat="server" />
    </div>
</body>
</html>