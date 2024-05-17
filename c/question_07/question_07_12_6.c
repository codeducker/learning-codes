// 1.
// #include <stdio.h>
// int main(void){
//     int ch;
//     int lines =0 , spaces =0, chars =0;
//     while((ch = getchar()) != '#'){
//         if (ch == ' '){
//             spaces++;
//         }else if(ch == '\n'){
//             lines++;
//         }
//         chars++;
//     }
//     printf("%d, %d ,%d",lines, spaces ,chars);
//     return 0;
// 
//2.
// #include <stdio.h>
// int main(void){
//     int ch;
//     int total = 0;
//     while((ch = getchar()) != '#'){
//         printf("%c ",ch);
//         printf("%d ",ch);
//         if (total != 0 && total % 8 == 0){
//             printf("\n");
//         }
//         total++;
//     }
//     return 0;
// }

// 3.
// #include <stdio.h>
// int main(void){
//     int ouNum = 0; //偶数个数
//     int ouSum = 0; //偶数总数
//     double ouAvg = 0;//偶数平均数
//     int jiNum = 0; //奇数个数
//     double jiAvg = 0; //奇数平均数
//     int jiSum = 0; //奇数总数
//     int ch = 0;
//     scanf("%d",&ch);
//     while(ch != 0){
//         if(ch % 2 == 0 ){
//             ouNum++;
//             ouSum += ch;
//         }else{
//             jiNum++;
//             jiSum += ch;
//         }
//         scanf("%d",&ch);
//     }
//     jiAvg = jiSum * 1.0 / jiNum;
//     ouAvg = ouSum * 1.0 / ouNum;
//     printf("偶数:%d,偶数平均值:%lf,奇数:%d,奇数平均值:%lf",ouNum,ouAvg,jiNum,jiAvg);
//     return 0;
// }

// 5.
// #include <stdio.h>
// int main(void){
//     int ch;
//     int replaces= 0;
//     while((ch = getchar()) != '#'){
//         if(ch == '.'){
//             replaces ++;
//             putchar('!');
//         }else if(ch  == '!'){
//             replaces ++;
//             putchar('!');
//             putchar('!');
//         }else{
//             putchar(ch);
//         }
//     }
//     printf("总计:%d替换",replaces);
//     return 0;
// }

//5.
// #include <stdio.h>
// int main(void)
// {
//     int ch;
//     int replaces = 0;
//     while ((ch = getchar()) != '#')
//     {
//         switch (ch)
//         {
//         case '.':
//         {
//             replaces++;
//             putchar('!');
//             break;
//         }
//         case '!':
//         {
//             replaces++;
//             putchar('!');
//             putchar('!');
//             break;
//         }
//         default:
//         {
//             putchar('!');
//             break;
//         }
//         }
//     }
//     printf("总计:%d替换", replaces);
//     return 0;
// }

//6.0
#include <stdio.h>
int main(void){
    return 0;
}

// #include <stdio.h>
// int main(void){
//     return 0;
// }

// #include <stdio.h>
// int main(void){
//     return 0;
// }

// #include <stdio.h>
// int main(void){
//     return 0;
// }

// #include <stdio.h>
// int main(void){
//     return 0;
// }

// #include <stdio.h>
// int main(void){
//     return 0;
// }

// #include <stdio.h>
// int main(void){
//     return 0;
// }