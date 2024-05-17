package com.loern.netty.demo.timeServer;

import io.netty.channel.ChannelInitializer;
import io.netty.channel.socket.SocketChannel;

/**
 * All rights Reserved, Designed By www.foodism.cc
 *
 * @Title: ChildChannelHandler
 * @Description: TDO 描述....
 * @author: loern
 * @date: 2023/1/3 15:29
 * @Copyright: www.foodism.cc Inc. All rights reserved.
 * 注意：本内容仅限于食物主义内部传阅，禁止外泄以及用于其他的商业目
 */
public class ChildChannelHandler extends ChannelInitializer<SocketChannel> {
    @Override
    protected void initChannel(SocketChannel ch) throws Exception {
        ch.pipeline().addLast(new TimeServerHandler());
    }
}
