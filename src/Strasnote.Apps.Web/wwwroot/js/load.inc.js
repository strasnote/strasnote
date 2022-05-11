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
