namespace Genocs.MicroserviceLight.Template.Handlers.Foo
{
    using System.Threading.Tasks;
    using Genocs.Microservices.Common.Handlers;
    using Genocs.Microservices.Common.RabbitMq;
    using Genocs.Microservices.Common.Types;
    using Genocs.MicroserviceLight.Template.Domain;
    using Genocs.MicroserviceLight.Template.Messages.Commands;
    using Genocs.MicroserviceLight.Template.Messages.Events;
    using Genocs.MicroserviceLight.Template.Repositories;

    public class CreateFooTemplateHandler : ICommandHandler<CreateFooTemplate>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IFooTemplatesRepository _fooTemplatesRepository;

        public CreateFooTemplateHandler(IBusPublisher busPublisher,
            IFooTemplatesRepository fooTemplatesRepository)
        {
            _busPublisher = busPublisher;
            _fooTemplatesRepository = fooTemplatesRepository;
        }

        public async Task HandleAsync(CreateFooTemplate command, ICorrelationContext context)
        {
            // Validation before insert
            FooTemplate fooTemplate = await _fooTemplatesRepository.GetAsync(command.Id);
            if (fooTemplate != null)
            {
                throw new GenocsException(Codes.FooTemplateAlreadyCreated,
                    $"FooTemplate was already created with id: '{command.Id}'.");
            }


            //               throw new UtuException(Codes.InvalidUser,
            //                   $"FooTemplate cannot be created by user with id: '{context.UserId}'.");

            // Missing validation about code that must be uninque
            //if (fooTemplate.Code == command.Code)
            //{
            //    throw new UtuException(Codes.FooTemplateAlreadyCreated,
            //        $"FooTemplate was already created with code: '{command.Code}'.");
            //}


            // Domain object creation and persistence
            fooTemplate = new FooTemplate(command.Id,
                command.Code,
                command.Label);

            await _fooTemplatesRepository.AddAsync(fooTemplate);

            // Event notification
            await _busPublisher.PublishAsync(new FooTemplateCreated(command.Id, fooTemplate.Code,
                fooTemplate.Label), context);
        }
    }
}