function pushState(currentPage) {
    var stateObj = { url: "?CurrentPage=" + currentPage + "" };
    history.pushState(stateObj, "", "?CurrentPage=" + currentPage + "");
}