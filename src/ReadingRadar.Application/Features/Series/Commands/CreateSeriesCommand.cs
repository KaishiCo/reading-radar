using MediatR;
using OneOf;
using ReadingRadar.Application.Errors;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.Commands;

public record CreateSeriesCommand(string Name) : IRequest<OneOf<Series, IError>>;
