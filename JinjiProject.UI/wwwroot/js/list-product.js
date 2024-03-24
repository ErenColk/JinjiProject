const productsPerPage = 5; // Her sayfada gösterilecek ürün sayısı
const paginationList = document.getElementById('pagination-list');
const totalPages = Math.ceil(productList.length / productsPerPage); // Toplam sayfa sayısı
const productTableBody = document.getElementById('product-table-body');
var currentPage = 1;
console.log(productList)
updatePagination(totalPages, productList)
updateActiveClass(currentPage);
showPage(currentPage, productList)

function updatePagination(totalPages, products) {
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
            showPage(i, products);
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

function showPage(pageNumber, products) {
    // Başlangıç ve bitiş indeksleri hesapla
    const startIndex = (pageNumber - 1) * productsPerPage;
    const endIndex = Math.min(startIndex + productsPerPage, productList.length);

    // Ürünleri temizle
    productTableBody.innerHTML = '';

    // productList'ten sadece o sayfadaki ürünleri al
    const productsOnPage = products.slice(startIndex, endIndex);

    fillproducts(productsOnPage);

}

function fillproducts(products) {
    products.forEach((product, index) => {
        const productTR = document.createElement('tr');
        productTR.innerHTML = `
                 <td>${productsPerPage * (currentPage - 1) + index + 1}</td>
                                                <td><img class="img-fluid img-thumbnail" style="width: 100px; height:100px; border-radius:5px; " src="${product.imagePath}" /></td>
                                                <td>${product.name}</td>
                                                <td>${product.price}₺</td>
                                                <td>${product.oldPrice ? product.oldPrice +"₺" : "-"}</td>
                                               <td>${product.oldPrice ? '<span class="badge badge-success">İndirim Var</span>' : '<span class="badge badge-danger">İndirim Yok</span>'}</td>
                                                <td>${product.stock} Adet</td>
                                                <td>${new Date(product.createdDate).toLocaleDateString()}</td>
                                                <td>
                                                    <button class="btn btn-outline-secondary dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown">
                                                        Seçenekler
                                                    </button>
                                                    <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu">
                                                        <a class="dropdown-item " href="/Admin/Product/UpdateProduct/${product.id}">Güncelle</a>
                                                         <a class="dropdown-item " id="modalOpener" onclick="addPriceToProduct(${product.id})" data-bs-toggle="modal" data-bs-target="#productAddDiscountModal" data-id="${product.id}">İndirim Ekle</a>
                                                        <a class="dropdown-item " id="modalOpener" onclick="loadProductData(${product.id})" data-bs-toggle="modal" data-bs-target="#productDetailModal" data-id="${product.id}">Detaylar</a>
                                                        <div class="dropdown-divider"></div>
                                                        <a class="dropdown-item text-danger" href="/Admin/Product/SoftDelete/${product.id}">Sil</a>
                                                    </ul>
                                                </td>`;
        productTableBody.appendChild(productTR);
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
        showPage(currentPage, productList);
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
        showPage(currentPage, productList);
    }

})