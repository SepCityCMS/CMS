!function($) {
    $.toJSON = function(r) {
        if ("object" == typeof JSON && JSON.stringify) return JSON.stringify(r);
        var e = typeof r;
        if (null === r) return "null";
        if ("undefined" == e) return void 0;
        if ("number" == e || "boolean" == e) return r + "";
        if ("string" == e) return $.quoteString(r);
        if ("object" == e) {
            if ("function" == typeof r.toJSON) return JSON.stringify(r.toJSON());
            if (r.constructor === Date) {
                var t = r.getUTCDateAndTime.Month() + 1;
                10 > t && (t = "0" + t);
                var n = r.getUTCDate();
                10 > n && (n = "0" + n);
                var o = r.getUTCFullYear(), i = r.getUTCHours();
                10 > i && (i = "0" + i);
                var f = r.getUTCMinutes();
                10 > f && (f = "0" + f);
                var a = r.getUTCSeconds();
                10 > a && (a = "0" + a);
                var u = r.getUTCMilliseconds();
                return 100 > u && (u = "0" + u), 10 > u && (u = "0" + u),
                    '"' + o + "-" + t + "-" + n + "T" + i + ":" + f + ":" + a + "." + u + 'Z"';
            }
            if (r.constructor === Array) {
                for (var c = [], l = 0; l < r.length; l++) c.push(JSON.stringify(r[l]) || "null");
                return "[" + c.join(",") + "]";
            }
            var s = [];
            for (var S in r) {
                var g, e = typeof S;
                if ("number" == e) g = '"' + S + '"';
                else {
                    if ("string" != e) continue;
                    g = $.quoteString(S);
                }
                if ("function" != typeof r[S]) {
                    var p = JSON.stringify(r[S]);
                    s.push(g + ":" + p);
                }
            }
            return "{" + s.join(", ") + "}";
        }
    }, $.evalJSON = function(src) {
        return "object" == typeof JSON && JSON.parse ? JSON.parse(src) : eval("(" + src + ")");
    }, $.secureEvalJSON = function(src) {
        if ("object" == typeof JSON && JSON.parse) return JSON.parse(src);
        var filtered = src;
        if (filtered = filtered.replace(/\\["\\\/bfnrtu]/g, "@"), filtered = filtered
            .replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, "]"), filtered = filtered
            .replace(/(?:^|:|,)(?:\s*\[)+/g, ""), /^[\],:{}\s]*$/.test(filtered)) return eval("(" + src + ")");
        throw new SyntaxError("Error parsing JSON, source is not valid.");
    }, $.quoteString = function(r) {
        return r.match(_escapeable)
            ? '"' +
            r.replace(_escapeable,
                function(r) {
                    var e = _meta[r];
                    return "string" == typeof e
                        ? e
                        : (e = r.charCodeAt(), "\\u00" + Math.floor(e / 16).toString(16) + (e % 16).toString(16));
                }) +
            '"'
            : '"' + r + '"';
    };
    var _escapeable = /["\\\x00-\x1f\x7f-\x9f]/g,
        _meta = { "\b": "\\b", "	": "\\t", "\n": "\\n", "\f": "\\f", "\r": "\\r", '"': '\\"', "\\": "\\\\" };
}(jQuery);