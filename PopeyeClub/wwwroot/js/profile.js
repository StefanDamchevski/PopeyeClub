let uploadBtn = document.getElementById('uploadBtn');
let readFileBtn = document.getElementById('readFileBtn');
let submitBtn = document.getElementById('submitBtn');
let cancelBtn = document.getElementById("closeMenu");
let changeProfilePicBtn = document.getElementById("changeProfilePicBtn");
let disableAccountBtn = document.getElementById("disableAccountBtn");
let closeChangePicModalBtn = document.getElementById("closeModalBtn");

uploadBtn.addEventListener('click', function () {
    readFileBtn.click();
});

readFileBtn.addEventListener('change', function () {
    submitBtn.click();
    closeChangePicModalBtn.click();
});

changeProfilePicBtn.addEventListener('click', function () {
    cancelBtn.click();
});

disableAccountBtn.addEventListener('click', function () {
    cancelBtn.click();
});