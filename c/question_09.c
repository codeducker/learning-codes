#include "stdio.h"

void show_menus(){
  printf("%s","Please choose one of the following:\n");
  printf("%s","1) copy files            2) move files\n");
  printf("%s","3) remove files          4) quit\n");
  printf("%s","Enter the number of your choice:\n");
}

int get_choice(int min,int max){
  int choice;
  int result = scanf("%d",&choice);
  if(1 != result){
    printf("%s","illegal input numberic\n");
    return 4;
  }
  while(1 == result && (choice >max || choice < min)){
    show_menus();
    scanf("%d",&choice);
  }
  return choice;
}


int main(void){
  int res;
  show_menus();
  while((res = get_choice(1,4)) !=4 ){
    printf("I like the choice :%d\n",res);
    show_menus();
  } 
  return 0;
}
