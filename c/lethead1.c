#include <stdio.h>
#define NAME "GIGATHINK, INC."
#define ADDRESS "101 Megabuck Plaza"
#define PLACE "Megapolis, CA 94904"
#define WIDTH 40

void starbar(void);  /* 函数原型 */
void show_n_char(char, int);

int main(void)
{
     // starbar();
    show_n_char('*',20);
     printf("%s\n", NAME);
     printf("%s\n", ADDRESS);
     printf("%s\n", PLACE);
     // starbar();       /* 使用函数 */
    show_n_char('*', 20);
     return 0;
}

void starbar(void)   /* 定义函数    */
{
     int count;

     for (count = 1; count <= WIDTH; count++)
          putchar('*');
     putchar('\n');
}

void show_n_char(char ch , int num){
  int count;
  for (count = 1; count <= num; count++) {
    putchar(ch);
  }
  putchar('\n');
}
