$(window).scroll(function () {
    var winScroll = $(this).scrollTop();

    $('.bg1').css({
        'transform': 'translate(0, ' + winScroll / 0 + '%)'
    });
    $('.bg2').css({
        'transform': 'translate(0, ' + winScroll / 22 + '%)'
    });
    $('.bg3').css({
        'transform': 'translate(0, ' + winScroll / 16 + '%)'
    });
    $('.bg4').css({
        'transform': 'translate(0, ' + winScroll / 14 + '%)'
    });
});
function downloadFile() {
    const filePath = 'B07_betatest.zip';
    
    const anchorElement = document.createElement('a');
    anchorElement.href = filePath;
    anchorElement.download = ''; 
    document.body.appendChild(anchorElement); 
    anchorElement.click(); 
    document.body.removeChild(anchorElement); 
}
