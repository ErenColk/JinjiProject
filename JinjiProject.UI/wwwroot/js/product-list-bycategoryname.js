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
const range3Radio = document.getElementById('range-3');
const range4Radio = document.getElementById('range-4');
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
range3Radio.addEventListener('change', function () {
    const rangeValues = range3Radio.value.split('-');
    minPriceInput.value = rangeValues[0];
    maxPriceInput.value = rangeValues[1];

    const minPrice = parseFloat(minPriceInput.value);
    const maxPrice = parseFloat(maxPriceInput.value);

    filterProductsByPrice(minPrice, maxPrice);
});
range4Radio.addEventListener('change', function () {
    const rangeValues = range4Radio.value.split('-');
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
    range3Radio.checked = false;
    range4Radio.checked = false;
});


var sizeFiltreItem = document.getElementById('filter-size');
var colorFiltreItem = document.getElementById('filter-color');

fillFilterMaterial();
fillFilterColor();


const clearSizeButton = document.getElementById('clear-size-filter');

clearSizeButton.addEventListener('click', function () {

    // Temizle düğmesini gizle
    clearSizeButton.style.display = 'none';

    var sizeRadioInputs = sizeFiltreItem.querySelectorAll('input[type="radio"]');
    sizeRadioInputs.forEach(radio => {
        radio.checked = false;
    });
    updateFilter();
});

const clearColorButton = document.getElementById('clear-color-filter');

clearColorButton.addEventListener('click', function () {

    // Temizle düğmesini gizle
    clearColorButton.style.display = 'none';

    var colorRadioInputs = colorFiltreItem.querySelectorAll('input[type="radio"]');
    colorRadioInputs.forEach(radio => {
        radio.checked = false;
    });
    updateFilter();
});

function updateFilter() {
    var sizeRadioInputs = sizeFiltreItem.querySelectorAll('input[type="radio"]');
    var colorRadioInputs = colorFiltreItem.querySelectorAll('input[type="radio"]');
    const selectedSizes = [];
    const selectedColors = [];
    sizeRadioInputs.forEach(radio => {
        if (radio.checked) {
            selectedSizes.push(radio.value);
        }
    });
    colorRadioInputs.forEach(radio => {
        if (radio.checked) {
            selectedColors.push(radio.value);
        }
    });


    const keyword = searchInput.value.toLowerCase();
    const minPrice = parseFloat(minPriceInput.value) || 0;
    const maxPrice = parseFloat(maxPriceInput.value) || Number.MAX_SAFE_INTEGER;

    const filteredProducts = productList.filter(product => {
        const nameMatches = product.name.toLowerCase().includes(keyword);
        const priceInRange = product.price >= minPrice && product.price <= maxPrice;
        let sizeMatches = true;
        let colorMatches = true;
        if (minPrice != 0 && maxPrice != Number.MAX_SAFE_INTEGER) {
            clearPriceButton.style.display = 'inline-block';
        }
        if (selectedSizes.length > 0) {
            sizeMatches = selectedSizes.includes(product.size);
            clearSizeButton.style.display = 'inline-block';
        }

        if (selectedColors.length > 0) {
            colorMatches = selectedColors.includes(product.color);
            clearColorButton.style.display = 'inline-block';
        }

        return nameMatches && priceInRange && sizeMatches && colorMatches;
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
range3Radio.addEventListener('change', updateFilter);
range4Radio.addEventListener('change', updateFilter);



async function fillFilterMaterial() {
    try {
        // Kategori isimlerini almak için bir fonksiyon çağırıyorsunuz, bu yüzden async kullanmalısınız.
        var sizeNames = await getCategoryNames();

        // Oluşturulan elementleri saklayacak bir fragment oluşturun
        const fragment = document.createDocumentFragment();

        // Her bir kategori için bir radio düğmesi ve etiket oluşturun ve fragmente ekleyin
        sizeNames.forEach((name, index) => {
            const radioInput = document.createElement('input');
            radioInput.type = 'radio';
            radioInput.name = 'material';
            radioInput.id = `size-${name}`;
            radioInput.value = name;

            const label = document.createElement('label');
            label.setAttribute('for', `size-${name}`);
            label.textContent = sizeNames[index];

            const br = document.createElement('br');

            fragment.appendChild(radioInput);
            fragment.appendChild(label);
            fragment.appendChild(br);
        });

        // Fragmenti filtre öğesine ekleyin
        sizeFiltreItem.appendChild(fragment);

        // Olay dinleyicilerini ekleyin
        sizeNames.forEach(name => {
            const radio = document.getElementById(`size-${name}`);
            radio.addEventListener('change', updateFilter);
        });

    } catch (error) {
        console.error(error);
    }
}

async function fillFilterColor() {
    try {
        var colorNames = await getColorNames();

        // Oluşturulan elementleri saklayacak bir fragment oluşturun
        const fragment = document.createDocumentFragment();

        // Renkleri ekleyin
        colorNames.forEach((name, index) => {
            const radioInput = document.createElement('input');
            radioInput.type = 'radio';
            radioInput.name = 'color';
            radioInput.id = `color-${name}`;
            radioInput.value = name;

            const label = document.createElement('label');
            label.setAttribute('for', `color-${name}`);
            label.textContent = colorNames[index];

            const br = document.createElement('br');

            fragment.appendChild(radioInput);
            fragment.appendChild(label);
            fragment.appendChild(br);
        });

        colorFiltreItem.appendChild(fragment);

        // Olay dinleyicilerini ekleyin
        colorNames.forEach(name => {
            const radio = document.getElementById(`color-${name}`);
            radio.addEventListener('change', updateFilter);
        });

    } catch (error) {
        console.error(error);
    }
}

async function getColorNames() {
    return $.ajax({
        url: '/Product/GetColorNames',
    });
}
async function getCategoryNames() {
    return $.ajax({
        url: '/Product/GetSizeNames',
    });
}