namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Registers;

using System.Threading.Tasks;

public interface IUseCase
{
    Task Execute(RegisterInput registerInput);
}