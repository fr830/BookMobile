using AutoMapper;
using BookClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookClient.Models
{
    // How to setup Automapper in ASP.NET Core
    // https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
    // Using AutoMapper with ASP.NET Core
    // https://stackoverflow.com/questions/35380456/configuring-automapper-4-2-with-built-in-ioc-in-asp-net-core-1-0-mvc6
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() : this("AutoMapperConfig")
        {
        }

        protected AutoMapperConfig(string profileName) : base(profileName)
        {
            //CreateMap<Book, BookViewModel>()
            //    .ForMember(dest => dest.BootTitle, opts => opts.MapFrom(src => src.Title));

            CreateMap<Book, BookViewModel>();
            CreateMap<Book, BookViewModel>().ReverseMap();
            //CreateMap<Contact, ContactVM>();
            //CreateMap<Contact, ContactVM>().ReverseMap();
        }
    }
}
