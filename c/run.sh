#!/bin/bash
rm -rf build
mkdir build
chmod 777 build
cd build
cmake -DEXECUTE_FILE_NAME=$1 ..
cmake --build .
./c_primer_plus_demo
