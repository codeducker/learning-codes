package com.loern.java.io.server.pool;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Objects;

/**
 * All rights Reserved, Designed By www.foodism.cc
 *
 * @Title: TimeServerHandler
 * @Description: TDO 描述....
 * @author: loern
 * @date: 2022/12/30 15:42
 * @Copyright: www.foodism.cc Inc. All rights reserved.
 * 注意：本内容仅限于食物主义内部传阅，禁止外泄以及用于其他的商业目
 */
public class TimeServerHandler implements Runnable{
    private Socket socket = null;

    private TimeServerHandler(){}

    public TimeServerHandler(Socket socket) {
        this.socket = socket;
    }

    @Override
    public void run() {
        BufferedReader reader = null;
        InputStreamReader inputStreamReader = null;
        PrintWriter printWriter = null;
        try {
            inputStreamReader = new InputStreamReader(this.socket.getInputStream());
            reader = new BufferedReader(inputStreamReader);
            printWriter = new PrintWriter(this.socket.getOutputStream(),true);
            String body = null;
            body = reader.readLine();
            if(!Objects.isNull(body) && body.equals("This Time")){
                printWriter.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date()));
            }else{
                printWriter.println("Illegal Request");
            }
            System.out.println("Current Body: "+ body);
        }catch (Exception e){
            e.printStackTrace();
        }finally {
            if(!Objects.isNull(inputStreamReader)){
                try {
                    inputStreamReader.close();
                } catch (IOException e) {
                    throw new RuntimeException(e);
                }
            }
            if(!Objects.isNull(reader)){
                try {
                    reader.close();
                } catch (IOException e) {
                    throw new RuntimeException(e);
                }
            }
            if(!Objects.isNull(printWriter)){
                printWriter.flush();
                printWriter.close();
            }
            if(!Objects.isNull(socket)){
                try {
                    socket.close();
                } catch (IOException e) {
                    throw new RuntimeException(e);
                }
                socket = null;
            }
        }
    }
}
