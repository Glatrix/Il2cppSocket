#pragma once

void RegisterPosts_Method(Server* svr) {

    svr->Post("/socket_create_params", [&](const Request& req, Response& res, const ContentReader& content_reader) {
        int count = stoi(req.get_header_value("p_count"));
        void** params = new void* [count];
        res.body = to_hex_string((intptr_t)params);
    });

    svr->Post("/socket_set_param", [&](const Request& req, Response& res, const ContentReader& content_reader) {

        void** params = (void**)hex_to_pointer(req.get_header_value("p_params"));
        void* value = (void*)hex_to_pointer(req.get_header_value("p_value"));
        int index = stoi(req.get_header_value("p_index"));

        params[index] = value;
        res.body = "";
    });

    svr->Post("/il2cpp_runtime_invoke", [&](const Request& req, Response& res, const ContentReader& content_reader) {
        intptr_t method = hex_to_pointer(req.get_header_value("p_method"));
        void** params = (void**)hex_to_pointer(req.get_header_value("p_params"));
        intptr_t object = NULL;

        if (req.has_header("p_object")) {
            object = hex_to_pointer(req.get_header_value("p_object"));
        }

        intptr_t ex = 0;
        intptr_t ret = 0;

        intptr_t _vm_thread_ = il2cpp_thread_attach(il2cpp_domain_get());
        // -- Running as VM thread --
        {
            ex = 0;
            ret = il2cpp_runtime_invoke(method, object, params, ex);
        }
        // -- Back to normal
        il2cpp_thread_detach(_vm_thread_);

        if (ex == 0) 
        {
            res.body = "RETURN\n";
            res.body += to_hex_string(ret);
        }
        else 
        {
            res.body = "ERROR\n";
            res.body += to_hex_string(ex);
        }
    });
}