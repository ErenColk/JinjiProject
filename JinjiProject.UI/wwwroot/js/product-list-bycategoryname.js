console.log("productlist çekildi", productList);

const productsContainer = document.getElementById('product-list');
const paginationList = document.getElementById('pagination-list');
const productsPerPage = 9; // Her sayfada gösterilecek ürün sayısı
const totalPages = Math.ceil(productList.length / productsPerPage); // Toplam sayfa sayısı

function updatePagination(totalPages, products) {
    paginationList.innerHTML = "";
    // Sayfa numaralarını oluştur
    for (let i = 1; i <= totalPages; i++) {
        const listItem = document.createElement('li');
        const pageLink = document.createElement('a');
        pageLink.href = '#'; // Sayfa numaralarının tıklanabilir olduğunu belirtmek için
        pageLink.textContent = i;
        listItem.appendChild(pageLink);
        paginationList.appendChild(listItem);

        // Her sayfa numarasına tıklanınca ilgili sayfayı göster
        pageLink.addEventListener('click', function () {
            showPage(i, products);
            updateActiveClass(i);
        });
    }
}


// İlk sayfayı göster
updatePagination(totalPages, productList);
showPage(1, productList);
updateActiveClass(1);
function showPage(pageNumber,products) {
    // Başlangıç ve bitiş indeksleri hesapla
    const startIndex = (pageNumber - 1) * productsPerPage;
    const endIndex = Math.min(startIndex + productsPerPage, productList.length);

    // Ürünleri temizle
    productsContainer.innerHTML = '';

    // productList'ten sadece o sayfadaki ürünleri al
    const productsOnPage = products.slice(startIndex, endIndex);

    // Ürünleri listele
    console.log('productOnPage', productsOnPage)
    fillProducts(productsOnPage);
}

function updateActiveClass(currentPage) {
    // Tüm li elementlerinden active sınıfını kaldır
    const pageLinks = paginationList.getElementsByTagName('a');
    for (let i = 0; i < pageLinks.length; i++) {
        pageLinks[i].parentNode.classList.remove('active');
    }

    // Aktif sayfa numarasının li elementine active sınıfını ekle
    pageLinks[currentPage - 1].parentNode.classList.add('active');
}

function fillProducts(products) {
    products.forEach((product, index) => {
        const productDiv = document.createElement('div');
        productDiv.classList.add('col-lg-3');
        productDiv.innerHTML = `
                <div class="item">
                    <div class="thumb">
                        <div class="hover-content">
                            <ul>
                                <li><a href="single-product.html"><i class="fa fa-eye"></i></a></li>
                                <li><a href="single-product.html"><i class="fa fa-star"></i></a></li>
                                <li><a href="single-product.html"><i class="fa fa-shopping-cart"></i></a></li>
                            </ul>
                        </div>
                        <img src="${product.imagePath}" style="height:320px" alt="">
                    </div>
                    <div class="down-content">
                        <h4>${product.name}</h4>
                        <span>${product.description}</span>
                        <ul class="stars">
                            <li><i class="fa fa-star"></i></li>
                            <li><i class="fa fa-star"></i></li>
                            <li><i class="fa fa-star"></i></li>
                            <li><i class="fa fa-star"></i></li>
                            <li><i class="fa fa-star"></i></li>
                        </ul>
                    </div>
                </div>`;
        productsContainer.appendChild(productDiv);
    });
}

//------------------------------ Filtreleme kısmı-----------------------------
function filterProductsByKeyword(keyword) {
    const filteredProducts = productList.filter(product => {
        return product.name.toLowerCase().includes(keyword.toLowerCase());
    });

    // Toplam sayfa sayısını güncelle
    const totalPages = Math.ceil(filteredProducts.length / productsPerPage);

    // Sayfa numaralarını güncelle
    updatePagination(totalPages, filteredProducts);
    console.log(filteredProducts)
    // İlk sayfayı göster
    showPage(1, filteredProducts);
    updateActiveClass(1);
}

// Arama kutusundaki değer değiştiğinde filtrelemeyi tetikle
const searchInput = document.getElementById('search-box');
searchInput.addEventListener('input', function (event) {
    const keyword = event.target.value;
    console.log("calisti",keyword)
    filterProductsByKeyword(keyword);
});