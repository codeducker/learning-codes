package com.loern.composite.myApp;

import com.loern.composite.myUtil.Numbers;
import com.loern.composite.myUtil.Strings;

public class Main {

    public static void main(String... args) {
        new Main().printAnswer();
    }

    public void printAnswer() {
        String output = Strings.concat(" The answer is    ", Numbers.add(19, 23));
        System.out.println(output);
    }
}