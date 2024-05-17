#!/bin/bash

declare -i sum=0
declare -i i=0
for (( i=0; i <= 100 ; i++ ))
do 
#	sum=$(expr $sum + $i );
      #sum=$(( $sum + $i ));
	#sum=sum+i;
	sum=$sum+$i;
done
echo $sum
