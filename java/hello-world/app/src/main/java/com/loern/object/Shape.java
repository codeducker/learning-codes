package com.loern.object;

public class Shape {
    public static int lx =  0;

    static {
        System.out.println("父类静态代码块");
    }

    {
        System.out.println("父类代码块");
    }

    public Shape(){
        System.out.println("父类无参构造函数");
    }

    public Shape(int max){
        System.out.println("父类有参构造函数");
    }
}   