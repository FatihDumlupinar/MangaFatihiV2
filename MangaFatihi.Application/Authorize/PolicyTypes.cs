namespace MangaFatihi.Application.Authorize
{
    public static class PolicyTypes
    {

        #region User

        /// <summary>
        /// Kullanıcı işlemleri için yetkiler
        /// </summary>
        public static class Claims_User
        {
            private const string UserClaimPrefix = "user";

            /// <summary>
            /// Kullanıcıları listeleme yetkisi
            /// </summary>
            public const string List = $"{UserClaimPrefix}.list";

            /// <summary>
            /// Kullanıcı oluşturma yetkisi
            /// </summary>
            public const string Create = $"{UserClaimPrefix}.create";

            /// <summary>
            /// Kullanıcı detayı görme yetkisi
            /// </summary>
            public const string Read = $"{UserClaimPrefix}.read";

            /// <summary>
            /// Kullanıcı güncelleme yetkisi
            /// </summary>
            public const string Update = $"{UserClaimPrefix}.update";

            /// <summary>
            /// Kullanıcı silme yetkisi
            /// </summary>
            public const string Delete = $"{UserClaimPrefix}.delete";
        }

        #endregion


    }
}
