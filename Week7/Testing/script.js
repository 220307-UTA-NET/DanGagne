function myFunction()
{
    console.log("My function running");

    let testDiv=document.getElementById("testdiv");

    let newParagraph = document.createElement('p');

    newParagraph.textContent = "this is a new paragraph";

    testDiv.append(newParagraph);
}

let counter = 0;
function countFunction()
{
    ++counter
    console.log(counter);
}
document.addEventListener("DOMContentLoaded", function()
{
    let somethingNew = document.getElementById("button2");
    somethingNew.addEventListener('click', ()=> {location.href = "https://www.google.com/"})
});
