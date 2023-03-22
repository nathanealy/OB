// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('[id^=update-]').click(function () {

    let requestVerificationToken = $('input[name="__RequestVerificationToken"]').val();
	let id = this.id;
	let row = id.split("-");
	let nickName = $(`#nickname-${row[1]}`).val();

	$.ajax({
		type: "GET",
		url: `Home/UpdateNickName?row=${row[1]}&nickName=${nickName}`,
		headers: { 'RequestVerificationToken': requestVerificationToken },
		dataType: "json",
		success: function (result, xhr) {
			console.info(result);
			$(`#error-${row[1]}`).html(`Success!`);
		},
		error: function (xhr, error) {
			console.info(error);
			$(`#error-${row[1]}`).html(`Error`);
		}
	});


});

$(document).ready(function () {



});