<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="wwwroot.spadmin.templatedesigner._default" %>

<!doctype html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <title>Template Designer</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/css/bootstrap.min.css" type="text/css">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" type="text/css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/grapesjs/0.18.2/css/grapes.min.css" integrity="sha512-NDzKd6WMSccrZDcBHuGVyn60rhif0uhABUiWIqZ7wwQAFqiCqQUDG9xvgPr4XAUuhZBzHKky5TkMinZ8+gSJqw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.1.4/toastr.min.css" integrity="sha512-6S2HWzVFxruDlZxI3sXOZZ4/eJ8AcxkQH1+JjSe/ONCEqR9L4Ysq5JdT5ipqtzU7WHalNwzwBv+iE51gNHJNqQ==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <link rel="stylesheet" href="https://unpkg.com/grapesjs-preset-webpage@0.1.11/dist/grapesjs-preset-webpage.min.css" type="text/css">
    <link rel="stylesheet" href="styles/styles.css" type="text/css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/grapesjs/0.18.2/grapes.min.js" integrity="sha512-Y1Ogf5eKqmkBwkDh3DHr4zMGVCrDBjWETFYTpsJrbwUyn7p3g20bz1Rgm1+hF4yY/Q15eBFf1WWqq9vMDWEm8g==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.1.4/toastr.min.js" integrity="sha512-lbwH47l/tPXJYG9AcFNoJaTMhGvYWhVM9YI43CT+uteTRRaiLCui8snIgyAN8XWgNjNhCqlAUdzZptso6OCoFQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="https://unpkg.com/grapesjs-preset-webpage@0.1.11/dist/grapesjs-preset-webpage.min.js"></script>
    <script src="https://unpkg.com/grapesjs-lory-slider@0.1.5/dist/grapesjs-lory-slider.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/grapesjs-tabs@1.0.6/dist/grapesjs-tabs.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/grapesjs-custom-code@0.1.3/dist/grapesjs-custom-code.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/grapesjs-touch@0.1.1/dist/grapesjs-touch.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/grapesjs-parser-postcss@0.1.1/dist/grapesjs-parser-postcss.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/grapesjs-tooltip@0.1.5/dist/grapesjs-tooltip.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/grapesjs-tui-image-editor@0.1.3/dist/grapesjs-tui-image-editor.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/ckeditor/4.9.2/ckeditor.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/grapesjs-plugin-ckeditor@0.0.9/dist/grapesjs-plugin-ckeditor.min.js"></script>
    <script src="js/save-template.min.js"></script>
    <style>
      body,
      html {
        height: 100%;
        margin: 0;
      }
    </style>
  </head>
  <body>

    <div id="loader" runat="server" clientidmode="static"></div>
  
    <input type="hidden" id="TemplateID" runat="server" />
    <input type="hidden" id="PortalID" runat="server" />
    <input type="hidden" id="PageID" runat="server" />
    <input type="hidden" id="PageTitle" runat="server" />
    <input type="hidden" id="MenuID" runat="server" />
    <input type="hidden" id="ModuleID" runat="server" />
      
    <div id="gjs" style="height:0px; overflow:hidden; display:none;" runat="server" clientidmode="static">
        <asp:literal ID="templateHTML" runat="server" clientidmode="static" />
    </div>
      
    <div id="jscript" runat="server" clientidmode="static">
        <script type="text/javascript">
            var editor = grapesjs.init({
                avoidInlineStyle: 1,
                height: '100%',
                container: '#gjs',
                fromElement: 1,
                showOffsets: 1,
                styleManager: { clearProperties: 1 },
                plugins: [
                    'gjs-preset-webpage',
                    'grapesjs-lory-slider',
                    'grapesjs-tabs',
                    'grapesjs-custom-code',
                    'grapesjs-touch',
                    'grapesjs-parser-postcss',
                    'grapesjs-tooltip',
                    'grapesjs-tui-image-editor',
                    'gjs-plugin-ckeditor',
                    'save-template'
                ],
                storageManager: { type: null },
                pluginsOpts: {
                    'gjs-preset-webpage': {
                        exportOpts: {
                            addExportBtn: 0
                        },
                        formsOpts: false
                    },
                    'save-template': {
                        blocks: ['account-menu', 'random-poll', 'breadcrumb', 'site-menu-1', 'site-menu-2', 'site-menu-3', 'site-menu-4', 'site-menu-5', 'site-menu-6', 'site-menu-7', 'site-menu-1v', 'site-menu-2v', 'site-menu-3v', 'site-menu-4v', 'site-menu-5v', 'site-menu-6v', 'site-menu-7v', 'module-top-menu', 'company-slogan', 'event-calendar', 'friends-list', 'newest-members', 'member-statistics', 'newsletters', 'site-search', 'site-logo', 'stock-quotes', 'website-name', 'whos-online', 'adserver', 'sponsors']
                    }
                },
                canvas: {
                    styles: ['https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/css/bootstrap.min.css', 'https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css', '<asp:literal ID="templateStyle" runat="server" />', '<asp:literal ID="customStyle" runat="server" />']
                }

            });

            var pn = editor.Panels;
            var modal = editor.Modal;
            var cmdm = editor.Commands;

            //////////////////////////////////////////////////////////////////
            //              SAVE TEMPLATE
            //////////////////////////////////////////////////////////////////
            var mdlClass = 'gjs-mdl-dialog-sm';
            pn.addButton('options', {
                id: 'save-template',
                className: 'fa fa-save',
                command: function () {
                    editor.runCommand('gjs-save-template')
                },
                attributes: {
                    'title': 'Save Template',
                    'data-tooltip-pos': 'bottom'
                },
            });
            //////////////////////////////////////////////////////////////////
            //              END SAVE TEMPLATE
            //////////////////////////////////////////////////////////////////

            //////////////////////////////////////////////////////////////////
            //              ADD DROPDOWN FOR PAGE SELECTION
            //////////////////////////////////////////////////////////////////
            pn.addButton('options', {
                id: 'change-page',
                className: 'change-page',
                label: 'Change Page (<asp:literal ID="PageName" runat="server" />)',
                command: function () {
                    editor.runCommand('gjs-change-page')
                },
                attributes: {
                    'data-tooltip-pos': 'bottom'
                },
            });
            //////////////////////////////////////////////////////////////////
            //              END DROPDOWN FOR PAGE SELECTION
            //////////////////////////////////////////////////////////////////

            cmdm.add('canvas-clear', function () {
                if (confirm('Are you sure to clean the canvas?')) {
                    editor.DomComponents.clear();
                    setTimeout(function () { localStorage.clear() }, 0)
                }
            });
            cmdm.add('set-device-desktop', {
                run: ed => ed.setDevice('Desktop'),
                stop() { },
            });
            cmdm.add('set-device-tablet', {
                run: ed => ed.setDevice('Tablet'),
                stop() { },
            });
            cmdm.add('set-device-mobile', {
                run: ed => ed.setDevice('Mobile portrait'),
                stop() { },
            });

            // Simple warn notifier
            var origWarn = console.warn;
            toastr.options = {
                closeButton: true,
                preventDuplicates: true,
                showDuration: 250,
                hideDuration: 150
            };
            console.warn = function (msg) {
                if (msg.indexOf('[undefined]') == -1) {
                    toastr.warning(msg);
                }
                origWarn(msg);
            };

            // Add and beautify tooltips
            [['sw-visibility', 'Show Borders'], ['preview', 'Preview'], ['fullscreen', 'Fullscreen'],
            ['undo', 'Undo'], ['redo', 'Redo'], ['canvas-clear', 'Clear canvas']]
                .forEach(function (item) {
                    pn.getButton('options', item[0]).set('attributes', { title: item[1], 'data-tooltip-pos': 'bottom' });
                });
            [['open-sm', 'Style Manager'], ['open-layers', 'Layers'], ['open-blocks', 'Blocks']]
                .forEach(function (item) {
                    pn.getButton('views', item[0]).set('attributes', { title: item[1], 'data-tooltip-pos': 'bottom' });
                });
            var titles = document.querySelectorAll('*[title]');

            for (var i = 0; i < titles.length; i++) {
                var el = titles[i];
                var title = el.getAttribute('title');
                title = title ? title.trim() : '';
                if (!title)
                    break;
                el.setAttribute('data-tooltip', title);
                el.setAttribute('title', '');
            }

            // Show borders by default
            pn.getButton('options', 'sw-visibility').set('active', 1);

            // Do stuff on load
            editor.on('load', function () {
                var $ = grapesjs.$;

                // Load and show settings and style manager
                var openTmBtn = pn.getButton('views', 'open-tm');
                openTmBtn && openTmBtn.set('active', 1);
                var openSm = pn.getButton('views', 'open-sm');
                openSm && openSm.set('active', 1);

                // Add Settings Sector
                var traitsSector = $('<div class="gjs-sm-sector no-select">' +
                    '<div class="gjs-sm-title"><span class="icon-settings fa fa-cog"></span> Settings</div>' +
                    '<div class="gjs-sm-properties" style="display: none;"></div></div>');
                var traitsProps = traitsSector.find('.gjs-sm-properties');
                traitsProps.append($('.gjs-trt-traits'));
                $('.gjs-sm-sectors').before(traitsSector);
                traitsSector.find('.gjs-sm-title').on('click', function () {
                    var traitStyle = traitsProps.get(0).style;
                    var hidden = traitStyle.display == 'none';
                    if (hidden) {
                        traitStyle.display = 'block';
                    } else {
                        traitStyle.display = 'none';
                    }
                });

                // Open block manager
                var openBlocksBtn = editor.Panels.getButton('views', 'open-blocks');
                openBlocksBtn && openBlocksBtn.set('active', 1);

                var moveBlockHtml = $("div.gjs-blocks-no-cat div,gjs-blocks-c div").html();
                $("div.gjs-blocks-no-cat div,gjs-blocks-c div").remove();
                $("div.gjs-block-category div.gjs-blocks-c").eq(1).append(moveBlockHtml);

                $('.gjs-block-category svg').css('max-height', '50px');
                $('.gjs-block-category svg').css('max-width', '50px');
            });
        </script>
    </div>

  </body>
</html>
