using GlobalExceptionMiddleware.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace GlobalExceptionMiddleware.Controllers
{
    [ApiController]
    public class ExceptionTestController : ControllerBase
    {
        [HttpGet]
        [Route("BadRequestException")]
        public IActionResult BadRequestException()
        {
            throw new BadRequestException("Example BadRequestException - 400 - Client Side Error");
            return Ok();
        }

        [HttpGet]
        [Route("NotFoundException")]
        public IActionResult NotFoundException()
        {
            throw new NotFoundException("Example NotFoundException - 404 - User not found.");
            return Ok();
        }

        [HttpGet]
        [Route("InternalServerError")]
        public IActionResult InternalServerError()
        {
            int number1 = 50, number2 = 0;
            int result = number1/number2;
            return Ok();
        }
    }
}
