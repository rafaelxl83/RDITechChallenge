#pragma once

#include <vector>

class CoinsCombinatorics
{
public:
	CoinsCombinatorics()
	{
		initialize();
	}

	int getNumberOfCombinationsR(int value, int size);
	int getNumberOfCombinations(int value);

private:
	void initialize();

	typedef std::vector<int> line;

	std::vector<int> coins;
};