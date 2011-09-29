$(document).ready(function () {

    $("#content div[id*=product]").draggable({
        revert: true,
        handle: '.handle',
        cursor: 'move',
        opacity: 0.7        
    });

});