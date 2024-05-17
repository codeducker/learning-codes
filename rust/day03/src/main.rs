use rand::Rng;
use std::cmp::Ordering;
use std::io;

fn main() {
    let input = rand::thread_rng().gen_range(1..101);

    println!("当前随机数为:{}", input);

    loop {
        println!("请输入一个数字");

        let mut guess: String = String::new();

        io::stdin().read_line(&mut guess).expect("请输入一个数字");
        // std::io::stdin().read_line(&mut guess).unwrap();
        println!("当前输入数字:{}", guess);

        let guess: i32 = match guess.trim().parse() {
            Ok(num) => num,
            Err(_) => continue,
        };

        match guess.cmp(&input) {
            Ordering::Less => println!("太小"),
            Ordering::Equal => {
                println!("正确");
                break;
            }
            Ordering::Greater => println!("太大"),
        }
    }
}
