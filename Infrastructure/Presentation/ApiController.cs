
using Microsoft.AspNetCore.Http;
using Shared.ErrorModels;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public class ApiController : ControllerBase
    {

    }
}
