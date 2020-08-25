using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Blog.Core.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Blog.Core.IService;

/// <summary>
/// 广告控制器
/// </summary>
[ApiController]
public class AdvertisementController : ControllerBase
{
    IAdvertisementService service;

    public AdvertisementController(IAdvertisementService advertisementService)
    {
        service = advertisementService;
    }

    /// <summary>
    /// 两数相加
    /// </summary>
    /// <param name="first">加数</param>
    /// <param name="second">被加数</param>
    /// <returns>相加结果</returns>
    [HttpGet]
    [Authorize]
    [Route("[controller]/[action]")]
    public int Sum(int first, int second)
    {
        return 5;
    }

    /// <summary>
    /// 添加广告
    /// </summary>
    /// <param name="model">广告</param>
    /// <returns>添加的广告编号</returns>
    [HttpPost]
    [Route("[controller]/[action]")]
    public int Add([Required]Advertisement model)
    {
        return service.Add(model).Result;
    }

    /// <summary>
    /// 删除广告
    /// </summary>
    /// <param name="model">广告</param>
    /// <returns>删除成功返回true，否则返回false</returns>
    [HttpPost]
    [Route("[controller]/[action]")]
    public bool Delete(Advertisement model)
    {
        return service.Delete(model).Result > 0;
    }

    /// <summary>
    /// 更新广告
    /// </summary>
    /// <param name="model">广告</param>
    /// <returns>更新成功返回true，否则返回false</returns>
    [HttpPost]
    [Route("[controller]/[action]")]
    public bool Update([Required]Advertisement model)
    {
        return service.Update(model).Result > 0;
    }

    /// <summary>
    /// 查询所有广告
    /// </summary>
    /// <returns>返回所有广告</returns>
    [HttpPost]
    [Route("[controller]/[action]")]
    public List<Advertisement> QueryAll()
    {
        return service.Query().Result.ToList();
    }

    /// <summary>
    /// 查询指定广告
    /// </summary>
    /// <param name="id">广告编号</param>
    /// <returns>返回指定编号的广告</returns>
    [HttpPost]
    [Route("[controller]/[action]")]
    public Advertisement QueryById([Required]int id)
    {
        return service.QueryById(id).Result;
    }
}