#!/bin/bash

read -p "输入数值: " data
if [ $data -gt 20 ]; then
  echo "valid number"
else
  echo "invalid number"
fi
