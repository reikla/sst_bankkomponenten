#pragma once

#pragma region OK
#define E_OK 0
#pragma endregion

#pragma region Unexpected
#define E_NOT_EXPECTED -1
#pragma endregion Unexpected

#pragma region Persistence
#define E_PERSISTENCE_ERROR -2
#pragma endregion Persistence

#pragma region Authorization
#define E_UNAUTHORIZED -10
#pragma endregion Unauthorized

#pragma region Initialize
#define E_NOT_INITIALIZED -20;
#pragma endregion Initialize

#pragma	region Parameter
#define E_INVALID_PARAMETER -30
#pragma	endregion Parameter

#pragma region Customer
#define E_CUSTOMER_NOT_FOUND -41
#pragma endregion Customer

#pragma region Account
#define E_ACCOUNT_NOT_FOUND -51
#define E_NEW_DISPOSER_NOT_FOUND -52
#define E_REMOVE_DISPOSER_NOT_FOUND -53
#define E_CANNOT_REMOVE_SELF -54
#pragma endregion Account


#pragma region CurrencyTranslation
#define E_CURRENCY_FACTOR_NOT_STORED -60
#pragma endregion CurrencyTranslation

#pragma region Transaction
#define E_INSUFFICIENT_FUNDS -70
#define E_TARGET_ACCOUNT_NOT_FOUND -71
#pragma endregion Transaction