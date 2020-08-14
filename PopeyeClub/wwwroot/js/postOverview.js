function addLike(event, postId) {
    if (postId != 0) {
        let addLikeBtn = event.currentTarget;
        let removeLikeBtn = event.currentTarget.parentNode.childNodes[3];
        let count = event.currentTarget.parentNode.parentNode.childNodes[5].childNodes[1].childNodes[1];
        count.innerHTML = parseInt(count.innerHTML) + 1;
        addLikeBtn.classList.add("hide");
        removeLikeBtn.classList.remove("hide");

        axios.post('/PostLike/AddPostLike/', {
            postId: postId
        })
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(error);
            });
    }
}

function removeLike(event, postId) {
    if (postId != 0) {
        let addLikeBtn = event.currentTarget.parentNode.childNodes[1];
        let removeLikeBtn = event.currentTarget;
        let count = event.currentTarget.parentNode.parentNode.childNodes[5].childNodes[1].childNodes[1];
        count.innerHTML = parseInt(count.innerHTML) - 1;
        addLikeBtn.classList.remove("hide");
        removeLikeBtn.classList.add("hide");

        axios.post('/PostLike/RemovePostLike/', {
            postId: postId
        })
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(error);
            });
    }
}

function disableButtons() {

    let submitButtons = document.getElementsByClassName("comment-submit");

    for (let i = 0; i < submitButtons.length; i++) {
        submitButtons[i].disabled = true;
    }
}

disableButtons();

function enablePostBtn(event) {

    let target = event.currentTarget.parentNode.parentNode.childNodes[5].childNodes[1];

    if (event.currentTarget.value.trim() != "") {
        target.disabled = false;
    } else {
        target.disabled = true;
    }
}

function addCommentLike(event, commentId) {
    if (commentId != 0) {
        let addCommentLikeBtn = event.currentTarget;
        let removeCommentLikeBtn = event.currentTarget.parentNode.childNodes[3];
        addCommentLikeBtn.classList.add("hide");
        removeCommentLikeBtn.classList.remove("hide");

        axios.post('/CommentLike/AddCommentLike/', {
            commentId: commentId
        })
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(error);
            });
    }
}

function removeCommentLike(event, commentId) {
    if (commentId != 0) {
        let addCommentLikeBtn = event.currentTarget.parentNode.childNodes[1];
        let removeCommentLikeBtn = event.currentTarget;
        addCommentLikeBtn.classList.remove("hide");
        removeCommentLikeBtn.classList.add("hide");

        axios.post('/CommentLike/RemoveCommentLike/', {
            commentId: commentId
        })
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(error);
            });
    }
}