package com.loern.pack2;

public class SelfObject {

    protected Object clone() throws CloneNotSupportedException{
        return super.clone();
    }
}