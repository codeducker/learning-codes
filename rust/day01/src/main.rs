//let mut x : i32 = 100;
//println!("{}",x);
//x = 3;
//println!("{}",x);
//let b = 3;
//println!("{}",b);
//let (mut a, mut b ) = (1,2);
//println!("{:#?}" ,(a,b));
//a = 4;
//b= 5;
//println!("{:#?}" ,(a,b));

//fn test(condition:bool){
//    let x : i32;
//    if condition {
//        x = 6;
//    }else{
//        x = 5;
//    }
//    println!("{}",x)
//}

//    test(true);
//    let x = 4;
//    println!("{}",x);
//    let x = 5;
//    println!("{}",x);
//    let x = Vec::new();
//    let mut x= x;
//    x.push(1);
//    x.push(2);
//    for v in &x {
//        println!("{}",v)
//    }

//    let player_scores = [("Jack", 20), ("Jane", 23), ("Jill", 18), ("John", 19)];
//// II players 是动态数组 ， 内部成员的 类型没有指定 ， 交给编译器自动推导
//    let players: Vec<_> = player_scores
//        .iter()
//        .map(|&(player, _scores) | player)
//        .collect();
//println!("{:?}", players);

//type Age = u32;
//
//fn grow(age : Age, low : u32)-> Age {
//    return age + low;
//}

type Double<T> = (T, Vec<T>); //II 小括号包围的是一个 tuple， 请参见后文中的复合数据类型

fn main() {
//    println!("{}",grow(1,2));
    static GROBAL : i32 = 0;
    println!("{}",GROBAL);
    static B : Double<u32> = (1, Vec::new());
    println!("{:?}",B);
    static mut C : i32 = 4;
    unsafe {
        C =5;
        println!("{}",C);
    }
    static D : [i32;3]= [1,2,3];
    println!("{:?}",D);
    static VEC :Vec<i32> ={ let v = Vec::new(); v};
    println!("{:?}",VEC);
    let println : i32 =4;
    // println = 3;
    println!("{:?}",println);
}
