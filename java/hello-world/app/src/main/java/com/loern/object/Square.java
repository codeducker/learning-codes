package com.loern.object;

public class Square extends Shape{
    public static int max = 0;

    int retangle;

    static {
        System.out.println("子类静态方法块");
    }

    {
        System.out.println("子类代码块");
    }

    public Square(){
        System.out.println("子类构造函数");
    }

    public Square(int retangle){
        System.out.println("子类有参构造函数");
    }
    
}