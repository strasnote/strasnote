const headerSearch = document.getElementById("search");
headerSearch.addEventListener("shown.bs.collapse", event => {
    document.querySelector("#search input").focus();
})