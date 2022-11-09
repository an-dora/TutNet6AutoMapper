const Pager = {

	/*
	return array of object {
		isCurrentPage: bool,
		text: string,
		page: string,
	}
	*/

	create(currentPage, pageSize, totalRow) {
		const ON_EACH_SIDE = 4;
		var arrPager = [];
		var totalPage = Math.ceil(totalRow / pageSize);

		currentPage = parseInt(currentPage);
		if (totalPage == 0) {
			return arrPager;
		}
		else {
			// current page
			arrPager.push({
				isCurrentPage: true,
				text: currentPage,
				page: currentPage,
			});
			// after current page
			var i = 1;
			while (currentPage + i <= totalPage) {
				arrPager.push({
					isCurrentPage: false,
					text: currentPage + i,
					page: currentPage + i,
				});
				i++;
				if (i > ON_EACH_SIDE) break;
			}
			if (currentPage + ON_EACH_SIDE < totalPage) {
				arrPager.push({
					isCurrentPage: false,
					text: "&rArr;",
					page: totalPage,
				});
			}

			// before current page
			i = 1;
			var tmp = currentPage;
			while (tmp > 1) {
				arrPager.unshift({
					isCurrentPage: false,
					text: currentPage - i,
					page: currentPage - i,
				});
				i++;
				tmp--;
				if (i > ON_EACH_SIDE) break;
			}
			if (currentPage - ON_EACH_SIDE > 1) {
				arrPager.unshift({
					isCurrentPage: false,
					text: "&lArr;",
					page: 1,
				});
			}
		}
		return arrPager;
	}
}