function triggerHomePagePartialView() {
	$.ajax({
		url: '/Product/BagList',
		type: 'GET',

		success: function (response) {
			$("#homePage-bag-list").html(response)
		}

	});

	runOwlCarousel();
}

