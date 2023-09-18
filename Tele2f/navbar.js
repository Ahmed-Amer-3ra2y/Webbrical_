openbtn = document.getElementById("open")
nav = document.getElementById("nav")
navm = document.getElementById("navm")
closebtn = document.getElementById("close")
main = document.getElementById("main")
openbtn.onclick = function(){
    nav.style.cssText = "display:block;"
    main.style.cssText = "display:none;"
    nav.style.cssText = "transition:0.5s;"
    navm.style.cssText = "width:100%;"
    openbtn.style.cssText = "visibility:hidden;"
}
closebtn.onclick = function(){
    nav.style.cssText = "display:none;"
    main.style.cssText = "display:block;"
    openbtn.style.cssText = "visibility:visible;"
    openbtn.classList("fixed")
}
