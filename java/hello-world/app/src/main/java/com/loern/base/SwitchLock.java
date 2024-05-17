package com.loern.base;

class CloseLock{
    int check = 0;

    public CloseLock(int check){
        this.check = check;
    }


    @Override
    protected void finalize() throws Throwable{
        if(check > 1){
            System.out.println("...finalize...");
        }
        throw new RuntimeException("RuntimeException...");
    }
    
}

public class SwitchLock {
  
    public static void main(String[] args) throws Throwable{
        for(int i = 0; i < 100; i++) {
            new CloseLock(2);
        }
       
//        switchLock.finalize();
//        System.gc();
        System.out.println("End....");
        Thread.sleep(1000);
    }
}