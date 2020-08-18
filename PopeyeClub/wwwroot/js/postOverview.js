
function addLike(postId,i) {
    if (postId != 0) {

        axios.post('/PostLike/AddPostLike/', {
            postId: postId
        })
            .then(function (response) {
                document.getElementById("addPostLikeBtn-" + i).classList.add('hide');
                document.getElementById("removePostLikeBtn-" + i).classList.remove('hide')
                let count = document.getElementById('likesCount' + i);
                count.innerHTML = parseInt(count.innerHTML) + 1;
            })
            .catch(function (error) {
                console.log(error);
            });
    }
}

function removeLike(postId, i) {
    if (postId != 0) {

        axios.post('/PostLike/RemovePostLike/', {
            postId: postId
        })
            .then(function (response) {
                document.getElementById("addPostLikeBtn-" + i).classList.remove('hide');
                document.getElementById("removePostLikeBtn-" + i).classList.add('hide')
                let count = document.getElementById('likesCount' + i);
                count.innerHTML = parseInt(count.innerHTML) - 1;
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

function enablePostBtn(i) {

    let commentInput = document.getElementById("commentInput-" + i).value.trim();

    if (commentInput != "") {
        document.getElementById("commentSubmit-" + i).disabled = false;
    } else {
        document.getElementById("commentSubmit-" + i).disabled = true;
    }
}

function addCommentLike(commentId, i, y) {

    if (commentId != 0) {

        axios.post('/CommentLike/AddCommentLike/', {
            commentId: commentId
        })
            .then(function (response) {
                document.getElementById("addCommentLikeBtn-" + i + "-" + y).classList.add('hide');
                document.getElementById("removeCommentLikeBtn-" + i + "-" + y).classList.remove('hide');
            })
            .catch(function (error) {
                console.log(error);
            });
    }
}

function removeCommentLike(commentId, i, y) {

    if (commentId != 0) {

        axios.post('/CommentLike/RemoveCommentLike/', {
            commentId: commentId
        })
            .then(function (response) {
                document.getElementById("addCommentLikeBtn-" + i + "-" + y).classList.remove('hide');
                document.getElementById("removeCommentLikeBtn-" + i + "-" + y).classList.add('hide');
            })
            .catch(function (error) {
                console.log(error);
            });
    }
}

function addComment(event, i, count, storageKey) {

    event.preventDefault();

    let data = new FormData(event.target);

    let storageData = JSON.parse(localStorage.getItem(storageKey));

    let lastCommentId = storageData[i].length - 1;

    if (lastCommentId >= count) {
        count = lastCommentId + 1;
    }

    storageData[i].push(count);

    localStorage.setItem(storageKey, JSON.stringify(storageData));

    axios.post('/PostComment/Create/', data)
        .then(function (response) {
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
            addLikeBtn.id = "addCommentLikeBtn-" + i + "-" + count;
            addLikeBtn.setAttribute('onclick', `addCommentLike(${response.data.commentId},${i}, ${count})`);
            commentLike.appendChild(addLikeBtn);

            let addLikeIcon = document.createElement("i");
            addLikeIcon.classList.add("far");
            addLikeIcon.classList.add("fa-thumbs-up");
            addLikeIcon.classList.add("fa-2x");
            addLikeIcon.classList.add("hvr-icon-grow");
            addLikeIcon.style.fontSize = "1em";
            addLikeBtn.appendChild(addLikeIcon);


            let removeLikeBtn = document.createElement("button");
            removeLikeBtn.classList.add("btn");
            removeLikeBtn.classList.add("hvr-grow");
            removeLikeBtn.classList.add("hide");
            removeLikeBtn.id = "removeCommentLikeBtn-" + i + "-" + count;
            removeLikeBtn.setAttribute('onclick', `removeCommentLike(${response.data.commentId},${i}, ${count})`);
            commentLike.appendChild(removeLikeBtn);


            let removeLikeIcon = document.createElement("i");
            removeLikeIcon.classList.add("fas");
            removeLikeIcon.classList.add("fa-thumbs-up");
            removeLikeIcon.classList.add("fa-2x");
            removeLikeIcon.classList.add("hvr-icon-grow");
            removeLikeIcon.style.fontSize = "1em";
            removeLikeBtn.appendChild(removeLikeIcon);

            let hr = document.createElement("hr");
            commentSection.appendChild(hr);

            document.getElementById("commentInput-" + i).value = "";
            disableButtons();
        })
        .catch(function (error) {
            console.log(error)
        });
}

function addToSaved(postId, i) {
    if (postId != 0) {

        axios.post('/PostSave/AddToSaved/', {
            postId: postId
        })
            .then(function (response) {
                document.getElementById("savePostButton-" + i).classList.add('hide');
                document.getElementById("unSavePostButton-" + i).classList.remove('hide');
            })
            .catch(function (error) {
                console.log(error);
            });
    }
}

function removeFromSaved(postId, i) {
    if (postId != 0) {

        axios.post('/PostSave/RemoveFromSaved/', {
            postId: postId
        })
            .then(function (response) {
                document.getElementById("savePostButton-" + i).classList.remove('hide');
                document.getElementById("unSavePostButton-" + i).classList.add('hide');
            })
            .catch(function (error) {
                console.log(error);
            });
    }
}