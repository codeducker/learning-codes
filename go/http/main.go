package main

import (
	"fmt"
	"io"
	"net/http"
	"os"
)

func main() {
	//Get请求
	resp, err := http.Get("http://example.com/")
	if err != nil {
		fmt.Println(err.Error())
		return
	}
	defer resp.Body.Close()
	headers := resp.Header
	for k, v := range headers {
		fmt.Printf("%v: %v\n", k, v)
	}
	io.Copy(os.Stdout, resp.Body)

	// //打开文件
	// file, err := os.open("./test.txt")
	// //Post请求
	// resp, err = http.Post("http://example.com/upload", "image/jpeg", &buf)
	// if err != nil {
	// 	fmt.Println(err.Error())
	// 	return
	// }
	// fmt.Println(resp)
	// //Post表单请求
	// resp, err = http.PostForm("http://example.com/form",
	// 	url.Values{"key": {"Value"}, "id": {"123"}})
	// if err != nil {
	// 	fmt.Println(err.Error())
	// 	return
	// }
	// fmt.Println(resp)
}
