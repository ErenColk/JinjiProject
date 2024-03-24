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

function homePageGenrePartialView() {
	$.ajax({
		url: '/Home/HomePageGenreList',
		type: 'GET',

		success: function (response) {
			$("#homePage-genrelist").html(response)
		}
	});

	
}

