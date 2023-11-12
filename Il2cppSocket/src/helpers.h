#pragma once
using namespace std;

// Convert Hex String to Pointer
intptr_t hex_to_pointer(std::string str)
{
    return stoi(str, 0, 16);
}

// Convert Pointer to String (Hex Format)
std::string to_hex_string(intptr_t value)
{
    std::stringstream sstream;
    sstream << std::hex << value;
    std::string result = sstream.str();
    return result;
}