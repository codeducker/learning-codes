#!/bin/bash
declare -i i
declare -i s
until [ "$i" == "101" ]
do 
	s+=i;
	i+=1;
done
echo "this cost is $s"
