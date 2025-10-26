using System;
using System.Collections.Generic;
using System.Linq;

using tyme.eightchar;
using tyme.sixtycycle;

namespace tyme.culture
{
    /// <summary>
    /// 个人八字神煞
    /// </summary>
    public static class PersonalGod
    {
        /// <summary>
        /// 六十甲子表
        /// </summary>
        private static readonly string[] JIAZI = new[]
        {
            "　",
            "甲子", "乙丑", "丙寅", "丁卯", "戊辰", "己巳", "庚午", "辛未", "壬申", "癸酉",
            "甲戌", "乙亥", "丙子", "丁丑", "戊寅", "己卯", "庚辰", "辛巳", "壬午", "癸未",
            "甲申", "乙酉", "丙戌", "丁亥", "戊子", "己丑", "庚寅", "辛卯", "壬辰", "癸巳",
            "甲午", "乙未", "丙申", "丁酉", "戊戌", "己亥", "庚子", "辛丑", "壬寅", "癸卯",
            "甲辰", "乙巳", "丙午", "丁未", "戊申", "己酉", "庚戌", "辛亥", "壬子", "癸丑",
            "甲寅", "乙卯", "丙辰", "丁巳", "戊午", "己未", "庚申", "辛酉", "壬戌", "癸亥"
        };

        /// <summary>
        /// 丧门地支对应表
        /// </summary>
        private static readonly string[] SANG_MEN = new[]
        {
            "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥", "子", "丑"
        };

        /// <summary>
        /// 吊客地支对应表
        /// </summary>
        private static readonly string[] DIAO_KE = new[]
        {
            "戌", "亥", "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉"
        };

        /// <summary>
        /// 披麻地支对应表
        /// </summary>
        private static readonly string[] PI_MA = new[]
        {
            "酉", "戌", "亥", "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申"
        };

        /// <summary>
        /// 柱位枚举
        /// </summary>
        public enum PillarPosition
        {
            /// <summary>年柱</summary>
            Year = 1,
            /// <summary>月柱</summary>
            Month = 2,
            /// <summary>日柱</summary>
            Day = 3,
            /// <summary>时柱</summary>
            Hour = 4,
            /// <summary>大运</summary>
            GreatLuck = 5,
            /// <summary>流年</summary>
            YearlyLuck = 6,
            /// <summary>流月</summary>
            MonthlyLuck = 7,
            /// <summary>流时</summary>
            HourlyLuck = 8
        }

        /// <summary>
        /// 根据干支和八字信息，查询神煞组合
        /// </summary>
        /// <param name="ganZhi">要查询神煞的某柱干支，例如年柱为甲寅，则参数值为：甲寅</param>
        /// <param name="eightChar">八字对象</param>
        /// <param name="isMale">性别，true为男，否则为女</param>
        /// <param name="position">查的是哪一柱</param>
        /// <param name="yearSound">年柱纳音，查询学堂、词馆神煞时用。例如：海中金</param>
        /// <returns>返回神煞名称列表</returns>
        public static List<string> QueryShenSha(SixtyCycle ganZhi, EightChar eightChar, bool isMale,
            PillarPosition position, string yearSound)
        {
            var shenShaList = new List<string>();

            var yearStem = eightChar.Year.HeavenStem;
            var yearBranch = eightChar.Year.EarthBranch;
            var monthStem = eightChar.Month.HeavenStem;
            var monthBranch = eightChar.Month.EarthBranch;
            var dayStem = eightChar.Day.HeavenStem;
            var dayBranch = eightChar.Day.EarthBranch;
            var hourStem = eightChar.Hour.HeavenStem;
            var hourBranch = eightChar.Hour.EarthBranch;

            var stem = ganZhi.HeavenStem;
            var branch = ganZhi.EarthBranch;

            // 天乙贵人
            if (IsTianYiGuiRen(dayStem, branch) || IsTianYiGuiRen(yearStem, branch))
            {
                shenShaList.Add("天乙贵人");
            }

            // 太极贵人
            if (IsTaiJiGuiRen(dayStem, branch) || IsTaiJiGuiRen(yearStem, branch))
            {
                shenShaList.Add("太极贵人");
            }

            // 天德贵人
            if (IsTianDeGuiRen(monthBranch, stem) || IsTianDeGuiRen(monthBranch, branch))
            {
                shenShaList.Add("天德贵人");
            }

            // 月德贵人
            if (IsYueDe(monthBranch, stem))
            {
                shenShaList.Add("月德贵人");
            }

            // 德秀贵人
            var allStems = new[] { yearStem, monthStem, dayStem, hourStem };
            if (IsDeXiuGuiRen(monthBranch, allStems))
            {
                shenShaList.Add("德秀贵人");
            }

            // 天德合
            if (IsTianDeHe(monthBranch, stem) || IsTianDeHe(monthBranch, branch))
            {
                shenShaList.Add("天德合");
            }

            // 月德合
            if (IsYueDeHe(monthBranch, stem))
            {
                shenShaList.Add("月德合");
            }

            // 福星贵人
            if (IsFuXing(yearStem, branch) || IsFuXing(dayStem, branch))
            {
                shenShaList.Add("福星贵人");
            }

            // 文昌贵人
            if (IsWenChang(dayStem, branch) || IsWenChang(yearStem, branch))
            {
                shenShaList.Add("文昌贵人");
            }

            // 学堂（不查日柱）
            if (position != PillarPosition.Day && IsXueTang(yearSound, stem, branch))
            {
                shenShaList.Add("学堂");
            }

            // 词馆（不查日柱）
            if (position != PillarPosition.Day && IsCiGuan(yearSound, stem, branch))
            {
                shenShaList.Add("词馆");
            }

            // 魁罡（仅查日柱）
            if (position == PillarPosition.Day && IsKuiGang(dayStem, dayBranch))
            {
                shenShaList.Add("魁罡");
            }

            // 国印贵人
            if (IsGuoYin(dayStem, branch) || IsGuoYin(yearStem, branch))
            {
                shenShaList.Add("国印贵人");
            }

            // 驿马
            if ((position != PillarPosition.Day && IsYiMa(dayBranch, branch)) ||
                (position != PillarPosition.Year && IsYiMa(yearBranch, branch)))
            {
                shenShaList.Add("驿马");
            }

            // 华盖
            if ((position != PillarPosition.Day && IsHuaGai(dayBranch, branch)) ||
                (position != PillarPosition.Year && IsHuaGai(yearBranch, branch)))
            {
                shenShaList.Add("华盖");
            }

            // 将星
            if ((position != PillarPosition.Day && IsJiangXing(dayBranch, branch)) ||
                (position != PillarPosition.Year && IsJiangXing(yearBranch, branch)))
            {
                shenShaList.Add("将星");
            }

            // 金舆
            if (IsJinYu(dayStem, branch) || IsJinYu(yearStem, branch))
            {
                shenShaList.Add("金舆");
            }

            // 金神
            if ((position == PillarPosition.Day && IsJinShen(dayStem, dayBranch)) ||
                (position == PillarPosition.Hour && IsJinShen(hourStem, hourBranch)))
            {
                shenShaList.Add("金神");
            }

            // 五鬼
            if (position != PillarPosition.Month && IsWuGui(monthBranch, branch))
            {
                shenShaList.Add("五鬼");
            }

            // 天医
            if (position != PillarPosition.Month && IsTianYi(monthBranch, branch))
            {
                shenShaList.Add("天医");
            }

            // 禄神
            if (IsLuShen(dayStem, branch))
            {
                shenShaList.Add("禄神");
            }

            // 天赦
            if (IsTianShe(monthBranch, dayStem, dayBranch))
            {
                shenShaList.Add("天赦");
            }

            // 红鸾
            if (position != PillarPosition.Year && IsHongLuan(yearBranch, branch))
            {
                shenShaList.Add("红鸾");
            }

            // 天喜
            if (position != PillarPosition.Year && IsTianXi(yearBranch, branch))
            {
                shenShaList.Add("天喜");
            }

            // 流霞
            if (IsLiuXia(dayStem, branch))
            {
                shenShaList.Add("流霞");
            }

            // 红艳
            if (IsHongYan(dayStem, branch))
            {
                shenShaList.Add("红艳煞");
            }

            // 天罗
            if ((position != PillarPosition.Day && IsTianLuo(dayBranch, branch)) ||
                (position != PillarPosition.Year && IsTianLuo(yearBranch, branch)))
            {
                shenShaList.Add("天罗");
            }

            // 地网
            if ((position != PillarPosition.Day && IsDiWang(dayBranch, branch)) ||
                (position != PillarPosition.Year && IsDiWang(yearBranch, branch)))
            {
                shenShaList.Add("地网");
            }

            // 羊刃
            if (IsYangRen(dayStem, branch))
            {
                shenShaList.Add("羊刃");
            }

            // 飞刃
            if (IsFeiRen(dayStem, branch))
            {
                shenShaList.Add("飞刃");
            }

            // 血刃
            if (IsXueRen(monthBranch, branch))
            {
                shenShaList.Add("血刃");
            }

            // 八专（仅查日柱）
            if (position == PillarPosition.Day && IsBaZhuan(dayStem, dayBranch))
            {
                shenShaList.Add("八专");
            }

            // 九丑（仅查日柱）
            if (position == PillarPosition.Day && IsJiuChou(dayStem, dayBranch))
            {
                shenShaList.Add("九丑日");
            }

            // 劫煞
            if (IsJieSha(dayBranch, branch) || IsJieSha(yearBranch, branch))
            {
                shenShaList.Add("劫煞");
            }

            // 灾煞
            if (IsZaiSha(yearBranch, branch))
            {
                shenShaList.Add("灾煞");
            }

            // 元辰
            if (position != PillarPosition.Year &&
                IsYuanChen(yearBranch, branch, isMale, IsYangStem(yearStem)))
            {
                shenShaList.Add("元辰");
            }

            // 空亡
            if ((position != PillarPosition.Day && IsKongWang(eightChar.Day, branch)) ||
                (position != PillarPosition.Year && IsKongWang(eightChar.Year, branch)))
            {
                shenShaList.Add("空亡");
            }

            // 童子
            if ((position == PillarPosition.Day && IsTongZi(monthBranch, yearSound, dayBranch)) ||
                (position == PillarPosition.Hour && IsTongZi(monthBranch, yearSound, hourBranch)))
            {
                shenShaList.Add("童子煞");
            }

            // 天厨
            if (IsTianChu(yearStem, dayStem, branch))
            {
                shenShaList.Add("天厨贵人");
            }

            // 孤辰
            if (position != PillarPosition.Year && IsGuChen(yearBranch, branch))
            {
                shenShaList.Add("孤辰");
            }

            // 寡宿
            if (position != PillarPosition.Year && IsGuaSu(yearBranch, branch))
            {
                shenShaList.Add("寡宿");
            }

            // 亡神
            if ((position != PillarPosition.Day && IsWangShen(dayBranch, branch)) ||
                (position != PillarPosition.Year && IsWangShen(yearBranch, branch)))
            {
                shenShaList.Add("亡神");
            }

            // 十恶大败（仅查日柱）
            if (position == PillarPosition.Day && IsShiEDaBai(dayStem, dayBranch))
            {
                shenShaList.Add("十恶大败");
            }

            // 桃花
            if (IsTaoHua(dayBranch, branch) || IsTaoHua(yearBranch, branch))
            {
                shenShaList.Add("桃花");
            }

            // 孤鸾（仅查日柱）
            if (position == PillarPosition.Day && IsGuLuan(dayStem, dayBranch))
            {
                shenShaList.Add("孤鸾");
            }

            // 阴差阳错（仅查日柱）
            if (position == PillarPosition.Day && IsYinYangChaCuo(dayStem, dayBranch))
            {
                shenShaList.Add("阴差阳错");
            }

            // 四废（仅查日柱）
            if (position == PillarPosition.Day && IsSiFei(monthBranch, dayStem, dayBranch))
            {
                shenShaList.Add("四废");
            }

            // 丧门
            if (position != PillarPosition.Year && IsSangMen(yearBranch, branch))
            {
                shenShaList.Add("丧门");
            }

            // 吊客
            if (position != PillarPosition.Year && IsDiaoKe(yearBranch, branch))
            {
                shenShaList.Add("吊客");
            }

            // 披麻
            if (position != PillarPosition.Year && IsPiMa(yearBranch, branch))
            {
                shenShaList.Add("披麻");
            }

            // 十灵（仅查日柱）
            if (position == PillarPosition.Day && IsShiLing(dayStem, dayBranch))
            {
                shenShaList.Add("十灵");
            }

            return shenShaList;
        }

