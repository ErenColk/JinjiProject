
async function loadProductData(id) {
    const product = await getDetailProduct(id);

    const table = document.getElementById('kt_modal_detail_product_form');
    const imgElement = document.getElementById('detail-product-img');
    const imgElementSecond = document.getElementById('detail-product-img-second');
    const imgElementThird = document.getElementById('detail-product-img-third');
    imgElementSecond.style.display = 'none';
    imgElementThird.style.display = 'none';

    const imgElementList = [];
    document.getElementById('text-muted-second').style.display = 'block'
    document.getElementById('text-muted-third').style.display =  'block'
    product.imagePath ? imgElementList.push(imgElement) : document.getElementById('text-muted-second').style.display = 'none';
    if (product.imagePathSecond) {
        imgElementList.push(imgElementSecond);
        imgElementSecond.style.display = 'block';
    } else {
        document.getElementById('text-muted-second').style.display = 'none';
    }
    if (product.imagePathThirth) {
        imgElementList.push(imgElementThird);
        imgElementThird.style.display = 'block';
    } else {
        document.getElementById('text-muted-third').style.display = 'none';
    }

    const rows = table.getElementsByTagName('tr');

    const productImageRow = rows[0];
    const productNameRow = rows[1];
    const productDescriptionRow = rows[2];
    const productColorRow = rows[3];
    const productPriceRow = rows[4];
    const productStockRow = rows[5];
    const productCapacityRow = rows[6];
    const productMeasureRow = rows[7];
    const productSizeRow = rows[8];
    const productStrapLengthRow = rows[9];
    const productCreatedDateRow = rows[10];
    const productModifiedDateRow = rows[11];

    const productNameCell = productNameRow.cells[1];
    const productDescriptionCell = productDescriptionRow.cells[1];
    const productColorCell = productColorRow.cells[1];
    const productPriceCell = productPriceRow.cells[1];
    const productStockCell = productStockRow.cells[1];

    const productCapacityCell = productCapacityRow.cells[1];

    const productXCell = productMeasureRow.cells[1];
    const productYCell = productMeasureRow.cells[2];
    const productZCell = productMeasureRow.cells[3];

    const productSizeCell = productSizeRow.cells[1];
    const productStrapLengthCell = productStrapLengthRow.cells[1];


    const productCreatedDateCell = productCreatedDateRow.cells[1];
    const productModifiedDateCell = productModifiedDateRow.cells[1];





    const productList = [];
    productList.push(product.imagePath)
    productList.push(product.imagePathSecond)
    productList.push(product.imagePathThirth)

    
    imgElementList.forEach(function (imgElement, index) {
        EnlargeImg(product.name,productList[index], imgElement);
    });
    productNameCell.textContent = ":" + "  " + product.name;
    productDescriptionCell.textContent = ":" + "   " + product.description;
    productColorCell.textContent = ":" + "   " + product.color;
    productPriceCell.textContent = ":" + "   " + product.price;
    productStockCell.textContent = ":" + "   " + product.stock;


    product.capacity != null ? (productCapacityRow.style.display = 'table-row') : (productCapacityRow.style.display = 'none');
    product.width != null || (product.length != null) || (product.height != null) ? (productMeasureRow.style.display = 'table-row') : (productMeasureRow.style.display = 'none');
    product.sizeName != "" ? (productSizeRow.style.display = 'table-row') : (productSizeRow.style.display = 'none');
    product.strapLength != null ? (productStrapLengthRow.style.display = 'table-row') : (productStrapLengthRow.style.display = 'none');

    product.capacity != null ? (productCapacityCell.textContent = ":" + "   " + product.capacity + " lt") : (productCapacityCell.textContent = "");
    product.width != null ? (productXCell.textContent = ":" + " " + product.width + " genişlik ") : (productXCell.textContent = "");
    product.length != null ? (productXCell.textContent += "- " + product.length + " en ") : (productXCell.textContent =  "");
    product.height != null ? (productXCell.textContent += "- " + product.height + " boy ") : (productXCell.textContent = "");
    product.sizeName != null ? (productSizeCell.textContent = ":" + "   " + product.sizeName) : (productSizeCell.textContent = "" ) ;
    product.strapLength != null ? (productStrapLengthCell.textContent = ":" + "   " + product.strapLength) : (productStrapLengthCell.textContent = "" ) ;

    productCreatedDateCell.textContent = ":" + "   " + new Date(product.createdDate).toLocaleDateString();
    const productModifiedDate =  await new Date(product.modifiedDate).getFullYear();
    if (productModifiedDate > 1) {
        productModifiedDateCell.textContent = ":" + "   " + new Date(product.modifiedDate).toLocaleDateString();
    } else {
        productModifiedDateCell.textContent = ":" + "   " + "Henüz güncellenmedi!";
    }
}


function EnlargeImg(productName, productImagePath, imgElement) {

    imgElement.alt = productName;
    imgElement.src = productImagePath;
    imgElement.style.width = '100px';
    imgElement.style.height = '150px';
    imgElement.classList.add('img-thumbnail');
    imgElement.addEventListener('click', function () {
        // Create a modal overlay
        const modalOverlay = document.createElement('div');
        modalOverlay.classList.add('modal-overlay');
        modalOverlay.style.position = 'fixed';
        modalOverlay.style.top = '0';
        modalOverlay.style.left = '0';
        modalOverlay.style.width = '100%';
        modalOverlay.style.height = '100%';
        modalOverlay.style.backgroundColor = 'rgba(0, 0, 0, 0)';
        modalOverlay.style.display = 'flex';
        modalOverlay.style.justifyContent = 'center';
        modalOverlay.style.alignItems = 'center';
        modalOverlay.style.zIndex = '9999';
        modalOverlay.style.transition = 'background-color 0.3s ease';

        // Create an image element inside the modal overlay
        const enlargedImg = document.createElement('img');
        enlargedImg.src = imgElement.src;
        enlargedImg.style.maxWidth = '80%';
        enlargedImg.style.maxHeight = '80%';
        enlargedImg.style.transition = 'transform 0.3s ease';
        // Add an event listener to the enlarged image element for mouseout
        enlargedImg.addEventListener('mouseout', function (event) {
            // Check if the mouse is outside of the image bounds
            const bounds = enlargedImg.getBoundingClientRect();
            if (
                event.clientX < bounds.left ||
                event.clientX > bounds.right ||
                event.clientY < bounds.top ||
                event.clientY > bounds.bottom
            ) {
                // If the mouse is outside of the image, close the modal
                closeModal();
            }
        });
        // Append the enlarged image to the modal overlay
        modalOverlay.appendChild(enlargedImg);

        // Append the modal overlay to the document body
        document.body.appendChild(modalOverlay);

        // Disable scrolling when the modal is open
        document.body.style.overflow = 'hidden';

        // Add an event listener to the modal overlay to close it when clicked
        modalOverlay.addEventListener('click', function (event) {
            if (event.target === modalOverlay) {
                closeModal();
            }
        });
    });


}


// Add an event listener to the image element for mouseover


// Function to close the modal overlay
function closeModal() {
    const modalOverlay = document.querySelector('.modal-overlay');
    if (modalOverlay) {
        // Transition background color before removing the overlay
        modalOverlay.style.backgroundColor = 'rgba(0, 0, 0, 0)';
        document.body.removeChild(modalOverlay);

    }

    // Enable scrolling when the modal is closed
    document.body.style.overflow = '';
}




async function getDetailProduct(productid) {
    return $.ajax({
        url: '/Admin/Product/GetProduct',
        data: { productid: productid }
    });

}