$(document).ready(function () {
    $('nav ul li').click(function () {
        var link = $(this).children('a').attr('href');
        document.location = link;
    });
});