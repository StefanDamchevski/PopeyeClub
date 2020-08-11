let uploadBtn = document.getElementById('uploadBtn');
let fileBtn = document.getElementById('fileBtn');
let submitBtn = document.getElementById('submitBtn');

uploadBtn.addEventListener('click', function () {
    fileBtn.click();
});

fileBtn.addEventListener('change', function () {
    submitBtn.click();
});