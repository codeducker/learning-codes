package com.loern.netty.demo.timeServer;

import io.netty.buffer.ByteBuf;
import io.netty.buffer.Unpooled;
import io.netty.channel.ChannelHandlerAdapter;
import io.netty.channel.ChannelHandlerContext;

import java.nio.charset.StandardCharsets;

/**
 * All rights Reserved, Designed By www.foodism.cc
 *
 * @Title: TimeClientHandler
 * @Description: TDO 描述....
 * @author: loern
 * @date: 2023/1/3 15:49
 * @Copyright: www.foodism.cc Inc. All rights reserved.
 * 注意：本内容仅限于食物主义内部传阅，禁止外泄以及用于其他的商业目
 */
public class TimeClientHandler extends ChannelHandlerAdapter {
    private final ByteBuf firstByteBuf;

    public TimeClientHandler(String message) {
        byte[] bytes = message.getBytes();
        firstByteBuf = Unpooled.buffer(bytes.length);
        firstByteBuf.writeBytes(bytes);
    }

    @Override
    public void channelActive(ChannelHandlerContext ctx) throws Exception {
        ctx.writeAndFlush(firstByteBuf);
    }

    @Override
    public void channelRead(ChannelHandlerContext ctx, Object msg) throws Exception {
        ByteBuf byteBuf = (ByteBuf) msg;
        int i = byteBuf.readableBytes();
        byte[] req = new byte[i];
        byteBuf.readBytes(req);
        String valueMsg = new String(req, StandardCharsets.UTF_8);
        System.out.printf("The Client receive body %s",valueMsg);
        System.out.println();
    }

    @Override
    public void exceptionCaught(ChannelHandlerContext ctx, Throwable cause) throws Exception {
        cause.printStackTrace();
        ctx.close();
    }
}
