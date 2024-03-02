async function loadCategoryHardDeleteData(id) {
    const table = document.getElementById('kt_modal_delete_category_form');

    const deletebutton = document.getElementById('modal-category-delete');
    await deletebutton.setAttribute('href', '');
    await deletebutton.setAttribute('href', '/Admin/Category/HardDelete/' + id);


    const rows = table.getElementsByTagName('tr');

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