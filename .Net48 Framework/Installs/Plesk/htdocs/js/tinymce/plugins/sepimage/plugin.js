﻿tinymce.PluginManager.add('sepimage', function (editor) {

    function openmanager() {
        editor.focus(true);
        var title = "Insert Image";
        if (typeof editor.settings.filemanager_title !== "undefined" && editor.settings.filemanager_title) {
            title = editor.settings.filemanager_title;
        }
        var sort_by = "";
        var descending = "false";
        if (typeof editor.settings.filemanager_sort_by !== "undefined" && editor.settings.filemanager_sort_by)
            sort_by = editor.settings.filemanager_sort_by;
        if (typeof editor.settings.filemanager_descending !== "undefined" && editor.settings.filemanager_descending)
            descending = editor.settings.filemanager_descending;
        win = editor.windowManager.open({
            title: title,
            file: editor.settings.external_filemanager_path,
            width: 680,
            height: 530,
            inline: 1,
            resizable: true,
            maximizable: true
        });
    }

    // Add a button that opens a window
    editor.addButton('sepimage', {
        tooltip: 'Insert Image',
        icon: 'browse',
        onclick: openmanager
    });

});