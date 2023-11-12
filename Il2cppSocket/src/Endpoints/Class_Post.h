#pragma once

void RegisterPosts_Class(Server* svr) {

    // Get Class Name
    svr->Post("/il2cpp_class_get_name", [&](const Request& req, Response& res, const ContentReader& content_reader) {
        intptr_t klass = hex_to_pointer(req.get_header_value("p_class"));
        char* name = il2cpp_class_get_name(klass);
        res.body = name;
    });

    // Get Class Type
    svr->Post("/il2cpp_class_get_type", [&](const Request& req, Response& res, const ContentReader& content_reader) {
        intptr_t klass = hex_to_pointer(req.get_header_value("p_class"));
        intptr_t type = il2cpp_class_get_type(klass);
        res.body = to_hex_string(type);
    });

    // Get Fields from Class
    svr->Post("/il2cpp_class_get_fields", [&](const Request& req, Response& res, const ContentReader& content_reader) {
        intptr_t klass = hex_to_pointer(req.get_header_value("p_class"));
        res.body = "";
        intptr_t iter = 0;
        while (intptr_t field = il2cpp_class_get_fields(klass, iter)) {
            res.body += to_hex_string(field);
            res.body += "\n";
        }
    });

    // Get Methods from Class
    svr->Post("/il2cpp_class_get_methods", [&](const Request& req, Response& res, const ContentReader& content_reader) {
        intptr_t klass = hex_to_pointer(req.get_header_value("p_class"));
        res.body = "";
        intptr_t iter = 0;
        while (intptr_t method = il2cpp_class_get_methods(klass, iter)) {
            res.body += to_hex_string(method);
            res.body += "\n";
        }
    });

    // Get Properties from Class
    svr->Post("/il2cpp_class_get_properties", [&](const Request& req, Response& res, const ContentReader& content_reader) {
        intptr_t klass = hex_to_pointer(req.get_header_value("p_class"));
        res.body = "";
        intptr_t iter = 0;
        while (intptr_t property = il2cpp_class_get_properties(klass, iter)) {
            res.body += to_hex_string(property);
            res.body += "\n";
        }
    });

    // il2cpp_class_get_method_from_name
    svr->Post("/il2cpp_class_get_method_from_name", [&](const Request& req, Response& res, const ContentReader& content_reader) {
        intptr_t klass = hex_to_pointer(req.get_header_value("p_class"));
        std::string name = req.get_header_value("p_name");
        int argsCount = stoi(req.get_header_value("p_argsCount"));
        intptr_t method = il2cpp_class_get_method_from_name(klass, name.c_str(), argsCount);
        res.body = to_hex_string(method);
    });

    // il2cpp_class_get_field_from_name
    svr->Post("/il2cpp_class_get_field_from_name", [&](const Request& req, Response& res, const ContentReader& content_reader) {
        intptr_t klass = hex_to_pointer(req.get_header_value("p_class"));
        std::string name = req.get_header_value("p_name");
        intptr_t method = il2cpp_class_get_field_from_name(klass, name.c_str());
        res.body = to_hex_string(method);
    });
}
