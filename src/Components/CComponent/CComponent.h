// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the CCOMPONENT_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// CCOMPONENT_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.


#ifdef CCOMPONENT_EXPORTS
#define CCOMPONENT_API __declspec(dllexport)
#else
#define CCOMPONENT_API __declspec(dllimport)
#endif
extern "C" {

// This class is exported from the CComponent.dll

extern CCOMPONENT_API int nCComponent;

CCOMPONENT_API int GetNumber(void);

typedef struct MyStruct {
	int size;
	wchar_t* mystr;
}MYSTRUCT;

CCOMPONENT_API int ManipulateStruct(MYSTRUCT* myStruct);

}
