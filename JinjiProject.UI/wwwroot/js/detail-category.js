
async function loadCategoryDetailsData(id) {
    //TABLE NULL GELİYOR ONU DÜZELT

    const table = await document.getElementById('kt_modal_details_category_form');
    console.log(table)
    const rows = await table.getElementsByTagName('tr');

    const categoryNameRow = rows[0];
    const categoryDescriptionRow = rows[1];

    const categoryNameCell = categoryNameRow.cells[1];
    const categoryDescriptionCell = categoryDescriptionRow.cells[1];

    const category = await getCategory(id);

    categoryNameCell.textContent = category.name;
    categoryDescriptionCell.textContent = category.description;
}

function getCategory(categoryid) {

    return $.ajax({
        url: '/Admin/Category/GetCategory',
        data: { categoryid: categoryid }
    });

}