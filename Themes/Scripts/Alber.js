$(document).ready(function () {

    //$('.navbar-nav li').hover(function (e) {
    //    e.stopPropagation();
    //    $(this).children('ul').first().stop(true, true).slideToggle();

    //});
    //$('.navbar-toggle').click(function () {
    //    $('.navbar-nav li').hover(function (e) {
    //        return false;

    //    });
    //    $('.navbar-nav li').click(function (e) {
    //        e.stopPropagation();
    //        $(this).children('ul').first().stop(true, true).slideToggle();

    //    });
    //});

    $('.collapse.in').parent().find('.panel-heading').addClass('selected_head');
    //$('.collapse').each(function () { 
    //    $(this).parent().find('.panel-heading').removeClass('selected_head');
    //});
    $('.panel-title a.acc_title').click(function () {

        $(this).parents('.panel-group').find('.panel-heading').removeClass('selected_head');
        $(this).parents('.panel-heading').addClass('selected_head');
    });
    $('.jssort07').parent().height(480);
    $('.events_container .event_items_cont').hide();
    $('.events_container .event_items_cont:eq(0)').show();
    $('#events_acc .panel-body li a').click(function () {
        $(this).parents('#events_acc').find('.panel-body li').removeClass('month_active');
        $(this).parent().addClass('month_active');
        $('.events_container .event_items_cont').hide();
        $($(this).attr('href')).show();

    });
    /*****************/
    $('.serv_container .service_items_cont').hide();
    $('.serv_container .service_items_cont:eq(0)').show();
    $('#events_acc .panel-body li a').click(function () {
        $(this).parents('#events_acc').find('.panel-body li').removeClass('month_active');
        $(this).parent().addClass('month_active');
        $('.serv_container .service_items_cont').hide();
        $($(this).attr('href')).show();

    });


    $(document).ready(function () {

        var partnersCount = $('.partners_body ul li').length;
        partnersWidth = $('.partners_body ul li').width();

        var partnersHeight = $('.partners_body ul li').height();
        var partnersUlWidth = partnersCount * (partnersWidth + 10);

        //$('.partners_body').css({ width: partnersWidth * 4});

        $('.partners_body ul').css({ width: partnersUlWidth });

        $('.partners_body ul li:last-child').prependTo('.partners_body ul');


        $('a.control_prev').click(function () {
            partners_moveLeft();
        });

        $('a.control_next').click(function () {
            partners_moveRight();
        });

    });
    function partners_moveLeft() {
        $('.partners_body ul').animate({
            left: +partnersWidth
        }, 200, function () {
            $('.partners_body ul li:last-child').prependTo('.partners_body ul');
            $('.partners_body ul').css('left', '');
        });
    };

    function partners_moveRight() {
        $('.partners_body ul').animate({
            left: -partnersWidth
        }, 200, function () {
            $('.partners_body ul li:first-child').appendTo('.partners_body ul');
            $('.partners_body ul').css('left', '');
        });
    };
});

$(document).ready(function () {
    //if(window.location.href.toLowerCase().indexOf("pages") > -1) {
    //    $('.main_body').addClass('innerPages');
    //}
});
$(document).on('click', '#close-preview', function () {
    $('.image-preview').popover('hide');
    // Hover befor close the preview
    $('.image-preview').hover(
        function () {
            $('.image-preview').popover('show');
        },
        function () {
            $('.image-preview').popover('hide');
        }
    );
});

$(function () {
    // Create the close button
    var closebtn = $('<button/>', {
        type: "button",
        text: ' x ',
        id: 'close-preview',
        style: 'font-size: initial;',
    });
    closebtn.attr("class", "close pull-right");
    // Set the popover default content
    $('.image-preview').popover({
        trigger: 'manual',
        html: true,
        title: "<strong>&nbsp;Preview&nbsp;</strong>" + $(closebtn)[0].outerHTML,
        content: "There's no image",
        placement: 'bottom'
    });
    // Clear event
    $('.image-preview-clear').click(function () {
        $('.image-preview').attr("data-content", "").popover('hide');
        $('.image-preview-filename').val("");
        $('.image-preview-clear').hide();
        $('.image-preview-input input:file').val("");
        $(".image-preview-input-title").text("Browse");
    });
    // Create the preview image
    $(".image-preview-input input:file").change(function () {
        var img = $('<img/>', {
            id: 'dynamic',
            width: 250,
            height: 200
        });
        var file = this.files[0];
        var reader = new FileReader();
        if (file.type == "image/jpeg" || file.type == "image/jpg" || file.type == "image/png" || file.type == "image/gif") {
            // Set preview image into the popover data-content
            reader.onload = function (e) {
                $(".image-preview-input-title").text("Change");
                $(".image-preview-clear").show();
                $(".image-preview-filename").val(file.name);
                img.attr('src', e.target.result);
                $(".image-preview").attr("data-content", $(img)[0].outerHTML).popover("show");
            }
        }
        else {
            reader.onload = function (e) {
                $(".image-preview-input-title").text("Change");
                $(".image-preview-clear").show();
                $(".image-preview-filename").val(file.name);
                //img.attr('src', e.target.result);
                //$(".image-preview").attr("data-content", $(img)[0].outerHTML).popover("show");
            }
        }
        reader.readAsDataURL(file);
    });
});