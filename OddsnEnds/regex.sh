#!/usr/bin/bash

test="Hello,Wo1rld!"
reg="^Hello" #compare to start of string
reg2="d!$" # compare to end of string


#check head
if [[ $test =~ [$reg] ]]; then
	echo "test matches"
else
	echo "test doesn't match"
fi


#check tail
if [[ $test =~ [$reg2] ]]; then
        echo "test matches"
else
        echo "test doesn't match"
fi

#check for numbers
if [[ $test =~ [0-9] ]]; then
        echo "there are numbers"
else
        echo "there are no numbers"
fi



