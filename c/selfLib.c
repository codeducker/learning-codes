#ifdef SELF_FUNC
#include <MathFunctions.h>
#endif /* ifdef TEST_ */
#include <stdio.h>
int main(){
#ifdef SELF_FUNC  
  double result = selfSqrt(2.0);
  printf("%f\n",result);
  printf("%s\n","sqrt finish");
#endif 
  printf("%s\n","Main finished");
}
