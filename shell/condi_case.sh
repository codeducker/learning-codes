#!/bin/bash

read -p "请输入是否继后续操作 yes / no :" choice
case $choice in 
	yes | ye* | Y* )
		echo "继续后续操作"
		;;
	no | n* | N* )
		echo "不继续后续操作"
		;;
	*)
	  echo "未输入任何操作"
	  ;;
esac
