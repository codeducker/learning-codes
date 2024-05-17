#!/bin/bash

function imax() {
	if [ $1 -gt $2 ]; then 
		return $1
	else 
		return $2
	fi	
}
read -p "数值 1 :" num1
read -p "数值 2 :" num2
imax $num1 $num2
echo "当前最大值:$?"
