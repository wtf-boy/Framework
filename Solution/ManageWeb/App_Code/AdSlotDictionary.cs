using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// AdSlotDictionary 的摘要说明
/// </summary>
public class AdSlotDictionary
{
    public static List<AdSlotInCoteKeyID> adSlotDictionary = new List<AdSlotInCoteKeyID>();
    static AdSlotDictionary()
    {
        //限时免费
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 398, CoteKeyID = 0, ItemValue = 1, AdTitle = "限时免费-首页自定义广告 第1位", AdPositionId = 1 });//首页自定义广告 第1位
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 399, CoteKeyID = 0, ItemValue = 1, AdTitle = "限时免费-首页自定义广告 第2位", AdPositionId = 2 });//首页自定义广告 第2位
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 400, CoteKeyID = 0, ItemValue = 1, AdTitle = "限时免费-首页自定义广告 第3位", AdPositionId = 3 });//首页自定义广告 第3位
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 401, CoteKeyID = 0, ItemValue = 1, AdTitle = "限时免费-首页自定义广告 第4位", AdPositionId = 4 });//首页自定义广告 第4位
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 402, CoteKeyID = 0, ItemValue = 1, AdTitle = "限时免费-首页自定义广告 第5位", AdPositionId = 5 });//首页自定义广告 第5位
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 403, CoteKeyID = 0, ItemValue = 1, AdTitle = "限时免费-首页自定义广告 第6位", AdPositionId = 6 });//首页自定义广告 第6位
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 404, CoteKeyID = 0, ItemValue = 1, AdTitle = "限时免费-首页自定义广告 第7位", AdPositionId = 7 });//首页自定义广告 第7位
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 405, CoteKeyID = 0, ItemValue = 1, AdTitle = "限时免费-首页自定义广告 第8位", AdPositionId = 8 });//首页自定义广告 第8位
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 406, CoteKeyID = 527, ItemValue = 0, AdTitle = "限时免费-内页轮播图", AdPositionId = 1 });//内页轮播图
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 407, CoteKeyID = 0, ItemValue = 19, AdTitle = "限时免费-内页底部", AdPositionId = 8 });//内页底部
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 408, CoteKeyID = 0, ItemValue = 24, AdTitle = "限时免费-猜你喜欢", AdPositionId = 24 });//猜你喜欢
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 409, CoteKeyID = 0, ItemValue = 20, AdTitle = "限时免费-分类-编辑推荐第三", AdPositionId = 8 });//分类-编辑推荐第三
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 410, CoteKeyID = 0, ItemValue = 30, AdTitle = "限时免费-搜索第二", AdPositionId = 8 });//搜索第二
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 411, CoteKeyID = 0, ItemValue = 26, AdTitle = "限时免费-精选-热门游戏", AdPositionId = 8 });//精选-热门游戏
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 412, CoteKeyID = 0, ItemValue = 31, AdTitle = "限时免费-精选-本周下载排行", AdPositionId = 8 });//精选-本周下载排行
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 413, CoteKeyID = 0, ItemValue = 36, AdTitle = "限时免费-精选-畅销游戏", AdPositionId = 8 });//精选-畅销游戏
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 414, CoteKeyID = 0, ItemValue = 4, AdTitle = "限时免费-排行第三", AdPositionId = 8 });//排行第三
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 416, CoteKeyID = 227, ItemValue = 0, AdTitle = "限时免费-default页", AdPositionId = 8 });//default页
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 417, CoteKeyID = 0, ItemValue = 35, AdTitle = "限时免费-热门网游第三", AdPositionId = 3 });//热门网游第三
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 418, CoteKeyID = 0, ItemValue = 35, AdTitle = "限时免费-热门网游第四", AdPositionId = 4 });//热门网游第四
        //手机助手
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 420, CoteKeyID = 0, ItemValue = 34, AdTitle = "手机助手-每日更新1-3轮播", AdPositionId = 13 });//每日更新1-3轮播
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 421, CoteKeyID = 0, ItemValue = 34, AdTitle = "手机助手-每日更新2-4轮播", AdPositionId = 24 });//每日更新2-4轮播
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 422, CoteKeyID = 0, ItemValue = 42, AdTitle = "手机助手-游戏首页第1", AdPositionId = 1 });//游戏首页第1
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 423, CoteKeyID = 0, ItemValue = 42, AdTitle = "手机助手-游戏首页第2", AdPositionId = 2 });//游戏首页第2
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 424, CoteKeyID = 575, ItemValue = 0, AdTitle = "手机助手-首页焦点", AdPositionId = 0 });//首页焦点
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 425, CoteKeyID = 0, ItemValue = 44, AdTitle = "手机助手-排行第三", AdPositionId = 0 });//排行第三
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 426, CoteKeyID = 0, ItemValue = 43, AdTitle = "手机助手-游戏排行第二", AdPositionId = 0 });//游戏排行第二
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 428, CoteKeyID = 0, ItemValue = 0, AdTitle = "手机助手-装机必备-本周必玩", AdPositionId = 0 });//装机必备-本周必玩
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 429, CoteKeyID = 0, ItemValue = 29, AdTitle = "手机助手-搜索栏第四", AdPositionId = 0 });//搜索栏第四
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 430, CoteKeyID = 12514, ItemValue = 0, AdTitle = "手机助手-default页", AdPositionId = 0 });//default页
        //装机宝典
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 432, CoteKeyID = 0, ItemValue = 13, AdTitle = "装机宝典-首页第一", AdPositionId = 1 });//首页第一
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 433, CoteKeyID = 0, ItemValue = 13, AdTitle = "装机宝典-首页第二", AdPositionId = 2 });//首页第二
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 434, CoteKeyID = 228, ItemValue = 0, AdTitle = "装机宝典-焦点图1", AdPositionId = 0 });//焦点图1
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 435, CoteKeyID = 228, ItemValue = 0, AdTitle = "装机宝典-焦点图2", AdPositionId = 0 });//焦点图2
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 436, CoteKeyID = 0, ItemValue = 6, AdTitle = "装机宝典-限免第一", AdPositionId = 1 });//限免第一
        //头条娱乐
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 438, CoteKeyID = 294, ItemValue = 0, AdTitle = "头条娱乐-default页", AdPositionId = 0 });//default页
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 439, CoteKeyID = 0, ItemValue = 0, AdTitle = "头条娱乐-关键词", AdPositionId = 0 });//关键词
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 440, CoteKeyID = 0, ItemValue = 0, AdTitle = "头条娱乐-列表第一", AdPositionId = 0 });//列表第一
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 441, CoteKeyID = 0, ItemValue = 16, AdTitle = "头条娱乐-今日限免第一", AdPositionId = 0 });//今日限免第一
        //玩机技巧
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 444, CoteKeyID = 12513, ItemValue = 0, AdTitle = "玩机技巧-default页", AdPositionId = 0 });//default页
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 445, CoteKeyID = 605, ItemValue = 0, AdTitle = "玩机技巧-焦点图2", AdPositionId = 0 });//焦点图2
        //最美壁纸
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 447, CoteKeyID = 12520, ItemValue = 0, AdTitle = "最美壁纸-default页", AdPositionId = 0 });//default页
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 448, CoteKeyID = 0, ItemValue = 39, AdTitle = "最美壁纸-每日更新1-3位轮播", AdPositionId = 0 });//每日更新1-3位轮播
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 455, CoteKeyID = 596, ItemValue = 0, AdTitle = "最美壁纸-焦点图2", AdPositionId = 0 });//焦点图2
        //最美铃声
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 450, CoteKeyID = 12506, ItemValue = 0, AdTitle = "最美铃声-default页", AdPositionId = 0 });//default页
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 451, CoteKeyID = 0, ItemValue = 45, AdTitle = "最美铃声-每日更新1-3位轮播", AdPositionId = 0 });//每日更新1-3位轮播
        //平板助手
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 453, CoteKeyID = 0, ItemValue = 40, AdTitle = "平板助手-编辑推荐", AdPositionId = 0 });//编辑推荐
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 454, CoteKeyID = 0, ItemValue = 41, AdTitle = "平板助手-新品推荐", AdPositionId = 0 });//新品推荐
        //壁纸HD ios 8
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 457, CoteKeyID = 12520, ItemValue = 0, AdTitle = "壁纸HD ios 8-Default图", AdPositionId = 0 });//Default图
        adSlotDictionary.Add(new AdSlotInCoteKeyID() { AdSlot = 458, CoteKeyID = 0, ItemValue = 0, AdTitle = "壁纸HD ios 8-壁纸ios 8插屏", AdPositionId = 0 });//壁纸ios 8插屏
    }
}
public class AdSlotInCoteKeyID
{
    public int AdSlot { get; set; }
    public int CoteKeyID { get; set; }
    public int ItemValue { get; set; }
    public string AdTitle { get; set; }
    public int AdPositionId { get; set; }
}