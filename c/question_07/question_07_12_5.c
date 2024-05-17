//5. 使用switch重写练习4。
#include <stdio.h>
int main(void)
{
    int ch;
    int replaces = 0;
    while ((ch = getchar()) != '#')
    {
        switch (ch)
        {
        case '.':
        {
            replaces++;
            putchar('!');
            break;
        }
        case '!':
        {
            replaces++;
            putchar('!');
            putchar('!');
            break;
        }
        default:
        {
            putchar('!');
            break;
        }
        }
    }
    printf("总计:%d替换", replaces);
    return 0;
}