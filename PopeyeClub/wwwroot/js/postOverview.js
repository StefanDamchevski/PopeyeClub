function addLike(event, postId) {
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

function removeLike(event, postId) {
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

