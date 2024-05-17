package com.loern.base;

import java.io.Console;
import java.nio.charset.Charset;
import java.util.Arrays;
import java.util.Scanner;

//public strictfp class Base {
public class Base {
    //strictfp 将浮点运算限制为IEEE 754 标准 ，JDK17 已支持该标准 ，关键字无效
    public static  void main(String[] args){
        int xx = 9 / 4;
        int zz = 9 % 4;
        double yy = 9 / 4.0;
        System.out.println("xx = "+xx +", yy = " + yy +", zz = "+zz);
        System.out.println(1.456736344 / 2.344);
        //0.6214745494880547
        //0.6214745494880547
        System.out.println(StrictMath.sqrt(11));
        System.out.println(Math.sqrt(11));
        
        String hel = "heloo";
        String xel = "heloo";
        System.out.println(hel == xel);
        String greeting = "hello中国";
        int i = greeting.codePointCount(0, greeting.length());
        System.out.println("i="+i);

        String maxStr = "Ƶis the set of integers";
        System.out.println(maxStr.length());
        for (byte bytes : maxStr.getBytes()) {
            System.out.print(bytes +" ");
        }
        System.out.println();
        System.out.println(maxStr.codePointAt(1));
        System.out.println(maxStr.charAt(1));//下标为index的代码单元并转化为字符
        System.out.println(maxStr.offsetByCodePoints(1, 3));

        String str = "这𝕆是一个𝕆！";
		System.out.println(str.charAt(1));
        System.out.println(str.charAt(4));
        System.out.println(str.offsetByCodePoints(1, 1));
        System.out.println(str.codePointAt(3));

        System.out.println("------");
        str = "这𝕆是一个𝕆！针不戳";
        System.out.println(str.codePointCount(0, str.length()));//获取总计代码单元
		i = str.offsetByCodePoints(2, 5);
		int cp = str.codePointAt(i);
		char[] chars = Character.toChars(cp);
		String str1 = new String(chars);
		System.out.println(str1);
        System.out.println("------");

        System.out.printf("%g",12.3);
        System.out.println();
        
        System.out.println(System.getProperty("user.dir"));
        // Scanner scanner = new Scanner(System.in);
        // String line = scanner.nextLine();
        // System.out.println(line);

        // //控制台读取密码
        // Console console = System.console();
        // char[] pass = console.readPassword();
        // System.out.println(new String(pass));
        int arr[] = {1,2,3,4};
        System.out.println(Arrays.toString(arr));
        int arr2[] = Arrays.copyOf(arr,arr.length);
        arr[1] = 4;
        System.out.println(Arrays.toString(arr2));
        System.out.println(Arrays.equals(arr,arr2));
        System.out.println(Integer.toBinaryString(21));
        System.out.println(Integer.numberOfTrailingZeros(21));//低位 0 个数
        System.out.println(Integer.numberOfLeadingZeros(21));// 高位 0 个数
    }
}