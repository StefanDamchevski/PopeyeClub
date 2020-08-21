window.addEventListener('load', function () {
    document.getElementById('storageBtn-0').click();
});


function addToLocalStorage(commentsCount, storageKey) {

    localStorage.clear();

    let storageData = [];

    for (let i = 0; i < commentsCount; i++) {
        if (storageData.indexOf(i) == -1) {
            storageData.push(i);
        }
    }

    localStorage.setItem(storageKey, JSON.stringify(storageData));
}


function updateScroll() {
    let element = document.getElementById("commentSection");
    element.scrollTop = element.scrollHeight;
}

updateScroll();

function disableButtons() {
    document.getElementById("commentSubmit").disabled = true;
}

disableButtons();

function enablePostBtn(event) {

    let commentInput = document.getElementById("commentInput").value.trim(); 

    if (commentInput != "") {
        document.getElementById("commentSubmit").disabled = false;
    } else {
        document.getElementById("commentSubmit").disabled = true;
    }
}

function addComment(event, count, storagekey) {
   
    event.preventDefault();
    let data = new FormData(event.target);

    let storageData = JSON.parse(localStorage.getItem(storagekey));

    let lastId = storageData.length - 1;

    if (lastId >= count) {
        count = lastId + 1;
    }

    storageData.push(count)

    localStorage.setItem(storagekey, JSON.stringify(storageData));

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
            name.innerHTML = response.data.username + " : ";
            userComment.appendChild(name);

            let commentText = document.createElement("span");
            commentText.innerHTML = response.data.comment;
            commentText.classList.add("comment-text");
            userComment.appendChild(commentText);

            let commentInfo = document.createElement("div");
            commentInfo.classList.add("comment-info");
            userComment.appendChild(commentInfo);

            let commentLikesCount = document.createElement("span");
            commentLikesCount.classList.add("comment-likes-count");
            commentLikesCount.innerText = "Likes ";
            commentInfo.appendChild(commentLikesCount);

            let commentLikesNum = document.createElement("span");
            commentLikesNum.id = "commentLikes-" + count;
            commentLikesNum.innerHTML = 0;
            commentLikesCount.appendChild(commentLikesNum);


            let dateCreated = document.createElement("span");
            dateCreated.classList.add("date-created");
            dateCreated.innerHTML = "Created " + 0 + " Days Ago";
            commentInfo.appendChild(dateCreated);

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

            let modalBtn = document.createElement("button");
            modalBtn.classList.add("btn");
            modalBtn.classList.add("hvr-grow");
            modalBtn.setAttribute('type', 'button');
            modalBtn.setAttribute('data-toggle', 'modal');
            modalBtn.setAttribute('data-target', '#commentActionsModal-' + count);
            commentLike.appendChild(modalBtn);

            let actionsIcon = document.createElement('i');
            actionsIcon.classList.add('fas');
            actionsIcon.classList.add('fa-ellipsis-h');
            actionsIcon.style.fontSize = '1em';
            modalBtn.appendChild(actionsIcon);

            let modal = document.createElement('div');
            modal.classList.add('modal');
            modal.classList.add('fade');
            modal.classList.add('modal-align');
            modal.id = 'commentActionsModal-' + count;
            modal.setAttribute('tabindex', '-1');
            modal.setAttribute('role', 'dialog');
            modal.setAttribute('aria-labelledby', 'commentActionsModalLabel');
            modal.setAttribute('aria-hidden', 'true');
            commentLike.appendChild(modal);

            let modalDialog = document.createElement('div');
            modalDialog.classList.add('modal-dialog');
            modal.setAttribute('role', 'document');
            modal.appendChild(modalDialog);

            let modalContent = document.createElement('div');
            modalContent.classList.add('modal-content')
            modalDialog.appendChild(modalContent);

            let deleteCommentBtn = document.createElement('a');
            deleteCommentBtn.classList.add('btn');
            deleteCommentBtn.classList.add('btn-outline-danger');
            deleteCommentBtn.href = `/PostComment/Delete?commentId=${response.data.commentId}&postId=${response.data.postId}`;
            deleteCommentBtn.innerHTML = "Delete";
            modalContent.appendChild(deleteCommentBtn);

            let reportCommentBtn = document.createElement('a');
            reportCommentBtn.classList.add('btn');
            reportCommentBtn.classList.add('btn-outline-danger');
            reportCommentBtn.href = `/Admin/RecieveReport?commentId=${response.data.commentId}&userId=${response.data.userId}`;
            reportCommentBtn.innerHTML = "Report Comment";
            modalContent.appendChild(reportCommentBtn);

            let closeModalBtn = document.createElement('button');
            closeModalBtn.classList.add('btn');
            closeModalBtn.classList.add('btn-outline-primary');
            closeModalBtn.setAttribute('data-dismiss', 'modal');
            closeModalBtn.innerHTML = "Close";
            modalContent.appendChild(closeModalBtn);

            let hr = document.createElement("hr");
            commentSection.appendChild(hr);

            document.getElementById("commentInput").value = "";
            updateScroll();
            disableButtons();
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
                let count = document.getElementById("commentLikes-" + i);
                count.innerHTML = parseInt(count.innerHTML) + 1;
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
                let count = document.getElementById("commentLikes-" + i);
                count.innerHTML = parseInt(count.innerHTML) - 1;
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

function addToSaved(postId) {
    if (postId != 0) {

        axios.post('/PostSave/AddToSaved/', {
            postId: postId
        })
            .then(function (response) {
                document.getElementById("savePostButton").classList.add('hide');
                document.getElementById("unSavePostButton").classList.remove('hide');
            })
            .catch(function (error) {
                console.log(error);
            });
    }
}

function removeFromSaved(postId) {
    if (postId != 0) {

        axios.post('/PostSave/RemoveFromSaved/', {
            postId: postId
        })
            .then(function (response) {
                document.getElementById("savePostButton").classList.remove('hide');
                document.getElementById("unSavePostButton").classList.add('hide');
            })
            .catch(function (error) {
                console.log(error);
            });
    }
}