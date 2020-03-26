
bool start_with(const std::string& str, const std::string& title){
    return str.rfind(title, 0)==0;
}

template <typename PrefixType>
bool start_with(const std::string& str, PrefixType prefix){
    return start_with(str, prefix);
}

bool start_with(std::string str1, std::string str2){
    return str1.rfind(str2, 0) == 0;
}