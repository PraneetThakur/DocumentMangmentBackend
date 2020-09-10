using AutoMapper;
using DocumentSystem.Data;
using DocumentSystem.Helper;
using DocumentSystem.Models.Entity;
using DocumentSystem.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DocumentSystem.Repository
{
    public class AccountsService : IIAccountsService
    {
        DocumentDBContext db;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public AccountsService(UserManager<AppUser> userManager, IMapper mapper,DocumentDBContext _db)
        {
            db = _db;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetPost(RegistrationViewModel model)
        {
            var userIdentity = _mapper.Map<AppUser>(model);
            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            //if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            await db.JobSeekers.AddAsync(new JobSeeker { IdentityId = userIdentity.Id, Location = model.Location });
            await db.SaveChangesAsync();

            return new OkResult();
        }
    }
}
