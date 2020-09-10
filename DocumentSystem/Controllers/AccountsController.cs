using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DocumentSystem.Data;
using DocumentSystem.Helper;
using DocumentSystem.Models;
using DocumentSystem.Models.Entity;
using DocumentSystem.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DocumentSystem.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly DocumentDBContext db;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager, IMapper mapper, DocumentDBContext _db)
        {
            _userManager = userManager;
            _mapper = mapper;
            db = _db;

        }

        // POST api/accounts
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Post([FromBody] RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            //await db.JobSeekers.AddAsync(new JobSeeker { IdentityId = userIdentity.Id, Location = model.Location });
            await db.MUserMaster.AddAsync(
                new MUserMaster { 
                    IdentityIds = userIdentity.Id,
                    DeptId = model.DepartmentId ,
                    FirstName=model.FirstName,
                    LastName=model.LastName,
                    OrganizationId=1
                    
                }
                );

            await db.SaveChangesAsync();

            return new OkObjectResult(result);
        }
    }
}
