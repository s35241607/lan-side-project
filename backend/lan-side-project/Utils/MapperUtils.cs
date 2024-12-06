using AutoMapper;
using lan_side_project.DTOs.Reponses.User;
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
        });

        _mapper = config.CreateMapper();
    }

    // 獲取靜態 IMapper 實例
    public static IMapper Mapper => _mapper;
}
