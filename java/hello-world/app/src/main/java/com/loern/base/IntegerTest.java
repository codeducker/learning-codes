package com.loern.base;

public class IntegerTest {
    public static void main(String[] args) {
        int num1 = 16; // 00010000
        int num2 = 20; // 00010100
        int num3 = 128;// 10000000

        int zeros1 = Integer.numberOfTrailingZeros(num1);
        int zeros2 = Integer.numberOfTrailingZeros(num2);
        int zeros3 = Integer.numberOfTrailingZeros(num3);

        //低位 0 个数
        System.out.println("Number of trailing zeros in " + num1 + " : " + zeros1);
        System.out.println("Number of trailing zeros in " + num2 + " : " + zeros2);
        System.out.println("Number of trailing zeros in " + num3 + " : " + zeros3);

        zeros1 = Integer.numberOfLeadingZeros(num1);
        zeros2 = Integer.numberOfLeadingZeros(num2);
        zeros3 = Integer.numberOfLeadingZeros(num3);

        //高位 0 个数 (Integer.size )默认占用位数 - Integer.toBinaryString(i) 无符号位数
        System.out.println("Number of leading zeros in " + num1 + " : " + zeros1);
        System.out.println("Number of leading zeros in " + num2 + " : " + zeros2);
        System.out.println("Number of leading zeros in " + num3 + " : " + zeros3);

        System.out.println(Integer.toBinaryString(-num1));
        System.out.println(Integer.SIZE - Integer.toBinaryString(-num1).length());
        System.out.println(Integer.SIZE - Integer.toBinaryString(num2).length());
        System.out.println(Integer.SIZE - Integer.toBinaryString(num3).length());
    }
}
