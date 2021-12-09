#include <iostream>
#include <sstream>
#include <string>

#include "CoinsCombinatorics.h"

/// <summary>
/// RDI Technical Challenge
/// Made by Rafael Xavier de Lima (rafael.xavier.lima@gmail.com)
/// </summary>
int main(int argc, char* argv[])
{
    if (argc < 2)
    {
        std::cout << "Invalid number" << std::endl;
        return -1;
    }

    std::string s(argv[1]);
    for (char c : s)
    {
        if (!(47 < c && c < 58))
        {
            std::cout << "Invalid number! Please, insert a valid number!" << std::endl;
            return -1;
        }
    }

    int val = 0;
    //val = stoi(s);
    std::stringstream aux(s);
    aux >> val;

    CoinsCombinatorics c;
    std::cout << c.getNumberOfCombinations(val) << std::endl;

    return 0;
}
