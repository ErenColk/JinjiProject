
async function loadAdminData(id) {
    const table = document.getElementById('kt_modal_delete_admin_form');

    const deletebutton = document.getElementById('modal-admin-delete');
    await deletebutton.setAttribute('href', '');
    await deletebutton.setAttribute('href', '/Admin/Admin/HardDelete/' + id);

    const deletebuttonhref2 = deletebutton.getAttribute('href');
    console.log(deletebuttonhref2)

    const rows = table.getElementsByTagName('tr');

    const adminNameRow = rows[0];
    const adminDescriptionRow = rows[1];

    const adminNameCell = adminNameRow.cells[1];
    const adminEmailCell = adminDescriptionRow.cells[1];

    const admin = await getAdmin(id);

    adminNameCell.textContent = admin.firstName + " " + admin.lastName;
    adminEmailCell.textContent = admin.email;
}


function getAdmin(adminId) {
    console.log(adminId)
    return $.ajax({
        url: '/Admin/Admin/GetAdmin',
        data: { adminId: adminId }
    });

}