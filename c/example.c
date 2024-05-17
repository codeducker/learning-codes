// #include <stdio.h>
// int main(void){
//     int num;
//     for (num = 1; num <= 11 ; num++){
//         if(num % 3 == 0)
//             putchar('$');
//         else 
//             putchar('*');
//             putchar('#');
//         putchar('%');
//     }
//     putchar('\n');
//     return 0;
// }

// #include <stdio.h>
// int main(void)
// {
//      int i = 0;
//      while (i < 3) {
//           switch (i++) {
//                case 0: printf("fat ");
//                case 1: printf("hat ");
//                case 2: printf("cat ");
//                default: printf("Oh no!");
//           }
//           putchar('\n');
//      }
//      return 0;
// }

// #include <stdio.h>
// int main(void)
// {
//      char ch;
//      int lc = 0; /* 统计小写字母 */
//      int uc = 0; /* 统计大写字母 */
//      int oc = 0; /* 统计其他字母 */

//      while ((ch = getchar()) != '#')
//      {
//           if ('a' <= ch >= 'z')
//                lc++;
//           else if (!(ch < 'A') || !(ch > 'Z'))
//                uc++;
//           oc++;
//      }
//      printf("%d lowercase, %d uppercase, %d other", lc, uc, oc);
//      return 0;
// }


/* retire.c */
// #include <stdio.h>
// int main(void)
// {
//      int age = 20;
//      while (age++ <= 65)
//      {
//           if ((age % 20) == 0) /* age是否能被20整除？ */
//                printf("You are %d. Here is a raise.\n", age);
//           if (age = 65)
//                printf("You are %d. Here is your gold watch.\n", age);
//      }
//      return 0;
// }

// q
// c
// h
// b
#include <stdio.h>
int main(void)
{
     char ch;

     while ((ch = getchar()) != '#')
     {
          if (ch == '\n')
               continue;
          printf("Step 1\n");
          if (ch == 'c')
               continue;
          else if (ch == 'b')
               break;
          else if (ch == 'h')
               goto laststep;
          printf("Step 2\n");
     laststep: printf("Step 3\n");
     }
     printf("Done\n");
     return 0;
}