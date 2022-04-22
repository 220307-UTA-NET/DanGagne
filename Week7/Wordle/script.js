let word = prompt("Enter a word to guess").toUpperCase();


let maxChances=6;
let chances = 0;
let guessInput;

document.getElementById("guess").addEventListener("submit",processGuess);

document.getElementById("guessInput").maxLength = word.length;

populateTable();

function populateTable(){
  
  for (c=1;c<7;c++){
    n=0;
    while (n<word.length){
      var row =document.getElementById("row"+c);
      var x = row.insertCell(n);
      x.innerHTML = "X";
      n++
    }
  }
}

function updateGuess(){
  for (c=0; c<word.length; c++){
    document.getElementById("row"+(chances)).cells[c].innerHTML=guessInput[c];

    if (word.includes(guessInput[c])){
      document.getElementById("row"+(chances)).cells[c].style.background = 'yellow';
    }
    if (word[c]==guessInput[c]){
      document.getElementById("row"+(chances)).cells[c].style.background = 'lightgreen';
    }
  }
}

function winConditions(){
  if (word == guessInput){
    alert("You guessed the word!")
  }
  if (chances == 6){
    alert("Game Over! \nThe Word was: " +word)
  }
}

function processGuess(e){
  e.preventDefault();

  guessInput = document.getElementById("guessInput").value.toUpperCase();

  document.getElementById("guessInput").value = "";

  if (chances<maxChances){
    chances++;
    updateGuess();
    winConditions();
   
  }
}
