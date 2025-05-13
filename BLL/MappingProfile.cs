using AutoMapper;
using BLL.DTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Маппинг для User
            CreateMap<User, RegisterDTO>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.RoleName))
                .ReverseMap(); // Двухсторонний маппинг

            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.RoleName));
            // Двухсторонний маппинг

            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // Маппинг для Role
            CreateMap<Role, RoleDTO>()
                .ReverseMap(); // Двухсторонний маппинг

            // Маппинг для Cargo
            CreateMap<Cargo, CargoDTO>()
                .ForMember(dest => dest.CargoType, opt => opt.MapFrom(src => src.CargoType.TypeName));

            CreateMap<CargoDTO, Cargo>()
                .ForMember(dest => dest.CargoTypeId, opt => opt.Ignore()) 
                .ForMember(dest => dest.CargoType, opt => opt.Ignore())
                .ForMember(dest => dest.Requests, opt => opt.Ignore());

            // Маппинг для Request
            CreateMap<Request, RequestDTO>()
                .ForMember(dest => dest.CargoName, opt => opt.MapFrom(src => src.Cargo.CargoName))
                .ForMember(dest => dest.RouteName, opt => opt.MapFrom(src => src.Route.RouteName))
                .ReverseMap(); // Двухсторонний маппинг

            // Маппинг для Payment
            CreateMap<Payment, PaymentDTO>()
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod.PaymentMethod1))
                .ReverseMap(); // Двухсторонний маппинг

            // Маппинг для MovementStatus
            CreateMap<MovementStatus, MovementStatusDTO>()
                .ForMember(dest => dest.CurrentStation, opt => opt.MapFrom(src => src.CurrentStation.StationName))
                .ReverseMap(); // Двухсторонний маппинг

            // Маппинг для Station
            CreateMap<Station, StationDTO>()
                .ReverseMap(); // Двухсторонний маппинг

            // Маппинг для Route
            CreateMap<Route, RouteDTO>()
                .ForMember(dest => dest.StartStation, opt => opt.MapFrom(src => src.StartStation.StationName))
                .ForMember(dest => dest.EndStation, opt => opt.MapFrom(src => src.EndStation.StationName))
                .ReverseMap(); // Двухсторонний маппинг

            // Маппинг для CargoType
            CreateMap<CargoType, CargoTypeDTO>()
                .ReverseMap(); // Двухсторонний маппинг

            // Маппинг для DowntimeCost
            CreateMap<DowntimeCost, DowntimeCostDTO>()
                .ForMember(dest => dest.CargoType, opt => opt.MapFrom(src => src.CargoType.TypeName))
                .ForMember(dest => dest.Station, opt => opt.MapFrom(src => src.Station.StationName))
                .ReverseMap(); // Двухсторонний маппинг

            // Маппинг для Segment
            CreateMap<Segment, SegmentDTO>()
                .ForMember(dest => dest.StationFrom, opt => opt.MapFrom(src => src.StationFrom.StationName))
                .ForMember(dest => dest.StationTo, opt => opt.MapFrom(src => src.StationTo.StationName))
                .ReverseMap(); // Двухсторонний маппинг

            // Маппинг для RouteSegment
            CreateMap<RouteSegment, RouteSegmentDTO>()
                .ForMember(dest => dest.RouteName, opt => opt.MapFrom(src => src.Route.RouteName))
                .ForMember(dest => dest.SegmentFrom, opt => opt.MapFrom(src => src.Segment.StationFrom.StationName))
                .ForMember(dest => dest.SegmentTo, opt => opt.MapFrom(src => src.Segment.StationTo.StationName))
                .ReverseMap(); // Двухсторонний маппинг
        }
    }
}
