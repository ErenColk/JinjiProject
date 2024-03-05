async function loadMaterialData(id) {
    const table = document.getElementById('kt_modal_delete_material_form');

    const deletebutton = document.getElementById('modal-material-delete');
    await deletebutton.setAttribute('href', '');
    await deletebutton.setAttribute('href', '/Admin/Material/HardDelete/' + id);

    const deletebuttonhref2 = deletebutton.getAttribute('href');
    console.log(deletebuttonhref2)

    const rows = table.getElementsByTagName('tr');

    const materialNameRow = rows[0];
    const materialDescriptionRow = rows[1];

    const materialNameCell = materialNameRow.cells[1];
    const materialDescriptionCell = materialDescriptionRow.cells[1];

    const material = await getMaterial(id);

    materialNameCell.textContent = ":" + " " + material.name;
    materialDescriptionCell.textContent = ":" + " " + material.description;
}


function getMaterial(materialid) {
    console.log(materialid)
    return $.ajax({
        url: '/Admin/Material/GetMaterial',
        data: { materialid: materialid }
    });

}