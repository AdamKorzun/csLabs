// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include "Sample.h"
#include "Sample.cpp"
extern "C" __declspec(dllexport) int AddNum(int x, int y) {
    return AddNumbers(x, y);
}
BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}


