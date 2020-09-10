using AutoMapper;
using DocumentSystem.Models;
using DocumentSystem.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSystem.ViewModel.Mappings
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<UserMaster, AppUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
            CreateMap<RegistrationViewModel, AppUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));

        }
    }
}
