
async function onCategoryChangeUpdate() {
    let selectedCategoryId = $("#product-category-update").val();
    genres = [];
    genres = genres ? await getGenre(selectedCategoryId) : genres;
    populateSelectList("product-genre", genres);
};

async function getGenre(selectedCategoryId) {
    return $.ajax({
        url: '/Admin/Product/AddGenreList',
        data: { id: selectedCategoryId }
    });
}



async function populateSelectList(selectListId, data) {

    let selectListOptions = data.map((item, index) => `<option value="${item.value}">${item.text}</option>`);
    let selectList = `<option value="" disabled="" selected="">--- Seçiniz ---</option>`.concat(selectListOptions);
    document.getElementById(selectListId).innerHTML = selectList;
}



window.onload = function () {
    var dimensionsInput = document.getElementById('input-dimensions-toggle');
    if (dimensionsInput.checked) {
        showDimensions('show-dimensions');
    }

    onCategoryChangeUpdate();

};

function previewImage(event) {
    var reader = new FileReader();
    reader.onload = function () {
        var output = document.getElementById('image-preview');
        output.src = reader.result;
        output.style.display = 'block';
        document.getElementById('image-preview-label').style.display = 'block';

    }
    reader.readAsDataURL(event.target.files[0]);
};

function showCapacity(id) {
    var capacity = document.getElementById(id);
    var lt = document.getElementById('product-lt');

    var inputElement = document.getElementById('input-capacity-toggle');
    var dataResultValue = inputElement.getAttribute('data-result');

    if (dataResultValue == "false") {
        capacity.style.display = 'block';
        lt.style.display = 'block';
        inputElement.setAttribute('data-result', 'true');
    } else {
        inputElement.setAttribute('data-result', 'false');
        capacity.style.display = 'none';
        lt.style.display = 'none';
    }
}

function showSize(id) {
    var size = document.getElementById(id);

    var inputElement = document.getElementById('input-size-toggle');
    var dataResultValue = inputElement.getAttribute('data-result');

    if (dataResultValue == "false") {
        size.style.display = 'block';
        inputElement.setAttribute('data-result', 'true');
    } else {
        inputElement.setAttribute('data-result', 'false');
        size.style.display = 'none';
    }
}


function showDimensions(className) {
    var elements = document.getElementsByClassName(className);
    for (var i = 0; i < elements.length; i++) {
        if (elements[i].style.display === "none") {
            elements[i].style.display = "block";
        } else {
            elements[i].style.display = "none";
        }
    }
}
