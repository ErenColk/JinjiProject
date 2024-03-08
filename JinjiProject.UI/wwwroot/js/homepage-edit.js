//function addCategory(button) {

//    var row = button.parentNode.parentNode;
//    var selectedTableBody = document.getElementById("selectedCategoriesTable");
//    var rowList = selectedTableBody.querySelectorAll('tr');
//    if (rowList.length >= 3) {
//        Swal.fire({
//            icon: 'error',
//            title: 'Üzgünüz...',
//            text: 'Maksimum kategori sayısına ulaşıldı!',
//            confirmButtonText: 'Tamam'
//        });
//        return false;
//    }

//    var selectedCategoriesTable = document.getElementById("selectedCategoriesTable");
//    var buttonCell = row.querySelector('td:last-child');
//    buttonCell.innerHTML = '';
//    var undoButton = document.createElement('button');
//    undoButton.className = 'btn btn-outline-danger';
//    undoButton.textContent = 'Geri Al';
//    undoButton.onclick = undoCategory;
//    buttonCell.appendChild(undoButton);
//    selectedCategoriesTable.appendChild(row);
//    resetIndicesSelected();
//    resetIndicesSelect();
//}
//function undoCategory() {
//    var deletedRow = this.parentNode.parentNode;
//    var selectCategoriesTable = document.getElementById("selectCategoriesTable");
//    var buttonCell = deletedRow.querySelector('td:last-child');
//    buttonCell.innerHTML = '';
//    var createButton = document.createElement('button');
//    createButton.className = 'btn btn-outline-success';
//    createButton.textContent = 'Ekle';
//    createButton.onclick = function () { addCategory(this); };
//    buttonCell.appendChild(createButton);
//    selectCategoriesTable.appendChild(deletedRow);
//    resetIndicesSelect();
//    resetIndicesSelected();
//}

//function resetIndicesSelect() {
//    var rows = document.querySelectorAll('#selectCategoriesTable tr');
//    rows.forEach((row, index) => {
//        var cells = row.getElementsByTagName('td');
//        cells[0].innerText = index + 1;
//    });
//}
//function resetIndicesSelected() {
//    var rows = document.querySelectorAll('#selectedCategoriesTable tr');
//    rows.forEach((row, index) => {
//        var cells = row.getElementsByTagName('td');
//        cells[0].innerText = index + 1;
//    });
//}