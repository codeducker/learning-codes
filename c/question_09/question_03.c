#include "stdio.h"


void dcount(char , int , int);


int main(void){
  char ch ;
  int i ,  j ;
  printf("Please input show char , show times, line num \n ");
  int result = scanf("%c %d %d",&ch, &i, &j);
  if (3 != result || i <=0 || j <= 0){
    printf("error input \n");
    return 0;
  }
  dcount(ch,i,j);
}

void dcount(char ch, int i, int j){
  for(int m = 0 ; m < j ; m++){
    for(int n = 0; n< i; n++){
      printf("%c",ch);
    }
    printf("\n");
  }
}
