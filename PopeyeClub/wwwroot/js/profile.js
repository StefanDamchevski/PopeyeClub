let uploadBtn = document.getElementById('uploadBtn');
let readFileBtn = document.getElementById('readFileBtn');
let submitBtn = document.getElementById('submitBtn');

uploadBtn.addEventListener('click', function () {
    readFileBtn.click();
});

readFileBtn.addEventListener('change', function () {
    submitBtn.click();
});