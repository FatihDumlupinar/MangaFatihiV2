namespace MangaFatihi.Domain.Constants
{
    /// <summary>
    /// Kullanıcıya dönen statik mesajlar
    /// </summary>
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
        public static readonly string ErrorSeriesNotFound = "SERIES_NOT_FOUND";
        public static readonly string ErrorSeriesEpisodeAlreadyAdded = "SERIES_EPISODE_ALREADY_ADDED";

        public static readonly string ErrorDefaultTypeError = "DEFAULT_TYPE_ERROR";
        public static readonly string ErrorDefaultIsNull = "DEFAULT_IS_NULL";
        public static readonly string ErrorDefaultNotFound = "DEFAULT_NOT_FOUND";


        //success
        public static readonly string SuccessLogin = "USER_LOGIN_SUCCESS";
        public static readonly string SuccessRefreshTokenLogin = "REFRESH_TOKEN_LOGIN_SUCCESS";
        public static readonly string SuccessGetListProcess = "SUCCESS_GET_LIST_PROCESS";
        public static readonly string SuccessGetDetailsProcess = "SUCCESS_GET_DETAILS_PROCESS";
        public static readonly string SuccessAddProcess = "SUCCESS_ADD_PROCESS";
        public static readonly string SuccessUpdateProcess = "SUCCESS_UPDATE_PROCESS";
        public static readonly string SuccessDeleteProcess = "SUCCESS_DELETE_PROCESS";



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
            { ErrorSeriesNotFound, "Veritabanında böyle bir seri bulunamadı!" },
            { ErrorSeriesEpisodeAlreadyAdded, "Bu bölüm daha öncesinden eklenmiştir!" },

            { ErrorDefaultIsNull, "\'{0}\' zorunlu!" },
            { ErrorDefaultTypeError, "\'{0}\' yanlış format!" },
            { ErrorDefaultNotFound, "Aradığınız \'{0}\' bulunamadı!" },

            { SuccessGetListProcess, "Veri listesi getirme işlemi başarılı." },
            { SuccessAddProcess, "Veri ekleme işlemi başarılı." },
            { SuccessUpdateProcess, "Veri güncelleme işlemi başarılı." },
            { SuccessDeleteProcess, "Veri silme işlemi başarılı." },
            { SuccessGetDetailsProcess, "Veri detayı getirme işlemi başarılı." },

        };


        #endregion

        public static string GetMessage(this string messageCode)
        {
            string result;
            _ = Messages.TryGetValue(messageCode, out result);
            return result??"";
        }

    }
}
