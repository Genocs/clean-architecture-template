namespace Genocs.Application.Boundaries.Withdraw
{
    public interface IOutputPort : IErrorHandler
    {
        void Default(WithdrawOutput withdrawOutput);
    }
}