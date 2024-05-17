#include <stdio.h>

#define MAX 100

int ga;

const char word;

int main(int argc, char **argv){
    char c  = 'w';
    int age = 20;
    float salary = 1000.1f;
    double height = 200.0;
    printf("%c\n",c);
    printf("%d\n",age);
    printf("%f\n",salary);
    printf("%lf\n",height);
    printf("%ld\n",sizeof(char));
    printf("%ld\n",sizeof(short));
    printf("%ld\n",sizeof(int));
    float lovetime ;
    printf("%f\n", lovetime);
    printf("%d\n",__SLBF);
    printf("%d\n",ga);
    // word = 'm';
    printf("%c\n",word);
    const int n = 7;
    int arr[n] = {1};
    const int nym = 20;
    printf("%d\n",nym);
    printf("%d\n",MAX);
}

int ga = 3;