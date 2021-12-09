#pragma once

#include <string>
#include <map>

class PalindromePermut
{
public:
	PalindromePermut()
	{
		initialize();
	}

	std::string isPalindromePermutation(std::string text);

private:
	void initialize();

	std::map<char, int>	letters;
};