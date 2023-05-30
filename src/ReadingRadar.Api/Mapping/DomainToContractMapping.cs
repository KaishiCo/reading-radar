using ReadingRadar.Contracts.Books;
using ReadingRadar.Contracts.Radars;
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
}
