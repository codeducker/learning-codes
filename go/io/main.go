package main

import (
	"bufio"
	"fmt"
	"io"
	"io/ioutil"
	"os"
	"sync"
	"syscall"
)

func main() {
	// ReadInMemory()
	// ReadIn()
	// ReadOpen()
	// ReadOpenBufio()
	ReadSyscall()
}

func ReadSyscall() {
	fd, err := syscall.Open("a.txt", syscall.O_RDONLY, 0)
	if err != nil {
		panic(err)
	}
	defer syscall.Close(fd)
	var wg sync.WaitGroup
	wg.Add(2)
	dataChan := make(chan []byte)
	go func() {
		defer wg.Done()
		for {
			data := make([]byte, 1024)
			n, err := syscall.Read(fd, data)
			if err != nil {
				panic(err)
			}
			if n == 0 {
				break
			}
			dataChan <- data[:n]
		}
	}()
	go func() {
		defer wg.Done()
		for data := range dataChan {
			fmt.Println(string(data))
		}
	}()
	wg.Wait()
}

func ReadOpenBufio() {
	fi, err := os.Open("a.txt")
	if err != nil {
		fmt.Println(err.Error())
		return
	}
	defer fi.Close()
	reader := bufio.NewReader(fi)
	buf := make([]byte, 1024)
	for {
		// line, err := reader.ReadBytes('\n')
		// lineStr := strings.TrimSpace(string(line))
		//这里可以替换成ReadString

		// line, err := reader.ReadString('\n')
		// lineStr := strings.TrimSpace(line)

		//使用固定数组读取
		n, err := reader.Read(buf)

		if err != nil && err != io.EOF {
			//非文件结尾
			panic(err)
		}
		fmt.Println(string(buf[:n]))
		if err == io.EOF {
			break
		}
	}

}

// ReadOpen
func ReadOpen() {
	// file, err := os.Open("a.txt")
	file, err := os.OpenFile("a.txt", os.O_RDONLY, 0)

	if err != nil {
		fmt.Println(err.Error())
		return
	}
	defer file.Close()
	content, err := ioutil.ReadAll(file)
	if err != nil {
		fmt.Println(err.Error())
		return
	}
	fmt.Println(string(content))
}

// ReadIn Go 1.16版本 该方法被弃用
func ReadIn() {
	content, err := ioutil.ReadFile("a.txt")
	if err != nil {
		fmt.Println(err.Error())
		return
	}
	fmt.Println(string(content))
}

// ReadInMemory
func ReadInMemory() {
	data, err := os.ReadFile("a.txt")
	if err != nil {
		fmt.Println(err.Error())
		return
	}
	fmt.Println(string(data))
}
