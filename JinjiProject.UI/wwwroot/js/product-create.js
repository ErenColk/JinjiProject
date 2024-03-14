


async function onCategoryChange() {
    let selectedCategoryId = $("#product-category").val();
    genres = [];
    debugger;
    
    console.log(selectedCategoryId);
    debugger;

    genres = genres ? await getGenre(selectedCategoryId) : genres;

    console.log(genres);
    populateSelectList("product-genre", genres);
};

async function getGenre(selectedCategoryId) {
    return $.ajax({
        url: '/Admin/Product/AddGenreList',
        data: {id : selectedCategoryId }
    });
}



async function populateSelectList(selectListId, data) {
    debugger;
    console.log(data)
    let selectListOptions = data.map((item, index) => `<option value="${item.value}">${item.text}</option>`);
    let selectList = `<option value="" disabled="" selected="">--- Seçiniz ---</option>`.concat(selectListOptions);
    document.getElementById(selectListId).innerHTML = selectList;
}