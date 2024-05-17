package com.loern.pack1;

import com.loern.pack2.Tec;

public class TestPack{
    
    public void getTec(){
        Tec tec = new Tec();
        System.out.println(tec.id);
        //tec.clone()//无法访问，编译报错
    }
    
    public static void main(String[] args){
        
    }
}