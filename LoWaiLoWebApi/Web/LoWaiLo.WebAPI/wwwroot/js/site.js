
$(document).ready(function () {
    $('#rate i').on('click', function () {
        var onStar = parseInt($(this).data('value'), 10);
        var stars = $(this).parent().children('i.fa').toArray().reverse();

        for (i = 0; i < stars.length; i++) {
            $(stars[i]).removeClass('active');
        }
        
        for (i = 0; i < onStar; i++) {
            $(stars[i]).addClass('active');
        }
        document.getElementById("Rating").value = onStar;
    });
});

