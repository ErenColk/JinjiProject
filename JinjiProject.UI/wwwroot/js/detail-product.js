async function loadProductData(id) {
    const table = document.getElementById('kt_modal_detail_product_form');



    const rows = table.getElementsByTagName('tr');

    const productImageRow = rows[0];
    const productNameRow = rows[1];
    const productDescriptionRow = rows[2];
    const productColorRow = rows[3];
    const productPriceRow = rows[4];
    const productStockRow = rows[5];
    const productCapacityRow = rows[6];
    const productCreatedDateRow = rows[7];
    const productModifiedDateRow = rows[8];

    const productImageCell = productImageRow.cells[1];
    const productNameCell = productNameRow.cells[1];
    const productDescriptionCell = productDescriptionRow.cells[1];
    const productColorCell = productColorRow.cells[1];
    const productPriceCell = productPriceRow.cells[1];
    const productStockCell = productStockRow.cells[1];
    const productCapacityCell = productCapacityRow.cells[1];
    const productCreatedDateCell = productCreatedDateRow.cells[1];
    const productModifiedDateCell = productModifiedDateRow.cells[1];




    const product = await getProduct(id);

    const imgElement = document.getElementById('detail-product-img');
    imgElement.alt = product.name;
    imgElement.src = product.imagePath;
    imgElement.style.width = '100px';
    imgElement.style.height = '150px';
    imgElement.classList.add('img-thumbnail'); 

    productNameCell.textContent = ":" +"  "+ product.name;
    productDescriptionCell.textContent = ":" + "   " + product.description;
    productColorCell.textContent = ":" + "   " + product.color;
    productPriceCell.textContent = ":" + "   " + product.price;
    productStockCell.textContent = ":" + "   " +product.stock;
    productCapacityCell.textContent = ":" + "   " + product.capacity;
    productCreatedDateCell.textContent = ":" + "   " + new Date(product.createdDate).toLocaleDateString();
    const productModifiedDate = ":" + "   " + await new Date(product.modifiedDate).getFullYear();

    if (productModifiedDate !== 0001) {
        productModifiedDateCell.textContent = ":" + "   " +new Date(product.modifiedDate).toLocaleDateString();
    } else {
        productModifiedDateCell.textContent = ":" + "   " + "Henüz güncellenmedi!";
    }
}


function getProduct(productid) {
    console.log(productid)
    return $.ajax({
        url: '/Admin/Product/GetProduct',
        data: { productid: productid }
    });

}