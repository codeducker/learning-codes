package com.loern.java.nio;

import java.io.IOException;
import java.net.InetSocketAddress;
import java.net.ServerSocket;
import java.nio.channels.SelectionKey;
import java.nio.channels.Selector;
import java.nio.channels.ServerSocketChannel;
import java.util.Set;

/**
 * All rights Reserved, Designed By www.foodism.cc
 *
 * @Title: DefineServerSocketChannel
 * @Description: TDO 描述....
 * @author: loern
 * @date: 2023/1/3 14:03
 * @Copyright: www.foodism.cc Inc. All rights reserved.
 * 注意：本内容仅限于食物主义内部传阅，禁止外泄以及用于其他的商业目
 */
public class DefineServerSocketChannel {
    public static void main(String[] args) {
        try {
            ServerSocketChannel serverSocketChannel = ServerSocketChannel.open();
            ServerSocket socket = serverSocketChannel.socket();
            socket.bind(InetSocketAddress.createUnresolved("127.0.0.1",80));
            serverSocketChannel.configureBlocking(false);//非阻塞
            Selector selector = Selector.open();
            serverSocketChannel.register(selector, SelectionKey.OP_ACCEPT, new Runnable() {
                @Override
                public void run() {

                }
            });
            int num = selector.select();
            Set<SelectionKey> selectionKeys = selector.selectedKeys();
            for (SelectionKey selectionKey : selectionKeys) {

            }
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }
}
