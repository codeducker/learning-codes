
// 3.编写一个程序，读取整数直到用户输入0。输入结束后，程序应报告用户输入的偶数（不包括0）个数
// 、这些偶数的平均值、输入的奇数个数及其奇数的平均值
#include <stdio.h>
int main(void){
    int ouNum = 0; //偶数个数
    int ouSum = 0; //偶数总数
    double ouAvg = 0;//偶数平均数
    int jiNum = 0; //奇数个数
    double jiAvg = 0; //奇数平均数
    int jiSum = 0; //奇数总数
    int ch = 0;
    scanf("%d",&ch);
    while(ch != 0){
        if(ch % 2 == 0 ){
            ouNum++;
            ouSum += ch;
        }else{
            jiNum++;
            jiSum += ch;
        }
        scanf("%d",&ch);
    }
    jiAvg = jiSum * 1.0 / jiNum;
    ouAvg = ouSum * 1.0 / ouNum;
    printf("偶数:%d,偶数平均值:%lf,奇数:%d,奇数平均值:%lf",ouNum,ouAvg,jiNum,jiAvg);
    return 0;
}