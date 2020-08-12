function enablePostBtn() {
    let commentSubmit = document.getElementById("commentSubmit");
    let commentInput = document.getElementById("commentInput").value.trim();

    if (commentInput !== "") {
        commentSubmit.disabled = false;
    } else {
        commentSubmit.disabled = true;
    }
}

function disableCommentButton() {
    let commentSubmit = document.getElementById("commentSubmit");
    commentSubmit.disabled = true;
}

disableCommentButton();