#!/bin/bash
data=10
(
#子 shell 不影响当前 shell
data=100
echo "子 shell data: $data"
)
echo "当前 data:$data"
