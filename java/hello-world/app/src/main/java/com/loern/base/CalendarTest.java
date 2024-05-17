package com.loern.base;

import java.text.DateFormatSymbols;
import java.text.SimpleDateFormat;
import java.util.Arrays;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.Locale;


public class CalendarTest {
    enum WeekDay {
        Sun,
        Mon,
        Tue,
        Wed,
        Thu,
        Fri,
        Sat
    }

    private static void printMonthDays(){  
        Calendar calendar = Calendar.getInstance();
        // calendar.set(Calendar.DAY_OF_MONTH,12);
        // calendar.set(Calendar.MONTH, 1);
        System.out.println(new SimpleDateFormat("yyyy-MM-dd").format(calendar.getTime()));
        System.out.println("----------------------------------------------------------------");
        System.out.println();

        for (WeekDay values : WeekDay.values()) {
            System.out.print("\t" + values.toString());
        }
        System.out.println();
        int day = calendar.get(Calendar.DAY_OF_MONTH);
       
        int month = calendar.get(Calendar.MONTH);
        calendar.set(Calendar.DAY_OF_MONTH, 1);
        int firstDayOfWeek = calendar.get(Calendar.DAY_OF_WEEK);
        int positions = firstDayOfWeek;
        //从第一个日期开始，填充空白
        for(int i = 0; i < positions-1; i++){
            System.out.print("\t");
        }
        while( calendar.get(Calendar.MONTH) == month){
            positions ++;
            System.out.print("\t"+calendar.get(Calendar.DAY_OF_MONTH)+(day == calendar.get(Calendar.DAY_OF_MONTH) ? "*" : ""));
            calendar.add(Calendar.DAY_OF_MONTH,1); 
            // System.out.println("++"+positions);
            if( (positions-1) % 7 == 0){
                System.out.println("");
            }
        }
        System.out.println();
    }


    public static void main(String[] args) {
        // Calendar calendar = Calendar.getInstance();
        // GregorianCalendar gregorianCalendar = new GregorianCalendar(0, 0, 0);
        // printMonthDays();
        System.out.println(Arrays.toString(new DateFormatSymbols(Locale.ENGLISH).getShortWeekdays()));
    }
}
