fn main() { 
  let x :[i32;6] = [1,2,3,4,5,6];
  for i in x.into_iter() {
    println!("{}",i);
  }
  let arr = [1, 2, 3];
  let mut arr_iter = arr.into_iter();

  assert_eq!(arr_iter.next(), Some(1));
  assert_eq!(arr_iter.next(), Some(2));
  assert_eq!(arr_iter.next(), Some(3));
  assert_eq!(arr_iter.next(), None);
    // let mut number = 3; 
 
    // while number != 0 { 
    //     println!("{}!", number); 
 
    //     number = number - 1; 
    // } 
 
    // println!("LIFTOFF!!!"); 
  // let y = {
  //     let x :i32 = 8;
  //     x+1
  // };
  // println!("{}",y);
  // let tup :(i32,u32,char,bool) = (1,255,'c',false);
  // println!("{:#?}",tup);
  // let (x,y,z,f) = tup;
  // println!("{},{},{},{}",x,y,z,f);
  // let array :[i32;2]= [1,2];
  // println!("{:#?}",array);
  // println!("{}",array[3]);
  // let x = 1_00;
  // let y = 100;
  // println!("{}",x==y);
  // let z:u8 = 234;
  // let m :u8 = z+1;
  // println!("{},{}",z,m);
  // let bol:bool = false;
  // println!("{}",bol);
  // let ch :char = 'c';
  // println!("{}",ch);
  // println!("{}",&ch);
  
  // let guess = "42".parse().expect("请输入数字");
  // let mut s = String::from("Hello World");
  // let x = second_word(&s);
  // s.clear();
  // println!("{}",x);
  // let x = 5;
  // let y = 5.4;
  // let z = 5.7;
  // println!("{}",x+y as i32);
  // println!("{}",x+z as i32);
}

// fn second_word(s: &String) -> usize {
//     let bytes = s.as_bytes();

//     for (i, &item) in bytes.iter().enumerate() {
//         if item == b' ' {
//             return i;
//         }
//     }
//     s.len()
// }

// fn first_word(s: &String) -> &str {
//     let bytes = s.as_bytes();

//     for (i, &item) in bytes.iter().enumerate() {
//         if item == b' ' {
//             return &s[0..i];
//         }
//     }

//     &s[..]
// }

// fn change(s:&mut String){
//     s.push_str(",word");
// }

  // let mut s = String::from("hello");
    
    // let r1 = &s; // 没问题
    // let r2 = &s; // 没问题
    // println!("{} and {}", r1, r2);
    // // 此位置之后 r1 和 r2 不再使用

    // let r3 = &mut s; // 没问题
    // // println!("{}", r3);
    // println!("{}", r3);


// let x = 5;
// let y = x;

// println!("x = {}, y = {}", x, y);

// let s1 = String::from("hello");
// let s2 = s1.clone();

// println!("s1 = {}, s2 = {}", s1, s2);
// // let s1 = String::from("hello");
// // let s2 = s1;

// // println!("{}, world!", s1);
// // println!("{}, world!", s2);

// let x:[i32;5] = [1,2,3,4,5];
// let mut y = 0;
// while y < 5 {
//     println!("x element :{}", x[y]);
//     y += 1;
// }

// for element in x{
//     println!("x2 element :{}", element);
// }

// // other();
// // let x = 1;
// // say_hello(x);
// // println!("{}",x);
// // let x = 2;
// //  return_call(x);
// // println!("{}",x);

// // let y = loop {
// //     if x  > 3 {
// //        break x -3
// //     }else{
// //         break x +3
// //     }
// // };
// // println!("{}",y);

// fn return_call(x : i32)->i32 {
//     return x+3;
// }

// fn say_hello(mut x : i32){
//     x= x+1;
//     println!("say_hello...{}",x);
// }

// fn other() {
//     let x = {
//         let y =1 ;
//         y+1
//     };
//     println!("{}", x);   
// }