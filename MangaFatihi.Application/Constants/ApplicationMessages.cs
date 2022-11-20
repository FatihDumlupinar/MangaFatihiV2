﻿namespace MangaFatihi.Application.Constants
{
    public static class ApplicationMessages
    {
        #region Message Codes

        //errors
        public static readonly string ErrorJWTAuthenticationFailed = "AUTHENTICATION_FAILED";
        public static readonly string ErrorJWTNotAuthorized = "NOT_AUTHORIZED";
        public static readonly string ErrorJWTTokenExpired = "TOKEN_EXPIRED";
        public static readonly string ErrorJWTForbidden = "FORBIDDEN";
        public static readonly string ErrorDefaultExceptionHandler = "DEFAULT_EXCEPTION";
        public static readonly string ErrorLoginUserNotFound = "USER_NOT_FOUND";
        public static readonly string ErrorLoginRefreshTokenNotFound = "REFRESH_TOKEN_NOT_FOUND";
        public static readonly string ErrorLoginRefreshTokenInvalidToken = "INVALID_TOKEN";
        public static readonly string ErrorModelStateValidation = "ERROR_MODEL_VALIDATION";
        public static readonly string ErrorUserLoginQueryEmailIsNull = "USER_LOGIN_EMAIL_IS_NULL";
        public static readonly string ErrorUserLoginQueryPasswordIsNull = "USER_LOGIN_PASSWORD_IS_NULL";
        public static readonly string ErrorRefreshTokenQueryRefreshTokenIsNull = "REFRESH_TOKEN_LOGIN_REFRESH_TOKEN_IS_NULL";


        //success
        public static readonly string SuccessLogin = "USER_LOGIN_SUCCESS";
        public static readonly string SuccessRefreshTokenLogin = "REFRESH_TOKEN_LOGIN_SUCCESS";


        #endregion


        #region Messages

        public static Dictionary<string, string> Messages = new()
        {
            { ErrorJWTAuthenticationFailed, "Token oluşturulurken hata!" },
            { ErrorJWTNotAuthorized, "Yetkisiz giriş!" },
            { ErrorJWTTokenExpired, "Token süresi bitmiş!" },
            { ErrorJWTForbidden, "Bu kaynağa erişim yetkiniz yok!" },
            { ErrorLoginUserNotFound, "Kullanıcı bulunamadı!" },
            { SuccessLogin, "Kullanıcı girişi başarılı." },
            { ErrorLoginRefreshTokenNotFound, "RefreshToken bulunamadı!" },
            { SuccessRefreshTokenLogin, "RefreshToken girişi başarılı." },
            { ErrorModelStateValidation, "Kabul edilemez model!" },
            { ErrorUserLoginQueryEmailIsNull, "E-Posta zorunlu!" },
            { ErrorUserLoginQueryPasswordIsNull, "Şifre zorunlu!" },
            { ErrorRefreshTokenQueryRefreshTokenIsNull, "Refresh Token zorunlu!" },


        };

        #endregion

        public static string GetMessage(this string messageCode)
        {
            string result;
            _ = Messages.TryGetValue(messageCode, out result);
            return result;
        }

    }
}
