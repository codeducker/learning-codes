package com.loern.base;

public class Main {
    public static void main(String[] args) {
        System.out.println(Double.valueOf(1/0.0)); //上述表示无穷
//        Double.valueOf(1/0); //这个会报错
        System.out.println("We will not use 'Hello World'!");
        
        if(Double.isNaN(0.0 / 0.0)){
            System.out.println("This is NaN");
        }
        Character c = 'c';
    }
}