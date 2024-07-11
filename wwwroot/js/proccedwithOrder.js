function submitOrder(id) {
    let Url = window.location.href
    let urlString = `/ProccedOrder/${id}`
    Url  = Url.replace("/SubmitOrder", urlString)

    fetch(Url, {
        method: "post"
    }).then(res => {
        console.log(res)

    })

}