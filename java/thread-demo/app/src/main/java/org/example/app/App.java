package org.example.app;

import java.util.concurrent.Callable;
import java.util.concurrent.FutureTask;

public class App {
    public static void main(String[] args) {
        //通过Runnable接口实现
        Thread thread = new Thread(new App().new IRunnable());
        thread.start();
        //通过Callable接口实现
        ICallable iCallable = new App().new ICallable();
        FutureTask<Integer> fetureTask = new FutureTask<Integer>(iCallable);
        Thread thread2 = new Thread(fetureTask);
        thread2.start();

        Thread thread3 = new App().new ExThread();
        thread3.start();
    }

    // static class IRunnable implements Runnable {

    //     @Override
    //     public void run() {
    //         System.out.println("----IRunnable Run ----");
    //     }
        
    // }
    class IRunnable implements Runnable {

        @Override
        public void run() {
            System.out.println("----IRunnable Run ----");
        }
        
    }

    class ICallable implements Callable<Integer> {

        @Override
        public Integer call() throws Exception {
            System.out.println("----ICallable Run ----");
            return Integer.MIN_VALUE;
        }
        
    }

    class ExThread extends Thread {
        @Override
        public void run() {
            System.out.println("----ExThread Run ----");
        }
    }
}
// class IRunnable implements Runnable {

//     @Overrid
//     public void run() {
//         System.out.println("----IRunnable Run ----");
//     }
    
// }