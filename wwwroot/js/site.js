
let button = document.querySelector(".addToCart");


const URL = "http://localhost:5236/"
let Id = document.URL.split("/")[5]
button.addEventListener("click",function () {
    fetch(URL+`Cart/AddtoCart/${Id}`,{
        method:"post"
    }).then(res =>{
        console.log(res)
    })


  
})