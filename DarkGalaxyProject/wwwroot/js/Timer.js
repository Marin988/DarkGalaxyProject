
var counter = 0;

function setup() {

    $('#timer').text(counter);

    function timeIt() {
        counter++;
    }
    setInterval(timeIt, 1000);
}