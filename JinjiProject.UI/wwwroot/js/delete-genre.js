async function loadGenreHardDeleteData(id) {
    const table = document.getElementById('kt_modal_delete_genre_form');

    const deletebutton = document.getElementById('modal-genre-delete');
    await deletebutton.setAttribute('href', '');
    await deletebutton.setAttribute('href', '/Admin/Genre/HardDelete/' + id);


    const rows = table.getElementsByTagName('tr');

    const genreImageRow = rows[0];
    const genreNameRow = rows[1];

    const genreImageCell = genreImageRow.cells[1];
    const genreNameCell = genreNameRow.cells[1];

    const genre = await getGenre(id);

    // Resim elementi oluştur
    const imgElement = document.createElement('img');
    imgElement.src = genre.imagePath; // Ürünün görüntü yolunu ata
    imgElement.style.width = '100px'; // Stil ekle - maksimum genişlik
    imgElement.style.height = '100px';
    imgElement.style.borderRadius = '50%'; // Stil ekle - kenar yarıçapı
    // Ürün fotoğrafı hücresine resmi ekle
    genreImageCell.innerHTML = ''; // Önce hücreyi temizle
    genreImageCell.appendChild(imgElement); // Resim elementini hücreye ekle
    genreNameCell.textContent = genre.name;
}


function getGenre(genreId) {

    return $.ajax({
        url: '/Admin/Genre/GetGenre',
        data: { genreid: genreId }
    });

}