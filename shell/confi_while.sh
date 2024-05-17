#!/bin/bash
declare -i i 
declare -i s 
while [ "$i" != "101" ]
do 
	s+=i;
	i=i+1;
done
echo "the cost is $s"
