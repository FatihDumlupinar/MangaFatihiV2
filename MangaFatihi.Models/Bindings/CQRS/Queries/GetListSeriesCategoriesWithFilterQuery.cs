﻿using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Base;
using MangaFatihi.Models.DTOs.CQRS.Commands;
using Mediator;

namespace MangaFatihi.Models.Bindings.CQRS.Queries;

public class GetListSeriesCategoriesWithFilterQuery : BasePaginationFilterModel, IQuery<DataResult<GetListSeriesCategoriesWithFilterQueryDto>>
{
}
