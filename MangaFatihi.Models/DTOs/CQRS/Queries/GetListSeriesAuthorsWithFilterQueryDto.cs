﻿using MangaFatihi.Models.Commonns;

namespace MangaFatihi.Models.DTOs.CQRS.Commands;

public class GetListSeriesAuthorsWithFilterQueryDto
{
    /// <summary>
    /// Filtreye göre veritabanında bulunan verilerin listesi
    /// </summary>
    public List<SeriesAuthorListModel> List { get; set; } = new();

    /// <summary>
    /// Filtreye göre veritabanında bulunan verilerin sayısı
    /// </summary>
    public int TotalCount { get; set; } = 0;
}
