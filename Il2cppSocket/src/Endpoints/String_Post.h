#pragma once
void RegisterPosts_String(Server* svr) {
    // Get Type Name
    svr->Post("/il2cpp_string_new", [&](const Request& req, Response& res, const ContentReader& content_reader) {
        std::string str = req.get_header_value("p_value");
        const char* value = str.c_str();
        intptr_t name = il2cpp_string_new(value);
        res.body = to_hex_string(name);
    });
}