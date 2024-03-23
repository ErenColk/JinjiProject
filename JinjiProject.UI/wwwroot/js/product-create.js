


async function onCategoryChange() {
    let selectedCategoryId = $("#product-category").val();
    genres = [];

    genres = genres ? await getGenre(selectedCategoryId) : genres;
    populateSelectList("product-genre", genres);
};

async function getGenre(selectedCategoryId) {
    return $.ajax({
        url: '/Admin/Product/AddGenreList',
        data: {id : selectedCategoryId }
    });
}

function formatPrice(inputElement, id) {
    // Giriş alanındaki değeri al
    let input = inputElement.value;

    // Para birimi formatına uygun bir regex deseni oluştur
    let regex = /^\d{0,}(\,\d{0,2})?$/;

    // Giriş değeri regex ile eşleşiyorsa, doğru formatta girilmiştir
    if (regex.test(input)) {
        document.getElementById(id).textContent = "";

    } else {
        // Eğer eşleşmiyorsa, hatalı formatta giriş yapılmıştır
        document.getElementById(id).textContent = "Doğru formatta giriş yapınız!";
        // Hatalı girişi temizleme
        if (input.length > 0) {
            // Eğer input alanı doluysa ve hatalı formatta giriş yapıldıysa temizleme
            inputElement.value = "";
            document.getElementById('product-price').textContent = "";
        }
    }
}


async function populateSelectList(selectListId, data) {

    let selectListOptions = data.map((item, index) => `<option value="${item.value}">${item.text}</option>`);
    let selectList = `<option value="" disabled="" selected="">--- Seçiniz ---</option>`.concat(selectListOptions);
    document.getElementById(selectListId).innerHTML = selectList;
}