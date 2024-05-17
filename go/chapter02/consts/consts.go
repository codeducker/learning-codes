package main

import (
	"fmt"
	"unsafe"
)

func main() {
	const x, y = 20, 30
	const (
		c        = "hello"
		d string = "world"
	)
	fmt.Println(x, y, c, d)
	{
		const x = 6.4
		fmt.Println(x)
	}
	//常量可以为编译器能计算出的值
	const (
		u  = unsafe.Sizeof(x)
		b  = 0x123
		ro //未赋值，则默认与上述非空行右值
		g  = len("she is a girl")
	)
	fmt.Println(u, b, ro, g)

	const (
		wek = iota
		wes //此时后续值分为为 itoa + n
		ths
		mde
	)
	fmt.Println(wek, wes, ths, mde)

	const (
		_  = iota
		KB = 1 << (10 * iota)
		MB
		GB
		TB
		PB
	)
	fmt.Println(KB, MB, GB, TB, PB)

	const (
		_, _ = iota, iota * 10 //多个iota 独立计数
		kh, khm
		mh, mhm
	)
	fmt.Println(kh, khm, mh, mhm)

	//此时中断iota 需要手动恢复
	const (
		aa = iota // 0
		ba        // 1
		ca = 100  // 100
		da        // 100（与上一行常量右值表达式相同）
		ea = iota // 4（恢复iota自增，计数包括c、d）
		fa        // 5
	)
	fmt.Println(aa, ba, ca, da, ea, fa)
	fmt.Println(black, red, blue)
}

type color byte // 自定义类型

const (
	black color = iota // 指定常量类型
	red
	blue
)
