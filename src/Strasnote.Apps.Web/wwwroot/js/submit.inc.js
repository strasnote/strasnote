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
