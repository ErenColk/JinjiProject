async function loadProductHardDeleteData(id) {
    const table = document.getElementById('kt_modal_delete_product_form');

    const deletebutton = document.getElementById('modal-product-delete');
    await deletebutton.setAttribute('href', '');
    await deletebutton.setAttribute('href', '/Admin/Product/HardDelete/' + id);


    const rows = table.getElementsByTagName('tr');

    const productImageRow = rows[0];
    const productNameRow = rows[1];

    const productImageCell = productImageRow.cells[1];
    const productNameCell = productNameRow.cells[1];

    const product = await getProduct(id);

    // Resim elementi oluştur
    const imgElement = document.createElement('img');
    imgElement.src = product.imagePath; // Ürünün görüntü yolunu ata
    imgElement.style.maxWidth = '100px'; // Stil ekle - maksimum genişlik
    imgElement.style.borderRadius = '50%'; // Stil ekle - kenar yarıçapı
    // Ürün fotoğrafı hücresine resmi ekle
    productImageCell.innerHTML = ''; // Önce hücreyi temizle
    productImageCell.appendChild(imgElement); // Resim elementini hücreye ekle
    productNameCell.textContent = product.name;
}


function getProduct(productId) {

    return $.ajax({
        url: '/Admin/Product/GetProduct',
        data: { productid: productId }
    });

}