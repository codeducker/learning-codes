#include <stdio.h>
#include <string.h>
#include <ctype.h>
#define PI 100

// void hello(char name[],int age);

int main(void){
    // char ch = getchar();
    // printf("%c\n",ch);
    // getchar();
    
    // printf(islower("Hello"));

    int ch;
    while((ch = getchar())!='\t'){

        if(isalpha(ch)){ 
            printf("is Alpha");
        }else{
            printf("Not Alpha");
        }
        putchar('\n');
        int x = 20;
        {
            int x = 30;
            printf("%d\n",x);
        }
        printf("%d\n",x);

    }

    // int ch;
    // ch = getchar();
    // while(ch != '\n'){
    //     putchar(ch);
    //     ch = getchar();
    // }
    // printf("%c\n",ch);
  

    // double num ,i;//声明输入次数和循环次数
    // printf("Please enter a number greater than zero (less than zero exit)");//请输入一个大于零的次数（小于零退出）

    // /*判断用户输入是否满足*/
    // while((scanf("%lf",&num) == 1) && (num > 0))
    // {
    //     double sum1 = 0,sum2 = 0;//结果保存在这里
    //     int sign = 1;//用于更改符号

    //     /*循环累加*/
    //     for(i = 1.0;i <= num;i++,sign = 0 - sign)//sign随i++变化
    //     {
    //         sum1 += 1.0/i;
    //         sum2 += sign*1.0/i;

    //     }
    //     /*打印*/
    //     printf("%.lf The sum of subformula 1 is %g\n",num,sum1);//公式一的和为
    //     printf("%.lf The sum of subformula 2 is %g\n",num,sum2);//公式二的和为
    //     /*继续输入*/
    //     printf("Please continue tying");//请继续输入

    // }
    // printf("Done");
    // printf("Hello is your name\n");
    // char name[25];
    // fgets(name,25,stdin);
    // printf("\n your name is:%s",name);
    // hello("bro",123);
    // char name[23] = "Hello Bro"; 
    // strlwr(name);
    // printf("%s",name);
    // int rows;

    // int a=10,b=20;
    // scanf("%d %d",&a,&b);
    // printf("a=%d b=%d\n",a,b);

    // char text[256];
    // while(scanf("%*[^:]: %[a-zA-Z]",text)==1) 
	//     puts(text);

    // int columns;
    // char symbol;
    // printf("\nEnter your rows and columns: ");
    // scanf("%d%d ",&rows,&columns);
    // printf("\nyour input columns: %d, columns : %d\n",rows,columns);
    // int a,b,c,d;
    // int first=scanf("%d",&a);
    // int second=scanf("%d%d",&a,&b);
    // int third=scanf("%d%d%d",&a,&b,&c);
    // int fourth=scanf("%d%d%d%d",&a,&b,&c,&d);
    // printf("第一次读取的返回值为：%d\n",first);
    // printf("第二次读取的返回值为：%d\n",second);
    // printf("第三次读取的返回值为：%d\n",third);
    // printf("第四次读取的返回值为：%d\n",fourth);

    // char line ,separator;
    // char values[20];
    // printf("\nEnter line and separator: ");
    // scanf("%c%c,%s",&line,&separator,values);
    // printf("\nEnter line:%c and separator :%c, values: %s,length : %lu \n",line,separator,values,strlen(values));
    // printf("\nEnter your rows: ");
    // scanf("%d",&rows);
    // printf("\nEnter your columns: ");
    // scanf("%d",&columns);
    // scanf("%c %c");
    // printf("\nEnter your symbol: ");
    // scanf("%c",&symbol);
    // for (int i=0; i<rows; i++)  {
    //     for( int j = 0; j < columns; j++)
    //     {
    //         printf("%c",symbol);
    //     }
    //     printf("\n");
    // }

    // int i, j;
    // char ch;

    // printf("Please enter a upper letter: ");
    // scanf("%c", &ch);
    // printf("The pyramid of %c is:\n", ch);
    // char length = ch - 'A';//计算字母长度。
    // /*循环行数*/
    // for (i = 0; i <= length; i++)
    // {
    //     char letter = 'A' - 1;//计算起始字母。
    //     /*空格递减*/
    //     for (j = 0; j < length - i; j++)
    //         printf(" ");//打印递减空格;
    //     /*递增字母*/
    //     for (j = 0; j <= i; j++)
    //         printf("%c", ++letter);//打印递增字母;
    //     /*递减字母*/
    //     for (j = 0; j < i; j++)
    //         printf("%c", --letter);//打印递减字母
    //     printf("\n");
    // }
    return 0;
}
 
// void hello(char name[],int age){
//     printf("%s is your name\n",name);
//     printf("your age is:%d\n",age);
// }
 