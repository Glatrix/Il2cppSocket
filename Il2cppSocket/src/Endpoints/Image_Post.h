#pragma once

void RegisterPosts_Image(Server* svr) {
    // Get Image Name
    svr->Post("/il2cpp_image_get_name", [&](const Request& req, Response& res, const ContentReader& content_reader) {
        intptr_t image = hex_to_pointer(req.get_header_value("p_image"));
        char* name = il2cpp_image_get_name(image);
        res.body = name;
    });

    // Get Image Filename
    svr->Post("/il2cpp_image_get_filename", [&](const Request& req, Response& res, const ContentReader& content_reader) {
        intptr_t image = hex_to_pointer(req.get_header_value("p_image"));
        char* name = (char*)il2cpp_image_get_filename(image);
        res.body = name;
    });

    // Get Class Count from Image
    svr->Post("/il2cpp_image_get_class_count", [&](const Request& req, Response& res, const ContentReader& content_reader) {
        intptr_t image = hex_to_pointer(req.get_header_value("p_image"));
        res.body = to_string(il2cpp_image_get_class_count(image));
    });

    // Get Class from Image by Index
    svr->Post("/il2cpp_image_get_class", [&](const Request& req, Response& res, const ContentReader& content_reader) {
        intptr_t image = hex_to_pointer(req.get_header_value("p_image"));
        int index = stoi(req.get_header_value("p_index"));
        intptr_t klass = il2cpp_image_get_class(image, index);
        res.body = to_hex_string(klass);
    });

    // Get Class from Image by Namespace and Name
    svr->Post("/il2cpp_class_from_name", [&](const Request& req, Response& res, const ContentReader& content_reader) {
        intptr_t image = hex_to_pointer(req.get_header_value("p_image"));
        std::string namespaze = req.get_header_value("p_namespace");
        std::string name = req.get_header_value("p_name");
        intptr_t klass = il2cpp_class_from_name(image, namespaze.c_str(), name.c_str());
        res.body = to_hex_string(klass);
    });
}