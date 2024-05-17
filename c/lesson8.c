#include <stdio.h>
#include <stdlib.h>
#include <ctype.h>
#include <math.h>
#include <stdbool.h>

void count() {
    FILE * file;
    file = fopen("C:\\Users\\PCSW015--PC\\Desktop\\easy","r");
    if (file == NULL) {
        printf("File Not Exists");
    }
    int c;
    int n =0;
    while(c != EOF) {
        c = fgetc(file);
        n++;
    }
    fclose(file);
    printf("%d",n);

    file = fopen("C:\\Users\\PCSW015--PC\\Desktop\\easyct","wb");
    if (file == NULL) {
        printf("Illegal File");
    }
    fprintf(file,"%d",n);
    // fputc(n,file);
    fclose(file);
}
void question2() {
    int i = 1;
    int ch ;
    while((ch = getchar()) != EOF) {
        if(i++ ==10) {
            i = 1;
            putchar('\n');
        }
        if(ch == '\t') {
            printf("\\t - \\t ");
        }else if(ch == '\n') {
            printf("\\n - \\n ");
            i = 0;
        }else if(ch >= 32) {
            printf("%c - %3d ", ch, ch);
        }else {
            printf("%c - %3d ", ch, ch+64);
        }
    }
}


void question3() {
    int ch;
    int cu = 0;
    int cl = 0;
    int co = 0;
    printf("Please input some characters!");
    while((ch = getchar()) != EOF) {
        if(isupper(ch)) {
            cu ++;
        }else if(islower(ch)) {
            cl ++;
        }else {
            co ++;
        }
    }
    printf("upper %d",cu);
    printf("lower %d",cl);
    printf("other %d",co);
}

void question4() {
    bool inWords = false;
    int ch , words = 0, letters = 0;
    printf("Please Input Some Words(EOF TO QUIT)!\n");
    while((ch = getchar()) != '#') {
        if(ispunct(ch)) continue;//标点符号忽略
        if(isalpha(ch)) letters++;//字母作为单词累计
        if(!isspace(ch) && !inWords) {
            //当前非空格切标志为为单词
            words++;
            inWords = true;
        }
        if(isspace(ch) && inWords) {
            inWords = false;
        }
    }
    printf("Words :  %d\n",words);
    printf("Letters :  %d\n",letters);
    printf("Avg Letters : %d", letters / words);
}
//helllo world firsty maker

void question41() {
    int ch ,letters,words = 0;
    int lastCh = -1;
    while((ch = getchar()) != '#') {
        if(ispunct(ch)) continue;
        if(isalpha(ch)) letters++;//只要是的字母就累加
        if(-1 == lastCh && !isalpha(ch)) {
            words++;
        }else if(isalpha(lastCh) && !isalpha(ch)){
            words++;
        }
        lastCh = ch;
    }
    printf("Words :  %d\n",words);
    printf("Letters :  %d\n",letters);
    printf("Avg Letters : %d", letters / words);
}
//leteter‘ like most's hobby #

int question5(void)
{
    int guess = 1;

    printf("Pick an integer from 1 to 100. I will try to guess ");
    printf("it.\nRespond with a y if my guess is right and with");
    printf("\nan n if it is wrong.\n");
    printf("Uh...is your number %d?\n", guess);
    while (getchar() != 'y')      /* 获取响应，与 y 做对比 */
        printf("Well, then, is it %d?\n", ++guess);
    printf("I knew I could do it!\n");

    return 0;
}

int main(void) {
    // setbuf(stdout,NULL);
    // printf("%f",0x1.Fp+0);
    //1. putchar(getchar()) 有效表达式 ，主要作用 讲输入字符打印出来 , getchar(putchar());//非有效表达式
    //2.
    // putchar('H');
    // putchar('\n');
    // putchar('\b');
    // putchar("\007");
    // putchar("\0000");
    //3.
    // count();
    //4.
    // b
    // question2();
    // question3();
    // question4();
    // question41();
     //printf("%s","hello world");
    question5();
}
