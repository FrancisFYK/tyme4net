using tyme.culture;
using tyme.eightchar;
using tyme.enums;
using tyme.solar;

namespace test;

public class BaZiCalculatorTest
{
    [Fact]
    public void Test0()
    {
        var childLimit = ChildLimit.FromSolarTime(SolarTime.FromYmdHms(1996, 02, 12, 04, 37, 0), Gender.Man);
        var eightCharFromSolar = childLimit.EightChar;
        var result = eightCharFromSolar.GetBaZi();
        Assert.NotNull(result);
        result.PrettyPrint();
    }
}