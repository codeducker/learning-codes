#include <stdio.h>

double min(double ,double);


int main(void){
  double value = min(2.0,3.0);
  printf("the min value : %f",value);
  return 0;
}


double min(double x,double y){
  return x - y > 0 ? x : y;
}
