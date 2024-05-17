package main

import (
	"fmt"
	"strings"
)

func main() {
	var mess string = "Hello, world!"
	suffix := "rld!"
	fmt.Println(strings.HasSuffix(mess, suffix))
}
