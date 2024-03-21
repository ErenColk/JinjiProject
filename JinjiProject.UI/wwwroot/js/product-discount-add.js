async function addPriceToProduct(id) {
    const table = document.getElementById('kt_modal_discount_product_form');
    const rows = table.getElementsByTagName('tr');
    var newPriceDiv = document.getElementById('product-newprice');
    var newPriceTextDangerDiv = document.getElementById('danger-newprice').textContent = ''
    newPriceDiv.value = "";
    newPriceTextDangerDiv.value = "";
    const productNameRow = rows[0];
    const productStockRow = rows[1];
    const productCreatedDateRow = rows[2];
    const productPriceRow = rows[3];


    const productNameCell = productNameRow.cells[1];
    const productStockCell = productStockRow.cells[1];
    const productCreatedDateCell = productCreatedDateRow.cells[1];
    const productPriceCell = productPriceRow.cells[1];
    const product = await getProduct(id);

    productNameCell.textContent = ":" + "  " + product.name;
    productStockCell.textContent = ":" + "   " + product.stock;
    productCreatedDateCell.textContent = ":" + "   " + new Date(product.createdDate).toLocaleDateString();
    productPriceCell.textContent = ":" + "   " + product.price + "₺";

    const prevDiscount = document.getElementById('product-oldprice');
    const prevDiscountDiv = document.getElementById('prev-discount-product');
    const removeDiscountButton = document.getElementById('btn-remove-discount');
    debugger
    console.log(product.oldPrice)
    if (product.oldPrice !== null && product.oldPrice !== 0) {
        prevDiscount.value = product.oldPrice;
        removeDiscountButton.style.display = "block";
        prevDiscountDiv.style.display = "block";
    }
    else {
        removeDiscountButton.style.display = "none";
        prevDiscount.value = "";
        prevDiscountDiv.style.display = "none";

    }

    document.getElementById('product-discount-id').value = id;
}

function formatPrice(inputElement, id) {
    // Giriş alanındaki değeri al
    let input = inputElement.value;

    // Para birimi formatına uygun bir regex deseni oluştur
    let regex = /^\d{0,}(\.\d{0,2})?$/;

    // Giriş değeri regex ile eşleşiyorsa, doğru formatta girilmiştir
    if (regex.test(input)) {
        document.getElementById(id).textContent = "";

    } else {
        // Eğer eşleşmiyorsa, hatalı formatta giriş yapılmıştır
        document.getElementById(id).textContent = "Doğru formatta giriş yapınız!";

        // Hatalı girişi temizleme
        if (input.length > 0) {
            // Eğer input alanı doluysa ve hatalı formatta giriş yapıldıysa temizleme
            inputElement.value = "";
            document.getElementById('newproduct-price').textContent = "";
        }
    }
}


function getProduct(productid) {
    console.log(productid)
    return $.ajax({
        url: '/Admin/Product/GetProduct',
        data: { productid: productid }
    });

}