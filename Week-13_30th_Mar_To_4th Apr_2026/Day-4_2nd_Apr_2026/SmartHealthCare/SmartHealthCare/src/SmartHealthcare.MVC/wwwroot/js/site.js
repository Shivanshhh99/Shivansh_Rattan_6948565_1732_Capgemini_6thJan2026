// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Small UI helpers: collapse navbar on link click and mark active nav item
(function () {
	'use strict';

	document.addEventListener('DOMContentLoaded', function () {
		// Collapse mobile navbar when a nav-link is clicked
		var navBar = document.getElementById('navBar');
		if (navBar) {
			var bsCollapse = new bootstrap.Collapse(navBar, { toggle: false });
			document.querySelectorAll('.nav-link').forEach(function (link) {
				link.addEventListener('click', function () {
					if (window.getComputedStyle(document.querySelector('.navbar-toggler')).display !== 'none') {
						bsCollapse.hide();
					}
				});
			});
		}

		// Set active nav item based on current URL
		var current = window.location.pathname.toLowerCase();
		document.querySelectorAll('.navbar .nav-link').forEach(function (link) {
			var href = (link.getAttribute('href') || '').toLowerCase();
			if (href && current.indexOf(href) !== -1) {
				link.classList.add('active');
			}
		});
	});
})();
