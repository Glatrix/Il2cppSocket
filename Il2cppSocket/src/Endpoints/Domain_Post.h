#pragma once

void RegisterPosts_Domain(Server* svr) {
    // Get Domain
    svr->Post("/il2cpp_domain_get", [&](const Request& req, Response& res, const ContentReader& content_reader) {
        intptr_t domain = il2cpp_domain_get();
        res.body = to_hex_string(domain);
    });

    // Get Assembly by Name
    svr->Post("/il2cpp_domain_assembly_open", [&](const Request& req, Response& res, const ContentReader& content_reader) {
        intptr_t domain = hex_to_pointer(req.get_header_value("p_domain"));
        std::string name = req.get_header_value("p_name");
        intptr_t assembly = il2cpp_domain_assembly_open(domain, name.c_str());
        res.body = to_hex_string(assembly);
    });

    // Get All Assemblies from Domain
    svr->Post("/il2cpp_domain_get_assemblies", [&](const Request& req, Response& res, const ContentReader& content_reader) {
        intptr_t domain = hex_to_pointer(req.get_header_value("p_domain"));
        int size = 0;
        intptr_t* assemblies = il2cpp_domain_get_assemblies(domain, size);
        res.body = "";
        for (int i = 0; i < size; i++) {
            res.body += to_hex_string(assemblies[i]);
            res.body += "\n";
        }
    });
}