        #region 神煞判断方法

        /// <summary>
        /// 天厨贵人
        /// 查法：以年干、日干查余四支
        /// 丙干见巳，丁干见午，戊干见申，己干见酉
        /// 庚干见亥，辛干见子，壬干见寅，癸干见卯
        /// </summary>
        private static bool IsTianChu(HeavenStem yearStem, HeavenStem dayStem, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 2, 5 },  // 丙见巳
                { 3, 6 },  // 丁见午
                { 4, 8 },  // 戊见申
                { 5, 9 },  // 己见酉
                { 6, 11 }, // 庚见亥
                { 7, 0 },  // 辛见子
                { 8, 2 },  // 壬见寅
                { 9, 3 }   // 癸见卯
            };

            return (rules.ContainsKey(yearStem.Index) && rules[yearStem.Index] == branch.Index) ||
                   (rules.ContainsKey(dayStem.Index) && rules[dayStem.Index] == branch.Index);
        }

        /// <summary>
        /// 德秀贵人
        /// 寅午戌月，丙丁为德，戊癸为秀
        /// 申子辰月，壬癸戊己为德，丙辛甲己为秀
        /// 巳酉丑月，庚辛为德，乙庚为秀
        /// 亥卯未月，甲乙为德，丁壬为秀
        /// </summary>
        private static bool IsDeXiuGuiRen(EarthBranch monthBranch, HeavenStem[] allStems)
        {
            bool HasStem(params int[] stemIndices)
            {
                return allStems.Any(s => stemIndices.Contains(s.Index));
            }

            bool HasCombination(int stem1, int stem2)
            {
                return allStems.Any(s => s.Index == stem1) && allStems.Any(s => s.Index == stem2);
            }

            // 火局：寅午戌
            if (new[] { 2, 6, 10 }.Contains(monthBranch.Index))
            {
                // 德：丙丁，秀：戊癸
                return HasStem(2, 3) && HasCombination(4, 9);
            }

            // 水局：申子辰
            if (new[] { 8, 0, 4 }.Contains(monthBranch.Index))
            {
                // 德：壬癸戊己，秀：丙辛或甲己
                return HasStem(8, 9, 4, 5) && (HasCombination(2, 7) || HasCombination(0, 5));
            }

            // 金局：巳酉丑
            if (new[] { 5, 9, 1 }.Contains(monthBranch.Index))
            {
                // 德：庚辛，秀：乙庚
                return HasStem(6, 7) && HasCombination(1, 6);
            }

            // 木局：亥卯未
            if (new[] { 11, 3, 7 }.Contains(monthBranch.Index))
            {
                // 德：甲乙，秀：丁壬
                return HasStem(0, 1) && HasCombination(3, 8);
            }

            return false;
        }

        /// <summary>
        /// 空亡
        /// 甲子旬在戌亥，甲戌旬在申酉，甲申旬在午未
        /// 甲午旬在辰巳，甲辰旬在寅卯，甲寅旬在子丑
        /// </summary>
        private static bool IsKongWang(SixtyCycle pillar, EarthBranch branch)
        {
            var extraBranches = pillar.ExtraEarthBranches;
            return extraBranches != null && extraBranches.Any(eb => eb.Index == branch.Index);
        }

        /// <summary>
        /// 桃花
        /// 申子辰在酉，寅午戌在卯，巳酉丑在午，亥卯未在子
        /// </summary>
        private static bool IsTaoHua(EarthBranch baseBranch, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 8, 9 }, { 0, 9 }, { 4, 9 },  // 申子辰见酉
                { 2, 3 }, { 6, 3 }, { 10, 3 }, // 寅午戌见卯
                { 5, 6 }, { 9, 6 }, { 1, 6 },  // 巳酉丑见午
                { 11, 0 }, { 3, 0 }, { 7, 0 }  // 亥卯未见子
            };

            return rules.ContainsKey(baseBranch.Index) && rules[baseBranch.Index] == branch.Index;
        }

        /// <summary>
        /// 阴差阳错
        /// 丙子、丁丑、戊寅、辛卯、壬辰、癸巳
        /// 丙午、丁未、戊申、辛酉、壬戌、癸亥
        /// </summary>
        private static bool IsYinYangChaCuo(HeavenStem dayStem, EarthBranch dayBranch)
        {
            var rules = new Dictionary<int, int[]>
            {
                { 2, new[] { 0, 6 } },   // 丙见子、午
                { 3, new[] { 1, 7 } },   // 丁见丑、未
                { 4, new[] { 2, 8 } },   // 戊见寅、申
                { 7, new[] { 3, 9 } },   // 辛见卯、酉
                { 8, new[] { 4, 10 } },  // 壬见辰、戌
                { 9, new[] { 5, 11 } }   // 癸见巳、亥
            };

            return rules.ContainsKey(dayStem.Index) && rules[dayStem.Index].Contains(dayBranch.Index);
        }

        /// <summary>
        /// 天乙贵人
        /// 甲戊并牛羊，乙己鼠猴乡，丙丁猪鸡位，壬癸兔蛇藏，庚辛逢虎马
        /// </summary>
        private static bool IsTianYiGuiRen(HeavenStem stem, EarthBranch branch)
        {
            var rules = new Dictionary<int, int[]>
            {
                { 0, new[] { 1, 7 } },   // 甲见丑、未
                { 4, new[] { 1, 7 } },   // 戊见丑、未
                { 1, new[] { 0, 8 } },   // 乙见子、申
                { 5, new[] { 0, 8 } },   // 己见子、申
                { 2, new[] { 11, 9 } },  // 丙见亥、酉
                { 3, new[] { 11, 9 } },  // 丁见亥、酉
                { 8, new[] { 3, 5 } },   // 壬见卯、巳
                { 9, new[] { 3, 5 } },   // 癸见卯、巳
                { 6, new[] { 2, 6 } },   // 庚见寅、午
                { 7, new[] { 2, 6 } }    // 辛见寅、午
            };

            return rules.ContainsKey(stem.Index) && rules[stem.Index].Contains(branch.Index);
        }

        /// <summary>
        /// 太极贵人
        /// 甲乙生人子午中，丙丁鸡兔定亨通
        /// 戊己两干临四季，庚辛寅亥禄丰隆
        /// 壬癸巳申偏喜美
        /// </summary>
        private static bool IsTaiJiGuiRen(HeavenStem stem, EarthBranch branch)
        {
            var rules = new Dictionary<int, int[]>
            {
                { 0, new[] { 0, 6 } },   // 甲见子、午
                { 1, new[] { 0, 6 } },   // 乙见子、午
                { 2, new[] { 9, 3 } },   // 丙见酉、卯
                { 3, new[] { 9, 3 } },   // 丁见酉、卯
                { 6, new[] { 2, 11 } },  // 庚见寅、亥
                { 7, new[] { 2, 11 } },  // 辛见寅、亥
                { 8, new[] { 5, 8 } },   // 壬见巳、申
                { 9, new[] { 5, 8 } }    // 癸见巳、申
            };

            if (rules.ContainsKey(stem.Index))
            {
                return rules[stem.Index].Contains(branch.Index);
            }

            // 戊己见四季（辰戌丑未）
            if ((stem.Index == 4 || stem.Index == 5) &&
                new[] { 1, 4, 7, 10 }.Contains(branch.Index))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 文昌贵人
        /// 甲乙巳午报君知，丙戊申宫丁己鸡
        /// 庚猪辛鼠壬逢虎，癸人见卯入云梯
        /// </summary>
        private static bool IsWenChang(HeavenStem stem, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 0, 5 },  // 甲见巳
                { 1, 6 },  // 乙见午
                { 2, 8 },  // 丙见申
                { 4, 8 },  // 戊见申
                { 3, 9 },  // 丁见酉
                { 5, 9 },  // 己见酉
                { 6, 11 }, // 庚见亥
                { 7, 0 },  // 辛见子
                { 8, 2 },  // 壬见寅
                { 9, 3 }   // 癸见卯
            };

            return rules.ContainsKey(stem.Index) && rules[stem.Index] == branch.Index;
        }

        /// <summary>
        /// 魁罡贵人
        /// 壬辰庚戌与庚辰，戊戌魁罡四座神
        /// </summary>
        private static bool IsKuiGang(HeavenStem dayStem, EarthBranch dayBranch)
        {
            return (dayStem.Index == 8 && dayBranch.Index == 4) ||  // 壬辰
                   (dayStem.Index == 6 && dayBranch.Index == 10) || // 庚戌
                   (dayStem.Index == 6 && dayBranch.Index == 4) ||  // 庚辰
                   (dayStem.Index == 4 && dayBranch.Index == 10);   // 戊戌
        }

        /// <summary>
        /// 驿马
        /// 申子辰马在寅，寅午戌马在申，巳酉丑马在亥，亥卯未马在巳
        /// </summary>
        private static bool IsYiMa(EarthBranch baseBranch, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 8, 2 }, { 0, 2 }, { 4, 2 },   // 申子辰见寅
                { 2, 8 }, { 6, 8 }, { 10, 8 },  // 寅午戌见申
                { 5, 11 }, { 9, 11 }, { 1, 11 },// 巳酉丑见亥
                { 11, 5 }, { 3, 5 }, { 7, 5 }   // 亥卯未见巳
            };

            return rules.ContainsKey(baseBranch.Index) && rules[baseBranch.Index] == branch.Index;
        }

        /// <summary>
        /// 华盖
        /// 寅午戌见戌，亥卯未见未，申子辰见辰，巳酉丑见丑
        /// </summary>
        private static bool IsHuaGai(EarthBranch baseBranch, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 8, 4 }, { 0, 4 }, { 4, 4 },   // 申子辰见辰
                { 2, 10 }, { 6, 10 }, { 10, 10 },// 寅午戌见戌
                { 5, 1 }, { 9, 1 }, { 1, 1 },   // 巳酉丑见丑
                { 11, 7 }, { 3, 7 }, { 7, 7 }   // 亥卯未见未
            };

            return rules.ContainsKey(baseBranch.Index) && rules[baseBranch.Index] == branch.Index;
        }

        /// <summary>
        /// 金舆
        /// 甲龙乙蛇丙戊羊，丁己猴歌庚犬方
        /// 辛猪壬牛癸逢虎，凡人遇此福气昌
        /// </summary>
        private static bool IsJinYu(HeavenStem stem, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 0, 4 },  // 甲见辰
                { 1, 5 },  // 乙见巳
                { 2, 7 },  // 丙见未
                { 4, 7 },  // 戊见未
                { 3, 8 },  // 丁见申
                { 5, 8 },  // 己见申
                { 6, 10 }, // 庚见戌
                { 7, 11 }, // 辛见亥
                { 8, 1 },  // 壬见丑
                { 9, 2 }   // 癸见寅
            };

            return rules.ContainsKey(stem.Index) && rules[stem.Index] == branch.Index;
        }

        /// <summary>
        /// 金神
        /// 乙丑、己巳、癸酉
        /// </summary>
        private static bool IsJinShen(HeavenStem stem, EarthBranch branch)
        {
            return (stem.Index == 1 && branch.Index == 1) ||  // 乙丑
                   (stem.Index == 5 && branch.Index == 5) ||  // 己巳
                   (stem.Index == 9 && branch.Index == 9);    // 癸酉
        }

        /// <summary>
        /// 五鬼星
        /// 子月见辰，丑月见巳，寅月见午，卯月见未
        /// 辰月见申，巳月见酉，午月见戌，未月见亥
        /// 申月见子，酉月见丑，戌月见寅，亥月见卯
        /// </summary>
        private static bool IsWuGui(EarthBranch monthBranch, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 0, 4 }, { 1, 5 }, { 2, 6 }, { 3, 7 },
                { 4, 8 }, { 5, 9 }, { 6, 10 }, { 7, 11 },
                { 8, 0 }, { 9, 1 }, { 10, 2 }, { 11, 3 }
            };

            return rules.ContainsKey(monthBranch.Index) && rules[monthBranch.Index] == branch.Index;
        }

        /// <summary>
        /// 国印贵人
        /// 甲见戌，乙见亥，丙见丑，丁见寅，戊见丑
        /// 己见寅，庚见辰，辛见巳，壬见未，癸见申
        /// </summary>
        private static bool IsGuoYin(HeavenStem stem, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 0, 10 }, // 甲见戌
                { 1, 11 }, // 乙见亥
                { 2, 1 },  // 丙见丑
                { 3, 2 },  // 丁见寅
                { 4, 1 },  // 戊见丑
                { 5, 2 },  // 己见寅
                { 6, 4 },  // 庚见辰
                { 7, 5 },  // 辛见巳
                { 8, 7 },  // 壬见未
                { 9, 8 }   // 癸见申
            };

            return rules.ContainsKey(stem.Index) && rules[stem.Index] == branch.Index;
        }

        /// <summary>
        /// 将星
        /// 寅午戌见午，巳酉丑见酉，申子辰见子，亥卯未见卯
        /// </summary>
        private static bool IsJiangXing(EarthBranch baseBranch, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 8, 0 }, { 0, 0 }, { 4, 0 },   // 申子辰见子
                { 2, 6 }, { 6, 6 }, { 10, 6 },  // 寅午戌见午
                { 5, 9 }, { 9, 9 }, { 1, 9 },   // 巳酉丑见酉
                { 11, 3 }, { 3, 3 }, { 7, 3 }   // 亥卯未见卯
            };

            return rules.ContainsKey(baseBranch.Index) && rules[baseBranch.Index] == branch.Index;
        }

        /// <summary>
        /// 孤鸾煞
        /// 甲寅、乙巳、丙午、丁巳、戊午、戊申、辛亥、壬子
        /// </summary>
        private static bool IsGuLuan(HeavenStem dayStem, EarthBranch dayBranch)
        {
            return (dayStem.Index == 0 && dayBranch.Index == 2) ||  // 甲寅
                   (dayStem.Index == 1 && dayBranch.Index == 5) ||  // 乙巳
                   (dayStem.Index == 2 && dayBranch.Index == 6) ||  // 丙午
                   (dayStem.Index == 3 && dayBranch.Index == 5) ||  // 丁巳
                   (dayStem.Index == 4 && dayBranch.Index == 6) ||  // 戊午
                   (dayStem.Index == 4 && dayBranch.Index == 8) ||  // 戊申
                   (dayStem.Index == 7 && dayBranch.Index == 11) || // 辛亥
                   (dayStem.Index == 8 && dayBranch.Index == 0);    // 壬子
        }

        /// <summary>
        /// 丧门
        /// </summary>
        private static bool IsSangMen(EarthBranch yearBranch, EarthBranch branch)
        {
            return CheckRelation(yearBranch, branch, SANG_MEN);
        }

        /// <summary>
        /// 吊客
        /// </summary>
        private static bool IsDiaoKe(EarthBranch yearBranch, EarthBranch branch)
        {
            return CheckRelation(yearBranch, branch, DIAO_KE);
        }

        /// <summary>
        /// 披麻
        /// </summary>
        private static bool IsPiMa(EarthBranch yearBranch, EarthBranch branch)
        {
            return CheckRelation(yearBranch, branch, PI_MA);
        }

        /// <summary>
        /// 关系查询通用方法
        /// </summary>
        private static bool CheckRelation(EarthBranch yearBranch, EarthBranch branch, string[] targetArray)
        {
            if (yearBranch.Index == -1) return false;
            return targetArray[yearBranch.Index] == branch.GetName();
        }

        /// <summary>
        /// 天德贵人
        /// 正月见丁，二月见申，三月见壬，四月见辛
        /// 五月见亥，六月见甲，七月见癸，八月见寅
        /// 九月见丙，十月见乙，十一月见巳，十二月见庚
        /// </summary>
        private static bool IsTianDeGuiRen(EarthBranch monthBranch, HeavenStem stem)
        {
            var rules = new Dictionary<int, int>
            {
                { 2, 3 },  // 寅月见丁
                { 3, 8 },  // 卯月见申（地支）
                { 4, 8 },  // 辰月见壬
                { 5, 7 },  // 巳月见辛
                { 6, 11 }, // 午月见亥（地支）
                { 7, 0 },  // 未月见甲
                { 8, 9 },  // 申月见癸
                { 9, 2 },  // 酉月见寅（地支）
                { 10, 2 }, // 戌月见丙
                { 11, 1 }, // 亥月见乙
                { 0, 5 },  // 子月见巳（地支）
                { 1, 6 }   // 丑月见庚
            };

            return rules.ContainsKey(monthBranch.Index) && rules[monthBranch.Index] == stem.Index;
        }

        /// <summary>
        /// 天德贵人（地支版本）
        /// </summary>
        private static bool IsTianDeGuiRen(EarthBranch monthBranch, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 3, 8 },  // 卯月见申
                { 6, 11 }, // 午月见亥
                { 9, 2 },  // 酉月见寅
                { 0, 5 }   // 子月见巳
            };

            return rules.ContainsKey(monthBranch.Index) && rules[monthBranch.Index] == branch.Index;
        }

        /// <summary>
        /// 元辰
        /// 阳男阴女与阴男阳女查法不同
        /// </summary>
        private static bool IsYuanChen(EarthBranch yearBranch, EarthBranch branch, bool isMale, bool isYangStem)
        {
            var isYangMaleOrYinFemale = isMale == isYangStem;

            var yangRules = new Dictionary<int, int>
            {
                { 0, 7 }, { 1, 8 }, { 2, 9 }, { 3, 10 },
                { 4, 11 }, { 5, 0 }, { 6, 1 }, { 7, 2 },
                { 8, 3 }, { 9, 4 }, { 10, 5 }, { 11, 6 }
            };

            var yinRules = new Dictionary<int, int>
            {
                { 0, 5 }, { 1, 6 }, { 2, 7 }, { 3, 8 },
                { 4, 9 }, { 5, 10 }, { 6, 11 }, { 7, 0 },
                { 8, 1 }, { 9, 2 }, { 10, 3 }, { 11, 4 }
            };

            var rules = isYangMaleOrYinFemale ? yangRules : yinRules;
            return rules.ContainsKey(yearBranch.Index) && rules[yearBranch.Index] == branch.Index;
        }

        /// <summary>
        /// 月德贵人
        /// 寅午戌月见丙，申子辰月见壬，亥卯未月见甲，巳酉丑月见庚
        /// </summary>
        private static bool IsYueDe(EarthBranch monthBranch, HeavenStem stem)
        {
            var rules = new Dictionary<int, int>
            {
                { 2, 2 }, { 6, 2 }, { 10, 2 },  // 寅午戌见丙
                { 8, 8 }, { 0, 8 }, { 4, 8 },   // 申子辰见壬
                { 11, 0 }, { 3, 0 }, { 7, 0 },  // 亥卯未见甲
                { 5, 6 }, { 9, 6 }, { 1, 6 }    // 巳酉丑见庚
            };

            return rules.ContainsKey(monthBranch.Index) && rules[monthBranch.Index] == stem.Index;
        }

        /// <summary>
        /// 天赦
        /// 春戊寅，夏甲午，秋戊申，冬甲子
        /// </summary>
        private static bool IsTianShe(EarthBranch monthBranch, HeavenStem dayStem, EarthBranch dayBranch)
        {
            // 春季：寅卯辰月生戊寅日
            if (new[] { 2, 3, 4 }.Contains(monthBranch.Index))
            {
                return dayStem.Index == 4 && dayBranch.Index == 2;
            }

            // 夏季：巳午未月生甲午日
            if (new[] { 5, 6, 7 }.Contains(monthBranch.Index))
            {
                return dayStem.Index == 0 && dayBranch.Index == 6;
            }

            // 秋季：申酉戌月生戊申日
            if (new[] { 8, 9, 10 }.Contains(monthBranch.Index))
            {
                return dayStem.Index == 4 && dayBranch.Index == 8;
            }

            // 冬季：亥子丑月生甲子日
            if (new[] { 11, 0, 1 }.Contains(monthBranch.Index))
            {
                return dayStem.Index == 0 && dayBranch.Index == 0;
            }

            return false;
        }

        /// <summary>
        /// 四废
        /// 春庚申辛酉，夏壬子癸亥，秋甲寅乙卯，冬丙午丁巳
        /// </summary>
        private static bool IsSiFei(EarthBranch monthBranch, HeavenStem dayStem, EarthBranch dayBranch)
        {
            var rules = new Dictionary<int, string[]>
            {
                { 2, new[] { "庚申", "辛酉" } },  // 春季
                { 3, new[] { "庚申", "辛酉" } },
                { 4, new[] { "庚申", "辛酉" } },
                { 5, new[] { "壬子", "癸亥" } },  // 夏季
                { 6, new[] { "壬子", "癸亥" } },
                { 7, new[] { "壬子", "癸亥" } },
                { 8, new[] { "甲寅", "乙卯" } },  // 秋季
                { 9, new[] { "甲寅", "乙卯" } },
                { 10, new[] { "甲寅", "乙卯" } },
                { 11, new[] { "丙午", "丁巳" } }, // 冬季
                { 0, new[] { "丙午", "丁巳" } },
                { 1, new[] { "丙午", "丁巳" } }
            };

            if (!rules.ContainsKey(monthBranch.Index)) return false;

            var dayPillar = dayStem.GetName() + dayBranch.GetName();
            return rules[monthBranch.Index].Contains(dayPillar);
        }

        /// <summary>
        /// 天医
        /// 正月见丑，二月见寅，三月见卯...（顺数下一位）
        /// </summary>
        private static bool IsTianYi(EarthBranch monthBranch, EarthBranch branch)
        {
            var targetIndex = (monthBranch.Index + 1) % 12;
            return branch.Index == targetIndex;
        }

        /// <summary>
        /// 禄神
        /// 甲禄在寅，乙禄在卯，丙戊禄在巳，丁己禄在午
        /// 庚禄在申，辛禄在酉，壬禄在亥，癸禄在子
        /// </summary>
        private static bool IsLuShen(HeavenStem stem, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 0, 2 },  // 甲禄在寅
                { 1, 3 },  // 乙禄在卯
                { 2, 5 },  // 丙禄在巳
                { 3, 6 },  // 丁禄在午
                { 4, 5 },  // 戊禄在巳
                { 5, 6 },  // 己禄在午
                { 6, 8 },  // 庚禄在申
                { 7, 9 },  // 辛禄在酉
                { 8, 11 }, // 壬禄在亥
                { 9, 0 }   // 癸禄在子
            };

            return rules.ContainsKey(stem.Index) && rules[stem.Index] == branch.Index;
        }

        /// <summary>
        /// 红鸾
        /// 子年卯，丑年寅，寅年丑，卯年子...
        /// </summary>
        private static bool IsHongLuan(EarthBranch yearBranch, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 0, 3 }, { 1, 2 }, { 2, 1 }, { 3, 0 },
                { 4, 11 }, { 5, 10 }, { 6, 9 }, { 7, 8 },
                { 8, 7 }, { 9, 6 }, { 10, 5 }, { 11, 4 }
            };

            return rules.ContainsKey(yearBranch.Index) && rules[yearBranch.Index] == branch.Index;
        }

        /// <summary>
        /// 天喜（红鸾对宫）
        /// 子年酉，丑年申，寅年未，卯年午...
        /// </summary>
        private static bool IsTianXi(EarthBranch yearBranch, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 0, 9 }, { 1, 8 }, { 2, 7 }, { 3, 6 },
                { 4, 5 }, { 5, 4 }, { 6, 3 }, { 7, 2 },
                { 8, 1 }, { 9, 0 }, { 10, 11 }, { 11, 10 }
            };

            return rules.ContainsKey(yearBranch.Index) && rules[yearBranch.Index] == branch.Index;
        }

        /// <summary>
        /// 天罗
        /// 戌见亥，亥见戌
        /// </summary>
        private static bool IsTianLuo(EarthBranch baseBranch, EarthBranch branch)
        {
            return (baseBranch.Index == 10 && branch.Index == 11) ||
                   (baseBranch.Index == 11 && branch.Index == 10);
        }

        /// <summary>
        /// 地网
        /// 辰见巳，巳见辰
        /// </summary>
        private static bool IsDiWang(EarthBranch baseBranch, EarthBranch branch)
        {
            return (baseBranch.Index == 4 && branch.Index == 5) ||
                   (baseBranch.Index == 5 && branch.Index == 4);
        }

        /// <summary>
        /// 羊刃
        /// 甲刃在卯，乙刃在寅，丙戊刃在午，丁己刃在巳
        /// 庚刃在酉，辛刃在申，壬刃在子，癸刃在亥
        /// </summary>
        private static bool IsYangRen(HeavenStem stem, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 0, 3 },  // 甲刃在卯
                { 1, 2 },  // 乙刃在寅
                { 2, 6 },  // 丙刃在午
                { 3, 5 },  // 丁刃在巳
                { 4, 6 },  // 戊刃在午
                { 5, 5 },  // 己刃在巳
                { 6, 9 },  // 庚刃在酉
                { 7, 8 },  // 辛刃在申
                { 8, 0 },  // 壬刃在子
                { 9, 11 }  // 癸刃在亥
            };

            return rules.ContainsKey(stem.Index) && rules[stem.Index] == branch.Index;
        }

        /// <summary>
        /// 飞刃（羊刃的六冲）
        /// </summary>
        private static bool IsFeiRen(HeavenStem stem, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 0, 9 },  // 甲见酉
                { 1, 8 },  // 乙见申
                { 2, 0 },  // 丙见子
                { 4, 0 },  // 戊见子
                { 3, 1 },  // 丁见丑
                { 5, 1 },  // 己见丑
                { 6, 3 },  // 庚见卯
                { 7, 4 },  // 辛见辰
                { 8, 6 },  // 壬见午
                { 9, 7 }   // 癸见未
            };

            return rules.ContainsKey(stem.Index) && rules[stem.Index] == branch.Index;
        }

        /// <summary>
        /// 劫煞
        /// 申子辰见巳，亥卯未见申，寅午戌见亥，巳酉丑见寅
        /// </summary>
        private static bool IsJieSha(EarthBranch baseBranch, EarthBranch branch)
        {
            if (branch.Index == 11) // 亥
                return new[] { 2, 6, 10 }.Contains(baseBranch.Index); // 寅午戌
            if (branch.Index == 5) // 巳
                return new[] { 8, 0, 4 }.Contains(baseBranch.Index); // 申子辰
            if (branch.Index == 2) // 寅
                return new[] { 5, 9, 1 }.Contains(baseBranch.Index); // 巳酉丑
            if (branch.Index == 8) // 申
                return new[] { 11, 3, 7 }.Contains(baseBranch.Index); // 亥卯未

            return false;
        }

        /// <summary>
        /// 灾煞
        /// 申子辰见午，亥卯未见酉，寅午戌见子，巳酉丑见卯
        /// </summary>
        private static bool IsZaiSha(EarthBranch yearBranch, EarthBranch branch)
        {
            if (branch.Index == 6) // 午
                return new[] { 8, 0, 4 }.Contains(yearBranch.Index); // 申子辰
            if (branch.Index == 0) // 子
                return new[] { 2, 6, 10 }.Contains(yearBranch.Index); // 寅午戌
            if (branch.Index == 3) // 卯
                return new[] { 5, 9, 1 }.Contains(yearBranch.Index); // 巳酉丑
            if (branch.Index == 9) // 酉
                return new[] { 11, 3, 7 }.Contains(yearBranch.Index); // 亥卯未

            return false;
        }

        /// <summary>
        /// 孤辰
        /// 亥子丑见寅，寅卯辰见巳，巳午未见申，申酉戌见亥
        /// </summary>
        private static bool IsGuChen(EarthBranch yearBranch, EarthBranch branch)
        {
            if (branch.Index == 2) // 寅
                return new[] { 11, 0, 1 }.Contains(yearBranch.Index); // 亥子丑
            if (branch.Index == 5) // 巳
                return new[] { 2, 3, 4 }.Contains(yearBranch.Index); // 寅卯辰
            if (branch.Index == 8) // 申
                return new[] { 5, 6, 7 }.Contains(yearBranch.Index); // 巳午未
            if (branch.Index == 11) // 亥
                return new[] { 8, 9, 10 }.Contains(yearBranch.Index); // 申酉戌

            return false;
        }

        /// <summary>
        /// 寡宿
        /// 亥子丑见戌，寅卯辰见丑，巳午未见辰，申酉戌见未
        /// </summary>
        private static bool IsGuaSu(EarthBranch yearBranch, EarthBranch branch)
        {
            if (branch.Index == 10) // 戌
                return new[] { 11, 0, 1 }.Contains(yearBranch.Index); // 亥子丑
            if (branch.Index == 1) // 丑
                return new[] { 2, 3, 4 }.Contains(yearBranch.Index); // 寅卯辰
            if (branch.Index == 4) // 辰
                return new[] { 5, 6, 7 }.Contains(yearBranch.Index); // 巳午未
            if (branch.Index == 7) // 未
                return new[] { 8, 9, 10 }.Contains(yearBranch.Index); // 申酉戌

            return false;
        }

        /// <summary>
        /// 亡神
        /// 寅午戌见巳，亥卯未见寅，巳酉丑见申，申子辰见亥
        /// </summary>
        private static bool IsWangShen(EarthBranch baseBranch, EarthBranch branch)
        {
            if (branch.Index == 11) // 亥
                return new[] { 8, 0, 4 }.Contains(baseBranch.Index); // 申子辰
            if (branch.Index == 5) // 巳
                return new[] { 2, 6, 10 }.Contains(baseBranch.Index); // 寅午戌
            if (branch.Index == 8) // 申
                return new[] { 5, 9, 1 }.Contains(baseBranch.Index); // 巳酉丑
            if (branch.Index == 2) // 寅
                return new[] { 11, 3, 7 }.Contains(baseBranch.Index); // 亥卯未

            return false;
        }

        /// <summary>
        /// 十恶大败
        /// 甲辰、乙巳、壬申、丙申、丁亥、庚辰、戊戌、癸亥、辛巳、己丑
        /// </summary>
        private static bool IsShiEDaBai(HeavenStem dayStem, EarthBranch dayBranch)
        {
            var validPairs = new Dictionary<int, int>
            {
                { 0, 4 },  // 甲辰
                { 1, 5 },  // 乙巳
                { 8, 8 },  // 壬申
                { 2, 8 },  // 丙申
                { 3, 11 }, // 丁亥
                { 6, 4 },  // 庚辰
                { 4, 10 }, // 戊戌
                { 9, 11 }, // 癸亥
                { 7, 5 },  // 辛巳
                { 5, 1 }   // 己丑
            };

            return validPairs.ContainsKey(dayStem.Index) && validPairs[dayStem.Index] == dayBranch.Index;
        }

        /// <summary>
        /// 词馆
        /// 年柱纳音为金命见其他三支有"申"为词馆，见"壬申"为正词馆
        /// 年柱纳音为木命见其他三支有"寅"为词馆，见"庚寅"为正词馆
        /// 年柱纳音为水命见其他三支有"亥"为词馆，见"癸亥"为正词馆
        /// 年柱纳音为土命见其他三支有"亥"为词馆，见"丁亥"为正词馆
        /// 年柱纳音为火命见其他三支有"巳"为词馆，见"乙巳"为正词馆
        /// </summary>
        private static bool IsCiGuan(string yearSound, HeavenStem stem, EarthBranch branch)
        {
            if (string.IsNullOrEmpty(yearSound) || yearSound.Length < 3)
                return false;

            var element = yearSound.Substring(2, 1); // 获取纳音的五行（如"海中金"取"金"）

            var rules = new Dictionary<string, (int branch, int stem, int stemBranch)>
            {
                { "金", (8, 8, 3) },  // 金命见申，壬申为正（壬=8，申=8，但实际应该是壬=8对应卯=3）
                { "木", (2, 6, 2) },  // 木命见寅，庚寅为正
                { "水", (11, 9, 11) }, // 水命见亥，癸亥为正
                { "土", (11, 3, 11) }, // 土命见亥，丁亥为正
                { "火", (5, 1, 5) }   // 火命见巳，乙巳为正
            };

            if (!rules.ContainsKey(element))
                return false;

            var (targetBranch, targetStem, targetStemBranch) = rules[element];

            // 见地支即可
            if (branch.Index == targetBranch)
                return true;

            // 正词馆：特定干支组合
            if (stem.Index == targetStem && branch.Index == targetStemBranch)
                return true;

            return false;
        }

        /// <summary>
        /// 学堂
        /// 年柱纳音为金命见其他三支有"巳"为学堂，见"辛巳"为正学堂
        /// 年柱纳音为木命见其他三支有"亥"为学堂，见"己亥"为正学堂
        /// 年柱纳音为水命见其他三支有"申"为学堂，见"甲申"为正学堂
        /// 年柱纳音为土命见其他三支有"申"为学堂，见"戊申"为正学堂
        /// 年柱纳音为火命见其他三支有"寅"为学堂，见"丙寅"为正学堂
        /// </summary>
        private static bool IsXueTang(string yearSound, HeavenStem stem, EarthBranch branch)
        {
            if (string.IsNullOrEmpty(yearSound) || yearSound.Length < 3)
                return false;

            var element = yearSound.Substring(2, 1);

            var rules = new Dictionary<string, (int branch, int stem, int stemBranch)>
            {
                { "金", (5, 7, 5) },   // 金命见巳，辛巳为正
                { "木", (11, 5, 11) }, // 木命见亥，己亥为正
                { "水", (8, 0, 8) },   // 水命见申，甲申为正
                { "土", (8, 4, 8) },   // 土命见申，戊申为正
                { "火", (2, 2, 2) }    // 火命见寅，丙寅为正
            };

            if (!rules.ContainsKey(element))
                return false;

            var (targetBranch, targetStem, targetStemBranch) = rules[element];

            // 见地支即可
            if (branch.Index == targetBranch)
                return true;

            // 正学堂：特定干支组合
            if (stem.Index == targetStem && branch.Index == targetStemBranch)
                return true;

            return false;
        }

        /// <summary>
        /// 血刃
        /// 寅月丑，卯月未，辰月寅，巳月申，午月卯，未月酉
        /// 申月辰，酉月戌，戌月巳，亥月亥，子月午，丑月子
        /// </summary>
        private static bool IsXueRen(EarthBranch monthBranch, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 0, 6 },  // 子月见午
                { 1, 0 },  // 丑月见子
                { 2, 1 },  // 寅月见丑
                { 3, 7 },  // 卯月见未
                { 4, 2 },  // 辰月见寅
                { 5, 8 },  // 巳月见申
                { 6, 3 },  // 午月见卯
                { 7, 9 },  // 未月见酉
                { 8, 4 },  // 申月见辰
                { 9, 10 }, // 酉月见戌
                { 10, 5 }, // 戌月见巳
                { 11, 11 } // 亥月见亥
            };

            return rules.ContainsKey(monthBranch.Index) && rules[monthBranch.Index] == branch.Index;
        }

        /// <summary>
        /// 十灵日
        /// 甲辰、乙亥、丙辰、丁酉、戊午、庚戌、庚寅、辛亥、壬寅、癸未
        /// </summary>
        private static bool IsShiLing(HeavenStem dayStem, EarthBranch dayBranch)
        {
            var validPairs = new HashSet<string>
            {
                "甲辰", "乙亥", "丙辰", "丁酉",
                "戊午", "庚戌", "庚寅", "辛亥",
                "壬寅", "癸未"
            };

            var dayPillar = dayStem.GetName() + dayBranch.GetName();
            return validPairs.Contains(dayPillar);
        }

        /// <summary>
        /// 流霞
        /// 甲日酉，乙日戌，丙日未，丁日申，戊日巳
        /// 己日午，庚日辰，辛日卯，壬日亥，癸日寅
        /// </summary>
        private static bool IsLiuXia(HeavenStem dayStem, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 0, 9 },  // 甲见酉
                { 1, 10 }, // 乙见戌
                { 2, 7 },  // 丙见未
                { 3, 8 },  // 丁见申
                { 4, 5 },  // 戊见巳
                { 5, 6 },  // 己见午
                { 6, 4 },  // 庚见辰
                { 7, 3 },  // 辛见卯
                { 8, 11 }, // 壬见亥
                { 9, 2 }   // 癸见寅
            };

            return rules.ContainsKey(dayStem.Index) && rules[dayStem.Index] == branch.Index;
        }

        /// <summary>
        /// 红艳
        /// 甲日午，乙日午，丙日寅，丁日未，戊日辰
        /// 己日辰，庚日戌，辛日酉，壬日子，癸日申
        /// </summary>
        private static bool IsHongYan(HeavenStem dayStem, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 0, 6 },  // 甲见午
                { 1, 6 },  // 乙见午
                { 2, 2 },  // 丙见寅
                { 3, 7 },  // 丁见未
                { 4, 4 },  // 戊见辰
                { 5, 4 },  // 己见辰
                { 6, 10 }, // 庚见戌
                { 7, 9 },  // 辛见酉
                { 8, 0 },  // 壬见子
                { 9, 8 }   // 癸见申
            };

            return rules.ContainsKey(dayStem.Index) && rules[dayStem.Index] == branch.Index;
        }

        /// <summary>
        /// 童子
        /// 春秋寅子贵，冬夏卯未辰
        /// 金木马卯合，水火鸡犬多
        /// 土命逢辰巳，童子定不错
        /// </summary>
        private static bool IsTongZi(EarthBranch monthBranch, string yearSound, EarthBranch targetBranch)
        {
            // 第一种查法：根据季节
            // 春秋（寅卯辰、申酉戌）见寅或子
            if (new[] { 2, 3, 4, 8, 9, 10 }.Contains(monthBranch.Index))
            {
                if (new[] { 2, 0 }.Contains(targetBranch.Index)) // 寅或子
                    return true;
            }

            // 冬夏（巳午未、亥子丑）见卯、未或辰
            if (new[] { 5, 6, 7, 11, 0, 1 }.Contains(monthBranch.Index))
            {
                if (new[] { 3, 7, 4 }.Contains(targetBranch.Index)) // 卯、未或辰
                    return true;
            }

            // 第二种查法：根据年柱纳音五行
            if (string.IsNullOrEmpty(yearSound) || yearSound.Length < 3)
                return false;

            var element = yearSound.Substring(2, 1);

            // 金木命见午或卯
            if ((element == "金" || element == "木") && new[] { 6, 3 }.Contains(targetBranch.Index))
                return true;

            // 水火命见酉或戌
            if ((element == "水" || element == "火") && new[] { 9, 10 }.Contains(targetBranch.Index))
                return true;

            // 土命见辰或巳
            if (element == "土" && new[] { 4, 5 }.Contains(targetBranch.Index))
                return true;

            return false;
        }



        /// <summary>
        /// 福星贵人
        /// 凡甲、丙两干见寅或子，乙、癸两干见卯或丑，戊干见申，己干见未，丁干见亥，庚干见午，辛干见巳，壬干见辰
        /// 查法：以年干/日干查四地支
        /// </summary>
        private static bool IsFuXing(HeavenStem stem, EarthBranch branch)
        {
            var rules = new[]
            {
                new { Gan = new[] { 0, 2 }, Zhi = new[] { 2, 0 } },    // 甲、丙见寅、子
                new { Gan = new[] { 1, 9 }, Zhi = new[] { 3, 1 } },    // 乙、癸见卯、丑
                new { Gan = new[] { 4 }, Zhi = new[] { 8 } },          // 戊见申
                new { Gan = new[] { 5 }, Zhi = new[] { 7 } },          // 己见未
                new { Gan = new[] { 3 }, Zhi = new[] { 11 } },         // 丁见亥
                new { Gan = new[] { 6 }, Zhi = new[] { 6 } },          // 庚见午
                new { Gan = new[] { 7 }, Zhi = new[] { 5 } },          // 辛见巳
                new { Gan = new[] { 8 }, Zhi = new[] { 4 } }           // 壬见辰
            };

            foreach (var rule in rules)
            {
                if (rule.Gan.Contains(stem.Index) && rule.Zhi.Contains(branch.Index))
                {
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// 天德合
        /// 寅月壬，卯月巳，辰月丁，巳月丙，午月寅，未月己
        /// 申月戊，酉月亥，戌月辛，亥月庚，子月申，丑月乙
        /// </summary>
        private static bool IsTianDeHe(EarthBranch monthBranch, HeavenStem stem)
        {
            var rules = new Dictionary<int, int>
            {
                { 2, 8 },  // 寅月见壬
                { 4, 3 },  // 辰月见丁
                { 5, 2 },  // 巳月见丙
                { 7, 5 },  // 未月见己
                { 8, 4 },  // 申月见戊
                { 10, 7 }, // 戌月见辛
                { 11, 6 }, // 亥月见庚
                { 1, 1 }   // 丑月见乙
            };

            return rules.ContainsKey(monthBranch.Index) && rules[monthBranch.Index] == stem.Index;
        }

        /// <summary>
        /// 天德合（地支版本）
        /// </summary>
        private static bool IsTianDeHe(EarthBranch monthBranch, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 3, 5 },  // 卯月见巳
                { 6, 2 },  // 午月见寅
                { 9, 11 }, // 酉月见亥
                { 0, 8 }   // 子月见申
            };

            return rules.ContainsKey(monthBranch.Index) && rules[monthBranch.Index] == branch.Index;
        }

        /// <summary>
        /// 月德合
        /// 寅午戌月见辛，申子辰月见丁，巳酉丑月见乙，亥卯未月见己
        /// </summary>
        private static bool IsYueDeHe(EarthBranch monthBranch, HeavenStem stem)
        {
            var rules = new Dictionary<int, int[]>
            {
                { 7, new[] { 2, 6, 10 } },  // 辛对应寅午戌
                { 3, new[] { 8, 0, 4 } },   // 丁对应申子辰
                { 1, new[] { 5, 9, 1 } },   // 乙对应巳酉丑
                { 5, new[] { 11, 3, 7 } }   // 己对应亥卯未
            };

            foreach (var rule in rules)
            {
                if (stem.Index == rule.Key && rule.Value.Contains(monthBranch.Index))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 九丑
        /// 丁酉、戊子、戊午、己卯、己酉、辛卯、辛酉、壬子、壬午
        /// </summary>
        private static bool IsJiuChou(HeavenStem dayStem, EarthBranch dayBranch)
        {
            var validPairs = new HashSet<string>
            {
                "丁酉", "戊子", "戊午", "己卯", "己酉",
                "辛卯", "辛酉", "壬子", "壬午"
            };

            var dayPillar = dayStem.GetName() + dayBranch.GetName();
            return validPairs.Contains(dayPillar);
        }

        /// <summary>
        /// 八专
        /// 甲寅、乙卯、丁未、戊戌、己未、庚申、辛酉、癸丑
        /// </summary>
        private static bool IsBaZhuan(HeavenStem dayStem, EarthBranch dayBranch)
        {
            var validPairs = new HashSet<string>
            {
                "甲寅", "乙卯", "丁未", "戊戌",
                "己未", "庚申", "辛酉", "癸丑"
            };

            var dayPillar = dayStem.GetName() + dayBranch.GetName();
            return validPairs.Contains(dayPillar);
        }

        /// <summary>
        /// 判断天干是否为阳干
        /// </summary>
        private static bool IsYangStem(HeavenStem stem)
        {
            // 甲、丙、戊、庚、壬为阳干（索引：0、2、4、6、8）
            return stem.Index % 2 == 0;
        }

        /// <summary>
        /// 获取干支在六十甲子中的顺序（1-60）
        /// </summary>
        private static int GetJiaZiOrder(string ganZhi)
        {
            var index = Array.IndexOf(JIAZI, ganZhi);
            return index;
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 根据地支名称获取地支对象
        /// </summary>
        private static EarthBranch GetEarthBranchByName(string name)
        {
            return EarthBranch.FromName(name);
        }

        /// <summary>
        /// 根据天干名称获取天干对象
        /// </summary>
        private static HeavenStem GetHeavenStemByName(string name)
        {
            return HeavenStem.FromName(name);
        }

        #endregion
    }

    /// <summary>
    /// 神煞扩展方法
    /// </summary>
    public static class ShenShaExtensions
    {
        /// <summary>
        /// 获取八字神煞
        /// </summary>
        /// <param name="eightChar">八字对象</param>
        /// <param name="isMale">性别，true为男性</param>
        /// <returns>神煞名称列表</returns>
        public static List<string> GetShenSha(this EightChar eightChar, bool isMale)
        {
            var yearSound = eightChar.Year.Sound.GetName();

            // 查询日柱神煞
            return PersonalGod.QueryShenSha(
                eightChar.Day,
                eightChar,
                isMale,
                PersonalGod.PillarPosition.Day,
                yearSound
            );
        }

        /// <summary>
        /// 获取指定柱位的神煞
        /// </summary>
        /// <param name="eightChar">八字对象</param>
        /// <param name="pillar">要查询的柱</param>
        /// <param name="position">柱位</param>
        /// <param name="isMale">性别</param>
        /// <returns>神煞名称列表</returns>
        public static List<string> GetShenShaByPillar(this EightChar eightChar,
            SixtyCycle pillar,
            PersonalGod.PillarPosition position,
            bool isMale)
        {
            var yearSound = eightChar.Year.Sound.GetName();
            return PersonalGod.QueryShenSha(pillar, eightChar, isMale, position, yearSound);
        }

        /// <summary>
        /// 获取四柱所有神煞（去重）
        /// </summary>
        /// <param name="eightChar">八字对象</param>
        /// <param name="isMale">性别</param>
        /// <returns>神煞名称列表</returns>
        public static List<string> GetAllShenSha(this EightChar eightChar, bool isMale)
        {
            var yearSound = eightChar.Year.Sound.GetName();
            var allShenSha = new HashSet<string>();

            // 年柱
            var yearShenSha = PersonalGod.QueryShenSha(
                eightChar.Year,
                eightChar,
                isMale,
                PersonalGod.PillarPosition.Year,
                yearSound
            );
            foreach (var sha in yearShenSha)
                allShenSha.Add(sha);

            // 月柱
            var monthShenSha = PersonalGod.QueryShenSha(
                eightChar.Month,
                eightChar,
                isMale,
                PersonalGod.PillarPosition.Month,
                yearSound
            );
            foreach (var sha in monthShenSha)
                allShenSha.Add(sha);

            // 日柱
            var dayShenSha = PersonalGod.QueryShenSha(
                eightChar.Day,
                eightChar,
                isMale,
                PersonalGod.PillarPosition.Day,
                yearSound
            );
            foreach (var sha in dayShenSha)
                allShenSha.Add(sha);

            // 时柱
            var hourShenSha = PersonalGod.QueryShenSha(
                eightChar.Hour,
                eightChar,
                isMale,
                PersonalGod.PillarPosition.Hour,
                yearSound
            );
            foreach (var sha in hourShenSha)
                allShenSha.Add(sha);

            return allShenSha.ToList();
        }



        /// <summary>
        /// 获取四柱神煞键值对（新增功能）
        /// </summary>
        /// <param name="eightChar">八字对象</param>
        /// <param name="isMale">性别</param>
        /// <returns>四柱神煞字典，键为柱位名称("年柱"/"月柱"/"日柱"/"时柱")，值为神煞列表</returns>
        public static Dictionary<string, List<string>> GetShenShaByPillars(this EightChar eightChar, bool isMale)
        {
            var yearSound = eightChar.Year.Sound.GetName();
            var result = new Dictionary<string, List<string>>();

            var pillars = new[]
            {
                ("年柱", eightChar.Year, PersonalGod.PillarPosition.Year),
                ("月柱", eightChar.Month, PersonalGod.PillarPosition.Month),
                ("日柱", eightChar.Day, PersonalGod.PillarPosition.Day),
                ("时柱", eightChar.Hour, PersonalGod.PillarPosition.Hour)
            };

            foreach (var (name, pillar, position) in pillars)
            {
                var shenSha = PersonalGod.QueryShenSha(pillar, eightChar, isMale, position, yearSound);
                result[name] = shenSha;
            }

            return result;
        }
    }
}
