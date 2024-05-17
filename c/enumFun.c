#include  <stdio.h>

enum Sex
{
    //枚举类型enum Sex的可能取值,他们都是枚举常量
	MALE, //默认从 0 开始  
	FEMALE, // 若此时 指定枚举值 则后续枚举在此枚举值上 +1
	SECRET
};
//定义枚举类型时，内部为枚举常量，常量的默认值为0，在创建时可以同时赋值

int main()
{
	enum Sex s1 = MALE;
	enum Sex s2 = FEMALE;
	enum Sex s3 = SECRET;
	printf("s1 = %d\n", s1);
	printf("s2 = %d\n", s2);
	printf("s3 = %d\n", s3);

	return 0;
}