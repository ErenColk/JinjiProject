
async function loadProductData(id) {
    const table = document.getElementById('kt_modal_detail_product_form');
    const imgElement = document.getElementById('detail-product-img');


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

    imgElement.alt = product.name;
    imgElement.src = product.imagePath;
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

    productNameCell.textContent = ":" + "  " + product.name;
    productDescriptionCell.textContent = ":" + "   " + product.description;
    productColorCell.textContent = ":" + "   " + product.color;
    productPriceCell.textContent = ":" + "   " + product.price;
    productStockCell.textContent = ":" + "   " + product.stock;
    productCapacityCell.textContent = ":" + "   " + product.capacity;
    productCreatedDateCell.textContent = ":" + "   " + new Date(product.createdDate).toLocaleDateString();
    const productModifiedDate = ":" + "   " + await new Date(product.modifiedDate).getFullYear();
    console.log(productModifiedDate);
    if (productModifiedDate == 1) {
        productModifiedDateCell.textContent = ":" + "   " + new Date(product.modifiedDate).toLocaleDateString();
    } else {
        productModifiedDateCell.textContent = ":" + "   " + "Henüz güncellenmedi!";
    }
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


async function getProduct(productid) {
    console.log(productid)
    return $.ajax({
        url: '/Admin/Product/GetProduct',
        data: { productid: productid }
    });

}