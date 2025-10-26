using tyme.culture;
using tyme.eightchar;
using tyme.enums;
using tyme.sixtycycle;
using tyme.solar;

using Xunit.Abstractions;

namespace test;

/// <summary>
/// 排盘Test
/// </summary>
public class PaiPanTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public PaiPanTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test0()
    {
        var childLimit = ChildLimit.FromSolarTime(SolarTime.FromYmdHms(1996, 02, 12, 04, 37, 0), Gender.Man);
        var eightCharFromSolar = childLimit.EightChar;
        var solarTime = childLimit.StartTime;

        _testOutputHelper.WriteLine("\n=== 从阳历日期转换的八字 ===");
        _testOutputHelper.WriteLine($"阳历时间：{solarTime}");
        _testOutputHelper.WriteLine($"对应农历：{solarTime.GetLunarHour().LunarDay.LunarMonth.LunarYear}年{solarTime.GetLunarHour().LunarDay.LunarMonth.Month}月{solarTime.GetLunarHour().LunarDay.Day}日{solarTime.GetLunarHour().Hour}时");
        PrintEightCharInfo(eightCharFromSolar, childLimit.Gender);
    }

    /// <summary>
    /// 打印八字详细信息的辅助方法
    /// </summary>
    /// <param name="eightChar">八字对象</param>
    private void PrintEightCharInfo(EightChar eightChar, Gender gender)
    {
        var year = eightChar.Year;
        var month = eightChar.Month;
        var day = eightChar.Day;
        var hour = eightChar.Hour;



        var me = day.HeavenStem;
        _testOutputHelper.WriteLine($"主星：{me.GetTenStar(year.HeavenStem)} {me.GetTenStar(month.HeavenStem)} {"元" + gender.GetName()}, {me.GetTenStar(hour.HeavenStem)}");
        _testOutputHelper.WriteLine($"八字：{year} {month} {day} {hour}");
        _testOutputHelper.WriteLine($"藏干：[{year.EarthBranch.HideHeavenStemMain} {year.EarthBranch.HideHeavenStemMiddle} {year.EarthBranch.HideHeavenStemResidual}] [{month.EarthBranch.HideHeavenStemMain} {month.EarthBranch.HideHeavenStemMiddle} {month.EarthBranch.HideHeavenStemResidual}] [{day.EarthBranch.HideHeavenStemMain} {day.EarthBranch.HideHeavenStemMiddle} {day.EarthBranch.HideHeavenStemResidual}] [{hour.EarthBranch.HideHeavenStemMain} {hour.EarthBranch.HideHeavenStemMiddle} {hour.EarthBranch.HideHeavenStemResidual}]");
        _testOutputHelper.WriteLine($"副星：[{me.GetTenStar(year.EarthBranch.HideHeavenStemMain)} {me.GetTenStar(year.EarthBranch.HideHeavenStemMiddle)} {me.GetTenStar(year.EarthBranch.HideHeavenStemResidual)}] [{me.GetTenStar(month.EarthBranch.HideHeavenStemMain)} {me.GetTenStar(month.EarthBranch.HideHeavenStemMiddle)} {me.GetTenStar(month.EarthBranch.HideHeavenStemResidual)}] [{me.GetTenStar(day.EarthBranch.HideHeavenStemMain)} {me.GetTenStar(day.EarthBranch.HideHeavenStemMiddle)} {me.GetTenStar(day.EarthBranch.HideHeavenStemResidual)}] [{me.GetTenStar(hour.EarthBranch.HideHeavenStemMain)} {me.GetTenStar(hour.EarthBranch.HideHeavenStemMiddle)} {me.GetTenStar(hour.EarthBranch.HideHeavenStemResidual)}]");
        _testOutputHelper.WriteLine($"五行：{year.HeavenStem.Element}{year.EarthBranch.Element} {month.HeavenStem.Element}{month.EarthBranch.Element} {day.HeavenStem.Element}{day.EarthBranch.Element} {hour.HeavenStem.Element}{hour.EarthBranch.Element}");
        _testOutputHelper.WriteLine($"纳音：{year.Sound} {month.Sound} {day.Sound} {hour.Sound}");
        _testOutputHelper.WriteLine($"星运：{me.GetTerrain(year.EarthBranch)} {me.GetTerrain(month.EarthBranch)} {me.GetTerrain(day.EarthBranch)} {me.GetTerrain(hour.EarthBranch)}");
        _testOutputHelper.WriteLine($"自坐：{year.HeavenStem.GetTerrain(year.EarthBranch)} {month.HeavenStem.GetTerrain(month.EarthBranch)} {day.HeavenStem.GetTerrain(day.EarthBranch)} {hour.HeavenStem.GetTerrain(hour.EarthBranch)}");

        var gods = eightChar.GetShenShaByPillars(gender == Gender.Man);
        // 拼接吉神和凶神的名称
        _testOutputHelper.WriteLine("神煞：" + string.Join("\r\n", gods.Select(g => g.Key + ":" + string.Join(",", g.Value))));

        var yearExtraEarthBranches = string.Join(",", year.ExtraEarthBranches.Select(o => o.ToString()));
        var monthExtraEarthBranches = string.Join(",", month.ExtraEarthBranches.Select(o => o.ToString()));
        var dayExtraEarthBranches = string.Join(",", day.ExtraEarthBranches.Select(o => o.ToString()));
        var hourExtraEarthBranches = string.Join(",", hour.ExtraEarthBranches.Select(o => o.ToString()));
        _testOutputHelper.WriteLine($"空亡：{yearExtraEarthBranches} {monthExtraEarthBranches} {dayExtraEarthBranches} {hourExtraEarthBranches}");

        _testOutputHelper.WriteLine($"胎元：{eightChar.FetalOrigin}({eightChar.FetalOrigin.Sound})");
        _testOutputHelper.WriteLine($"胎息：{eightChar.FetalBreath}({eightChar.FetalBreath.Sound})");
        _testOutputHelper.WriteLine($"命宫：{eightChar.OwnSign}({eightChar.OwnSign.Sound})");
        _testOutputHelper.WriteLine($"身宫：{eightChar.BodySign}({eightChar.BodySign.Sound})");
    }



    /// <summary>
    /// 根据阳历日期创建八字的专门测试方法
    /// </summary>
    [Fact]
    public void TestCreateEightCharFromSolarDate()
    {
        // 测试多个不同的阳历日期转换
        var testCases = new[]
        {
            new { Year = 2005, Month = 12, Day = 23, Hour = 8, Minute = 37, Second = 0, Expected = "乙酉 戊子 辛巳 壬辰" },
            new { Year = 1988, Month = 2, Day = 15, Hour = 23, Minute = 30, Second = 0, Expected = "戊辰 甲寅 辛丑 戊子" },
            new { Year = 1990, Month = 3, Day = 15, Hour = 10, Minute = 30, Second = 0, Expected = "庚午 己卯 己卯 己巳" }
        };

        foreach (var testCase in testCases)
        {
            var solarTime = SolarTime.FromYmdHms(testCase.Year, testCase.Month, testCase.Day,
                                                 testCase.Hour, testCase.Minute, testCase.Second);
            var eightChar = solarTime.GetLunarHour().EightChar;
            var actual = $"{eightChar.Year} {eightChar.Month} {eightChar.Day} {eightChar.Hour}";

            _testOutputHelper.WriteLine($"阳历: {solarTime} => 八字: {actual}");

            // 验证结果是否符合预期
            Assert.Equal(testCase.Expected, actual);
        }
    }

}