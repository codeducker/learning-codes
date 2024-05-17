package com.loern.listDemo;

import java.util.*;

import com.google.common.collect.Lists;
import com.google.common.collect.Maps;
import org.checkerframework.checker.nullness.qual.Nullable;

public class Main {
    public static void main(String[] args) {
        // Collection
        // RandomAccess
        Set<Integer> treeSets = new TreeSet<Integer>();
        List<Integer> list = Lists.newArrayList();
        Collections.fill(list, 1);
        for (int i = 0; i < list.size(); i++) {
            System.out.print(" "+ list.get(i));
        }
        Iterator<Integer> iterator = list.iterator();
        list.clear();
        list.addAll(Lists.newArrayList(1,2,3));
        ListIterator<Integer> listIterator = list.listIterator();
        while(listIterator.hasNext()){
            Integer next = listIterator.next();
            if(next != null && 3 == next){
                // list.add(2 * next);
            }
            System.out.println(next);
        }

        Set<String> hashSet = new HashSet<String>();
        // System.out.println(hashSet.add("hello"));
        // System.out.println(hashSet.add("hello"));
        // System.out.println(hashSet.add(null));

        HashMap<@Nullable Object, @Nullable Object> objectObjectHashMap = Maps.newHashMap();

        ListIterator<Integer> integerListIterator = list.listIterator();

        new Stack<>().forEach(
                item -> {

                }
        );

//        Iterable
    }
}