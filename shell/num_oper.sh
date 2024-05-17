#!/bin/bash
read -p "输入数值 1 :" dt
read -p "输入数值 2 :" et
test $dt -eq $et
echo "相等? $?"

[ $dt -lt $et ]
echo "数值 1 小于 数值 2 ? $?"

[ $dt -ge $dt ]
echo "数值 1 大于等于 数值 2 ? $?"
