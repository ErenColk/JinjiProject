
const categoryContainer = document.getElementById("categories")


fillCategoryNames();

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


async function getCategoryNames() {
    return $.ajax({
        url: '/Admin/Category/GetActiveCategoryNames',
    });
}