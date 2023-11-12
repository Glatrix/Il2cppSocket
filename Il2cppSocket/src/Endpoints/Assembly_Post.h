#pragma once

void RegisterPosts_Assembly(Server* svr) {
    // Get Image from Assembly
    svr->Post("/il2cpp_assembly_get_image", [&](const Request& req, Response& res, const ContentReader& content_reader) {
        intptr_t assembly = hex_to_pointer(req.get_header_value("p_assembly"));
        intptr_t image = il2cpp_assembly_get_image(assembly);
        res.body = to_hex_string(image);
    });
}