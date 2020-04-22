namespace Genocs.MicroserviceLight.Template.Services
{
    using System;
    using System.Threading.Tasks;
    using Genocs.MicroserviceLight.Template.Dto;
    using RestEase;

    public interface IMyExternalService
    {
        [AllowAnyStatusCode]
        [Get("myexternalroute/{id}")]
        Task<FooTemplateDto> GetAsync([Path] Guid id);
    }
}