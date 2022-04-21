#!/usr/bin/bash/

echo "Running timeclock.sh..."
ClockIns=()
IFS=""
while [ true ]; do

	read -p $'Enter your name here:\n' input
	timestamp=$(date +"%Y-%m-%d %T")


	if [[ ${input^^} == "STOP" ]]; then
		break
	elif [[ -z $input ]]; then
		echo $'Empty input. \nPlease enter a valid name.'
	else
		sleep 1
		echo $'\n'$input
		echo "Clocked in: "$timestamp$'\n'
		combine=$"$input: $timestamp"
		ClockIns+=($combine)
	fi
done

for name in ${ClockIns[@]}; do
	echo $name >> ./ClockInOut.txt
done

unset IFS
echo "Exiting Timeclock"
