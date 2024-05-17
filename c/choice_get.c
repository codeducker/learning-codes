#include <stdio.h>

char get_choice();

char get_char();

char get_first_char();

void count();

int get_int();

int main(){
    setbuf(stdout,NULL);
    int choice;
    while ( (choice = get_choice()) != 'q') {
        switch (choice) {
            case 'a': {
                printf("%s","Buy low ,Sell high\n");
                break;
            }
            case 'b': {
                putchar('\a');
                break;
            }
            case 'c': {
                count();
                break;
            }
            default: {
                printf("%s","Wrong Parameter\n");
                break;
            }
        }
    }
}

char get_choice() {
    printf("%s","Enter the letter of your choice!\n");
    printf("%s","a. advice                b. bell\n");
    printf("%s","c. count                 q. quit\n");
    int ch = get_char();
    while(( ch < 'a' || ch > 'c') && ch != 'q') {
        printf("%s","Please response the letter of a b c q \n");
        ch = get_char();
    }
    return ch;
}

char get_char() {
    return get_first_char();
}

char get_first_char() {
    const int ch = getchar();
    while(getchar() != '\n')
        continue;
    return ch;
}

void count(void)
{
    int n, i;
    printf("Count how far? Enter an integer:\n");
    n = get_int();
    for (i = 1; i <= n; i++)
        printf("%d\n", i);
    while(getchar()!='\n') {
        continue;
    }
}

int get_int() {
    int input;
    char ch;
    while(scanf("%d",&input) != 1) {
        while((ch = getchar()) != '\n') {
            putchar(ch);
        }
        printf(" is not an integer. \n Please enter \n");
        printf("integer value is such as 128, 28, 32\n");
    }
    return input;
}