#include "CoinsCombinatorics.h"

#include <iostream>

int
CoinsCombinatorics::getNumberOfCombinationsR(int value, int size)
{
    if (value == 0)
        return 1;

    if (value < 0)
        return 0;

    if (size <= 0 && value >= 1)
        return 0;

	// it is a sum of solutions that respectively include (size-1)
	// and excludes(size-1) a denomination
    return getNumberOfCombinationsR(value, size - 1) +
		   getNumberOfCombinationsR(value - coins[size - 1], size);
}

int
CoinsCombinatorics::getNumberOfCombinations(int value)
{
	std::unique_ptr<line[]> table;
	table = std::unique_ptr<line[]>(new line[value + 1]);
	for (int i = 0; i < value + 1; i++)
		table[i] = line(coins.size(), 1);

	int i, j;
	int x, y;

	// Fill rest of the table entries
	// in bottom up manner
	for (i = 1; i < value + 1; i++)
	{
		for (j = 0; j < coins.size(); j++)
		{
			// Count of solutions including coins[j]
			x = (i - coins[j] >= 0) ? table[i - coins[j]][j] : 0;

			// Count of solutions excluding coins[j]
			y = (j >= 1) ? table[i][j - 1] : 0;

			// total count
			table[i][j] = x + y;
		}
	}

    return table[value][coins.size() - 1];
}

void 
CoinsCombinatorics::initialize()
{
	// valid BRL coins
	//coins = {1, 2, 5, 10, 25, 50, 100};
	coins = {1, 5, 10, 20, 25, 50};
}