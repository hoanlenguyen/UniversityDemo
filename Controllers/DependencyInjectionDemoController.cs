using Microsoft.AspNetCore.Mvc;
using UniversityDemo.Test;

namespace UniversityDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DependencyInjectionDemoController : ControllerBase
    {
        public OperationService OperationService { get; }
        public IOperationTransient TransientOperation { get; }
        public IOperationScoped ScopedOperation { get; }
        public IOperationSingleton SingletonOperation { get; }
        public IOperationSingletonInstance SingletonInstanceOperation { get; }

        public DependencyInjectionDemoController(
            OperationService operationService,
            IOperationTransient transientOperation,
            IOperationScoped scopedOperation,
            IOperationSingleton singletonOperation,
            IOperationSingletonInstance singletonInstanceOperation)
        {
            OperationService = operationService;
            TransientOperation = transientOperation;
            ScopedOperation = scopedOperation;
            SingletonOperation = singletonOperation;
            SingletonInstanceOperation = singletonInstanceOperation;
        }

        [HttpGet("GetOperationResults")]
        public IActionResult GetResults()
        {
            var operationController = "Controller operations \n"
                + "Transient " + TransientOperation.OperationId.ToString() + "\n"
                + "Scoped " + ScopedOperation.OperationId + "\n"
                + "Singleton " + SingletonOperation.OperationId + "\n"
                + "Instance " + SingletonInstanceOperation.OperationId + "\n\n";

            var operationService = "OperationService \n"
                + "Transient " + OperationService.TransientOperation.OperationId.ToString() + "\n"
                + "Scoped " + OperationService.ScopedOperation.OperationId + "\n"
                + "Singleton " + OperationService.SingletonOperation.OperationId + "\n"
                + "Instance " + OperationService.SingletonInstanceOperation.OperationId + "\n";

            return Ok(operationController + operationService);
        }
    }
}