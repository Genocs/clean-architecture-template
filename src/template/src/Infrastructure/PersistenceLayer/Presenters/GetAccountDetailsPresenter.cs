using Genocs.CleanArchitecture.Template.Application.Boundaries.GetAccountDetails;
using System.Collections.ObjectModel;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.Presenters;

public sealed class GetAccountDetailsPresenter : IOutputPort
{
    public Collection<string> Errors { get; }
    public Collection<GetAccountDetailsOutput> GetAccountDetails { get; }
    public Collection<string> NotFounds { get; }

    public GetAccountDetailsPresenter()
    {
        Errors = [];
        GetAccountDetails = [];
        NotFounds = [];
    }

    public void Error(string message)
        => Errors.Add(message);

    public void Default(GetAccountDetailsOutput output)
        => GetAccountDetails.Add(output);

    public void NotFound(string message)
        => NotFounds.Add(message);
}