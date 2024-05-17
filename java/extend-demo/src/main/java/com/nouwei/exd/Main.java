package com.nouwei.exd;

import com.nouwei.exd.inter.*;
import com.nouwei.exd.inter.impl.*;

import java.time.Instant;
import java.time.LocalDateTime;
import java.time.ZoneId;
import java.util.*;
import java.util.stream.Collectors;

public class Main{


        public static Integer compute(String st){
                return st.length();
        }

        public static String compute(Object o){
                return "Nohp";
        }

    int i ;
    public static void main(String[] args) throws Exception{
            Object lock = "luck";
            System.out.println(compute(lock));

//        for(int x = 0; x <10 ; x++){
//            for(int i = 0; i < x; i++) {
//                System.out.println("j="+x);
//                if(i == 5) {
//                    break exit;
//                }
//            }
//            exit: System.out.println("i="+x);
//        }

        long beginStart =   1706716800L;
        Instant instant = Instant.ofEpochMilli(beginStart * 1000);

        boolean flag = LocalDateTime.ofInstant(instant, ZoneId.systemDefault()).isAfter(LocalDateTime.now());
        System.out.println(flag);

        Dog dog = new Dog("lucky");
        Dog dog2  = new Dog("lucky");
        System.out.println(dog == dog2);
//        System.out.println(5.toString());

        List<Dog> dogs = new ArrayList<>();
        dogs.add(dog);
        dogs.add(dog2);
        System.out.println(
                dogs.stream()
        .map(p->p.getName()).collect(Collectors.joining()));

        try{
            boolean i1 = Main.class.getDeclaredField("i").getType() == int.class;
            System.out.println(i1);
        }catch(Exception e){
            e.printStackTrace();
        }

//        List<Animal> animals = new ArrayList<Dog>();
//         Animal[] animals = new Dog[10];
        // animals[0] = new Dog();
        // animals[1] = new Cat();

       // int[][] ab = new int[2][3];
        // for (int i = 0; i < ab.length; i++) {
            // for (int j = 0; j < ab[i].length; j++) {
                // System.out.print(ab[i][j]);
            // }
            // System.out.println("");
        // }

        // GetAnimal(new ArrayList<Dog>(0));
    }

    public static void GetAnimal(List<? extends Animal> animals){
        //animals.contains(new Dog());
        //animals.remove(new Cat());
        // animals.add(0, new Dog());
    }
}
