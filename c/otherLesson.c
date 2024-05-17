#include <stdio.h>
#include <math.h>
#include <string.h>
#include <ctype.h>
#define lineNum  5

// void printSum(int a,int b){
//     int i = a * a;
//     int m = b * b;
//     int total = 0;
//     for (int j = a  ; j <= b; j++){
//         total += j * j;
//     }
//     printf("The sums of the squares from %d to %d is %d\n",i,m,total);
// }


int main(void){
    printf("Please input some charactors: ");
    char chars[25];

    scanf("%s",chars);
    int count = 0;
    int ch;
    while (ch = getchar() != '\n'){
        if (ch == '\'' || ch  == '\"'){
            continue;
        }else{
            count++;
        }
    }
    printf("total input charactors : %d", count);
 
    // int number;
    // scanf("%d",&number);
    // if(number >6)
    //     if(number < 12)
    //         printf("You're close\n");
    // else
    //     printf("Sorry ,you lose a turn\n");

    
    // double num1[8];
    // double num2[8];
    // double total = 0;
    // for (int i =0 ;i < 8 ; i++){
    //     printf("The index : %d ,input value :", (i +1));
    //     scanf("%lf",&num1[i]);
    //     total += num1[i];
    //     num2[i] = total;
    // }
    // for (int i =0 ;i < 8; i++){
    //     printf("%3.2lf ",num1[i]);
    // }
    // printf("\n");
    // for (int i =0 ;i < 8; i++){
    //     printf("%3.2lf ",num2[i]);
    // }

    // int numbers[8];
    // for (int i =0; i< 8 ; i ++){
    //     numbers[i] = pow(2,i+1);
    // }
    // int j = 0; 
    // do {
    //     printf("%-5d", numbers[j]);
    //     j++;
    // }while(j < 8);

    // int times;
    // printf("Enter The Times:");
    // scanf("%d",&times);
    // double total = 1.0;
    // double minTotal = 1.0;
    // for (int i =2 ; i <= times; i++){
    //     total += ( 1.0 / i * 1.0 );
    //     if (i % 2 == 0 ){
    //         minTotal -= ( 1.0 / i * 1.0 );
    //     }else{
    //         minTotal += ( 1.0 / i * 1.0 );
    //     }
    // }
    // printf("The Sum:%lf, %lf",minTotal , total);


    // int numbers[8];
    // for (int i = 0 ; i < 8 ; i ++){
    //     printf("Enter the index : %d of number :", i);
    //     scanf("%d", &numbers[i]);
    // }
    // printf("The input Array:");
    // for (int i = 0 ; i < 8 ; i ++){
    //     printf("%2d", numbers[ i]);
    // }
    // printf("\nThe Revert Array:");
    // for (int i = 8-1 ; i >= 0 ; i--){
    //     printf("%2d", numbers[i]);
    // }

    // int a , b;
    // printf("Enter lower and upper integer limits:");
    // scanf("%d %d",&a,&b);
    // while ( a != b){
    //     printSum(a,b);
    //     printf("Enter next set of limits:");
    //     scanf("%d %d",&a,&b);
    // };
    // printf("Done");

    // double a , b;
    // while(2 == scanf("%lf,%lf",&a,&b)){
    //     printf("%3.4lf\n",(a - b ) / (a *b));
    // }

    // int lower, higher;
    // scanf("%d,%d",&lower,&higher);

    // for (int i = lower; i <= higher; i++){
    //     printf("%5d %5d %5d\n", i,  i*i , i *i*i);
    // }

    // char str[20];
    // scanf("%s",str);
    // for (int i = strlen(str) -1; i>= 0 ; i--){
    //     printf("%c",str[i]);
    // }

    //     A
    //    ABA
    //   ABCBA
    //  ABCDCBA
    // ABCDEDCBA
    // char input;
    // printf("请输入字母!\n");
    // scanf("%c",&input);
    // char line[lineNum][2 * lineNum - 1];
    // //初始化一下
    // for (int i = 0 ; i < lineNum ; i ++){
    //     for (int j = 0 ; j < 2 * lineNum -1 ; j ++){
    //         line[i][j] =' ';
    //     }
    // }

    // for (int i = 0; i < lineNum ; i++){
    //     char midChar = input - (lineNum - ( i  + 1 ) );//定义中心字母  0 = A 
    //     int midIndex = (( (lineNum * 2 ) - 1 ) / 2 ) + 1;
    //     printf("i = %d  ,c = %c\n", i , midChar);
    //     for (int j = 0 ; j <= i ; j ++){
    //         line[i][midIndex + j -1] = midChar - j ;
    //         line[i][midIndex - j -1] = midChar - j ;
    //     }
    // }
    // for (int i = 0 ; i < lineNum ; i ++){
    //       for (int j = 0 ; j < 2 * lineNum -1 ; j ++){
    //         printf("%c", line[i][j]);
    //      }
    //      printf("\n");
    // }


    // char c = 'A';
    // for (int i = 0;i< 6;i++){
    //     for (int j = 0 ; j < i+1; j++){
    //          printf("%c",c++);
    //     }
    //     printf("\n");
    // }


    // for(int i =0 ;i < 6 ;i++)
    // {
    //     char c = 'F';
    //     for(int j = 0 ; j < i+1; j++){
    //         printf("%c",c--);
    //     }
    //     printf("\n");
    // }

    // for(int i =0 ;i < 5 ;i++)
    // {
    //     for(int j =0;j < i+1; j++){
    //         printf("$");
    //     }
    //     printf("\n");
    // }

    // char chars[26];
    // for (char i = 'a'; i <= 'z'; i++){
    //     chars[i - 'a'] = i ;
    // }
    // printf("%s",chars);
    // printChar();
    // int rows;
    // int columns;
    // char symbol;
    // printf("\nEnter your rows: ");
    // scanf("%d",&rows);
    // printf("\nEnter your columns: ");
    // scanf("%d",&columns);
    // scanf("%c");
    // printf("\nEnter your symbol: ");
    // scanf("%c",&symbol);
    // for (int i=0; i<rows; i++)  {
    //     for( int j = 0; j < columns; j++)
    //     {
    //         printf("%c",symbol);
    //     }
    //     printf("\n");
    // }
    // printf("%3d",1/2);
    // int x, y ,arr[10];

    //  char ch;

    //  scanf("%c", &ch);
    //  while (ch != 'g')
    //  {
    //       printf("%c", ch);
    //       scanf("%c", &ch);
    //  }

        // double min[10];
        // scanf("%f",&min[2]);
        // printf("%f",min[2]);
    // for (int n =5 ; n > 0 ;n--){
    //     for (int m = 0 ; m <= n ; m++){
    //         printf("=");
    //     }
    //    printf("\n");
    // }

    // char line ,separator;
    // char values[20];
    // printf("\nEnter line and separator: ");
    // scanf("%c%c,%19s",&line,&separator,values);
    // printf("\nEnter line:%c and separator :%c, values: %s \n",line,separator,values);

         return 0;
     
}