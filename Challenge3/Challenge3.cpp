#include <iostream>
#include <algorithm>

#include "PalindromePermut.h"


int main(int argc, char* argv[])
{
    PalindromePermut p;

    if (argc < 2)
    {
        std::cout << "Invalid text" << std::endl;
        return -1;
    }

    std::string s(argv[1]);
    for (char c : s)
    {
        if (!(96 < c && c < 123))
        {
            std::cout << "Invalid text! Only lowercase letters" << std::endl;
            return -1;
        }
    }

    std::cout << p.isPalindromePermutation(s) << std::endl;
    return 0;
}
