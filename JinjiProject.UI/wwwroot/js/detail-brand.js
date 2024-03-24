async function loadBrandData(id) {
    const table = document.getElementById('kt_modal_detail_brand_form');

  

    const rows = table.getElementsByTagName('tr');

    const brandNameRow = rows[0];
    const brandDescriptionRow = rows[1];
    const brandCreatedDateRow = rows[2];
    const brandModifiedDateRow = rows[3];

    const brandNameCell = brandNameRow.cells[1];
    const brandDescriptionCell = brandDescriptionRow.cells[1];
    const brandCreatedDateCell = brandCreatedDateRow.cells[1];
    const brandModifiedDateCell = brandModifiedDateRow.cells[1];

    const brand = await getBrand(id);

    brandNameCell.textContent = ":" + " " + brand.name;
    brandDescriptionCell.textContent = ":" + " " + brand.description;
    brandCreatedDateCell.textContent = ":" + " " + new Date(brand.createdDate).toLocaleDateString();
   
    brandModifiedDate = await new Date(brand.modifiedDate).getFullYear();
    
    
    if (brandModifiedDate !== 0001) {
        brandModifiedDateCell.textContent = ":" + " " + new Date(brand.modifiedDate).toLocaleDateString();
    } else {
        brandModifiedDateCell.textContent = ":" + " " + "Henüz güncellenmedi!";
    }
}


function getBrand(brandid) {
    return $.ajax({
        url: '/Admin/Brand/GetDetailBrand',
        data: { brandid: brandid }
    });

}