package com.loern.pack1;

import com.loern.pack2.SelfObject;

public class TestSelfObject extends SelfObject{
    
    Integer x ;

    public class Loader{

    }

    public static class InnerTest{

    }
    private static int i = 3;

    public static int i(){
        return i;
    }

    public static void main(String[] args) throws Exception{
        TestSelfObject.Loader loader = new TestSelfObject().new Loader();//通过外部类实例创建内部类实例
        System.out.println(loader.toString());
        SelfObject so = new SelfObject();
//        so.clone();
        TestSelfObject tso = new TestSelfObject();
//        tso.clone();
        System.out.println(i());
        TestSelfObject ts = new TestSelfObject();
        System.out.println(i());
        //创建静态内部类
        InnerTest it = new InnerTest();
        TestSelfObject.InnerTest it2 = new TestSelfObject.InnerTest();
        System.out.println(it.toString());

        System.getProperties().list(System.out);
        //Integer 存放在IntegerCache (low = -128 h = 127 (可配置 ))
        Integer max = 24;
        Integer max2 = 24;
        System.out.println(max == max2);
        Integer low = 128;
        Integer low2 = 128;
        System.out.println(low == low2);
    }

}