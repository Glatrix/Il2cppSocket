#pragma once
void RegisterPosts_Field(Server* svr) {
    svr->Post("/il2cpp_field_static_get_value", [&](const Request& req, Response& res, const ContentReader& content_reader) {
        intptr_t field = hex_to_pointer(req.get_header_value("p_field"));

        void* ptr = 0;
        il2cpp_field_static_get_value(field, &ptr);

        if (ptr) {
            res.body = to_hex_string((intptr_t)ptr);
        }
        else {
            res.body = to_hex_string(0);
        }
    });
}