
function updateScroll() {
    var element = document.getElementById("commentSection");
    element.scrollTop = element.scrollHeight;
}

function disableButtons() {
    document.getElementById("comment-submit").disabled = true;
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

function addComment(event, count, postId) {
   
    event.preventDefault();
    let data = new FormData(event.target);

    let storageData = JSON.parse(localStorage.getItem(postId));

    let lastId = storageData.length - 1;
    console.log(lastId)

    if (lastId >= count) {
        count = lastId + 1;
    }

    storageData.push(count)

    localStorage.setItem(postId, JSON.stringify(storageData));

    axios.post('/PostComment/Create/', data)
        .then(function (response) {
            let commentSection = document.getElementById("commentSection");

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
            addLikeBtn.id = "addCommentLikeBtn-" + count;
            addLikeBtn.setAttribute('onclick', `addCommentLike(${response.data.commentId},${count})`);
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
            removeLikeBtn.id = "removeCommentLikeBtn-" + count;
            removeLikeBtn.setAttribute('onclick', `removeCommentLike(${response.data.commentId}, ${count})`);
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

            document.getElementById("commentInput").value = "";
            updateScroll();
        })
        .catch(function (error) {
            console.log(error)
        });
}

function addCommentLike(commentId, i) {

    if (commentId != 0) {
        axios.post('/CommentLike/AddCommentLike/', {
            commentId: commentId
        })
            .then(function (response) {
                document.getElementById("addCommentLikeBtn-" + i).classList.add('hide');
                document.getElementById("removeCommentLikeBtn-" + i).classList.remove('hide');
            })
            .catch(function (error) {
                console.log(error);
            });
    }
}

function removeCommentLike(commentId, i) {

    if (commentId != 0) {
        axios.post('/CommentLike/RemoveCommentLike/', {
            commentId: commentId
        })
            .then(function (response) {
                document.getElementById("addCommentLikeBtn-" + i).classList.remove('hide');
                document.getElementById("removeCommentLikeBtn-" + i).classList.add('hide');
            })
            .catch(function (error) {
                console.log(error);
            });
    }
}

function addLike(postId) {

    if (postId != 0) {
        axios.post('/PostLike/AddPostLike/', {
            postId: postId
        })
            .then(function (response) {
                document.getElementById("addPostLikeBtn").classList.add('hide');
                document.getElementById("removePostLikeBtn").classList.remove('hide');
                let count = document.getElementById("likesCount");
                count.innerHTML = parseInt(count.innerHTML) + 1;
            })
            .catch(function (error) {
                console.log(error);
            });
    }
}

function removeLike(postId) {
    if (postId != 0) {
        axios.post('/PostLike/RemovePostLike/', {
            postId: postId
        })
            .then(function (response) {
                document.getElementById("addPostLikeBtn").classList.remove('hide');
                document.getElementById("removePostLikeBtn").classList.add('hide');
                let count = document.getElementById("likesCount");
                count.innerHTML = parseInt(count.innerHTML) - 1;
            })
            .catch(function (error) {
                console.log(error);
            });
    }
}