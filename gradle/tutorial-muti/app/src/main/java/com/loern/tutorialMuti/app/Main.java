package com.loern.tutorialMuti.app;

import com.loern.tutorialMuti.lib.Standard;

public class Main{

    private static String greeting(){
        return Standard.GET_WORDS;
    }

    public static void main(String[] args){
        System.out.println("Word from "+ greeting());
    }
}
