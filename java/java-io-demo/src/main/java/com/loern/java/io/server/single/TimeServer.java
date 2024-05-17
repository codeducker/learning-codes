package com.loern.java.io.server.single;

import cn.hutool.core.util.ArrayUtil;

import java.net.ServerSocket;
import java.net.Socket;
import java.util.Objects;

/**
 * All rights Reserved, Designed By www.foodism.cc
 *
 * @Title: TimeServer
 * @Description: TDO 描述....
 * @author: loern
 * @date: 2022/12/30 15:32
 * @Copyright: www.foodism.cc Inc. All rights reserved.
 * 注意：本内容仅限于食物主义内部传阅，禁止外泄以及用于其他的商业目
 */
public class TimeServer {
    public static void main(String[] args) throws Exception {
        int port = 8080;
        if(ArrayUtil.isNotEmpty(args) && !Objects.isNull(args[0])){
            port = Integer.parseInt(args[0]);
        }
        ServerSocket serverSocket = null;
        try {
            serverSocket = new ServerSocket(port);
            System.out.println("Server Socket Start In Port : " + port);
            while (true) {
                Socket socket = serverSocket.accept();
                new Thread(new TimeServerHandler(socket)).start();
            }
        }finally {
            if(!Objects.isNull(serverSocket)){
                serverSocket.close();
                serverSocket = null;
            }
            System.out.println(
                    "Server Socket Stop"
            );
        }
    }

}
