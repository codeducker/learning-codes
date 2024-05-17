#include "stdio.h"

void chline(char ,int ,int);


int main(void){
  chline('*',2,4);
}

void chline(char ch ,int i ,int j){
  for(int n = 0;n<j;n++){
    for(int m = 0 ;m<i;m++){
      printf("%c",ch);
    }
    printf("\n");
  }
}
