// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the THECOMPONENT_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// THECOMPONENT_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef THECOMPONENT_EXPORTS
#define THECOMPONENT_API __declspec(dllexport)
#else
#define THECOMPONENT_API __declspec(dllimport)
#endif

#include "../TheApi/IComponent.h"

// This class is exported from the TheComponent.dll
class THECOMPONENT_API CTheComponent : public IComponent {
public:
	CTheComponent(void);

	// Inherited via IComponent
	virtual void Foo() override;
	// TODO: add your methods here.
};

extern THECOMPONENT_API int nTheComponent;

THECOMPONENT_API int fnTheComponent(void);
