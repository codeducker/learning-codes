package com.loern.pack2;

public class Tec {
    public long id;
    private String name;
    protected int age;
    byte sex;
    
    public void MangaeStu(){
        Stu stu = new Stu();
        System.out.println(stu.nickname + stu.score);
        try{
            this.clone();//本类实例可以调用clone()
        }catch (Exception e){
            e.printStackTrace();
        }
    }
//    
//    public static void main(String\u005B\u005D args){符合语法规则
//        System.out.println("Unicode");
//    }
}