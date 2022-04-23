using AutoMapper;
using RBS.Email.Sender.Common.Models;
using RBS.Email.Sender.WebApi.Models;

namespace RBS.Email.Sender.WebApi.MappingProfiles
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            CreateMap<EmailRequest, EmailModel>();
        }
    }
}