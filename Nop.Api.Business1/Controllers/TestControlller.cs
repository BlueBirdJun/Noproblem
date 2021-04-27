using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Api.Business1.Controllers
{
    [ApiController]
    //[ValidateAntiForgeryToken]
    [Route("[controller]")]
    public class TestControlller : Controller
    {
        private readonly ILogger<TestControlller> _logger;
        private readonly NopContext _context;
             
        public TestControlller(ILogger<TestControlller> logger,
            NopContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public string  Get()
        {
            _context.boards.Add(new Repository.NopModels.CommonBoards() { UserId="ccc",Contents = "쏼라쏼라", DelYN='N', Title="제목이예요" });
            _context.SaveChanges();

            
            return _context.boards.ToList()[0].Title;
        }
    }
}
