// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the USESHAREDMEMB_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// USESHAREDMEMB_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef USESHAREDMEMB_EXPORTS
#define USESHAREDMEMB_API __declspec(dllexport)
#else
#define USESHAREDMEMB_API __declspec(dllimport)
#endif

USESHAREDMEMB_API int getSharedMemB(void);
USESHAREDMEMB_API void setSharedMemB(int value);