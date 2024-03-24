const categorysPerPage = 8; // Her sayfada gösterilecek ürün sayısı
const paginationList = document.getElementById('pagination-list');
const totalPages = Math.ceil(categoryList.length / categorysPerPage); // Toplam sayfa sayısı
const categoryTableBody = document.getElementById('category-table-body');
var currentPage = 1;

updatePagination(totalPages, categoryList)
updateActiveClass(currentPage);
showPage(currentPage, categoryList)

function updatePagination(totalPages, categorys) {
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
            showPage(i, categorys);
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

function showPage(pageNumber, categorys) {
    // Başlangıç ve bitiş indeksleri hesapla
    const startIndex = (pageNumber - 1) * categorysPerPage;
    const endIndex = Math.min(startIndex + categorysPerPage, categoryList.length);

    // Ürünleri temizle
    categoryTableBody.innerHTML = '';

    // productList'ten sadece o sayfadaki ürünleri al
    const categorysOnPage = categorys.slice(startIndex, endIndex);

    fillcategorys(categorysOnPage);

}

function fillcategorys(categorys) {
    categorys.forEach((category, index) => {
        const categoryTR = document.createElement('tr');
        categoryTR.innerHTML = `
               <td>${categorysPerPage * (currentPage - 1) + index + 1}</td>
                                                <td>${category.name}</td>
                                                <td>${category.statusName}</td>
                                                <td>

                                                    <button class="btn btn-outline-secondary dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown">
                                                        Seçenekler
                                                    </button>
                                                    <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu">
                                                        <a class="dropdown-item text-primary" href="/Admin/Category/UpdateCategory/${category.id}">Güncelle</a>
                                                        <a class="dropdown-item text-primary" onclick="loadCategoryData(${category.id})" id="modalOpener" data-bs-toggle="modal" data-bs-target="#exampleModal" data-id="${category.id}">Detaylar</a>
                                                        <div class="dropdown-divider"></div>
                                                        <a class="dropdown-item text-danger" href="/Admin/Category/SoftDelete/${category.id}">Sil</a>
                                                    </ul>

                                                </td>`;
        categoryTableBody.appendChild(categoryTR);
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
        showPage(currentPage, categoryList);
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
        showPage(currentPage, categoryList);
    }

})