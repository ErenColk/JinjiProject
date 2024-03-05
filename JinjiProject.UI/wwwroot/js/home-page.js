async function triggerHomePagePartialView() {
    $.ajax({
        url: '/Product/BagList',
        method:'GET',
    });


}