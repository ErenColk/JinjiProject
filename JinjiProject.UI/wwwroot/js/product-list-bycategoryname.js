console.log("productlist çekildi", productList);

const productsContainer = document.getElementById('product-list');
const paginationList = document.getElementById('pagination-list');
const productsPerPage = 8; // Her sayfada gösterilecek ürün sayısı
const totalPages = Math.ceil(productList.length / productsPerPage); // Toplam sayfa sayısı

// Sayfa numaralarını günceller. Parametre olarak toplam sayfa sayısı ve ürün listesini alır.
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

// Sayfada toplam 9 ürün olmasını sağlar.
function showPage(pageNumber, products) {
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

// Pagination numaralarının aktif olanını göstermeye yarar.
function updateActiveClass(currentPage) {
    // Tüm li elementlerinden active sınıfını kaldır
    const pageLinks = paginationList.getElementsByTagName('a');
    for (let i = 0; i < pageLinks.length; i++) {
        pageLinks[i].parentNode.classList.remove('active');
    }

    // Aktif sayfa numarasının li elementine active sınıfını ekle
    pageLinks[currentPage - 1].parentNode.classList.add('active');
}

// Ürünleri doldurmaya yarar.
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
                            <li>${product.price} Tl</i></li>
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
searchInput.addEventListener('input', function () {
    var clearButton = this.nextElementSibling;
    if (this.value.length > 0) {
        clearButton.style.display = 'inline-block';
    } else {
        clearButton.style.display = 'none';
    }
});

// X simgesine tıklandığında içeriği temizle
var clearButton = document.getElementById('clear-search');
clearButton.addEventListener('click', function () {
    searchInput.value = '';
    this.style.display = 'none';
    updateFilter();
});

// Fiyata göre filtreleme
const minPriceInput = document.getElementById('min-price');
const maxPriceInput = document.getElementById('max-price');
const searchPriceButton = document.getElementById('search-price');
const range1Radio = document.getElementById('range-1');
const range2Radio = document.getElementById('range-2');
const clearPriceButton = document.getElementById('clear-price-filter');

// Fiyat aralığına göre filtreleme işlevi
function filterProductsByPrice(minPrice, maxPrice) {
    minPrice = minPrice || 0; // Varsayılan değerler
    maxPrice = maxPrice || Number.MAX_SAFE_INTEGER;

    const filteredProducts = productList.filter(product => {
        return product.price >= minPrice && product.price <= maxPrice;
    });

    const totalPages = Math.ceil(filteredProducts.length / productsPerPage);
    updatePagination(totalPages, filteredProducts);
    showPage(1, filteredProducts);
    updateActiveClass(1);

    // Temizle düğmesini görünür hale getir
    clearPriceButton.style.display = 'inline-block';
}

searchPriceButton.addEventListener('click', function () {
    const minPrice = parseFloat(minPriceInput.value);
    const maxPrice = parseFloat(maxPriceInput.value);

    filterProductsByPrice(minPrice, maxPrice);
});

range1Radio.addEventListener('change', function () {
    const rangeValues = range1Radio.value.split('-');
    minPriceInput.value = rangeValues[0];
    maxPriceInput.value = rangeValues[1];

    const minPrice = parseFloat(minPriceInput.value);
    const maxPrice = parseFloat(maxPriceInput.value);

    filterProductsByPrice(minPrice, maxPrice);
});

range2Radio.addEventListener('change', function () {
    const rangeValues = range2Radio.value.split('-');
    minPriceInput.value = rangeValues[0];
    maxPriceInput.value = rangeValues[1];

    const minPrice = parseFloat(minPriceInput.value);
    const maxPrice = parseFloat(maxPriceInput.value);

    filterProductsByPrice(minPrice, maxPrice);
});

// Temizle düğmesi işlevi
clearPriceButton.addEventListener('click', function () {
    minPriceInput.value = '';
    maxPriceInput.value = '';
    updateFilter();

    // Temizle düğmesini gizle
    clearPriceButton.style.display = 'none';

    // Seçili radio düğmesini temizle
    range1Radio.checked = false;
    range2Radio.checked = false;
});
// Boyuta göre filtreleme işlevi
function filterProductsBySize(size) {
    size = size || 'yok'
    var filteredProducts = []
    if (size === 'yok') {
        filteredProducts = productList;
    }
    else {
        console.log(productList)
         filteredProducts = productList.filter(product => {
            return product.size === size;
        });
    }

    

    const totalPages = Math.ceil(filteredProducts.length / productsPerPage);
    updatePagination(totalPages, filteredProducts);
    showPage(1, filteredProducts);
    updateActiveClass(1);
    clearSizeButton.style.display = 'inline-block';
}

// Boyut radio düğmelerine tıklanınca filtreleme işlevini çağırma
const sizeSmallRadio = document.getElementById('size-small');
const sizeMediumRadio = document.getElementById('size-medium');
const sizeLargeRadio = document.getElementById('size-large');
const clearSizeButton = document.getElementById('clear-size-filter');
//sizeSmallRadio.addEventListener('change', function () {
//    if (sizeSmallRadio.checked) {
//        filterProductsBySize('Small');
//    }
//});

//sizeMediumRadio.addEventListener('change', function () {
//    if (sizeMediumRadio.checked) {
//        filterProductsBySize('Medium');
//    }
//});

//sizeLargeRadio.addEventListener('change', function () {
//    if (sizeLargeRadio.checked) {
//        filterProductsBySize('Large');
//    }
//});
clearSizeButton.addEventListener('click', function () {

    // Temizle düğmesini gizle
    clearSizeButton.style.display = 'none';

    // Seçili radio düğmesini temizle
    sizeSmallRadio.checked = false;
    sizeMediumRadio.checked = false;
    sizeLargeRadio.checked = false;
    updateFilter();
});
const clearColorButton = document.getElementById('clear-color-filter');
clearColorButton.addEventListener('click', function () {

    // Temizle düğmesini gizle
    clearColorButton.style.display = 'none';

    // Seçili radio düğmesini temizle
    colorBlackRadio.checked = false;
    colorBlueRadio.checked = false;
    colorRedRadio.checked = false;
    updateFilter();
});
// Arama kutusundaki değer değiştiğinde ve fiyat aralığı güncellendiğinde filtrelemeyi tetikle
const colorRedRadio = document.getElementById('color-red');
const colorBlueRadio = document.getElementById('color-blue');
const colorBlackRadio = document.getElementById('color-black');
function updateFilter() {
    const keyword = searchInput.value.toLowerCase();
    const minPrice = parseFloat(minPriceInput.value) || 0;
    const maxPrice = parseFloat(maxPriceInput.value) || Number.MAX_SAFE_INTEGER;
    const smallSize = sizeSmallRadio.checked;
    const mediumSize = sizeMediumRadio.checked;
    const largeSize = sizeLargeRadio.checked;
    const colorBlue = colorBlueRadio.checked;
    const colorRed = colorRedRadio.checked;
    const colorBlack = colorBlackRadio.checked;

    const filteredProducts = productList.filter(product => {
        const nameMatches = product.name.toLowerCase().includes(keyword);
        const priceInRange = product.price >= minPrice && product.price <= maxPrice;
        if (!smallSize && !mediumSize && !largeSize && !colorBlack && !colorRed && !colorBlue) {
            return nameMatches && priceInRange;
        }
        else {
           

            if (!colorBlue && !colorRed && !colorBlack) {
                const sizeMatches = (smallSize && product.size === 'Small') ||
                    (mediumSize && product.size === 'Medium') ||
                    (largeSize && product.size === 'Large');
                clearSizeButton.style.display = 'inline-block';
                return nameMatches && priceInRange && sizeMatches;
            }
            else if (!smallSize && !mediumSize && !largeSize) {
                const colorMatches = (colorBlue && product.color === 'mavi') ||
                    (colorRed && product.color === 'kırmızı') ||
                    (colorBlack && product.color === 'siyah');
                clearColorButton.style.display = 'inline-block';
                return nameMatches && priceInRange && colorMatches;
            }
            else {
                const sizeMatches = (smallSize && product.size === 'Small') ||
                    (mediumSize && product.size === 'Medium') ||
                    (largeSize && product.size === 'Large');
                clearSizeButton.style.display = 'inline-block';

                const colorMatches = (colorBlue && product.color === 'mavi') ||
                    (colorRed && product.color === 'kırmızı') ||
                    (colorBlack && product.color === 'siyah');
                clearColorButton.style.display = 'inline-block';
                return nameMatches && priceInRange && sizeMatches && colorMatches;
            }
           
        }
    });

    const totalPages = Math.ceil(filteredProducts.length / productsPerPage);
    updatePagination(totalPages, filteredProducts);
    showPage(1, filteredProducts);
    updateActiveClass(1);
}


searchInput.addEventListener('input', updateFilter);
searchPriceButton.addEventListener('click', updateFilter);
range1Radio.addEventListener('change', updateFilter);
range2Radio.addEventListener('change', updateFilter);
sizeSmallRadio.addEventListener('change', updateFilter);
sizeMediumRadio.addEventListener('change', updateFilter);
sizeLargeRadio.addEventListener('change', updateFilter);
colorRedRadio.addEventListener('change', updateFilter);
colorBlueRadio.addEventListener('change', updateFilter);
colorBlackRadio.addEventListener('change', updateFilter);