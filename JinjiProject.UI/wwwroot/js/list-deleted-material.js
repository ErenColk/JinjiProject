const materialsPerPage = 8; // Her sayfada gösterilecek ürün sayısı
const paginationList = document.getElementById('pagination-list');
const totalPages = Math.ceil(materialList.length / materialsPerPage); // Toplam sayfa sayısı
const materialTableBody = document.getElementById('material-table-body');
var currentPage = 1;

updatePagination(totalPages, materialList)
updateActiveClass(currentPage);
showPage(currentPage, materialList)

function updatePagination(totalPages, materials) {
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
            showPage(i, materials);
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

function showPage(pageNumber, materials) {
    // Başlangıç ve bitiş indeksleri hesapla
    const startIndex = (pageNumber - 1) * materialsPerPage;
    const endIndex = Math.min(startIndex + materialsPerPage, materialList.length);

    // Ürünleri temizle
    materialTableBody.innerHTML = '';

    // productList'ten sadece o sayfadaki ürünleri al
    const materialsOnPage = materials.slice(startIndex, endIndex);

    fillmaterials(materialsOnPage);

}

function fillmaterials(materials) {
    materials.forEach((material, index) => {
        const materialTR = document.createElement('tr');
        materialTR.innerHTML = `
               <td>${materialsPerPage * (currentPage - 1) + index + 1}</td>
                                            <td>${material.name}</td>
                                            <td>${material.statusName}</td>
                                            <td>${new Date(material.deletedDate).toLocaleDateString()}</td>
                                            <td>
                                                <button class="btn btn-outline-secondary dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown">
                                                    Seçenekler
                                                </button>
                                                <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu">
                                                    <a class="dropdown-item text-success " href="/Admin/Material/AddAgainMaterial/${material.id}">Tekrar Ekle</a>
                                                    <div class="dropdown-divider"></div>
                                                    <a class="dropdown-item text-danger" onclick="loadMaterialData(${material.id})" id="modalOpener" data-bs-toggle="modal" data-bs-target="#exampleModal" data-id="${material.id}">Veriyi Sil</a>
                                                </ul>
                                            </td>`;
        materialTableBody.appendChild(materialTR);
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
        showPage(currentPage, materialList);
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
        showPage(currentPage, materialList);
    }

})