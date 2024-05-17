package com.loern.base;

import java.util.Arrays;

public class ArrayInit {
    public static void main(String[] args) {

        int[][] magicSquare = new int[2][3];
        System.out.println(Arrays.toString(magicSquare));
        for(int[] array : magicSquare){
            for(int a :array){
                System.out.print(a +" ");
            }
            System.out.println("");
        }
        System.out.println("--------------------------------");
        System.out.println(Arrays.deepToString(magicSquare));

        Integer[] a = {
                1, 2,
                3, // Autoboxing
        };
        Integer[] b = new Integer[] {
                1, 2,
                3, // Autoboxing
        };
        System.out.println(Arrays.toString(a));
        System.out.println(Arrays.toString(b));

        printArr((Object[])new Integer[]{1,2,3,4});
        
        // System.out.println(System.getProperties());
        
//        f(1, 'a');
        f('a', 'b');
    }
    
    static void f(int i ,Character... charactes){
        System.out.println("Third");
    }
    
    static void f(float i, Character... args) {
        System.out.println("first");
    }

//    static void f(Character... args) {
//        System.out.println("second");
//    }
    
    private static void printArr(Object... args){
        System.out.println(args.getClass());
        for (int i = 0; i < args.length; i++) {
            System.out.print(args[i] +" ");
        }
        System.out.println("");
    }
}