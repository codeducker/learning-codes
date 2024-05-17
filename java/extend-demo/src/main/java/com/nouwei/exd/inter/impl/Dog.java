package com.nouwei.exd.inter.impl;

import com.nouwei.exd.inter.Animal;

public class Dog implements Animal {
  private String name;

   public String getName(){
    return name;
  }

  public void setName(String name){
    this.name = name;
  }

  public Dog(String name){
    this.name = name;
  }

    @Override
    public String toString() {
        return super.toString();
    }
}
