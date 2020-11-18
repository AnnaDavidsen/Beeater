using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Beeater.Domain.Entities;
using Beeater.Persistence;

namespace Beeater.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : CommonController<Authorization>
    {
        public AuthorizationController(beeaterContext context)
            : base(context)
        {

        }
    }
}
