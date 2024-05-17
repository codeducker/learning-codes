package com.loern.java.io.server.single;

import java.io.*;
import java.net.Socket;
import java.util.Objects;
import java.util.Scanner;

/**
 * All rights Reserved, Designed By www.foodism.cc
 *
 * @Title: TimeClient
 * @Description: TDO 描述....
 * @author: loern
 * @date: 2022/12/30 16:08
 * @Copyright: www.foodism.cc Inc. All rights reserved.
 * 注意：本内容仅限于食物主义内部传阅，禁止外泄以及用于其他的商业目
 */
public class TimeClient {
    public static void main(String[] args) {
        BufferedReader reader = null;
        Socket socket = null;
        PrintWriter printWriter = null;
        Scanner scanner = null;
        try {
            socket = new Socket("127.0.0.1",8080);
            OutputStream outputStream = socket.getOutputStream();
            printWriter = new PrintWriter(outputStream, true);
            reader = new BufferedReader(new InputStreamReader(socket.getInputStream()));
            scanner = new Scanner(System.in);
            while(scanner.hasNext()){
                String line = scanner.nextLine();
                printWriter.write(line);
                if("exit".equals(line)) {
                    printWriter.write("");
                    break;
                }
            }
            String resp = reader.readLine();
            System.out.println(resp);
        } catch (IOException e) {
            e.printStackTrace();
        }finally {
            if(!Objects.isNull(reader)){
                try {
                    reader.close();
                } catch (IOException e) {
                    throw new RuntimeException(e);
                }
            }
            if(!Objects.isNull(socket)){
                try {
                    socket.close();
                } catch (IOException e) {
                    throw new RuntimeException(e);
                }
                socket = null;
            }
            if(!Objects.isNull(scanner)){
                scanner.close();
                scanner = null;
            }
        }
    }
}
