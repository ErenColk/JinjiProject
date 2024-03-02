async function loadMaterialData(id) {
    const table = document.getElementById('kt_modal_detail_material_form');



    const rows = table.getElementsByTagName('tr');

    const materialNameRow = rows[0];
    const materialDescriptionRow = rows[1];
    const materialCreatedDateRow = rows[2];
    const materialModifiedDateRow = rows[3];

    const materialNameCell = materialNameRow.cells[1];
    const materialDescriptionCell = materialDescriptionRow.cells[1];
    const materialCreatedDateCell = materialCreatedDateRow.cells[1];
    const materialModifiedDateCell = materialModifiedDateRow.cells[1];

    const material = await getMaterial(id);

    materialNameCell.textContent = material.name;
    materialDescriptionCell.textContent = material.description;
    materialCreatedDateCell.textContent = new Date(material.createdDate).toLocaleDateString();

    materialModifiedDate = await new Date(material.modifiedDate).getFullYear();
    console.log(materialModifiedDate);

    if (materialModifiedDate !== 0001) {
        materialModifiedDateCell.textContent = new Date(material.modifiedDate).toLocaleDateString();
    } else {
        materialModifiedDateCell.textContent = "Henüz güncellenmedi!";
    }
}


function getMaterial(materialid) {
    console.log(materialid)
    return $.ajax({
        url: '/Admin/Material/GetDetailMaterial',
        data: { materialid: materialid }
    });

}