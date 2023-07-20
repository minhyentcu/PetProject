﻿using AutoMapper;
using BaseSource.Data.Entities;
using BaseSource.ViewModels.Setting;
using BaseSource.ViewModels.User;

namespace BaseSource.Services.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region User

            CreateMap<AppUser, UserInfoResponse>();
            #endregion
            #region Setting
            
                CreateMap<ConfigSystem, ConfigSettingVm>();
            #endregion

        }

    }
}
