fn main() {
    //    let x= [1,2,3,4];
    //    println!("{:?}", x);
    //    let x = [3;5];
    //    println!("{:?}", x);
    //    println!("{:?}", x[0]);
    let x = "hello \
    world !
    say ni hao";
    let records = x.lines();
    for (i, record) in records.enumerate() {
        println!("{},{}", i, record)
    }
    println!("{} of {:b} people know binary, the other half don't", 1, 2);
    println!("{subject}", subject = 1)

    /*
     * 块注释
     */
    // println!("{:?}", x[6]);
    // let x:[i32;5] = [1,2,3];//报错 元素数量不一致
    // println!("{:?}", x);
}
