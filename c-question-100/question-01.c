#include <math.h>
#include <stdio.h>

// 题目: 有 1，2 ，3，4
// 个数字，能组成多少个互不相同且无重复数字的三位数，分别为?

int main(void) {
  int array[4] = {1, 3, 2, 4};
  int total = 0;
  for (int i = 0; i < 4; i++) {
    for (int j = 0; j < 4; j++) {
      for (int m = 0; m < 4; m++) {
        int number = 100 * array[i] + 10 * array[j] + array[m];
        if (!( array[i] == array[j] || array[j] == array[m] || array[i] == array[m])){
               total++;
          printf("数字:%d\n", number);
        }
      }
    }
  }
  printf("可生成数字个数:%d\n", total);
  return 0;
}
