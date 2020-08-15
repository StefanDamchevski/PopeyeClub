
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

        if (event.currentTarget.parentNode.childNodes[3] == undefined) {
            removeCommentLikeBtn = event.currentTarget.parentNode.childNodes[1];
        }

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

function addComment(event, i) {

    event.preventDefault();
    let data = new FormData(event.target);

    axios.post('/PostComment/Create/', data)
        .then(function (response) {
            debugger;
            let commentSection = document.getElementById("commentSection-" + i);

            let comment = document.createElement("div");
            comment.classList.add("comment");
            commentSection.appendChild(comment);

            let userComment = document.createElement("div");
            userComment.classList.add("user-comment");
            comment.appendChild(userComment);

            let userImg = document.createElement("img");
            userImg.src = `data:image/jpeg;base64,${response.data.userImage}`
            userComment.appendChild(userImg);

            let name = document.createElement("span");
            name.innerHTML = response.data.username;
            userComment.appendChild(name);

            let commentText = document.createElement("div");
            commentText.innerHTML = response.data.comment;
            commentText.classList.add("comment-text");
            comment.appendChild(commentText);

            let commentLike = document.createElement("div");
            commentLike.classList.add("comment-like");
            comment.appendChild(commentLike);

            let addLikeBtn = document.createElement("button");
            addLikeBtn.classList.add("btn");
            addLikeBtn.classList.add("hvr-grow");
            addLikeBtn.setAttribute('onclick', `addCommentLike(event, ${response.data.commentId})`);
            commentLike.appendChild(addLikeBtn);

            let addLikeIcon = document.createElement("i");
            addLikeIcon.classList.add("far");
            addLikeIcon.classList.add("fa-thumbs-up");
            addLikeIcon.classList.add("fa-2x");
            addLikeIcon.classList.add("hvr-icon-grow");
            addLikeBtn.appendChild(addLikeIcon);


            let removeLikeBtn = document.createElement("button");
            removeLikeBtn.classList.add("btn");
            removeLikeBtn.classList.add("hvr-grow");
            removeLikeBtn.classList.add("hide");
            removeLikeBtn.setAttribute('onclick', `removeCommentLike(event, ${response.data.commentId})`);
            commentLike.appendChild(removeLikeBtn);


            let removeLikeIcon = document.createElement("i");
            removeLikeIcon.classList.add("fas");
            removeLikeIcon.classList.add("fa-thumbs-up");
            removeLikeIcon.classList.add("fa-2x");
            removeLikeIcon.classList.add("hvr-icon-grow");
            removeLikeBtn.appendChild(removeLikeIcon);

            let hr = document.createElement("hr");
            commentSection.appendChild(hr);

            document.getElementById("commentInput-" + i).value = "";
        })
        .catch(function (error) {
            console.log(error)
        });
}
