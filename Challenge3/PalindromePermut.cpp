#include "PalindromePermut.h"

void
PalindromePermut::initialize()
{
	for (int i = 0; i < 26; i++)
		letters.insert(std::pair<char, int>('a' + i, 0));
}

std::string
PalindromePermut::isPalindromePermutation(std::string text)
{
	// for even sized texts the amount of letters 
	// must be even too, this clause makes the 
	// palindrome possible.
	// for odd sized texts only one of the 
	// letters must have an odd amount.
	bool isEven = text.size() % 2 == 0;
	bool singleOdd = false;

	for (char c : text)
		letters.at(c)++;

	for (std::pair<char, int> p : letters)
	{
		if (isEven)
		{
			if (p.second % 2 != 0)
				return "NO";
		}
		else
		{
			if (p.second % 2 != 0)
			{
				if (!singleOdd)
					singleOdd = true;
				else
					return "NO";
			}
		}
	}

	return "YES";
}