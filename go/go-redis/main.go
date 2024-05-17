package main

import (
	"context"
	"fmt"

	"github.com/redis/go-redis/v9"
)

func setAndGet() {
	ctx := context.Background()

	opt, err := redis.ParseURL("redis://:@127.0.0.1:6379/0")
	if err != nil {
		panic(err)
	}

	client := redis.NewClient(opt)

	err = client.Set(ctx, "foo", "bar", 0).Err()
	if err != nil {
		panic(err)
	}

	val, err := client.Get(ctx, "foo").Result()
	if err != nil {
		panic(err)
	}
	fmt.Println("foo", val)
}

func ping() {
	client := redis.NewClient(&redis.Options{
		Addr:     "127.0.0.1:6379",
		Password: "", // no password set
		DB:       0,  // use default DB
	})
	fmt.Println(client.Ping(context.Background()).Result())
}

func parseUrl() {
	//链接url redis://username:password@host:port/db
	opt, err := redis.ParseURL("redis://:@127.0.0.1:6379/0")
	if err != nil {
		panic(err)
	}

	client := redis.NewClient(opt)
	fmt.Println(client.Ping(context.Background()).Result())
}

func hashSetOperator() {
	ctx := context.Background()
	opt, err := redis.ParseURL("redis://:@127.0.0.1:6379/0")
	if err != nil {
		panic(err)
	}
	client := redis.NewClient(opt)

	session := map[string]string{"name": "John", "surname": "Smith", "company": "Redis", "age": "29"}
	for k, v := range session {
		err := client.HSet(ctx, "user-session:123", k, v).Err()
		if err != nil {
			panic(err)
		}
	}

	userSession := client.HGetAll(ctx, "user-session:123").Val()
	fmt.Println(userSession)
}

func testCluster() {
	client := redis.NewClusterClient(&redis.ClusterOptions{
		Addrs:      []string{"127.0.0.1:6379"},
		ClientName: "test-client",
		// To route commands by latency or randomly, enable one of the following.
		//RouteByLatency: true,
		RouteRandomly: true,
	})
	fmt.Println(client.Ping(context.Background()).Result())
}

func main() {
	// ping()
	// parseUrl()
	// setAndGet()
	// hashSetOperator()
	testCluster()
}
