

$(document).ready(function () {

    //Initialize variable
    itemReceiver = $('#item-receiver-0'),
  itemList = $('.item-list'),
  thumbnailWrapper = $('#thumbnail-wrapper'),
  documentWidth = $(document).width() + 'px';

    //Show item list
    $('#plus').on('click', function () {

        me = $(this);

        if (!me.hasClass('clicked')) {

            me.addClass('clicked');
            thumbnailWrapper.animate({ 'width': documentWidth }, function () {
                itemList.fadeIn();
            });

        } else {

            me.removeClass('clicked');
            itemList.fadeOut();
            thumbnailWrapper.animate({ 'width': '60px' });
        }
    });

    //Image on click
    $('.item').on('click', function () {

        //Scroll to bottom if overflowing the page height
        $('body').animate({ scrollTop: $(document).height() }, 1000);

        //Add to itemReceiver with bouncing effect
        $(this).clone().appendTo(itemReceiver).addClass('show');
        //if (('.thumbnail').html()!= null) {
            
        //}
        var attr_id = $(this).attr('id');
        $('.item-list img').remove('#' + attr_id);
        $("#item-receiver").delegate(".item", "click", function () {
            var p = $(this).attr('id');
            window.location = "/House/ShowHouseInfo/" + p;
        });

        if ($('#item-receiver-0').html() != null) {
            $(this).clone().appendTo($('#item-receiver-1')).addClass('show');
        }
    });

    
    //    jQuery('#item-receiver img').each(function () {
    //       
    //       $(this).live('click', function () {
    //            var path = $(this).attr('id');
    //            alert(path);
    //          window.location = "/House/HouseList/" + path;
    //        });

    //  });


    //    $('#item-receiver').on('click', function () {

    //        //Scroll to bottom if overflowing the page height

    //        var path = $(this).children('item show').attr('id');
    //        window.location = "/House/HouseList/" + path;

    //        //Add to itemReceiver with bouncing effect

    //    });

});

$(".item show").live("click", function () {

    var path = $(this).children('item show').attr('id');
    window.location = "/House/HouseList/" + path;
});

//$(".item show").click(function () {
//    alert("234");

//})