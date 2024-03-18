
const categoryContainer = document.getElementById("categories")
const navBar = document.getElementById('navbar-ul')

fillCategoryNames();
fillNavbarCategories();
async function fillCategoryNames() {
    const categoryNames = await getCategoryNames();
    categoryNames.forEach((category, index) => {
        const categoryLi = document.createElement('li');
        const categoryLink = document.createElement('a');

        // Dynamically set href attribute using JavaScript
        categoryLink.href = `/Product/Index?category=${category.name}`;
        categoryLink.textContent = category.name;

        categoryLi.appendChild(categoryLink);
        categoryContainer.appendChild(categoryLi);
    });
}

async function fillNavbarCategories() {
    const categoryNames = await getHomePageCategoryNames();

    function capitalizeFirstLetter(string) {
        return string.charAt(0).toUpperCase() + string.slice(1).toLowerCase();
    }
    categoryNames.forEach((category, index) => {
        const categoryLi = document.createElement('li');
        const categoryLink = document.createElement('a');

        var hastagName = ''
        if (category.order === 1) {
            hastagName = 'men'
        }
        else if (category.order === 2) {
            hastagName = 'women'
        }
        else {
            hastagName = 'kids'
        }
        categoryLink.href = `/#${hastagName}`;
        categoryLink.onclick = function () {
            // Buraya tıklandığında yapılacak işlemleri ekleyin
            // Örneğin, kategoriye tıklandığında bir fonksiyon çağrılabilir veya bir sayfa yüklenmesi sağlanabilir
            console.log('Kategoriye tıklandı:', category.name);

            // Tüm <a> elementlerinden 'active' sınıfını kaldır
            const allNavLinks = document.querySelectorAll('#navbar-ul a');
            allNavLinks.forEach(link => {
                link.classList.remove('active');
            });
            categoryLink.classList.add('active');

        };
        categoryLink.textContent = category.name.split(' ').map(capitalizeFirstLetter).join(' ');
        categoryLi.classList.add("scroll-to-section");
        categoryLi.appendChild(categoryLink);
        navBar.insertBefore(categoryLi, navBar.children[navBar.children.length - 3]);
    });
}


async function getCategoryNames() {
    return $.ajax({
        url: '/Admin/Category/GetActiveCategoryNames',
    });
}

async function getHomePageCategoryNames() {
    return $.ajax({
        url: '/Admin/Category/GetHomePageCategoryNames',
    });
}