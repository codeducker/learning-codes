package com.nouwei.exd;

public class ObjectMain {
    public static void main(String[] args){
          Object obj1 = new Object();
          System.out.println(obj1.toString());
          System.out.println(obj1.hashCode());
          Object obj2 = obj1;
          System.out.println(obj1.equals(obj2));
          System.out.println(obj1.getClass());
          obj2.clone
    }
}