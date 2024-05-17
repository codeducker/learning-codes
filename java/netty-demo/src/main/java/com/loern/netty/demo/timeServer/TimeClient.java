package com.loern.netty.demo.timeServer;

import io.netty.bootstrap.Bootstrap;
import io.netty.channel.ChannelFuture;
import io.netty.channel.ChannelInitializer;
import io.netty.channel.ChannelOption;
import io.netty.channel.EventLoopGroup;
import io.netty.channel.nio.NioEventLoopGroup;
import io.netty.channel.socket.SocketChannel;
import io.netty.channel.socket.nio.NioSocketChannel;

import java.net.InetAddress;

/**
 * All rights Reserved, Designed By www.foodism.cc
 *
 * @Title: TimeClient
 * @Description: TDO 描述....
 * @author: loern
 * @date: 2023/1/3 15:45
 * @Copyright: www.foodism.cc Inc. All rights reserved.
 * 注意：本内容仅限于食物主义内部传阅，禁止外泄以及用于其他的商业目
 */
public class TimeClient {
    public static void main(String[] args) {
        EventLoopGroup clientGroup = new NioEventLoopGroup();
        try{
            Bootstrap bootstrap = new Bootstrap();
            bootstrap.group(clientGroup)
                    .channel(NioSocketChannel.class)
                    .handler(new ChannelInitializer<SocketChannel>() {
                        @Override
                        protected void initChannel(SocketChannel ch) throws Exception {
                            ch.pipeline().addLast(new TimeClientHandler("OrderTime"));
                        }
                    });
            ChannelFuture channelFuture = bootstrap.connect(InetAddress.getLocalHost(),80);
            channelFuture.channel().closeFuture().sync();
        }catch (Exception e){
            e.printStackTrace();
        }finally {
            clientGroup.shutdownGracefully();
        }
    }
}
