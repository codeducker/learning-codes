#include <stdio.h>
#include <stdbool.h>
#include <ctype.h>
#define STOP '|'

int main(void){
    int lines = 0; 
    int words = 0;
    char prev = '\n';
    bool isWord = false;
    int notLines = 0;
    char ch;
    long charactors = 0;
    printf("请输入字符串:\n");
    while((ch = getchar()) != STOP){
        charactors++;
        if(ch == '\n'){
            lines++;
        }
        if (!isspace(ch) && !isWord){
            //非空格 且前一位是非单词
            isWord = true;
        }
        if(isspace(ch) && isWord){
            prev = ch;
            words++;
        }
    }
    if(prev != '\n'){
        notLines++;
    }
    printf("charactors: %ld , lines: %d, words: %d, notLines: %d\n", charactors,lines,words,notLines);
    return 0;
}