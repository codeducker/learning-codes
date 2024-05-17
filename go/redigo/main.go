package main

import (
	"fmt"
	"time"

	"github.com/gomodule/redigo/redis"
)

var RedisClientPool *redis.Pool

func main() {
	RedisClientPool = &redis.Pool{
		MaxIdle:     10,
		MaxActive:   10,
		IdleTimeout: time.Duration(10),
		Dial: func() (redis.Conn, error) {
			c, err := redis.Dial("tcp", "127.0.0.1:6379", redis.DialPassword("xxxxxx"), redis.DialReadTimeout(time.Second), redis.DialWriteTimeout(time.Second))
			if err != nil {
				fmt.Printf("redisClient dial host: %s, auth: %s err: %s", "127.0.0.1:6379", "xxxxxx", err.Error())
				return nil, err
			}
			return c, nil
		},
		TestOnBorrow: func(c redis.Conn, t time.Time) error {
			if time.Since(t) < time.Minute {
				return nil
			}
			_, err := c.Do("PING")
			if err != nil {
				fmt.Printf("redisClient ping err: %s", err.Error())
			}
			return err
		},
	}
	fmt.Println(RedisClientPool)
	defer RedisClientPool.Close()
	conn := RedisClientPool.Get()
	defer func() {
		conn.Close()
	}()
	reply, err := conn.Do("SET", "stu:one:1", "value1")
	if err != nil {
		fmt.Printf("do set %s,%s error:%s", "1", "valuea", err.Error())
		return
	}
	fmt.Println(reply)

	reply, err = conn.Do("GET", "stu:one:1")
	if err != nil {
		fmt.Printf("do set %s,%s error:%s", "1", "valuea", err.Error())
		return
	}
	fmt.Println(reply)
}
