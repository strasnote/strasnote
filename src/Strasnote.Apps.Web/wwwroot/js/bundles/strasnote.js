/**
 * Get the current window hash without the actual #.
 *
 */
function getHash() {
    return window.location.hash.replace("#", "");
}

/**
 * Capture anchor clicks and load relative page links using AJAX.
 *
 */
function setupAnchors() {
    $("body").on("click", "a", function (e) {
        // get hyperlink
        var href = $(this).attr("href");

        // ignore absolute links
        if (href.startsWith("https")) {
            return;
        }

        // ignore javascript links
        if (href == "javascript:void(0)") {
            return;
        }

        // don't follow the link
        e.preventDefault();

        // if there is a target, load directly into it
        var target = $(this).data("target");
        if (target) {
            load(href, $(target));
        }

        // otherwise, update hash (load 'href' because of window.onhashchange)
        else {
            updateHash(href);
        }
    });
}
ready(setupAnchors);

/**
 * Handle user clicking back / forward buttons as well as dynamically changing the 
 * hash - for example in loadPage().
 * 
 */
function setupHashChange() {
    window.onhashchange = loadHash;
}
ready(setupHashChange);

/**
 * Open whatever is in the URL hash into the main page content block.
 *
 */
function loadHash() {
    // get hash - if it's not there or it's /, use home URL (defined in Index page)
    var url = getHash();
    if (!url || url.length == 1) {
        url = homeUrl;
    }

    // Load URL contents into main content block
    load(url, $("main"));
}

/**
 * Load a page into the main content block - changing the hash triggers loadHash()
 * thanks to setupHistory().
 * 
 * @param {any} url
 */
function updateHash(url) {
    if (getHash() == url) {
        loadHash(); // refresh page
    } else {
        window.location.hash = url; // trigger window.onhashchange event to load new page
    }
}

/**
 * Make a URL GET request and insert HTML response into DOM (if parent is set), or handle
 * JSON response.
 * 
 * @param {string} url URL to load.
 * @param {JQuery<any>} parent
 */
function load(url, parent) {
    $
        // make the request
        .ajax({
            url: url,
            method: "GET"
        })

        // handle success
        .done(function (data, status, xhr) {
            // if we are expecting an HTML response, insert it into the parent container
            if (parent) {
                parent.html(data);
            }

            // otherwise, log to console
            else {
                console.log(data, status, xhr);
            }

            // scroll to top of page
            window.scrollTo(0, 0);
        })

        // handle failure
        .fail(function (xhr) {
            console.log(xhr);
        });
}

/**
 * Submit all forms via AJAX and handle results.
 *
 */
function setupAjaxSubmit() {
    $("body").on("submit", "form", function (e) {
        // stop default submit behaviour
        e.preventDefault();

        // check validity
        var form = $(this);
        if (this.checkValidity() === false) {
            return;
        }

        // support GET forms using URL hash
        if (form.attr("method").toLowerCase() == "get") {
            var data = form.serialize();
            var url = form.attr("action") + "?" + data;
            return updateHash(url);
        }

        // submit form
        submitForm(form);
    });
}
ready(setupAjaxSubmit);

/**
 * Close a modal and submit a form, optionally overriding URL and data
 * 
 * @param {JQuery<any>} form Form to submit
 * @param {string} url Override form action and submit to this URL instead
 * @param {any} data Override form data and submit this data instead
 */
function submitForm(form, url, data) {
    // get form info
    var method = form.attr("method") ?? "POST";
    var target = form.data("target");
    var replace = form.data("replace") ?? false;

    // post data and handle result
    $.ajax(
        {
            method: method,
            url: url || form.attr("action"),
            data: data || form.serialize()
        }
    ).done(
        function (data, status, xhr) {
            // handle JSON response
            if (xhr && xhr.responseJSON) {
                handleJsonResponse(xhr.responseJSON);
            }

            // handle HTML response
            else if (data && target) {
                // get the DOM element to be replaced
                var targetEl = $(target);

                // replace contents or the element itself
                if (replace) {
                    targetEl.replaceWith(data);
                } else {
                    targetEl.html(data);
                }
            }

            // otherwise, something unexpected has happened so log and refresh
            else {
                console.log(data, status, xhr);
                loadHash();
            }
        }
    ).fail(
        function (xhr) {
            // handle JSON response
            if (xhr && xhr.responseJSON) {
                handleJsonResponse(xhr.responseJSON);
            }

            // otherwise, something unexpected has happened so log and refresh
            else {
                console.log(xhr);
                loadHash();
            }
        }
    );
}

/**
 * Handle a JSON response object.
 * 
 * @param {any} json JSON response
 */
function handleJsonResponse(json) {
    if (json.redirect) {
        if (json.redirect == "refresh") {
            loadHash();
        } else {
            updateHash(json.redirect);
        }
    }
}
