#include "stdio.h"
#include "string.h"
#include "math.h"


int main(){
    char buf[5] = {'a','b','c','d','e'};
    printf("%ld\n",strlen(buf));
    printf("%f\n",5/3.0);
    printf("%f\n",floor(5/3.0));
    printf("%d\n",0 && "bar" );
    return 0;
}