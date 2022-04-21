#!/usr/bin/bash/

time=$(date +"%T")

date=$(date +"%Y-%d-%m")

if [[ -e $date.txt ]]

then
	echo "Script run at ${time}" >> ./$date.txt
else
	echo  "Starting file for $date at $time" >> ./$date.txt
fi
