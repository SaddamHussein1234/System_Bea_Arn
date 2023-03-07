Drupal.locale = { 'pluralFormula': function ($n) { return Number((($n == 1) ? (0) : (($n == 0) ? (1) : (($n == 2) ? (2) : (((($n % 100) >= 3) && (($n % 100) <= 10)) ? (3) : (((($n % 100) >= 11) && (($n % 100) <= 99)) ? (4) : 5)))))); }, 'strings': { "": { "An AJAX HTTP error occurred.": "\u062d\u062f\u062b \u062e\u0637\u0623 \u0623\u062c\u0627\u0643\u0633 HTTP.", "HTTP Result Code: !status": "\u0646\u062a\u064a\u062c\u0629 \u0643\u0648\u062f PHP: !status", "An AJAX HTTP request terminated abnormally.": "\u062a\u0645 \u0625\u0646\u0647\u0627\u0621 \u0637\u0644\u0628 AJAX HTTP \u0628\u0634\u0643\u0644 \u063a\u064a\u0631 \u0639\u0627\u062f\u064a.", "Debugging information follows.": "\u064a\u0644\u064a\u0647 \u0645\u0639\u0644\u0648\u0645\u0627\u062a \u0627\u0644\u062a\u0646\u0642\u064a\u062d.", "Path: !uri": "\u0627\u0644\u0645\u0633\u0627\u0631: !uri", "StatusText: !statusText": "\u0646\u0635 \u0627\u0644\u062d\u0627\u0644\u0629: !statusText", "ResponseText: !responseText": "ResponseText: !responseText", "ReadyState: !readyState": "ReadyState: !readyState", "Hide": "\u0625\u062e\u0641\u0627\u0621", "Show": "\u0639\u0631\u0636", "Re-order rows by numerical weight instead of dragging.": "\u0625\u0639\u0627\u062f\u0629 \u062a\u0631\u062a\u064a\u0628 \u0627\u0644\u0633\u0637\u0648\u0631 \u062d\u0633\u0628 \u0648\u0632\u0646 \u0631\u0642\u0645\u064a \u0628\u062f\u0644\u0627 \u0645\u0646 \u0633\u062d\u0628\u0647\u0627.", "Show row weights": "\u0625\u0638\u0647\u0627\u0631 \u0623\u0648\u0632\u0627\u0646 \u0627\u0644\u0623\u0633\u0637\u0631", "Hide row weights": "\u0625\u062e\u0641\u0627\u0621 \u0623\u0648\u0632\u0627\u0646 \u0627\u0644\u0633\u0637\u0648\u0631", "Drag to re-order": "\u0627\u0633\u062d\u0628 \u0644\u062a\u063a\u064a\u0631 \u0627\u0644\u062a\u0631\u062a\u064a\u0628", "Changes made in this table will not be saved until the form is submitted.": "\u0627\u0644\u062a\u063a\u064a\u064a\u0631\u0627\u062a \u0627\u0644\u062d\u0627\u062f\u062b\u0629 \u0639\u0644\u0649 \u0647\u0630\u0627 \u0627\u0644\u062c\u062f\u0648\u0644 \u0644\u0646 \u062a\u064f\u062d\u0641\u0638 \u0625\u0644\u0627 \u0628\u0639\u062f \u0625\u0631\u0633\u0627\u0644 \u0627\u0644\u0627\u0633\u062a\u0645\u0627\u0631\u0629.", "Customize dashboard": "\u062a\u062e\u0635\u064a\u0635 \u0644\u0648\u062d\u0629 \u0627\u0644\u062a\u062d\u0643\u0645", "Edit": "\u062a\u062d\u0631\u064a\u0631", "(active tab)": "(\u0639\u0644\u0627\u0645\u0629 \u0627\u0644\u062a\u0628\u0648\u064a\u0628 \u0627\u0644\u0646\u0634\u0637\u0629)", "Enabled": "\u0645\u0641\u0639\u0644", "Next": "\u0627\u0644\u062a\u0627\u0644\u064a", "Status": "\u0627\u0644\u062d\u0627\u0644\u0629", "Cancel": "\u0625\u0644\u063a\u0627\u0621", "Disabled": "\u0645\u0639\u0637\u0644", "Size": "\u0627\u0644\u062d\u062c\u0645", "- None -": "- \u0644\u0627 \u0634\u064a\u0621 -", "none": "\u0644\u0627 \u0634\u064a\u0621", "Save": "\u0625\u0631\u0633\u0627\u0644", "Sunday": "\u0627\u0644\u0623\u062d\u062f", "Monday": "\u0627\u0644\u0627\u062b\u0646\u064a\u0646", "Tuesday": "\u0627\u0644\u062b\u0644\u0627\u062b\u0627\u0621", "Wednesday": "\u0627\u0644\u0623\u0631\u0628\u0639\u0627\u0621", "Thursday": "\u0627\u0644\u062e\u0645\u064a\u0633", "Friday": "\u0627\u0644\u062c\u0645\u0639\u0629", "Saturday": "\u0627\u0644\u0633\u0628\u062a", "Add": "\u0625\u0636\u0627\u0641\u0629", "Filename": "\u0627\u0633\u0645 \u0627\u0644\u0645\u0644\u0641", "Upload": "\u0631\u0641\u0639", "Configure": "\u0636\u0628\u0637", "Done": "\u062a\u0645\u0651", "N\/A": "\u063a\u064a\u0631 \u0645\u0648\u062c\u0648\u062f", "OK": "\u0645\u0648\u0627\u0641\u0642", "This field is required.": "\u0647\u0630\u0627 \u0627\u0644\u062d\u0642\u0644 \u0645\u0637\u0644\u0648\u0628.", "Prev": "\u0627\u0644\u0633\u0627\u0628\u0642", "Mon": "\u0627\u0644\u0627\u062b\u0646\u064a\u0646", "Tue": "\u0627\u0644\u062b\u0644\u0627\u062b\u0627\u0621", "Wed": "\u0627\u0644\u0627\u0631\u0628\u0639\u0627\u0621", "Thu": "\u0627\u0644\u062e\u0645\u064a\u0633", "Fri": "\u0627\u0644\u062c\u0645\u0639\u0629", "Sat": "\u0627\u0644\u0633\u0628\u062a", "Sun": "\u0627\u0644\u0623\u062d\u062f", "January": "\u064a\u0646\u0627\u064a\u0631", "February": "\u0641\u0628\u0631\u0627\u064a\u0631", "March": "\u0645\u0627\u0631\u0633", "April": "\u0623\u0628\u0631\u064a\u0644", "May": "\u0645\u0627\u064a\u0648", "June": "\u064a\u0648\u0646\u064a\u0648", "July": "\u064a\u0648\u0644\u064a\u0648", "August": "\u0623\u063a\u0633\u0637\u0633", "September": "\u0633\u0628\u062a\u0645\u0628\u0631", "October": "\u0623\u0643\u062a\u0648\u0628\u0631", "November": "\u0646\u0648\u0641\u0645\u0628\u0631", "December": "\u062f\u064a\u0633\u0645\u0628\u0631", "Allowed HTML tags": "\u0648\u0633\u0648\u0645 \u0625\u062a\u0634.\u062a\u064a.\u0625\u0645.\u0625\u0644 \u0627\u0644\u0645\u0633\u0645\u0648\u062d \u0628\u0647\u0627", "Select all rows in this table": "\u0627\u062e\u062a\u0631 \u0643\u0644 \u0627\u0644\u0635\u0641\u0648\u0641 \u0641\u064a \u0647\u0630\u0627 \u0627\u0644\u062c\u062f\u0648\u0644", "Deselect all rows in this table": "\u0623\u0644\u063a \u0627\u062e\u062a\u064a\u0627\u0631 \u0643\u0644 \u0627\u0644\u0635\u0641\u0648\u0641 \u0641\u064a \u0647\u0630\u0627 \u0627\u0644\u062c\u062f\u0648\u0644", "Today": "\u0627\u0644\u064a\u0648\u0645", "Jan": "\u064a\u0646\u0627\u064a\u0631", "Feb": "\u0641\u0628\u0631\u0627\u064a\u0631", "Mar": "\u0645\u0627\u0631\u0633", "Apr": "\u0623\u0628\u0631\u064a\u0644", "Jun": "\u064a\u0648\u0646\u064a\u0648", "Jul": "\u064a\u0648\u0644\u064a\u0648", "Aug": "\u0623\u063a\u0633\u0637\u0633", "Sep": "\u0633\u0628\u062a\u0645\u0628\u0631", "Oct": "\u0623\u0643\u062a\u0648\u0628\u0631", "Nov": "\u0646\u0648\u0641\u0645\u0628\u0631", "Dec": "\u062f\u064a\u0633\u0645\u0628\u0631", "Su": "\u0627\u0644\u0623\u062d\u062f", "Mo": "\u0627\u0644\u0625\u062b\u0646\u064a\u0646", "Tu": "\u0627\u0644\u062b\u0644\u0627\u062b\u0627\u0621", "We": "\u0627\u0644\u0623\u0631\u0628\u0639\u0627\u0621", "Th": "\u0627\u0644\u062e\u0645\u064a\u0633", "Fr": "\u0627\u0644\u062c\u0645\u0639\u0629", "Sa": "\u0627\u0644\u0633\u0628\u062a", "Not published": "\u0644\u0645 \u064a\u0646\u0634\u0631", "Please wait...": "\u064a\u0631\u062c\u0649 \u0627\u0644\u0627\u0646\u062a\u0638\u0627\u0631...", "mm\/dd\/yy": "mm\/dd\/yy", "Only files with the following extensions are allowed: %files-allowed.": "\u064a\u064f\u0633\u0645\u062d \u0628\u0627\u0644\u0645\u0644\u0641\u0627\u062a \u0627\u0644\u062a\u064a \u062a\u062d\u0645\u0644 \u0647\u0630\u0647 \u0627\u0644\u0627\u0645\u062a\u062f\u0627\u062f\u0627\u062a \u0641\u0642\u0637: %files-allowed.", "Not in book": "\u0644\u064a\u0633 \u0641\u064a \u0627\u0644\u0643\u062a\u0627\u0628", "New book": "\u0643\u062a\u0627\u0628 \u062c\u062f\u064a\u062f", "By @name on @date": "\u0645\u0646 @name \u0628\u062a\u0627\u0631\u064a\u062e \u00a0@date", "By @name": "\u0645\u0646 %name", "Not in menu": "\u063a\u064a\u0631 \u0645\u0648\u062c\u0648\u062f \u0641\u064a \u0627\u0644\u0642\u0627\u0626\u0645\u0629", "Alias: @alias": "\u0627\u0644\u0628\u062f\u064a\u0644: @alias", "No alias": "\u0644\u0627 \u064a\u0648\u062c\u062f \u0628\u062f\u0627\u0626\u0644", "New revision": "\u0645\u0631\u0627\u062c\u0639\u0629 \u062c\u062f\u064a\u062f\u0629", "The changes to these blocks will not be saved until the \u003Cem\u003ESave blocks\u003C\/em\u003E button is clicked.": "\u0644\u0646 \u062a\u064f\u062d\u0641\u0638 \u0627\u0644\u062a\u063a\u064a\u064a\u0631\u0627\u062a \u0641\u064a \u0647\u0630\u0647 \u0627\u0644\u0635\u0646\u0627\u062f\u064a\u0642 \u0642\u0628\u0644 \u0627\u0644\u0646\u0642\u0631 \u0639\u0644\u0649 \u0632\u0631 \u003Cem\u003E\u0627\u062d\u0641\u0638 \u0627\u0644\u0635\u0646\u0627\u062f\u064a\u0642\u003C\/em\u003E.", "Flag translations as outdated": "\u0639\u0644\u0651\u0645 \u0627\u0644\u062a\u0631\u062c\u0645\u0627\u062a \u0642\u062f\u064a\u0645\u0629", "This permission is inherited from the authenticated user role.": "\u0647\u0630\u0647 \u0627\u0644\u0635\u0644\u0627\u062d\u064a\u0629 \u0645\u0646\u0628\u062b\u0642\u0629 \u0645\u0646 \u062f\u0648\u0631 \u0627\u0644\u0645\u0633\u062a\u062e\u062f\u0645 \u0627\u0644\u0645\u0639\u0631\u0641.", "No revision": "\u0644\u0627 \u062a\u0648\u062c\u062f \u0623\u064a \u0645\u0631\u0627\u062c\u0639\u0629", "@number comments per page": "@number \u062a\u0639\u0644\u064a\u0642 \u0641\u064a \u0627\u0644\u0635\u0641\u062d\u0629", "Requires a title": "\u0627\u0644\u0639\u0646\u0648\u0627\u0646 \u0636\u0631\u0648\u0631\u064a", "Not restricted": "\u063a\u064a\u0631 \u0645\u0642\u064a\u062f", "Not customizable": "\u063a\u064a\u0631 \u0642\u0627\u0628\u0644 \u0644\u0644\u062a\u062e\u0635\u064a\u0635", "Restricted to certain pages": "\u062e\u0627\u0636\u0639 \u0644\u0628\u0639\u0636 \u0627\u0644\u0635\u0641\u062d\u0627\u062a", "The block cannot be placed in this region.": "\u0644\u0627 \u064a\u0645\u0643\u0646 \u0648\u0636\u0639 \u0627\u0644\u0635\u0646\u062f\u0648\u0642 \u0641\u064a \u0647\u0630\u0647 \u0627\u0644\u0645\u0646\u0637\u0642\u0629.", "Hide summary": "\u0625\u062e\u0641\u0627\u0621 \u0627\u0644\u0645\u0648\u062c\u0632", "Edit summary": "\u062a\u0639\u062f\u064a\u0644 \u0627\u0644\u0645\u0644\u062e\u0635", "Don\u0027t display post information": "\u0639\u062f\u0645 \u0639\u0631\u0636 \u0645\u0639\u0644\u0648\u0645\u0627\u062a \u0627\u0644\u0645\u0634\u0627\u0631\u0643\u0629", "The selected file %filename cannot be uploaded. Only files with the following extensions are allowed: %extensions.": "\u0644\u0627 \u064a\u0645\u0643\u0646 \u0631\u0641\u0639 \u0627\u0644\u0645\u0644\u0641 \u0627\u0644\u0645\u062d\u062f\u062f %filename. \u064a\u0633\u0645\u062d \u0641\u0642\u0637 \u0628\u0627\u0644\u0645\u0644\u0641\u0627\u062a \u0630\u0627\u062a \u0627\u0644\u0644\u0648\u0627\u062d\u0642 \u0627\u0644\u062a\u0627\u0644\u064a\u0629: %extensions.", "Autocomplete popup": "\u0646\u0627\u0641\u0630\u0629 \u0627\u0644\u0625\u0643\u0645\u0627\u0644 \u0627\u0644\u062a\u0644\u0642\u0627\u0626\u064a", "Searching for matches...": "\u062c\u0627\u0631\u064a \u0627\u0644\u0628\u062d\u062b \u0639\u0646 \u0627\u0644\u0643\u0644\u0645\u0627\u062a \u0627\u0644\u0645\u0637\u0627\u0628\u0642\u0629...", "Previous": "\u0627\u0644\u0633\u0627\u0628\u0642", "Close": "\u0625\u063a\u0644\u0627\u0642", "Select": "\u0627\u062e\u062a\u0631" } } };;
(function ($) {

    Drupal.googleanalytics = {};

    $(document).ready(function () {

        // Attach mousedown, keyup, touchstart events to document only and catch
        // clicks on all elements.
        $(document.body).bind("mousedown keyup touchstart", function (event) {

            // Catch the closest surrounding link of a clicked element.
            $(event.target).closest("a,area").each(function () {

                // Is the clicked URL internal?
                if (Drupal.googleanalytics.isInternal(this.href)) {
                    // Skip 'click' tracking, if custom tracking events are bound.
                    if ($(this).is('.colorbox') && (Drupal.settings.googleanalytics.trackColorbox)) {
                        // Do nothing here. The custom event will handle all tracking.
                        //console.info("Click on .colorbox item has been detected.");
                    }
                        // Is download tracking activated and the file extension configured for download tracking?
                    else if (Drupal.settings.googleanalytics.trackDownload && Drupal.googleanalytics.isDownload(this.href)) {
                        // Download link clicked.
                        ga("send", {
                            "hitType": "event",
                            "eventCategory": "Downloads",
                            "eventAction": Drupal.googleanalytics.getDownloadExtension(this.href).toUpperCase(),
                            "eventLabel": Drupal.googleanalytics.getPageUrl(this.href),
                            "transport": "beacon"
                        });
                    }
                    else if (Drupal.googleanalytics.isInternalSpecial(this.href)) {
                        // Keep the internal URL for Google Analytics website overlay intact.
                        ga("send", {
                            "hitType": "pageview",
                            "page": Drupal.googleanalytics.getPageUrl(this.href),
                            "transport": "beacon"
                        });
                    }
                }
                else {
                    if (Drupal.settings.googleanalytics.trackMailto && $(this).is("a[href^='mailto:'],area[href^='mailto:']")) {
                        // Mailto link clicked.
                        ga("send", {
                            "hitType": "event",
                            "eventCategory": "Mails",
                            "eventAction": "Click",
                            "eventLabel": this.href.substring(7),
                            "transport": "beacon"
                        });
                    }
                    else if (Drupal.settings.googleanalytics.trackOutbound && this.href.match(/^\w+:\/\//i)) {
                        if (Drupal.settings.googleanalytics.trackDomainMode !== 2 || (Drupal.settings.googleanalytics.trackDomainMode === 2 && !Drupal.googleanalytics.isCrossDomain(this.hostname, Drupal.settings.googleanalytics.trackCrossDomains))) {
                            // External link clicked / No top-level cross domain clicked.
                            ga("send", {
                                "hitType": "event",
                                "eventCategory": "Outbound links",
                                "eventAction": "Click",
                                "eventLabel": this.href,
                                "transport": "beacon"
                            });
                        }
                    }
                }
            });
        });

        // Track hash changes as unique pageviews, if this option has been enabled.
        if (Drupal.settings.googleanalytics.trackUrlFragments) {
            window.onhashchange = function () {
                ga("send", {
                    "hitType": "pageview",
                    "page": location.pathname + location.search + location.hash
                });
            };
        }

        // Colorbox: This event triggers when the transition has completed and the
        // newly loaded content has been revealed.
        if (Drupal.settings.googleanalytics.trackColorbox) {
            $(document).bind("cbox_complete", function () {
                var href = $.colorbox.element().attr("href");
                if (href) {
                    ga("send", {
                        "hitType": "pageview",
                        "page": Drupal.googleanalytics.getPageUrl(href)
                    });
                }
            });
        }

    });

    /**
     * Check whether the hostname is part of the cross domains or not.
     *
     * @param string hostname
     *   The hostname of the clicked URL.
     * @param array crossDomains
     *   All cross domain hostnames as JS array.
     *
     * @return boolean
     */
    Drupal.googleanalytics.isCrossDomain = function (hostname, crossDomains) {
        /**
         * jQuery < 1.6.3 bug: $.inArray crushes IE6 and Chrome if second argument is
         * `null` or `undefined`, http://bugs.jquery.com/ticket/10076,
         * https://github.com/jquery/jquery/commit/a839af034db2bd934e4d4fa6758a3fed8de74174
         *
         * @todo: Remove/Refactor in D8
         */
        if (!crossDomains) {
            return false;
        }
        else {
            return $.inArray(hostname, crossDomains) > -1 ? true : false;
        }
    };

    /**
     * Check whether this is a download URL or not.
     *
     * @param string url
     *   The web url to check.
     *
     * @return boolean
     */
    Drupal.googleanalytics.isDownload = function (url) {
        var isDownload = new RegExp("\\.(" + Drupal.settings.googleanalytics.trackDownloadExtensions + ")([\?#].*)?$", "i");
        return isDownload.test(url);
    };

    /**
     * Check whether this is an absolute internal URL or not.
     *
     * @param string url
     *   The web url to check.
     *
     * @return boolean
     */
    Drupal.googleanalytics.isInternal = function (url) {
        var isInternal = new RegExp("^(https?):\/\/" + window.location.host, "i");
        return isInternal.test(url);
    };

    /**
     * Check whether this is a special URL or not.
     *
     * URL types:
     *  - gotwo.module /go/* links.
     *
     * @param string url
     *   The web url to check.
     *
     * @return boolean
     */
    Drupal.googleanalytics.isInternalSpecial = function (url) {
        var isInternalSpecial = new RegExp("(\/go\/.*)$", "i");
        return isInternalSpecial.test(url);
    };

    /**
     * Extract the relative internal URL from an absolute internal URL.
     *
     * Examples:
     * - http://mydomain.com/node/1 -> /node/1
     * - http://example.com/foo/bar -> http://example.com/foo/bar
     *
     * @param string url
     *   The web url to check.
     *
     * @return string
     *   Internal website URL
     */
    Drupal.googleanalytics.getPageUrl = function (url) {
        var extractInternalUrl = new RegExp("^(https?):\/\/" + window.location.host, "i");
        return url.replace(extractInternalUrl, '');
    };

    /**
     * Extract the download file extension from the URL.
     *
     * @param string url
     *   The web url to check.
     *
     * @return string
     *   The file extension of the passed url. e.g. "zip", "txt"
     */
    Drupal.googleanalytics.getDownloadExtension = function (url) {
        var extractDownloadextension = new RegExp("\\.(" + Drupal.settings.googleanalytics.trackDownloadExtensions + ")([\?#].*)?$", "i");
        var extension = extractDownloadextension.exec(url);
        return (extension === null) ? '' : extension[1];
    };

})(jQuery);
;