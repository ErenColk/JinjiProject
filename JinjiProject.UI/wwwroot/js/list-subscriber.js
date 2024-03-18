const subscribersPerPage = 8; // Her sayfada gösterilecek ürün sayısı
const paginationList = document.getElementById('pagination-list');
const totalPages = Math.ceil(subscriberList.length / subscribersPerPage); // Toplam sayfa sayısı
const subscriberTableBody = document.getElementById('subscriber-table-body');
var currentPage = 1;

updatePagination(totalPages, subscriberList)
updateActiveClass(currentPage);
showPage(currentPage, subscriberList)

function updatePagination(totalPages, subscribers) {
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
            showPage(i, subscribers);
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

function showPage(pageNumber, subscribers) {
    // Başlangıç ve bitiş indeksleri hesapla
    const startIndex = (pageNumber - 1) * subscribersPerPage;
    const endIndex = Math.min(startIndex + subscribersPerPage, subscriberList.length);

    // Ürünleri temizle
    subscriberTableBody.innerHTML = '';

    // productList'ten sadece o sayfadaki ürünleri al
    const subscribersOnPage = subscribers.slice(startIndex, endIndex);

    fillsubscribers(subscribersOnPage);

}

function fillsubscribers(subscribers) {
    subscribers.forEach((subscriber, index) => {
        const subscriberTR = document.createElement('tr');
        subscriberTR.innerHTML = `
               <td>${subscriber.id}</td>
                                                <td>${subscriber.fullName}</td>
                                                <td>${subscriber.email}</td>
                                                <td>${subscriber.statusName}</td>
                                                <td>
                                                    <button class="btn btn-outline-secondary dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown">
                                                        Seçenekler
                                                    </button>
                                                    <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu">
                                                        <li>
                                                            <a class="dropdown-item text-danger" href="/Admin/Subscriber/SoftDelete/${subscriber.id}">Sil</a>
                                                        </li>
                                                    </ul>
                                                </td>`;
        subscriberTableBody.appendChild(subscriberTR);
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
        showPage(currentPage, subscriberList);
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
        showPage(currentPage, subscriberList);
    }

})