#include<stdio.h>

#define MAX(x,y) (x > y ? x : y)
//MAX的参数x和y分别被下面的a,b所替代，所以就变成了MAX(a,b) (a > b ? a : b)
int main()
{
	int a = 0;
	int b = 0;
	scanf("%d %d", &a, &b);
	int m = MAX(a, b);
    //int m = (x > y ? x : y);
	printf("%d\n", m);
	return 0;
}