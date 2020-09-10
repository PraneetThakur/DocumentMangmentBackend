using DocumentSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSystem.Repository
{
    public interface IIAccountsService
    {
        Task<IActionResult> GetPost(RegistrationViewModel model);

    }
}
