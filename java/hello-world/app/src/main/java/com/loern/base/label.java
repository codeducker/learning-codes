package com.loern.base;

public class label {
    
    public static void main(String[] args){
      
        label1:
        for(int i = 0; i < 15; i++) {
            for(int j = 0; j < 20; j++) {
                System.out.println("i:="+i+" , j = "+j);
                if(j > 10){
                    System.out.println("break...");
                    break;
                }
                if(j < 5) {
                    System.out.println("continue...");
                    continue ;
                }
                if(j + i < 6 ) {
                    System.out.println("continue label ...");
                    continue label1; //中断内部循环，继续外部循环
                }
                if(i  < j ) {
                    System.out.println("break label...");
                    break label1;//中断循环，并且不重新进入循环(中断循环)
                }
            }
        }
        
        
        int i = 0;
        outer: 
        for(; true ;) { // 无限循环
            inner: 
              for(; i < 10; i++) {
                  System.out.println("i = " + i);
                  if(i == 2) {
                      System.out.println("continue");
                      continue;
                  }
                  if(i == 3) {
                      System.out.println("break");
                      i++; // 否则 i 永远无法获得自增 
                      // 获得自增 
                      break;
                  }
                  if(i == 7) {
                      System.out.println("continue outer");
                      i++;  // 否则 i 永远无法获得自增 
                      // 获得自增 
                      continue outer;
                  }
                  if(i == 8) {
                      System.out.println("break outer");
                      break outer;
                  }
                  for(int k = 0; k < 5; k++) {
                      if(k == 3) {
                          System.out.println("continue inner");
                          continue inner;
                      }
                  }
              }
        }
    }
}