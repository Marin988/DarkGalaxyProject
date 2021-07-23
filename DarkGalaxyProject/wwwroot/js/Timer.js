
var counter = 0;

function upgrade() {

    $('#timer').text(counter);

    function timeIt() {
        counter++;
    }
    setInterval(timeIt, 1000);
}