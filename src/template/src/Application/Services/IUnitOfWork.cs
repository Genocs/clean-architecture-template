namespace Genocs.CleanArchitecture.Template.Application.Services;

public interface IUnitOfWork
{
    Task<int> Save();
}