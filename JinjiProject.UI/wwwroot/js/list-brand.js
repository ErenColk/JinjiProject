const brandsPerPage = 8; // Her sayfada gösterilecek ürün sayısı
const paginationList = document.getElementById('pagination-list');
const totalPages = Math.ceil(brandList.length / brandsPerPage); // Toplam sayfa sayısı
const brandTableBody = document.getElementById('brand-table-body');
var currentPage = 1;

updatePagination(totalPages, brandList)
updateActiveClass(currentPage);
showPage(currentPage, brandList)

function updatePagination(totalPages, brands) {
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
            showPage(i, brands);
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

function showPage(pageNumber, brands) {
    // Başlangıç ve bitiş indeksleri hesapla
    const startIndex = (pageNumber - 1) * brandsPerPage;
    const endIndex = Math.min(startIndex + brandsPerPage, brandList.length);

    // Ürünleri temizle
    brandTableBody.innerHTML = '';

    // productList'ten sadece o sayfadaki ürünleri al
    const brandsOnPage = brands.slice(startIndex, endIndex);

    // Ürünleri listele
    fillBrands(brandsOnPage);

}

function fillBrands(brands) {
    brands.forEach((brand, index) => {
        const brandTR = document.createElement('tr');
        brandTR.innerHTML = `
                <td>${brandsPerPage*(currentPage-1)+index+1}</td>
                                                <td>${brand.name}</td>
                                                <td>${brand.statusName}</td>
                                                <td>
                                                    <button class="btn btn-outline-secondary dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown">
                                                        Seçenekler
                                                    </button>
                                                    <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu">
                                                        <a class="dropdown-item text-primary" href="/Admin/Brand/UpdateBrand/${brand.id}">Güncelle</a>
                                                        <a class="dropdown-item text-primary" onclick="loadBrandData(${brand.id})" id="modalOpener" data-bs-toggle="modal" data-bs-target="#exampleModal" data-id="${brand.id}">Detaylar</a>
                                                        <div class="dropdown-divider"></div>
                                                        <a class="dropdown-item text-danger" href="/Admin/Brand/SoftDelete/${brand.id}">Sil</a>
                                                    </ul>
                                                </td>`;
        brandTableBody.appendChild(brandTR);
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
        showPage(currentPage, brandList);
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
        showPage(currentPage, brandList);
    }

})