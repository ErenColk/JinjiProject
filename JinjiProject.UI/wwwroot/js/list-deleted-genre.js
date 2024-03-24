const genresPerPage = 5; // Her sayfada gösterilecek ürün sayısı
const paginationList = document.getElementById('pagination-list');
const totalPages = Math.ceil(genreList.length / genresPerPage); // Toplam sayfa sayısı
const genreTableBody = document.getElementById('genre-table-body');
var currentPage = 1;

updatePagination(totalPages, genreList)
updateActiveClass(currentPage);
showPage(currentPage, genreList)

function updatePagination(totalPages, genres) {
    // paginationList içindeki ilk öğeyi bul
    paginationList.innerHTML = "";
    paginationList.appendChild(createListItem(`<li class="page-item">
                                    <a class="page-link" href="#" id="PreviousButton" aria-label="Previous">
                                        <span aria-hidden="true">&laquo;</span>
                                        <span class="sr-only">Previous</span>
                                    </a>
                                </li>`));
    // Sayfa numaralarını oluştur
    for (let i = 1; i <= totalPages; i++) {
        const listItem = document.createElement('li');
        const pageLink = document.createElement('a');
        pageLink.href = '#'; // Sayfa numaralarının tıklanabilir olduğunu belirtmek için
        listItem.classList.add('page-item');
        pageLink.classList.add('page-link');
        pageLink.textContent = i;
        listItem.appendChild(pageLink);

        paginationList.appendChild(listItem);

        // Her sayfa numarasına tıklanınca ilgili sayfayı göster
        pageLink.addEventListener('click', function () {
            currentPage = i;
            showPage(i, genres);
            updateActiveClass(i);
        });
    }
    paginationList.appendChild(createListItem(`<li class="page-item">
                                    <a class="page-link" href="#" id="NextButton" aria-label="Next">
                                        <span aria-hidden="true">&raquo;</span>
                                        <span class="sr-only">Next</span>
                                    </a>
                                </li>`));
}
function createListItem(htmlString) {
    const tempElement = document.createElement('div');
    tempElement.innerHTML = htmlString.trim();
    return tempElement.firstChild;
}

function updateActiveClass(currentPage) {
    // Tüm li elementlerinden active sınıfını kaldır
    const pageLinks = paginationList.getElementsByTagName('li');

    for (let i = 0; i < pageLinks.length; i++) {
        pageLinks[i].classList.remove('active');
    }

    // Aktif sayfa numarasının li elementine active sınıfını ekle
    pageLinks[currentPage].classList.add('active');
}

function showPage(pageNumber, genres) {
    // Başlangıç ve bitiş indeksleri hesapla
    const startIndex = (pageNumber - 1) * genresPerPage;
    const endIndex = Math.min(startIndex + genresPerPage, genreList.length);

    // Ürünleri temizle
    genreTableBody.innerHTML = '';

    // productList'ten sadece o sayfadaki ürünleri al
    const genresOnPage = genres.slice(startIndex, endIndex);

    fillgenres(genresOnPage);

}

function fillgenres(genres) {
    genres.forEach((genre, index) => {
        const genreTR = document.createElement('tr');
        genreTR.innerHTML = `
                <td>${genresPerPage * (currentPage - 1) + index + 1}</td>
                                                <td><img class="img-fluid" style="width: 100px; height:100px; border-radius:50%;" src="${genre.imagePath}" /></td>
                                                <td>${genre.name}</td>
                                                <td>${new Date(genre.deletedDate).toLocaleDateString()}</td>
                                                <td>
                                                    <button class="btn btn-outline-secondary dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown">
                                                        Seçenekler
                                                    </button>
                                                    <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu">
                                                        <li>
                                                            <a class="dropdown-item " href="/Admin/Genre/AddAgainGenre/${genre.id}">Tekrar Ekle</a>
                                                        </li>
                                                        <li>
                                                            <a class="dropdown-item text-danger" onclick="loadGenreHardDeleteData(${genre.id})" id="modalOpener" data-bs-toggle="modal" data-bs-target="#hardDeleteModal" data-id="${genre.id}">Veriyi Sil</a>
                                                        </li>
                                                    </ul>
                                                </td>`;
        genreTableBody.appendChild(genreTR);
    });
}

const nextButton = document.getElementById('NextButton');
const previousButton = document.getElementById('PreviousButton')

nextButton.addEventListener('click', function (event) {
    event.preventDefault();
    if (currentPage === totalPages) {
        nextButton.disabled = true;
    }
    else {
        nextButton.disabled = false;
        currentPage = currentPage + 1;
        updateActiveClass(currentPage);
        showPage(currentPage, genreList);
    }
})

previousButton.addEventListener('click', function (event) {
    event.preventDefault();
    if (currentPage === 1) {
        previousButton.disabled = true;
    }
    else {
        previousButton.disabled = false;
        currentPage = currentPage - 1;
        updateActiveClass(currentPage);
        showPage(currentPage, genreList);
    }

})