using System;
using System.Collections.Generic;
using System.Linq;
using tyme.eightchar;

namespace tyme.culture
{
    /// <summary>
    /// 八字返回结果
    /// </summary>
    public class BaZiResult
    {
        /// <summary>
        /// 八字
        /// </summary>
        public string BaZi { get; set; }

        /// <summary>
        /// 五行数量
        /// </summary>
        public Dictionary<string, int> WuXingCount { get; set; }

        /// <summary>
        /// 藏干数量
        /// </summary>
        public Dictionary<string, int> CangGanCount { get; set; }

        /// <summary>
        /// 十神数量
        /// </summary>
        public Dictionary<string, string> ShiShenCount { get; set; }

        /// <summary>
        /// 五行状态
        /// </summary>
        public Dictionary<string, string> WuXingStatus { get; set; }

        /// <summary>
        /// 五行能量
        /// </summary>
        public Dictionary<string, double> WuXingEnergy { get; set; }

        /// <summary>
        /// 打印
        /// </summary>
        public void PrettyPrint()
        {
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("八字排盘分析结果");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"八字: {BaZi}");
            Console.WriteLine();

            Console.WriteLine("【五行个数】");
            foreach (var kvp in WuXingCount)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}个");
            }

            Console.WriteLine();

            Console.WriteLine("【含藏干数】");
            foreach (var kvp in CangGanCount)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}个");
            }

            Console.WriteLine();

            Console.WriteLine("【十神个数】");
            foreach (var kvp in ShiShenCount)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }

            Console.WriteLine();

            Console.WriteLine("【五行状态】");
            var statusDisplay = new List<string>();
            foreach (var kvp in WuXingStatus)
            {
                statusDisplay.Add($"{kvp.Key}{kvp.Value}");
            }

            Console.WriteLine(string.Join("，", statusDisplay));
            Console.WriteLine();

            Console.WriteLine("【五行能量】");
            foreach (var kvp in WuXingEnergy)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}%");
            }

            Console.WriteLine(new string('=', 50));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BaZiCalculator
    {
        /// <summary>
        /// 天干五行属性
        /// </summary>
        private readonly Dictionary<string, string> _tianGanWuXing = new Dictionary<string, string>
        {
            {"甲", "木"}, {"乙", "木"}, {"丙", "火"}, {"丁", "火"},
            {"戊", "土"}, {"己", "土"}, {"庚", "金"}, {"辛", "金"},
            {"壬", "水"}, {"癸", "水"}
        };

        /// <summary>
        /// 地支藏干表（本气、中气、余气）
        /// </summary>
        private readonly Dictionary<string, string[]> _diZhiCangGan = new Dictionary<string, string[]>
        {
            {"子", new[] {"癸"}},
            {"丑", new[] {"己", "癸", "辛"}},
            {"寅", new[] {"甲", "丙", "戊"}},
            {"卯", new[] {"乙"}},
            {"辰", new[] {"戊", "乙", "癸"}},
            {"巳", new[] {"丙", "庚", "戊"}},
            {"午", new[] {"丁", "己"}},
            {"未", new[] {"己", "丁", "乙"}},
            {"申", new[] {"庚", "壬", "戊"}},
            {"酉", new[] {"辛"}},
            {"戌", new[] {"戊", "辛", "丁"}},
            {"亥", new[] {"壬", "甲"}}
        };

        /// <summary>
        /// 月令五行旺衰状态
        /// </summary>
        private readonly Dictionary<string, Dictionary<string, string>> _yueLingStatus =
            new Dictionary<string, Dictionary<string, string>>
            {
                {"寅", new Dictionary<string, string> {{"旺", "木"}, {"相", "火"}, {"休", "水"}, {"囚", "金"}, {"死", "土"}}},
                {"卯", new Dictionary<string, string> {{"旺", "木"}, {"相", "火"}, {"休", "水"}, {"囚", "金"}, {"死", "土"}}},
                {"辰", new Dictionary<string, string> {{"旺", "土"}, {"相", "金"}, {"休", "火"}, {"囚", "木"}, {"死", "水"}}},
                {"巳", new Dictionary<string, string> {{"旺", "火"}, {"相", "土"}, {"休", "木"}, {"囚", "水"}, {"死", "金"}}},
                {"午", new Dictionary<string, string> {{"旺", "火"}, {"相", "土"}, {"休", "木"}, {"囚", "水"}, {"死", "金"}}},
                {"未", new Dictionary<string, string> {{"旺", "土"}, {"相", "金"}, {"休", "火"}, {"囚", "木"}, {"死", "水"}}},
                {"申", new Dictionary<string, string> {{"旺", "金"}, {"相", "水"}, {"休", "土"}, {"囚", "火"}, {"死", "木"}}},
                {"酉", new Dictionary<string, string> {{"旺", "金"}, {"相", "水"}, {"休", "土"}, {"囚", "火"}, {"死", "木"}}},
                {"戌", new Dictionary<string, string> {{"旺", "土"}, {"相", "金"}, {"休", "火"}, {"囚", "木"}, {"死", "水"}}},
                {"亥", new Dictionary<string, string> {{"旺", "水"}, {"相", "木"}, {"休", "金"}, {"囚", "土"}, {"死", "火"}}},
                {"子", new Dictionary<string, string> {{"旺", "水"}, {"相", "木"}, {"休", "金"}, {"囚", "土"}, {"死", "火"}}},
                {"丑", new Dictionary<string, string> {{"旺", "土"}, {"相", "金"}, {"休", "火"}, {"囚", "木"}, {"死", "水"}}}
            };

        /// <summary>
        /// 通根权重（本气、中气、余气）
        /// </summary>
        private readonly Dictionary<string, double> _tongGenWeight = new Dictionary<string, double>
        {
            {"本气", 1.0}, {"中气", 0.7}, {"余气", 0.4}
        };

        /// <summary>
        /// 宫位权重（年、月、日、时）
        /// </summary>
        private readonly Dictionary<string, double> _gongWeiWeight = new Dictionary<string, double>
        {
            {"年", 0.8}, {"月", 1.5}, {"日", 1.2}, {"时", 1.0}
        };

        /// <summary>
        /// 十神映射（根据日主确定）
        /// </summary>
        private readonly Dictionary<string, string> _shiShenMap = new Dictionary<string, string>
        {
            {"木", "印绶"}, {"火", "比劫"}, {"土", "食伤"}, {"金", "财才"}, {"水", "官杀"}
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baziStr"></param>
        /// <returns></returns>
        public BaZiResult CalculateAll(string baziStr)
        {
            var pillars = ParseBazi(baziStr);
            var yueZhi = pillars[1].Item2; // 月支

            return new BaZiResult
            {
                BaZi = baziStr,
                WuXingCount = CalculateWuXingCount(pillars),
                CangGanCount = CalculateCangGanCount(pillars),
                ShiShenCount = CalculateShiShenCount(pillars, pillars[2].Item1),
                WuXingStatus = GetWuXingStatus(yueZhi),
                WuXingEnergy = CalculateWuXingEnergy(pillars, yueZhi)
            };
        }

        private List<(string, string)> ParseBazi(string baziStr)
        {
            var pillars = new List<(string, string)>();
            var parts = baziStr.Split(' ');

            foreach (var part in parts)
            {
                if (part.Length == 2)
                {
                    pillars.Add((part[0].ToString(), part[1].ToString()));
                }
            }

            return pillars;
        }

        private Dictionary<string, int> CalculateWuXingCount(List<(string, string)> pillars)
        {
            var wuXingCount = new Dictionary<string, int>
            {
                {"木", 0}, {"火", 0}, {"土", 0}, {"金", 0}, {"水", 0}
            };

            // 统计天干
            foreach (var (tianGan, _) in pillars)
            {
                if (_tianGanWuXing.TryGetValue(tianGan, out var wuXing))
                {
                    wuXingCount[wuXing]++;
                }
            }

            // 统计地支本气（每个地支的第一个藏干）
            foreach (var (_, diZhi) in pillars)
            {
                if (_diZhiCangGan.TryGetValue(diZhi, out var cangGanList) && cangGanList.Length > 0)
                {
                    var benQi = cangGanList[0]; // 本气藏干
                    if (_tianGanWuXing.TryGetValue(benQi, out var wuXing))
                    {
                        wuXingCount[wuXing]++;
                    }
                }
            }

            return wuXingCount;
        }

        private Dictionary<string, int> CalculateCangGanCount(List<(string, string)> pillars)
        {
            var cangGanCount = new Dictionary<string, int>
            {
                {"木", 0}, {"火", 0}, {"土", 0}, {"金", 0}, {"水", 0}
            };

            // 统计天干
            foreach (var (tianGan, _) in pillars)
            {
                if (_tianGanWuXing.TryGetValue(tianGan, out var wuXing))
                {
                    cangGanCount[wuXing]++;
                }
            }

            // 统计所有地支藏干
            foreach (var (_, diZhi) in pillars)
            {
                if (_diZhiCangGan.TryGetValue(diZhi, out var cangGanList))
                {
                    foreach (var cangGan in cangGanList)
                    {
                        if (_tianGanWuXing.TryGetValue(cangGan, out var wuXing))
                        {
                            cangGanCount[wuXing]++;
                        }
                    }
                }
            }

            return cangGanCount;
        }

        private Dictionary<string, string> CalculateShiShenCount(List<(string, string)> pillars, string riZhu)
        {
            var cangGanCount = CalculateCangGanCount(pillars);
            var shiShenCount = new Dictionary<string, string>();

            foreach (var kvp in cangGanCount)
            {
                var wuXing = kvp.Key;
                var count = kvp.Value;
                _shiShenMap.TryGetValue(wuXing, out var shiShen);
                shiShenCount[wuXing] = $"{count}个{shiShen ?? ""}";
            }

            return shiShenCount;
        }

        private Dictionary<string, string> GetWuXingStatus(string yueZhi)
        {
            if (!_yueLingStatus.TryGetValue(yueZhi, out var statusMap))
                return new Dictionary<string, string>();

            // 反转映射：五行->状态
            var wuXingStatus = new Dictionary<string, string>();
            foreach (var kvp in statusMap)
            {
                var status = kvp.Key;
                var wuXing = kvp.Value;
                wuXingStatus[wuXing] = status;
            }

            return wuXingStatus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pillars"></param>
        /// <param name="yueZhi"></param>
        /// <returns></returns>
        private Dictionary<string, double> CalculateWuXingEnergy(List<(string, string)> pillars, string yueZhi)
        {
            var wuXingEnergyRaw = new Dictionary<string, double>
            {
                {"木", 0.0}, {"火", 0.0}, {"土", 0.0}, {"金", 0.0}, {"水", 0.0}
            };

            // 获取月令状态权重
            var yueStatus = _yueLingStatus.TryGetValue(yueZhi, out var status)
                ? status
                : new Dictionary<string, string>();
            var statusWeight = new Dictionary<string, double>
            {
                {"旺", 2.0}, {"相", 1.5}, {"休", 1.0}, {"囚", 0.6}, {"死", 0.3}
            };

            var gongWeiNames = new[] {"年", "月", "日", "时"};

            for (int i = 0; i < pillars.Count; i++)
            {
                var (tianGan, diZhi) = pillars[i];
                var gongWei = gongWeiNames[i];

                // 处理天干
                if (_tianGanWuXing.TryGetValue(tianGan, out var tianGanWuXing))
                {
                    // 基础权重 + 宫位权重
                    var weight = 1.0 * _gongWeiWeight[gongWei];

                    // 月令状态权重
                    var statusKey = yueStatus.FirstOrDefault(x => x.Value == tianGanWuXing).Key;
                    if (statusKey != null && statusWeight.TryGetValue(statusKey, out var statusWt))
                    {
                        weight *= statusWt;
                    }

                    wuXingEnergyRaw[tianGanWuXing] += weight;
                }

                // 处理地支藏干
                if (_diZhiCangGan.TryGetValue(diZhi, out var cangGanList))
                {
                    for (int j = 0; j < cangGanList.Length; j++)
                    {
                        var cangGan = cangGanList[j];
                        if (_tianGanWuXing.TryGetValue(cangGan, out var cangGanWuXing))
                        {
                            // 确定藏干权重（本气/中气/余气）
                            string cangGanType;
                            if (j == 0) cangGanType = "本气";
                            else if (j == 1) cangGanType = "中气";
                            else cangGanType = "余气";

                            var weight = _tongGenWeight[cangGanType] * _gongWeiWeight[gongWei];

                            // 月令状态权重
                            var statusKey = yueStatus.FirstOrDefault(x => x.Value == cangGanWuXing).Key;
                            if (statusKey != null && statusWeight.TryGetValue(statusKey, out var statusWt))
                            {
                                weight *= statusWt;
                            }

                            wuXingEnergyRaw[cangGanWuXing] += weight;
                        }
                    }
                }
            }

            // 转换为百分比
            var totalEnergy = wuXingEnergyRaw.Values.Sum();
            if (totalEnergy > 0)
            {
                var wuXingEnergyPercent = new Dictionary<string, double>();
                foreach (var kvp in wuXingEnergyRaw)
                {
                    wuXingEnergyPercent[kvp.Key] = Math.Round(kvp.Value / totalEnergy * 100, 1);
                }

                return wuXingEnergyPercent;
            }
            else
            {
                return new Dictionary<string, double>
                {
                    {"木", 0.0}, {"火", 0.0}, {"土", 0.0}, {"金", 0.0}, {"水", 0.0}
                };
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class BaZiCalculatorExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eightChar"></param>
        /// <returns></returns>
        public static BaZiResult GetBaZi(this EightChar eightChar)
        {
            var year = eightChar.Year;
            var month = eightChar.Month;
            var day = eightChar.Day;
            var hour = eightChar.Hour;
            var calculator = new BaZiCalculator();
            return calculator.CalculateAll($"{year} {month} {day} {hour}");
        }
    }
}