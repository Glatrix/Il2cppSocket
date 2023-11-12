#pragma once
void RegisterPosts_Type(Server* svr) {
    // Get Type Name
    svr->Post("/il2cpp_type_get_name", [&](const Request& req, Response& res, const ContentReader& content_reader) {
        intptr_t type = hex_to_pointer(req.get_header_value("p_type"));
        char* name = il2cpp_type_get_name(type);
        res.body = name;
    });
}