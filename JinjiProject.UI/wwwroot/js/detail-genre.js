﻿const genreImgElement = document.getElementById('detail-genre-img');
async function loadGenreData(id) {
    const table = document.getElementById('kt_modal_detail_genre_form');



    const rows = table.getElementsByTagName('tr');

    const genreImageRow = rows[0];
    const genreNameRow = rows[1];
    const genreDescriptionRow = rows[2];
    const genreTitleRow = rows[3];
    const genreCreatedDateRow = rows[4];
    const genreModifiedDateRow = rows[5];

    const genreImageCell = genreImageRow.cells[1];
    const genreNameCell = genreNameRow.cells[1];
    const genreDescriptionCell = genreDescriptionRow.cells[1];
    const genreTitleCell = genreTitleRow.cells[1];
    const genreCreatedDateCell = genreCreatedDateRow.cells[1];
    const genreModifiedDateCell = genreModifiedDateRow.cells[1];




    const genre = await getDetailGenre(id);


    genreImgElement.alt = genre.name;
    genreImgElement.src = genre.imagePath;
    genreImgElement.style.width = '100px';
    genreImgElement.style.height = '150px';
    genreImgElement.classList.add('img-thumbnail');

    genreNameCell.textContent = ":" + "  " + genre.name;
    genreDescriptionCell.textContent = ":" + "   " + genre.description;
    genreTitleCell.textContent = ":" + "   " + genre.title;
    genreCreatedDateCell.textContent = ":" + "   " + new Date(genre.createdDate).toLocaleDateString();
    const genreModifiedDate = await new Date(genre.modifiedDate).getFullYear();

    if (genreModifiedDate !== 0001) {
        genreModifiedDateCell.textContent = ":" + "   " + new Date(genre.modifiedDate).toLocaleDateString();
    } else {
        genreModifiedDateCell.textContent = ":" + "   " + "Henüz güncellenmedi!";
    }
}

// Add an event listener to the image element for mouseover
genreImgElement.addEventListener('click', function () {
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
    const enlargedImgGenre = document.createElement('img');
    enlargedImgGenre.src = genreImgElement.src;
    enlargedImgGenre.style.maxWidth = '80%';
    enlargedImgGenre.style.maxHeight = '80%';
    enlargedImgGenre.style.transition = 'transform 0.3s ease';

    // Add an event listener to the enlarged image element for mouseout
    enlargedImgGenre.addEventListener('mouseout', function (event) {
        // Check if the mouse is outside of the image bounds
        const bounds = enlargedImgGenre.getBoundingClientRect();
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
    modalOverlay.appendChild(enlargedImgGenre);

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




async function getDetailGenre(genreid) {
    return $.ajax({
        url: '/Admin/Genre/GetGenre',
        data: { genreid: genreid }
    });

}