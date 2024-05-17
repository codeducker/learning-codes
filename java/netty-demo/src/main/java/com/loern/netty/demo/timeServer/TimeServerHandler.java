package com.loern.netty.demo.timeServer;

import io.netty.buffer.ByteBuf;
import io.netty.buffer.Unpooled;
import io.netty.channel.ChannelHandlerAdapter;
import io.netty.channel.ChannelHandlerContext;

import java.nio.ByteBuffer;
import java.nio.charset.StandardCharsets;
import java.text.SimpleDateFormat;
import java.util.Date;

/**
 * All rights Reserved, Designed By www.foodism.cc
 *
 * @Title: TimeServerHandler
 * @Description: TDO 描述....
 * @author: loern
 * @date: 2023/1/3 15:36
 * @Copyright: www.foodism.cc Inc. All rights reserved.
 * 注意：本内容仅限于食物主义内部传阅，禁止外泄以及用于其他的商业目
 */
public class TimeServerHandler extends ChannelHandlerAdapter {
    @Override
    public void channelRead(ChannelHandlerContext ctx, Object msg) throws Exception {
        ByteBuf byteBuf = (ByteBuf) msg;
        int i = byteBuf.readableBytes();
        byte[] req = new byte[i];
        byteBuf.readBytes(req);
        String valueMsg = new String(req, StandardCharsets.UTF_8);
        System.out.printf("The Server receive body %s",valueMsg);
        System.out.println();
        String resp  = "";
        if("OrderTime".equals(valueMsg)){
            resp = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date());
        }else{
            resp = "Bad Req";
        }
        ByteBuf respByteBuf = Unpooled.copiedBuffer(resp.getBytes());
        ctx.write(respByteBuf);
    }

    @Override
    public void channelReadComplete(ChannelHandlerContext ctx) throws Exception {
        ctx.flush();
    }

    @Override
    public void exceptionCaught(ChannelHandlerContext ctx, Throwable cause) throws Exception {
        ctx.close();
    }
}
