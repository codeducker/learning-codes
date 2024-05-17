// 1.编写一个程序读取输入，读到#字符停止，然后报告读取的空格数、换行符数和所有其他字符的数量
#include <stdio.h>
int main(void){
    int ch;
    int lines =0 , spaces =0, chars =0;
    while((ch = getchar()) != '#'){
        if (ch == ' '){
            spaces++;
        }else if(ch == '\n'){
            lines++;
        }
        chars++;
    }
    printf("%d, %d ,%d",lines, spaces ,chars);
    return 0;
}