package com.loern.base;

import java.util.ArrayList;

// operators/CastingNumbers.java
// 尝试转换 float 和 double 型数据为整型数据
public class CastingNumbers {
    public static void main(String[] args) {
        double above = 0.7, below = 0.4;
        float fabove = 0.7f, fbelow = 0.4f;
        System.out.println("(int)above: " + (int)above);
        System.out.println("(int)below: " + (int)below);
        System.out.println("(int)fabove: " + (int)fabove);
        System.out.println("(int)fbelow: " + (int)fbelow);
        
        //四舍五入
        System.out.println(
                "Math.round(above): " + Math.round(above));
        System.out.println(
                "Math.round(below): " + Math.round(below));
        System.out.println(
                "Math.round(fabove): " + Math.round(fabove));
        System.out.println(
                "Math.round(fbelow): " + Math.round(fbelow));
        
        int a = Integer.MAX_VALUE;
        int c = 4 * a ;
        System.out.println(a );
        System.out.println(c);
    }
}