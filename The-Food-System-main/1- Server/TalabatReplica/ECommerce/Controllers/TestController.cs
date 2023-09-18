
//namespace ECommerce.API.Controllers
//{
//    [Route( "[controller]" )]
//    [ApiController]
//    public class TestController : ControllerBase
//    {
//        private readonly TestManager manager;

//        public TestController( TestManager manager )
//        {
//            this.manager = manager;
//        }
//        [Authorize]
//        [HttpGet( Name = "test" )]
//        public async Task<IActionResult> GetTests( )
//        {
//            var data = await manager.GetAllTestsAsync( );
//            return Ok( data );
//        }

//}
