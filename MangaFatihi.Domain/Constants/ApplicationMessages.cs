namespace MangaFatihi.Domain.Constants
{
    /// <summary>
    /// Kullanıcıya dönen statik mesajlar
    /// </summary>
    public static class ApplicationMessages
    {
        #region Message Codes

        //errors
        public const string ErrorJWTAuthenticationFailed = "AUTHENTICATION_FAILED";
        public const string ErrorJWTNotAuthorized = "NOT_AUTHORIZED";
        public const string ErrorJWTTokenExpired = "TOKEN_EXPIRED";
        public const string ErrorJWTForbidden = "FORBIDDEN";
        public const string ErrorDefaultExceptionHandler = "DEFAULT_EXCEPTION";
        public const string ErrorLoginUserNotFound = "USER_NOT_FOUND";
        public const string ErrorLoginRefreshTokenNotFound = "REFRESH_TOKEN_NOT_FOUND";
        public const string ErrorLoginRefreshTokenInvalidToken = "INVALID_TOKEN";
        public const string ErrorModelStateValidation = "ERROR_MODEL_VALIDATION";
        public const string ErrorUserLoginQueryEmailIsNull = "USER_LOGIN_EMAIL_IS_NULL";
        public const string ErrorUserLoginQueryPasswordIsNull = "USER_LOGIN_PASSWORD_IS_NULL";
        public const string ErrorSeriesNotFound = "SERIES_NOT_FOUND";
        public const string ErrorSeriesEpisodeAlreadyAdded = "SERIES_EPISODE_ALREADY_ADDED";
        public const string ErrorLargeFile = "LARGE_FILE";
        public const string ErrorNotAllowedFileExtension = "NOT_ALLOWED_FILE_EXTENSION";

        public const string ErrorDefaultTypeError = "DEFAULT_TYPE_ERROR";
        public const string ErrorDefaultIsNull = "DEFAULT_IS_NULL";
        public const string ErrorDefaultNotFound = "DEFAULT_NOT_FOUND";

        //success
        public const string SuccessLogin = "USER_LOGIN_SUCCESS";
        public const string SuccessRefreshTokenLogin = "REFRESH_TOKEN_LOGIN_SUCCESS";
        public const string SuccessGetListProcess = "SUCCESS_GET_LIST_PROCESS";
        public const string SuccessGetDetailsProcess = "SUCCESS_GET_DETAILS_PROCESS";
        public const string SuccessAddProcess = "SUCCESS_ADD_PROCESS";
        public const string SuccessUpdateProcess = "SUCCESS_UPDATE_PROCESS";
        public const string SuccessDeleteProcess = "SUCCESS_DELETE_PROCESS";



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
            {
                ErrorLargeFile,
                "Belirlenen maksimum dosya boyutundan ( \'{0}\' byte ) daha büyük dosya!"
            },
            { ErrorNotAllowedFileExtension, "İzin verilmeyen dosya türü. İzin verilen dosya türüler : \'{0}\'" },

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
            return result ?? "";
        }

    }
}
