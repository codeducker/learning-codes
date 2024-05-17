package com.loern.base;

import java.util.Date;

public class Flower {
    
    public static final double CR_PER_MIN = 12.4;
    int petalCount = 0;
    String s = "initial value";

    private Date hireDay;

    public Date getHireDay() {
        // return hireDay;//不建议这样返回,破坏封装性
        return (Date)hireDay.clone();
    }

    private double lockAge;

    public double getLockAge() {
        return lockAge;
    }

    Flower(double lockAgex){
        double lockAge = lockAgex;
        System.out.println(lockAge);
        System.out.println(getLockAge());
    }

    Flower(int petals) {
        petalCount = petals;
        System.out.println("Constructor w/ int arg only, petalCount = " + petalCount);
    }

    Flower(String ss) {
        System.out.println("Constructor w/ string arg only, s = " + ss);
        s = ss;
    }

    Flower(String s, int petals) {
        this(petals);
        //- this(s); // Can't call two!
        this.s = s; // Another use of "this"
        System.out.println("String & int args");
    }

    Flower() {
        this("hi", 47);
        System.out.println("no-arg constructor");
    }

    void printPetalCount() {
        //- this(11); // Not inside constructor!
        System.out.println("petalCount = " + petalCount + " s = " + s);
    }

    public static void main(String[] args) {
        Flower x = new Flower();
        x.printPetalCount();
    }
}