package com.loern.object;

public class Test {

    public static void invoke(Shape shape) {
        System.out.println(shape.toString());
    }

    public static void main(String[] args) {

        Square square = new Square();
        System.out.println(square.toString());


        Circle circle = new Circle();
        System.out.println(circle.getRadix());//此时对象创建时，基础类型数据会赋默认值
        invoke(circle);//向上转型
        int date = 4;
        change(date);
        System.out.println(date);

//        int x = 20;
//        {
//            int x = 30;
//        }
//        int c;
//        System.out.printf("c="+c);//局部变量不会赋默认值
    }

    public static void change(int i){
        i =234;
    }

}