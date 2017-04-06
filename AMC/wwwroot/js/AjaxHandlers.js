
function AjaxGet(url, data, cb) {
    return $.get(url, data, function (response) {
        // TODO: Implement response error checking/display
        if (response.Error != null) {
            // Do something
            return;
        }

        // Call the callback function with the response data
        cb(response.Data);
    });
}

function AjaxPost(url, data, cb) {
    return $.post(url, data, function (response) {
        // TODO: Implement response error checking/display
        if (response.Error != null) {
            // Do something
            return;
        }

        // Call the callback function with the response data
        cb(response.Data);
    });
}
