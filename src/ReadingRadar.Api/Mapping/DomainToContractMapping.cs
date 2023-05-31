using ReadingRadar.Contracts.Books;
using ReadingRadar.Contracts.Radars;
using ReadingRadar.Contracts.Series;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Api.Mapping;

public static class DomainToContractMapping
{
    public static GetBookResponse AsResponse(this Book book) =>
        new(
            book.Id,
            book.Title,
            book.Author,
            (int)book.MediaType,
            book.Description,
            book.Pages,
            book.Chapters,
            book.ImageLink,
            book.PublishDate,
            book.SeriesId);

    public static UpsertRadarResponse AsResponse(this Radar radar) =>
        new(
            (int)radar.Status,
            radar.ChaptersCompleted,
            radar.CompletionDate);

    public static GetRadarResponse AsRadarResponse(this Radar radar) =>
        new(
            (int)radar.Status,
            radar.ChaptersCompleted,
            radar.CompletionDate,
            radar.Book.AsResponse());

    public static GetRadarsResponse AsRadarsResponse(this IEnumerable<Radar> radars) =>
        new(radars.Select(r => r.AsRadarResponse()));

    public static GetSeriesResponse AsSeriesResponse(this Series series) =>
        new(series.Id, series.Name, series.LastUpdated);

    public static GetSeriesResponses AsSeriesResponses(this IEnumerable<Series> series) =>
        new(series.Select(s => s.AsSeriesResponse()));

    public static GetSeriesWithBooksResponse AsSeriesWithBooksResponse(this Series series) =>
        new(series.Name, series.LastUpdated, series.Books.Select(b => b.AsResponse()));
}
