#define WIN32_LEAN_AND_MEAN
#include <Windows.h>
#include <string>
#include <vector>
#include <format>
#include "Il2cpp.h"
#include "httplistener.h"
#include "helpers.h"
using namespace httplib;
#include "endpoints.h"

void Main() 
{
    il2cpp_header_init();
    il2cpp_header_AllocConsole();
    // HTTP Server
    Server svr;
    // Register Endpoints
    {
        Registers::RegisterGet_Defaults(&svr);

        Registers::RegisterPosts_Domain(&svr);
        Registers::RegisterPosts_Assembly(&svr);
        Registers::RegisterPosts_Image(&svr);
        Registers::RegisterPosts_Class(&svr);
        Registers::RegisterPosts_Type(&svr);
        Registers::RegisterPosts_Field(&svr);
        Registers::RegisterPosts_Method(&svr);
        Registers::RegisterPosts_Property(&svr);
        Registers::RegisterPosts_String(&svr);
    }
    svr.listen("0.0.0.0", 1212);
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved)
{
    if (ul_reason_for_call == DLL_PROCESS_ATTACH) { 
        CreateThread(0, 0, (LPTHREAD_START_ROUTINE)Main, 0, 0, 0);
    }
    return TRUE;
}