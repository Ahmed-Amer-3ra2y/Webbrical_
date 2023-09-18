var i=0,text;
text = "Welcome To Webbrical. The Feature Way.";


function type(){
    if(i<text.length){
        document.getElementById('heading').innerHTML += text.charAt(i)
        i++
        setTimeout(type,100)
    }
}

type()

//valdition form
let email =document.getElementById('email')
let area = document.getElementById('area')
let submit_btn = document.getElementById('submit')

submit_btn.onclick = function validation(e){
    function check(name){
        e.preventDefault()
        alert(`please enter the ${name}`)
    }

    if(email.value == ''){
        check('email')
    }
    else if(area.value == ''){
        check('your message')
    }}
