async function loadSubscriberHardDeleteData(id) {
    const table = document.getElementById('kt_modal_delete_subscriber_form');

    const deletebutton = document.getElementById('modal-subscriber-delete');
    await deletebutton.setAttribute('href', '');
    await deletebutton.setAttribute('href', '/Admin/Subscriber/HardDelete/' + id);


    const rows = table.getElementsByTagName('tr');

    const categoryNameRow = rows[0];
    const categoryDescriptionRow = rows[1];

    const categoryNameCell = categoryNameRow.cells[1];
    const categoryDescriptionCell = categoryDescriptionRow.cells[1];

    const subscriber = await getSubscriber(id);

    categoryNameCell.textContent = ":" + " " + subscriber.fullName;
    categoryDescriptionCell.textContent = ":" + " " + subscriber.email;
}


function getSubscriber(subscriberid) {

    return $.ajax({
        url: '/Admin/Subscriber/GetSubscriber',
        data: { subscriberid: subscriberid }
    });

}