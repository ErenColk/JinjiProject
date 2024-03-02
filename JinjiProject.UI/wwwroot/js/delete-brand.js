async function loadBrandData(id) {
    const table = document.getElementById('kt_modal_delete_brand_form');

    const deletebutton = document.getElementById('modal-brand-delete');
    await deletebutton.setAttribute('href', '');
    await deletebutton.setAttribute('href', '/Admin/Brand/HardDelete/' + id);

    const deletebuttonhref2 = deletebutton.getAttribute('href');
    console.log(deletebuttonhref2)

    const rows = table.getElementsByTagName('tr');

    const brandNameRow = rows[0];
    const brandDescriptionRow = rows[1];

    const brandNameCell = brandNameRow.cells[1];
    const brandDescriptionCell = brandDescriptionRow.cells[1];

    const brand = await getBrand(id);

    brandNameCell.textContent = brand.name;
    brandDescriptionCell.textContent = brand.description;
}


function getBrand(brandid) {
    console.log(brandid)
    return $.ajax({
        url: '/Admin/Brand/GetBrand',
        data: { brandid: brandid }
    });

}