#include "framework.h"

BOOL APIENTRY DllMain(HMODULE hModule,
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
#include <cstdio>
#include <iostream>
#include <C:\Users\Иван\.jdks\openjdk-17.0.2\include\jni.h>
extern "C"
{
    jdouble __declspec (dllexport) __stdcall Java_MyClass_AddTwoElements(JNIEnv* env, jobject obj, jdouble firstElement, jdouble secondElement){
        std::cout << "Hello from C++" << std::endl;
        std::cout << firstElement << std::endl;
        std::cout << secondElement << std::endl;
        return firstElement + secondElement;
    }

    double __declspec (dllexport) __stdcall AddTwoElements(double firstElement, double secondElement) {
        std::cout << "Hello from C++" << std::endl;
        std::cout << firstElement << std::endl;
        std::cout << secondElement << std::endl;
        return firstElement + secondElement;
    }
}
