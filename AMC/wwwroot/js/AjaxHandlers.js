
function AjaxGet(url, data, cb) {
    return $.get(url, data, function (response) {
        // TODO: Implement response error checking/display
        if (response.error != null) {
            // Do something
            return;
        }

        // Call the callback function with the response data
        cb(response.data);
    });
}

function AjaxPost(url, data, cb) {
    return $.post(url, data, function (response) {
        // TODO: Implement response error checking/display
        if (response.error != null) {
            // Do something
            return;
        }

        // Call the callback function with the response data
        cb(response.data);
    });
}
