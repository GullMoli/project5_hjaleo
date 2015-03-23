var tag = document.createElement('script');
tag.src = "https://www.youtube.com/iframe_api";
var firstScriptTag = document.getElementsByTagName('script')[0];
firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

var player;
function onYouTubeIframeAPIReady() {
    player = new YT.Player('player', {
        height: '390',
        width: '640',
        videoId: $('.modal-body').attr('data-videoid'),
        events: {
            'onReady': onPlayerReady
        }
    });
}
function onPlayerReady(event) {
    //event.target.playVideo();
}
function stopVideo() {
    player.stopVideo();
    player.seekTo(0, false);
}
function playVideo() {
    player.playVideo();
}

$(document).ready(function () {

    $('#myModal').on('shown.bs.modal', function (e) {
        playVideo();
    });
    $('#myModal').on('hide.bs.modal', function (e) {
        stopVideo();
    });

    $(".movie-views .btn input:radio").change(function () {
        if ($(this).val() == 'poster') {
            $('.movies-list').fadeOut('fast', function () {
                $('.movies-poster').fadeIn('fast');
            });
        }
        else {
            $('.movies-poster').fadeOut('fast', function () {
                $('.movies-list').fadeIn('fast');
            });
        }
    });
});
