using AutoMapper;
using lan_side_project.DTOs.Requests.PriceTable;
using lan_side_project.DTOs.Requests.Role;
using lan_side_project.DTOs.Responses.Role;
using lan_side_project.DTOs.Responses.User;
using lan_side_project.Models;

namespace lan_side_project.Utils;

public static class MapperUtils
{
    // 靜態 IMapper 實例
    private static readonly IMapper _mapper;

    // 靜態初始化 AutoMapper 配置
    static MapperUtils()
    {
        var config = new MapperConfiguration(cfg =>
        {
            // 定義所有需要的映射配置
            cfg.CreateMap<User, User>();
            cfg.CreateMap<User, UserResponse>();
            cfg.CreateMap<Role, Role>();
            cfg.CreateMap<Role, RoleResponse>();
            cfg.CreateMap<CreateRoleRequest, Role>();
            cfg.CreateMap<UpdateRoleRequest, Role>();


            cfg.CreateMap<CreatePriceTableRequest, PriceTable>()
                .ForMember(dest => dest.PriceTableItems, opt => opt.MapFrom(src => src.PriceTableItems));
            cfg.CreateMap<CreatePriceTableItemRequest, PriceTableItem>();

            cfg.CreateMap<UpdatePriceTableRequest, PriceTable>()
                .ForMember(dest => dest.PriceTableItems, opt => opt.MapFrom(src => src.PriceTableItems));
            cfg.CreateMap<UpdatePriceTableItemRequest, PriceTableItem>();
        });

        _mapper = config.CreateMapper();
    }

    // 獲取靜態 IMapper 實例
    public static IMapper Mapper => _mapper;
}
