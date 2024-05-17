package main

import (
	"fmt"
	"io"
	"net/http"
	"net/url"
)

func main() {
	//解析url
	url_str := "https://www.example.com/test.html?name=test&type=1#ifmacth"
	u, err := url.Parse(url_str)
	if err != nil {
		panic(err)
	}
	fmt.Println("scheme:", u.Scheme, "host:", u.Host, "path:", u.Path, "port:", u.Port(),
		"rawQuery:", u.RawQuery, "fragment:", u.Fragment)
	//创建url
	url_str2 := url.URL{Scheme: "https", Host: "www.example.com", Path: "/test.doc", RawQuery: "type=1&name=andy"}
	fmt.Println(url_str2.String())
	//解析查询参数
	query_values, err2 := url.ParseQuery(url_str2.RawQuery)
	if err2 != nil {
		panic(err2)
	}
	type_value := query_values.Get("type")
	fmt.Println("type=", type_value)

	//获取网页内容
	request_url := "https://www.baidu.com"
	r, err3 := http.Get(request_url)
	if err3 != nil {
		panic(err3)
	}
	defer r.Body.Close()
	b, _ := io.ReadAll(r.Body)
	fmt.Println(string(b))

}
