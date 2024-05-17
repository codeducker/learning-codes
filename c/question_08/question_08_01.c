#include "stdio.h"
#include "stdlib.h"
#include "string.h"
#define line 10

int count_file();

int count_buffer();


/**
  * 设计一个程序，统计在读到文件结尾之前读取的字符数
  */
int main(void){
  count_file();
  return 0;
}

int count_file(){
  FILE *file = fopen("/Users/loern/Documents/github/c-primer-plus-demo/question_08/file.txt", "r");
  if(file == NULL){
    printf("%s\n","open file failed");
    return -1;
  }
  char str[line + 1];
  int total = 0;
  while(fgets(str, line, file) != NULL){
    printf("%s",str); 
    total+= strlen(str);
  }

  fclose(file);
  printf("file total: %d\n",total);
  return total;
}


int count_buffer(){
  int total = 0;
  char ch ;
  while((ch = getchar()) != EOF){
    total++;
  }
  printf("buffer total: %d",total);
  return total;
}
