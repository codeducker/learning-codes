package main

import (
	"fmt"
)

type Ifvar interface {

	// type
	// int, int8, int16, int32, int64,
	//     uint, uint8, uint16, uint32, uint64, uintptr,
	//     float32, float64, complex64, complex128,
	//     string
}

func If[T Ifvar](b bool, a1, a2 T) T {
	if b {
		return a1
	}
	return a2
}

func init() {
	fmt.Println("init.....")
}

func main() {
	fmt.Println("main.....")
	var a string = "name"
	fmt.Println(a)
	fmt.Println(0x1.Fp+0 == 0x1.Fp-0) //15/16*2^0 = 0.9375  小数部分乘以进制 得到指定进制的数据
	fmt.Println(4_1 == 41)
	fmt.Println(If(1 < 2, 1, 2))
	fmt.Println(If("foo" > "bar", "foo", "bar"))
	fmt.Println(If(If("aoo" > "bar1111111111", "aoo", "bar1111111111") > "bb", "hello1", "hello2"))
}
