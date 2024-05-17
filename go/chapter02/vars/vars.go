package main

import (
	"fmt"
	"math"
	"os"
	"strconv"
	"unsafe"
)

var c int = 8

func main() {

	var ai int8 = 10
	fmt.Printf("%d", unsafe.Sizeof(ai))

	fmt.Println(math.MaxFloat32) //3.4028234663852886e+38
	fmt.Println(math.MaxFloat64) //1.7976931348623157e+308

	var x, y int = 10, 20
	fmt.Println(x, y)

	var (
		a string = "hello"
		b int8   = 1
	)
	fmt.Println(a, b)

	z := 100
	fmt.Println(z)

	fmt.Println(&c, c)

	c := "hello"
	fmt.Println(&c, c)

	var ma int = 8
	fmt.Println(&ma, ma)
	ma, ba := 10, "heloo" //此时ma为退化操作
	fmt.Println(ma, ba)
	fmt.Println(&ma, ma)

	{
		//此时不同执行域，默认认为新变量
		ma := 8.3
		fmt.Println(&ma, ma)
	}

	fmt.Println(&ma, ma)

	f, err := os.OpenFile("d:\tmp.txt", os.O_RDONLY, os.ModePerm)
	if err != nil {
		fmt.Println(err)
	}
	b2 := make([]byte, 1024)
	n, err := f.Read(b2)
	if err != nil {
		fmt.Println(err)
	}
	fmt.Println(n)

	ag, xy := 1, 2
	ag, xy = xy+1, ag+2
	fmt.Println(ag, xy)

	//符号名称的首字母大小写决定是否为 导出成员，可被包外引用
	// _ 空标识符 表示忽略占位符
	i, _ := strconv.Atoi("1")
	fmt.Println(i)
}
