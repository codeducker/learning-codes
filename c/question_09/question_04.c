#include <stdio.h>

double daoshu(double,double);

int main(void){
  double a ,b;
  int result = scanf("%lf %lf",&a,&b);
  if(2 != result){
    printf("illegal input\n");
    return 0;
  }
  double value = daoshu(a,b);
  printf("the result is %.4f\n",value);
}

double daoshu(double a, double b){
 return  1 / (( 1 / a  + 1/ b  ) / 2 );
}
