using Genocs.MicroserviceLight.Template.BusHost.ExternalServices;
using Genocs.MicroserviceLight.Template.Shared.Commands;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Genocs.MicroserviceLight.Template.BusHost.RequestProcessing
{
    public class RequestProcessor : IRequestProcessor
    {
        private readonly ILogger<RequestProcessor> _logger;
        private readonly ISimpleServiceCaller _simpleServiceCaller;

        public RequestProcessor(
            ILogger<RequestProcessor> logger,
            ISimpleServiceCaller simpleServiceCaller)
        {
            _logger = logger;
            _simpleServiceCaller = simpleServiceCaller;
        }

        public async Task<bool> ProcessSimpleMessageAsync(SimpleMessage message, IReadOnlyDictionary<string, object> properties)
        {
            _logger.LogInformation("Processing Simple Message {MessageId}", message.MessageId);

            try
            {
                var requestStatus = await _simpleServiceCaller.GetSimpleModelAsync("10");
                if (requestStatus != null)
                {
                    _logger.LogInformation("Completed change transaction status request {MessageId}", message.MessageId);
                    return true;
                }
                else
                {
                    _logger.LogError("Failed process Simple Message status for request {MessageId}", message.MessageId);
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error processing Simple Message {MessageId}", message.MessageId);
            }

            return false;
        }
    }
}
