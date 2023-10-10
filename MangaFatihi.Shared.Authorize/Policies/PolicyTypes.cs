namespace MangaFatihi.Shared.Authorize.Policies
{
    public static class PolicyTypes
    {
        public const string PermissionClaimTypeName = "permissions";

        public const string RoleClaimTypeName = "role";

        public const string UserFullNameClaimTypeName = "fullname";

        #region Identity

        public static class Claims_AppUser
        {
            private const string ClaimPrefix = "user";

            public const string List = $"{ClaimPrefix}.list";

            public const string Create = $"{ClaimPrefix}.create";

            public const string Read = $"{ClaimPrefix}.read";

            public const string Update = $"{ClaimPrefix}.update";

            public const string Delete = $"{ClaimPrefix}.delete";
        }

        public static class Claims_AppRole
        {
            private const string ClaimPrefix = "role";

            public const string List = $"{ClaimPrefix}.list";

            public const string Create = $"{ClaimPrefix}.create";

            public const string Read = $"{ClaimPrefix}.read";

            public const string Update = $"{ClaimPrefix}.update";

            public const string Delete = $"{ClaimPrefix}.delete";
        }

        #endregion

        public static class Claim_Series
        {
            private const string ClaimPrefix = "series";

            public const string List = $"{ClaimPrefix}.list";

            public const string Create = $"{ClaimPrefix}.create";

            public const string Read = $"{ClaimPrefix}.read";

            public const string Update = $"{ClaimPrefix}.update";

            public const string Delete = $"{ClaimPrefix}.delete";
        }

        public static class Claim_SeriesAndSeriesArtist
        {
            private const string ClaimPrefix = "seriesandseriesartist";

            public const string List = $"{ClaimPrefix}.list";

            public const string Create = $"{ClaimPrefix}.create";

            public const string Read = $"{ClaimPrefix}.read";

            public const string Update = $"{ClaimPrefix}.update";

            public const string Delete = $"{ClaimPrefix}.delete";
        }

        public static class Claim_SeriesAndSeriesAuthor
        {
            private const string ClaimPrefix = "seriesandseriesauthor";

            public const string List = $"{ClaimPrefix}.list";

            public const string Create = $"{ClaimPrefix}.create";

            public const string Read = $"{ClaimPrefix}.read";

            public const string Update = $"{ClaimPrefix}.update";

            public const string Delete = $"{ClaimPrefix}.delete";
        }

        public static class Claim_SeriesAndSeriesCategory
        {
            private const string ClaimPrefix = "seriesandseriescategory";

            public const string List = $"{ClaimPrefix}.list";

            public const string Create = $"{ClaimPrefix}.create";

            public const string Read = $"{ClaimPrefix}.read";

            public const string Update = $"{ClaimPrefix}.update";

            public const string Delete = $"{ClaimPrefix}.delete";
        }

        public static class Claim_SeriesArtist
        {
            private const string ClaimPrefix = "seriesartist";

            public const string List = $"{ClaimPrefix}.list";

            public const string Create = $"{ClaimPrefix}.create";

            public const string Read = $"{ClaimPrefix}.read";

            public const string Update = $"{ClaimPrefix}.update";

            public const string Delete = $"{ClaimPrefix}.delete";
        }

        public static class Claim_SeriesAuthor
        {
            private const string ClaimPrefix = "seriesauthor";

            public const string List = $"{ClaimPrefix}.list";

            public const string Create = $"{ClaimPrefix}.create";

            public const string Read = $"{ClaimPrefix}.read";

            public const string Update = $"{ClaimPrefix}.update";

            public const string Delete = $"{ClaimPrefix}.delete";
        }

        public static class Claim_SeriesCategory
        {
            private const string ClaimPrefix = "seriescategory";

            public const string List = $"{ClaimPrefix}.list";

            public const string Create = $"{ClaimPrefix}.create";

            public const string Read = $"{ClaimPrefix}.read";

            public const string Update = $"{ClaimPrefix}.update";

            public const string Delete = $"{ClaimPrefix}.delete";
        }

        public static class Claim_SeriesEpisode
        {
            private const string ClaimPrefix = "seriesepisode";

            public const string List = $"{ClaimPrefix}.list";

            public const string Create = $"{ClaimPrefix}.create";

            public const string Read = $"{ClaimPrefix}.read";

            public const string Update = $"{ClaimPrefix}.update";

            public const string Delete = $"{ClaimPrefix}.delete";

        }

        public static class Claim_SeriesEpisodesPage
        {
            private const string ClaimPrefix = "seriesepisodespage";

            public const string List = $"{ClaimPrefix}.list";

            public const string Create = $"{ClaimPrefix}.create";

            public const string Read = $"{ClaimPrefix}.read";

            public const string Update = $"{ClaimPrefix}.update";

            public const string Delete = $"{ClaimPrefix}.delete";
        }

        public static class Claim_Team
        {
            private const string ClaimPrefix = "team";

            public const string List = $"{ClaimPrefix}.list";

            public const string Create = $"{ClaimPrefix}.create";

            public const string Read = $"{ClaimPrefix}.read";

            public const string Update = $"{ClaimPrefix}.update";

            public const string Delete = $"{ClaimPrefix}.delete";
        }

        public static class Claim_TeamAndAppUser
        {
            private const string ClaimPrefix = "teamandappuser";

            public const string List = $"{ClaimPrefix}.list";

            public const string Create = $"{ClaimPrefix}.create";

            public const string Read = $"{ClaimPrefix}.read";

            public const string Update = $"{ClaimPrefix}.update";

            public const string Delete = $"{ClaimPrefix}.delete";
        }


    }
}
