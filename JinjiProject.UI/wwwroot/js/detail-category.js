
﻿async function loadCategoryData(id) {
    const table = document.getElementById('kt_modal_detail_category_form');



    const rows = table.getElementsByTagName('tr');

    const categoryNameRow = rows[0];
    const categoryDescriptionRow = rows[1];
    const categoryCreatedDateRow = rows[2];
    const categoryModifiedDateRow = rows[3];

    const categoryNameCell = categoryNameRow.cells[1];
    const categoryDescriptionCell = categoryDescriptionRow.cells[1];
    const categoryCreatedDateCell = categoryCreatedDateRow.cells[1];
    const categoryModifiedDateCell = categoryModifiedDateRow.cells[1];


    const category = await getCategory(id);

     categoryNameCell.textContent = ":" + " " + category.name;
     categoryDescriptionCell.textContent = ":" + " " + category.description;

     categoryCreatedDateCell.textContent = ":" + " " + new Date(category.createdDate).toLocaleDateString();

     categoryModifiedDate = await new Date(category.modifiedDate).getFullYear();
    

    if (categoryModifiedDate !== 0001) {
        categoryModifiedDateCell.textContent = ":" + " " + new Date(category.modifiedDate).toLocaleDateString();
    } else {
        categoryModifiedDateCell.textContent = ":" + " " + "Henüz güncellenmedi!";
    }
}


function getCategory(categoryid) {
    return $.ajax({
        url: '/Admin/Category/GetDetailCategory',
        data: { categoryid: categoryid }
    });

}