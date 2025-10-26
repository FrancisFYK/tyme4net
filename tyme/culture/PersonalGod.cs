using System;
using System.Collections.Generic;
using System.Linq;

using tyme.eightchar;
using tyme.sixtycycle;

namespace tyme.culture
{
    /// <summary>
    /// ���˰�����ɷ
    /// </summary>
    public static class PersonalGod
    {
        /// <summary>
        /// ��ʮ���ӱ�
        /// </summary>
        private static readonly string[] JIAZI = new[]
        {
            "��",
            "����", "�ҳ�", "����", "��î", "�쳽", "����", "����", "��δ", "����", "����",
            "����", "�Һ�", "����", "����", "����", "��î", "����", "����", "����", "��δ",
            "����", "����", "����", "����", "����", "����", "����", "��î", "�ɳ�", "����",
            "����", "��δ", "����", "����", "����", "����", "����", "����", "����", "��î",
            "�׳�", "����", "����", "��δ", "����", "����", "����", "����", "����", "���",
            "����", "��î", "����", "����", "����", "��δ", "����", "����", "����", "�ﺥ"
        };

        /// <summary>
        /// ��֧˳��
        /// </summary>
        private static readonly string[] EARTH_BRANCHES = new[]
        {
            "��", "��", "��", "î", "��", "��", "��", "δ", "��", "��", "��", "��"
        };

        /// <summary>
        /// ɥ�ŵ�֧��Ӧ��
        /// </summary>
        private static readonly string[] SANG_MEN = new[]
        {
            "��", "î", "��", "��", "��", "δ", "��", "��", "��", "��", "��", "��"
        };

        /// <summary>
        /// ���͵�֧��Ӧ��
        /// </summary>
        private static readonly string[] DIAO_KE = new[]
        {
            "��", "��", "��", "��", "��", "î", "��", "��", "��", "δ", "��", "��"
        };

        /// <summary>
        /// �����֧��Ӧ��
        /// </summary>
        private static readonly string[] PI_MA = new[]
        {
            "��", "��", "��", "��", "��", "��", "î", "��", "��", "��", "δ", "��"
        };

        /// <summary>
        /// ��λö��
        /// </summary>
        public enum PillarPosition
        {
            /// <summary>����</summary>
            Year = 1,
            /// <summary>����</summary>
            Month = 2,
            /// <summary>����</summary>
            Day = 3,
            /// <summary>ʱ��</summary>
            Hour = 4,
            /// <summary>����</summary>
            GreatLuck = 5,
            /// <summary>����</summary>
            YearlyLuck = 6,
            /// <summary>����</summary>
            MonthlyLuck = 7,
            /// <summary>��ʱ</summary>
            HourlyLuck = 8
        }

        /// <summary>
        /// ���ݸ�֧�Ͱ�����Ϣ����ѯ��ɷ���
        /// </summary>
        /// <param name="ganZhi">Ҫ��ѯ��ɷ��ĳ����֧����������Ϊ�����������ֵΪ������</param>
        /// <param name="eightChar">���ֶ���</param>
        /// <param name="isMale">�Ա�trueΪ�У�����ΪŮ</param>
        /// <param name="position">�������һ��</param>
        /// <param name="yearSound">������������ѯѧ�á��ʹ���ɷʱ�á����磺���н�</param>
        /// <returns>������ɷ�����б�</returns>
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

            // ���ҹ���
            if (IsTianYiGuiRen(dayStem, branch) || IsTianYiGuiRen(yearStem, branch))
            {
                shenShaList.Add("���ҹ���");
            }

            // ̫������
            if (IsTaiJiGuiRen(dayStem, branch) || IsTaiJiGuiRen(yearStem, branch))
            {
                shenShaList.Add("̫������");
            }

            // ��¹���
            if (IsTianDeGuiRen(monthBranch, stem) || IsTianDeGuiRen(monthBranch, branch))
            {
                shenShaList.Add("��¹���");
            }

            // �µ¹���
            if (IsYueDe(monthBranch, stem))
            {
                shenShaList.Add("�µ¹���");
            }

            // �������
            var allStems = new[] { yearStem, monthStem, dayStem, hourStem };
            if (IsDeXiuGuiRen(monthBranch, allStems))
            {
                shenShaList.Add("�������");
            }

            // ��º�
            if (IsTianDeHe(monthBranch, stem) || IsTianDeHe(monthBranch, branch))
            {
                shenShaList.Add("��º�");
            }

            // �µº�
            if (IsYueDeHe(monthBranch, stem))
            {
                shenShaList.Add("�µº�");
            }

            // ���ǹ���
            if (IsFuXing(yearStem, branch) || IsFuXing(dayStem, branch))
            {
                shenShaList.Add("���ǹ���");
            }

            // �Ĳ�����
            if (IsWenChang(dayStem, branch) || IsWenChang(yearStem, branch))
            {
                shenShaList.Add("�Ĳ�����");
            }

            // ѧ�ã�����������
            if (position != PillarPosition.Day && IsXueTang(yearSound, stem, branch))
            {
                shenShaList.Add("ѧ��");
            }

            // �ʹݣ�����������
            if (position != PillarPosition.Day && IsCiGuan(yearSound, stem, branch))
            {
                shenShaList.Add("�ʹ�");
            }

            // �������������
            if (position == PillarPosition.Day && IsKuiGang(dayStem, dayBranch))
            {
                shenShaList.Add("���");
            }

            // ��ӡ����
            if (IsGuoYin(dayStem, branch) || IsGuoYin(yearStem, branch))
            {
                shenShaList.Add("��ӡ����");
            }

            // ����
            if ((position != PillarPosition.Day && IsYiMa(dayBranch, branch)) ||
                (position != PillarPosition.Year && IsYiMa(yearBranch, branch)))
            {
                shenShaList.Add("����");
            }

            // ����
            if ((position != PillarPosition.Day && IsHuaGai(dayBranch, branch)) ||
                (position != PillarPosition.Year && IsHuaGai(yearBranch, branch)))
            {
                shenShaList.Add("����");
            }

            // ����
            if ((position != PillarPosition.Day && IsJiangXing(dayBranch, branch)) ||
                (position != PillarPosition.Year && IsJiangXing(yearBranch, branch)))
            {
                shenShaList.Add("����");
            }

            // ����
            if (IsJinYu(dayStem, branch) || IsJinYu(yearStem, branch))
            {
                shenShaList.Add("����");
            }

            // ����
            if ((position == PillarPosition.Day && IsJinShen(dayStem, dayBranch)) ||
                (position == PillarPosition.Hour && IsJinShen(hourStem, hourBranch)))
            {
                shenShaList.Add("����");
            }

            // ���
            if (position != PillarPosition.Month && IsWuGui(monthBranch, branch))
            {
                shenShaList.Add("���");
            }

            // ��ҽ
            if (position != PillarPosition.Month && IsTianYi(monthBranch, branch))
            {
                shenShaList.Add("��ҽ");
            }

            // »��
            if (IsLuShen(dayStem, branch))
            {
                shenShaList.Add("»��");
            }

            // ����
            if (IsTianShe(monthBranch, dayStem, dayBranch))
            {
                shenShaList.Add("����");
            }

            // ���
            if (position != PillarPosition.Year && IsHongLuan(yearBranch, branch))
            {
                shenShaList.Add("���");
            }

            // ��ϲ
            if (position != PillarPosition.Year && IsTianXi(yearBranch, branch))
            {
                shenShaList.Add("��ϲ");
            }

            // ��ϼ
            if (IsLiuXia(dayStem, branch))
            {
                shenShaList.Add("��ϼ");
            }

            // ����
            if (IsHongYan(dayStem, branch))
            {
                shenShaList.Add("����ɷ");
            }

            // ����
            if ((position != PillarPosition.Day && IsTianLuo(dayBranch, branch)) ||
                (position != PillarPosition.Year && IsTianLuo(yearBranch, branch)))
            {
                shenShaList.Add("����");
            }

            // ����
            if ((position != PillarPosition.Day && IsDiWang(dayBranch, branch)) ||
                (position != PillarPosition.Year && IsDiWang(yearBranch, branch)))
            {
                shenShaList.Add("����");
            }

            // ����
            if (IsYangRen(dayStem, branch))
            {
                shenShaList.Add("����");
            }

            // ����
            if (IsFeiRen(dayStem, branch))
            {
                shenShaList.Add("����");
            }

            // Ѫ��
            if (IsXueRen(monthBranch, branch))
            {
                shenShaList.Add("Ѫ��");
            }

            // ��ר������������
            if (position == PillarPosition.Day && IsBaZhuan(dayStem, dayBranch))
            {
                shenShaList.Add("��ר");
            }

            // �ų󣨽���������
            if (position == PillarPosition.Day && IsJiuChou(dayStem, dayBranch))
            {
                shenShaList.Add("�ų���");
            }

            // ��ɷ
            if (IsJieSha(dayBranch, branch) || IsJieSha(yearBranch, branch))
            {
                shenShaList.Add("��ɷ");
            }

            // ��ɷ
            if (IsZaiSha(yearBranch, branch))
            {
                shenShaList.Add("��ɷ");
            }

            // Ԫ��
            if (position != PillarPosition.Year &&
                IsYuanChen(yearBranch, branch, isMale, IsYangStem(yearStem)))
            {
                shenShaList.Add("Ԫ��");
            }

            // ����
            if ((position != PillarPosition.Day && IsKongWang(eightChar.Day, branch)) ||
                (position != PillarPosition.Year && IsKongWang(eightChar.Year, branch)))
            {
                shenShaList.Add("����");
            }

            // ͯ��
            if ((position == PillarPosition.Day && IsTongZi(monthBranch, yearSound, dayBranch)) ||
                (position == PillarPosition.Hour && IsTongZi(monthBranch, yearSound, hourBranch)))
            {
                shenShaList.Add("ͯ��ɷ");
            }

            // ���
            if (IsTianChu(yearStem, dayStem, branch))
            {
                shenShaList.Add("�������");
            }

            // �³�
            if (position != PillarPosition.Year && IsGuChen(yearBranch, branch))
            {
                shenShaList.Add("�³�");
            }

            // ����
            if (position != PillarPosition.Year && IsGuaSu(yearBranch, branch))
            {
                shenShaList.Add("����");
            }

            // ����
            if ((position != PillarPosition.Day && IsWangShen(dayBranch, branch)) ||
                (position != PillarPosition.Year && IsWangShen(yearBranch, branch)))
            {
                shenShaList.Add("����");
            }

            // ʮ���ܣ�����������
            if (position == PillarPosition.Day && IsShiEDaBai(dayStem, dayBranch))
            {
                shenShaList.Add("ʮ����");
            }

            // �һ�
            if (IsTaoHua(dayBranch, branch) || IsTaoHua(yearBranch, branch))
            {
                shenShaList.Add("�һ�");
            }

            // ��𽣨����������
            if (position == PillarPosition.Day && IsGuLuan(dayStem, dayBranch))
            {
                shenShaList.Add("���");
            }

            // ������������������
            if (position == PillarPosition.Day && IsYinYangChaCuo(dayStem, dayBranch))
            {
                shenShaList.Add("��������");
            }

            // �ķϣ�����������
            if (position == PillarPosition.Day && IsSiFei(monthBranch, dayStem, dayBranch))
            {
                shenShaList.Add("�ķ�");
            }

            // ɥ��
            if (position != PillarPosition.Year && IsSangMen(yearBranch, branch))
            {
                shenShaList.Add("ɥ��");
            }

            // ����
            if (position != PillarPosition.Year && IsDiaoKe(yearBranch, branch))
            {
                shenShaList.Add("����");
            }

            // ����
            if (position != PillarPosition.Year && IsPiMa(yearBranch, branch))
            {
                shenShaList.Add("����");
            }

            // ʮ�飨����������
            if (position == PillarPosition.Day && IsShiLing(dayStem, dayBranch))
            {
                shenShaList.Add("ʮ��");
            }

            return shenShaList;
        }

        #region ��ɷ�жϷ���

        /// <summary>
        /// �������
        /// �鷨������ɡ��ոɲ�����֧
        /// ���ɼ��ȣ����ɼ��磬��ɼ��꣬���ɼ���
        /// ���ɼ��������ɼ��ӣ��ɸɼ�������ɼ�î
        /// </summary>
        private static bool IsTianChu(HeavenStem yearStem, HeavenStem dayStem, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 2, 5 },  // ������
                { 3, 6 },  // ������
                { 4, 8 },  // �����
                { 5, 9 },  // ������
                { 6, 11 }, // ������
                { 7, 0 },  // ������
                { 8, 2 },  // �ɼ���
                { 9, 3 }   // ���î
            };

            return (rules.ContainsKey(yearStem.Index) && rules[yearStem.Index] == branch.Index) ||
                   (rules.ContainsKey(dayStem.Index) && rules[dayStem.Index] == branch.Index);
        }

        /// <summary>
        /// �������
        /// �������£�����Ϊ�£����Ϊ��
        /// ���ӳ��£��ɹ��켺Ϊ�£������׼�Ϊ��
        /// ���ϳ��£�����Ϊ�£��Ҹ�Ϊ��
        /// ��îδ�£�����Ϊ�£�����Ϊ��
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

            // ��֣�������
            if (new[] { 2, 6, 10 }.Contains(monthBranch.Index))
            {
                // �£��������㣺���
                return HasStem(2, 3) && HasCombination(4, 9);
            }

            // ˮ�֣����ӳ�
            if (new[] { 8, 0, 4 }.Contains(monthBranch.Index))
            {
                // �£��ɹ��켺���㣺������׼�
                return HasStem(8, 9, 4, 5) && (HasCombination(2, 7) || HasCombination(0, 5));
            }

            // ��֣����ϳ�
            if (new[] { 5, 9, 1 }.Contains(monthBranch.Index))
            {
                // �£��������㣺�Ҹ�
                return HasStem(6, 7) && HasCombination(1, 6);
            }

            // ľ�֣���îδ
            if (new[] { 11, 3, 7 }.Contains(monthBranch.Index))
            {
                // �£����ң��㣺����
                return HasStem(0, 1) && HasCombination(3, 8);
            }

            return false;
        }

        /// <summary>
        /// ����
        /// ����Ѯ���纥������Ѯ�����ϣ�����Ѯ����δ
        /// ����Ѯ�ڳ��ȣ��׳�Ѯ����î������Ѯ���ӳ�
        /// </summary>
        private static bool IsKongWang(SixtyCycle pillar, EarthBranch branch)
        {
            var extraBranches = pillar.ExtraEarthBranches;
            return extraBranches != null && extraBranches.Any(eb => eb.Index == branch.Index);
        }

        /// <summary>
        /// �һ�
        /// ���ӳ����ϣ���������î�����ϳ����磬��îδ����
        /// </summary>
        private static bool IsTaoHua(EarthBranch baseBranch, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 8, 9 }, { 0, 9 }, { 4, 9 },  // ���ӳ�����
                { 2, 3 }, { 6, 3 }, { 10, 3 }, // �������î
                { 5, 6 }, { 9, 6 }, { 1, 6 },  // ���ϳ����
                { 11, 0 }, { 3, 0 }, { 7, 0 }  // ��îδ����
            };

            return rules.ContainsKey(baseBranch.Index) && rules[baseBranch.Index] == branch.Index;
        }

        /// <summary>
        /// ��������
        /// ���ӡ�������������î���ɳ�������
        /// ���硢��δ�����ꡢ���ϡ����硢�ﺥ
        /// </summary>
        private static bool IsYinYangChaCuo(HeavenStem dayStem, EarthBranch dayBranch)
        {
            var rules = new Dictionary<int, int[]>
            {
                { 2, new[] { 0, 6 } },   // �����ӡ���
                { 3, new[] { 1, 7 } },   // ������δ
                { 4, new[] { 2, 8 } },   // ���������
                { 7, new[] { 3, 9 } },   // ����î����
                { 8, new[] { 4, 10 } },  // �ɼ�������
                { 9, new[] { 5, 11 } }   // ����ȡ���
            };

            return rules.ContainsKey(dayStem.Index) && rules[dayStem.Index].Contains(dayBranch.Index);
        }

        /// <summary>
        /// ���ҹ���
        /// ���첢ţ���Ҽ�����磬������λ���ɹ����߲أ������껢��
        /// </summary>
        private static bool IsTianYiGuiRen(HeavenStem stem, EarthBranch branch)
        {
            var rules = new Dictionary<int, int[]>
            {
                { 0, new[] { 1, 7 } },   // �׼���δ
                { 4, new[] { 1, 7 } },   // �����δ
                { 1, new[] { 0, 8 } },   // �Ҽ��ӡ���
                { 5, new[] { 0, 8 } },   // �����ӡ���
                { 2, new[] { 11, 9 } },  // ����������
                { 3, new[] { 11, 9 } },  // ����������
                { 8, new[] { 3, 5 } },   // �ɼ�î����
                { 9, new[] { 3, 5 } },   // ���î����
                { 6, new[] { 2, 6 } },   // ����������
                { 7, new[] { 2, 6 } }    // ����������
            };

            return rules.ContainsKey(stem.Index) && rules[stem.Index].Contains(branch.Index);
        }

        /// <summary>
        /// ̫������
        /// �������������У��������ö���ͨ
        /// �켺�������ļ�����������»��¡
        /// �ɹ�����ƫϲ��
        /// </summary>
        private static bool IsTaiJiGuiRen(HeavenStem stem, EarthBranch branch)
        {
            var rules = new Dictionary<int, int[]>
            {
                { 0, new[] { 0, 6 } },   // �׼��ӡ���
                { 1, new[] { 0, 6 } },   // �Ҽ��ӡ���
                { 2, new[] { 9, 3 } },   // �����ϡ�î
                { 3, new[] { 9, 3 } },   // �����ϡ�î
                { 6, new[] { 2, 11 } },  // ����������
                { 7, new[] { 2, 11 } },  // ����������
                { 8, new[] { 5, 8 } },   // �ɼ��ȡ���
                { 9, new[] { 5, 8 } }    // ����ȡ���
            };

            if (rules.ContainsKey(stem.Index))
            {
                return rules[stem.Index].Contains(branch.Index);
            }

            // �켺���ļ��������δ��
            if ((stem.Index == 4 || stem.Index == 5) &&
                new[] { 1, 4, 7, 10 }.Contains(branch.Index))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// �Ĳ�����
        /// �������籨��֪�������깬������
        /// ���������ɷ껢�����˼�î������
        /// </summary>
        private static bool IsWenChang(HeavenStem stem, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 0, 5 },  // �׼���
                { 1, 6 },  // �Ҽ���
                { 2, 8 },  // ������
                { 4, 8 },  // �����
                { 3, 9 },  // ������
                { 5, 9 },  // ������
                { 6, 11 }, // ������
                { 7, 0 },  // ������
                { 8, 2 },  // �ɼ���
                { 9, 3 }   // ���î
            };

            return rules.ContainsKey(stem.Index) && rules[stem.Index] == branch.Index;
        }

        /// <summary>
        /// �����
        /// �ɳ������������������������
        /// </summary>
        private static bool IsKuiGang(HeavenStem dayStem, EarthBranch dayBranch)
        {
            return (dayStem.Index == 8 && dayBranch.Index == 4) ||  // �ɳ�
                   (dayStem.Index == 6 && dayBranch.Index == 10) || // ����
                   (dayStem.Index == 6 && dayBranch.Index == 4) ||  // ����
                   (dayStem.Index == 4 && dayBranch.Index == 10);   // ����
        }

        /// <summary>
        /// ����
        /// ���ӳ��������������������꣬���ϳ����ں�����îδ������
        /// </summary>
        private static bool IsYiMa(EarthBranch baseBranch, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 8, 2 }, { 0, 2 }, { 4, 2 },   // ���ӳ�����
                { 2, 8 }, { 6, 8 }, { 10, 8 },  // ���������
                { 5, 11 }, { 9, 11 }, { 1, 11 },// ���ϳ����
                { 11, 5 }, { 3, 5 }, { 7, 5 }   // ��îδ����
            };

            return rules.ContainsKey(baseBranch.Index) && rules[baseBranch.Index] == branch.Index;
        }

        /// <summary>
        /// ����
        /// ��������磬��îδ��δ�����ӳ����������ϳ����
        /// </summary>
        private static bool IsHuaGai(EarthBranch baseBranch, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 8, 4 }, { 0, 4 }, { 4, 4 },   // ���ӳ�����
                { 2, 10 }, { 6, 10 }, { 10, 10 },// ���������
                { 5, 1 }, { 9, 1 }, { 1, 1 },   // ���ϳ����
                { 11, 7 }, { 3, 7 }, { 7, 7 }   // ��îδ��δ
            };

            return rules.ContainsKey(baseBranch.Index) && rules[baseBranch.Index] == branch.Index;
        }

        /// <summary>
        /// ����
        /// �������߱����򣬶�������Ȯ��
        /// ������ţ��껢���������˸�����
        /// </summary>
        private static bool IsJinYu(HeavenStem stem, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 0, 4 },  // �׼���
                { 1, 5 },  // �Ҽ���
                { 2, 7 },  // ����δ
                { 4, 7 },  // ���δ
                { 3, 8 },  // ������
                { 5, 8 },  // ������
                { 6, 10 }, // ������
                { 7, 11 }, // ������
                { 8, 1 },  // �ɼ���
                { 9, 2 }   // �����
            };

            return rules.ContainsKey(stem.Index) && rules[stem.Index] == branch.Index;
        }

        /// <summary>
        /// ����
        /// �ҳ󡢼��ȡ�����
        /// </summary>
        private static bool IsJinShen(HeavenStem stem, EarthBranch branch)
        {
            return (stem.Index == 1 && branch.Index == 1) ||  // �ҳ�
                   (stem.Index == 5 && branch.Index == 5) ||  // ����
                   (stem.Index == 9 && branch.Index == 9);    // ����
        }

        /// <summary>
        /// �����
        /// ���¼��������¼��ȣ����¼��磬î�¼�δ
        /// ���¼��꣬���¼��ϣ����¼��磬δ�¼���
        /// ���¼��ӣ����¼������¼��������¼�î
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
        /// ��ӡ����
        /// �׼��磬�Ҽ����������󣬶������������
        /// ���������������������ȣ��ɼ�δ�������
        /// </summary>
        private static bool IsGuoYin(HeavenStem stem, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 0, 10 }, // �׼���
                { 1, 11 }, // �Ҽ���
                { 2, 1 },  // ������
                { 3, 2 },  // ������
                { 4, 1 },  // �����
                { 5, 2 },  // ������
                { 6, 4 },  // ������
                { 7, 5 },  // ������
                { 8, 7 },  // �ɼ�δ
                { 9, 8 }   // �����
            };

            return rules.ContainsKey(stem.Index) && rules[stem.Index] == branch.Index;
        }

        /// <summary>
        /// ����
        /// ��������磬���ϳ���ϣ����ӳ����ӣ���îδ��î
        /// </summary>
        private static bool IsJiangXing(EarthBranch baseBranch, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 8, 0 }, { 0, 0 }, { 4, 0 },   // ���ӳ�����
                { 2, 6 }, { 6, 6 }, { 10, 6 },  // ���������
                { 5, 9 }, { 9, 9 }, { 1, 9 },   // ���ϳ����
                { 11, 3 }, { 3, 3 }, { 7, 3 }   // ��îδ��î
            };

            return rules.ContainsKey(baseBranch.Index) && rules[baseBranch.Index] == branch.Index;
        }

        /// <summary>
        /// ���ɷ
        /// ���������ȡ����硢���ȡ����硢���ꡢ����������
        /// </summary>
        private static bool IsGuLuan(HeavenStem dayStem, EarthBranch dayBranch)
        {
            return (dayStem.Index == 0 && dayBranch.Index == 2) ||  // ����
                   (dayStem.Index == 1 && dayBranch.Index == 5) ||  // ����
                   (dayStem.Index == 2 && dayBranch.Index == 6) ||  // ����
                   (dayStem.Index == 3 && dayBranch.Index == 5) ||  // ����
                   (dayStem.Index == 4 && dayBranch.Index == 6) ||  // ����
                   (dayStem.Index == 4 && dayBranch.Index == 8) ||  // ����
                   (dayStem.Index == 7 && dayBranch.Index == 11) || // ����
                   (dayStem.Index == 8 && dayBranch.Index == 0);    // ����
        }

        /// <summary>
        /// ɥ��
        /// </summary>
        private static bool IsSangMen(EarthBranch yearBranch, EarthBranch branch)
        {
            return CheckRelation(yearBranch, branch, SANG_MEN);
        }

        /// <summary>
        /// ����
        /// </summary>
        private static bool IsDiaoKe(EarthBranch yearBranch, EarthBranch branch)
        {
            return CheckRelation(yearBranch, branch, DIAO_KE);
        }

        /// <summary>
        /// ����
        /// </summary>
        private static bool IsPiMa(EarthBranch yearBranch, EarthBranch branch)
        {
            return CheckRelation(yearBranch, branch, PI_MA);
        }

        /// <summary>
        /// ��ϵ��ѯͨ�÷���
        /// </summary>
        private static bool CheckRelation(EarthBranch yearBranch, EarthBranch branch, string[] targetArray)
        {
            var index = Array.IndexOf(EARTH_BRANCHES, yearBranch.GetName());
            if (index == -1) return false;
            return targetArray[index] == branch.GetName();
        }

        /// <summary>
        /// ��¹���
        /// ���¼��������¼��꣬���¼��ɣ����¼���
        /// ���¼��������¼��ף����¼�����¼���
        /// ���¼�����ʮ�¼��ң�ʮһ�¼��ȣ�ʮ���¼���
        /// </summary>
        private static bool IsTianDeGuiRen(EarthBranch monthBranch, HeavenStem stem)
        {
            var rules = new Dictionary<int, int>
            {
                { 2, 3 },  // ���¼���
                { 3, 8 },  // î�¼��꣨��֧��
                { 4, 8 },  // ���¼���
                { 5, 7 },  // ���¼���
                { 6, 11 }, // ���¼�������֧��
                { 7, 0 },  // δ�¼���
                { 8, 9 },  // ���¼���
                { 9, 2 },  // ���¼�������֧��
                { 10, 2 }, // ���¼���
                { 11, 1 }, // ���¼���
                { 0, 5 },  // ���¼��ȣ���֧��
                { 1, 6 }   // ���¼���
            };

            return rules.ContainsKey(monthBranch.Index) && rules[monthBranch.Index] == stem.Index;
        }

        /// <summary>
        /// ��¹��ˣ���֧�汾��
        /// </summary>
        private static bool IsTianDeGuiRen(EarthBranch monthBranch, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 3, 8 },  // î�¼���
                { 6, 11 }, // ���¼���
                { 9, 2 },  // ���¼���
                { 0, 5 }   // ���¼���
            };

            return rules.ContainsKey(monthBranch.Index) && rules[monthBranch.Index] == branch.Index;
        }

        /// <summary>
        /// Ԫ��
        /// ������Ů��������Ů�鷨��ͬ
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
        /// �µ¹���
        /// �������¼��������ӳ��¼��ɣ���îδ�¼��ף����ϳ��¼���
        /// </summary>
        private static bool IsYueDe(EarthBranch monthBranch, HeavenStem stem)
        {
            var rules = new Dictionary<int, int>
            {
                { 2, 2 }, { 6, 2 }, { 10, 2 },  // ���������
                { 8, 8 }, { 0, 8 }, { 4, 8 },   // ���ӳ�����
                { 11, 0 }, { 3, 0 }, { 7, 0 },  // ��îδ����
                { 5, 6 }, { 9, 6 }, { 1, 6 }    // ���ϳ����
            };

            return rules.ContainsKey(monthBranch.Index) && rules[monthBranch.Index] == stem.Index;
        }

        /// <summary>
        /// ����
        /// ���������ļ��磬�����꣬������
        /// </summary>
        private static bool IsTianShe(EarthBranch monthBranch, HeavenStem dayStem, EarthBranch dayBranch)
        {
            // ��������î������������
            if (new[] { 2, 3, 4 }.Contains(monthBranch.Index))
            {
                return dayStem.Index == 4 && dayBranch.Index == 2;
            }

            // �ļ�������δ����������
            if (new[] { 5, 6, 7 }.Contains(monthBranch.Index))
            {
                return dayStem.Index == 0 && dayBranch.Index == 6;
            }

            // �＾������������������
            if (new[] { 8, 9, 10 }.Contains(monthBranch.Index))
            {
                return dayStem.Index == 4 && dayBranch.Index == 8;
            }

            // ���������ӳ�����������
            if (new[] { 11, 0, 1 }.Contains(monthBranch.Index))
            {
                return dayStem.Index == 0 && dayBranch.Index == 0;
            }

            return false;
        }

        /// <summary>
        /// �ķ�
        /// ���������ϣ������ӹﺥ���������î�������綡��
        /// </summary>
        private static bool IsSiFei(EarthBranch monthBranch, HeavenStem dayStem, EarthBranch dayBranch)
        {
            var rules = new Dictionary<int, string[]>
            {
                { 2, new[] { "����", "����" } },  // ����
                { 3, new[] { "����", "����" } },
                { 4, new[] { "����", "����" } },
                { 5, new[] { "����", "�ﺥ" } },  // �ļ�
                { 6, new[] { "����", "�ﺥ" } },
                { 7, new[] { "����", "�ﺥ" } },
                { 8, new[] { "����", "��î" } },  // �＾
                { 9, new[] { "����", "��î" } },
                { 10, new[] { "����", "��î" } },
                { 11, new[] { "����", "����" } }, // ����
                { 0, new[] { "����", "����" } },
                { 1, new[] { "����", "����" } }
            };

            if (!rules.ContainsKey(monthBranch.Index)) return false;

            var dayPillar = dayStem.GetName() + dayBranch.GetName();
            return rules[monthBranch.Index].Contains(dayPillar);
        }

        /// <summary>
        /// ��ҽ
        /// ���¼��󣬶��¼��������¼�î...��˳����һλ��
        /// </summary>
        private static bool IsTianYi(EarthBranch monthBranch, EarthBranch branch)
        {
            var targetIndex = (monthBranch.Index + 1) % 12;
            return branch.Index == targetIndex;
        }

        /// <summary>
        /// »��
        /// ��»��������»��î������»���ȣ�����»����
        /// ��»���꣬��»���ϣ���»�ں�����»����
        /// </summary>
        private static bool IsLuShen(HeavenStem stem, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 0, 2 },  // ��»����
                { 1, 3 },  // ��»��î
                { 2, 5 },  // ��»����
                { 3, 6 },  // ��»����
                { 4, 5 },  // ��»����
                { 5, 6 },  // ��»����
                { 6, 8 },  // ��»����
                { 7, 9 },  // ��»����
                { 8, 11 }, // ��»�ں�
                { 9, 0 }   // ��»����
            };

            return rules.ContainsKey(stem.Index) && rules[stem.Index] == branch.Index;
        }

        /// <summary>
        /// ���
        /// ����î���������������î����...
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
        /// ��ϲ�����Թ���
        /// �����ϣ������꣬����δ��î����...
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
        /// ����
        /// �������������
        /// </summary>
        private static bool IsTianLuo(EarthBranch baseBranch, EarthBranch branch)
        {
            return (baseBranch.Index == 10 && branch.Index == 11) ||
                   (baseBranch.Index == 11 && branch.Index == 10);
        }

        /// <summary>
        /// ����
        /// �����ȣ��ȼ���
        /// </summary>
        private static bool IsDiWang(EarthBranch baseBranch, EarthBranch branch)
        {
            return (baseBranch.Index == 4 && branch.Index == 5) ||
                   (baseBranch.Index == 5 && branch.Index == 4);
        }

        /// <summary>
        /// ����
        /// ������î���������������������磬����������
        /// �������ϣ��������꣬�������ӣ������ں�
        /// </summary>
        private static bool IsYangRen(HeavenStem stem, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 0, 3 },  // ������î
                { 1, 2 },  // ��������
                { 2, 6 },  // ��������
                { 3, 5 },  // ��������
                { 4, 6 },  // ��������
                { 5, 5 },  // ��������
                { 6, 9 },  // ��������
                { 7, 8 },  // ��������
                { 8, 0 },  // ��������
                { 9, 11 }  // �����ں�
            };

            return rules.ContainsKey(stem.Index) && rules[stem.Index] == branch.Index;
        }

        /// <summary>
        /// ���У����е����壩
        /// </summary>
        private static bool IsFeiRen(HeavenStem stem, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 0, 9 },  // �׼���
                { 1, 8 },  // �Ҽ���
                { 2, 0 },  // ������
                { 4, 0 },  // �����
                { 3, 1 },  // ������
                { 5, 1 },  // ������
                { 6, 3 },  // ����î
                { 7, 4 },  // ������
                { 8, 6 },  // �ɼ���
                { 9, 7 }   // ���δ
            };

            return rules.ContainsKey(stem.Index) && rules[stem.Index] == branch.Index;
        }

        /// <summary>
        /// ��ɷ
        /// ���ӳ����ȣ���îδ���꣬��������������ϳ����
        /// </summary>
        private static bool IsJieSha(EarthBranch baseBranch, EarthBranch branch)
        {
            if (branch.Index == 11) // ��
                return new[] { 2, 6, 10 }.Contains(baseBranch.Index); // ������
            if (branch.Index == 5) // ��
                return new[] { 8, 0, 4 }.Contains(baseBranch.Index); // ���ӳ�
            if (branch.Index == 2) // ��
                return new[] { 5, 9, 1 }.Contains(baseBranch.Index); // ���ϳ�
            if (branch.Index == 8) // ��
                return new[] { 11, 3, 7 }.Contains(baseBranch.Index); // ��îδ

            return false;
        }

        /// <summary>
        /// ��ɷ
        /// ���ӳ����磬��îδ���ϣ���������ӣ����ϳ��î
        /// </summary>
        private static bool IsZaiSha(EarthBranch yearBranch, EarthBranch branch)
        {
            if (branch.Index == 6) // ��
                return new[] { 8, 0, 4 }.Contains(yearBranch.Index); // ���ӳ�
            if (branch.Index == 0) // ��
                return new[] { 2, 6, 10 }.Contains(yearBranch.Index); // ������
            if (branch.Index == 3) // î
                return new[] { 5, 9, 1 }.Contains(yearBranch.Index); // ���ϳ�
            if (branch.Index == 9) // ��
                return new[] { 11, 3, 7 }.Contains(yearBranch.Index); // ��îδ

            return false;
        }

        /// <summary>
        /// �³�
        /// ���ӳ��������î�����ȣ�����δ���꣬���������
        /// </summary>
        private static bool IsGuChen(EarthBranch yearBranch, EarthBranch branch)
        {
            if (branch.Index == 2) // ��
                return new[] { 11, 0, 1 }.Contains(yearBranch.Index); // ���ӳ�
            if (branch.Index == 5) // ��
                return new[] { 2, 3, 4 }.Contains(yearBranch.Index); // ��î��
            if (branch.Index == 8) // ��
                return new[] { 5, 6, 7 }.Contains(yearBranch.Index); // ����δ
            if (branch.Index == 11) // ��
                return new[] { 8, 9, 10 }.Contains(yearBranch.Index); // ������

            return false;
        }

        /// <summary>
        /// ����
        /// ���ӳ���磬��î����������δ�������������δ
        /// </summary>
        private static bool IsGuaSu(EarthBranch yearBranch, EarthBranch branch)
        {
            if (branch.Index == 10) // ��
                return new[] { 11, 0, 1 }.Contains(yearBranch.Index); // ���ӳ�
            if (branch.Index == 1) // ��
                return new[] { 2, 3, 4 }.Contains(yearBranch.Index); // ��î��
            if (branch.Index == 4) // ��
                return new[] { 5, 6, 7 }.Contains(yearBranch.Index); // ����δ
            if (branch.Index == 7) // δ
                return new[] { 8, 9, 10 }.Contains(yearBranch.Index); // ������

            return false;
        }

        /// <summary>
        /// ����
        /// ��������ȣ���îδ���������ϳ���꣬���ӳ�����
        /// </summary>
        private static bool IsWangShen(EarthBranch baseBranch, EarthBranch branch)
        {
            if (branch.Index == 11) // ��
                return new[] { 8, 0, 4 }.Contains(baseBranch.Index); // ���ӳ�
            if (branch.Index == 5) // ��
                return new[] { 2, 6, 10 }.Contains(baseBranch.Index); // ������
            if (branch.Index == 8) // ��
                return new[] { 5, 9, 1 }.Contains(baseBranch.Index); // ���ϳ�
            if (branch.Index == 2) // ��
                return new[] { 11, 3, 7 }.Contains(baseBranch.Index); // ��îδ

            return false;
        }

        /// <summary>
        /// ʮ����
        /// �׳������ȡ����ꡢ���ꡢ���������������硢�ﺥ�����ȡ�����
        /// </summary>
        private static bool IsShiEDaBai(HeavenStem dayStem, EarthBranch dayBranch)
        {
            var validPairs = new Dictionary<int, int>
            {
                { 0, 4 },  // �׳�
                { 1, 5 },  // ����
                { 8, 8 },  // ����
                { 2, 8 },  // ����
                { 3, 11 }, // ����
                { 6, 4 },  // ����
                { 4, 10 }, // ����
                { 9, 11 }, // �ﺥ
                { 7, 5 },  // ����
                { 5, 1 }   // ����
            };

            return validPairs.ContainsKey(dayStem.Index) && validPairs[dayStem.Index] == dayBranch.Index;
        }

        /// <summary>
        /// �ʹ�
        /// ��������Ϊ������������֧��"��"Ϊ�ʹݣ���"����"Ϊ���ʹ�
        /// ��������Ϊľ����������֧��"��"Ϊ�ʹݣ���"����"Ϊ���ʹ�
        /// ��������Ϊˮ����������֧��"��"Ϊ�ʹݣ���"�ﺥ"Ϊ���ʹ�
        /// ��������Ϊ������������֧��"��"Ϊ�ʹݣ���"����"Ϊ���ʹ�
        /// ��������Ϊ������������֧��"��"Ϊ�ʹݣ���"����"Ϊ���ʹ�
        /// </summary>
        private static bool IsCiGuan(string yearSound, HeavenStem stem, EarthBranch branch)
        {
            if (string.IsNullOrEmpty(yearSound) || yearSound.Length < 3)
                return false;

            var element = yearSound.Substring(2, 1); // ��ȡ���������У���"���н�"ȡ"��"��

            var rules = new Dictionary<string, (int branch, int stem, int stemBranch)>
            {
                { "��", (8, 8, 3) },  // �������꣬����Ϊ������=8����=8����ʵ��Ӧ������=8��Ӧî=3��
                { "ľ", (2, 6, 2) },  // ľ������������Ϊ��
                { "ˮ", (11, 9, 11) }, // ˮ���������ﺥΪ��
                { "��", (11, 3, 11) }, // ��������������Ϊ��
                { "��", (5, 1, 5) }   // �������ȣ�����Ϊ��
            };

            if (!rules.ContainsKey(element))
                return false;

            var (targetBranch, targetStem, targetStemBranch) = rules[element];

            // ����֧����
            if (branch.Index == targetBranch)
                return true;

            // ���ʹݣ��ض���֧���
            if (stem.Index == targetStem && branch.Index == targetStemBranch)
                return true;

            return false;
        }

        /// <summary>
        /// ѧ��
        /// ��������Ϊ������������֧��"��"Ϊѧ�ã���"����"Ϊ��ѧ��
        /// ��������Ϊľ����������֧��"��"Ϊѧ�ã���"����"Ϊ��ѧ��
        /// ��������Ϊˮ����������֧��"��"Ϊѧ�ã���"����"Ϊ��ѧ��
        /// ��������Ϊ������������֧��"��"Ϊѧ�ã���"����"Ϊ��ѧ��
        /// ��������Ϊ������������֧��"��"Ϊѧ�ã���"����"Ϊ��ѧ��
        /// </summary>
        private static bool IsXueTang(string yearSound, HeavenStem stem, EarthBranch branch)
        {
            if (string.IsNullOrEmpty(yearSound) || yearSound.Length < 3)
                return false;

            var element = yearSound.Substring(2, 1);

            var rules = new Dictionary<string, (int branch, int stem, int stemBranch)>
            {
                { "��", (5, 7, 5) },   // �������ȣ�����Ϊ��
                { "ľ", (11, 5, 11) }, // ľ������������Ϊ��
                { "ˮ", (8, 0, 8) },   // ˮ�����꣬����Ϊ��
                { "��", (8, 4, 8) },   // �������꣬����Ϊ��
                { "��", (2, 2, 2) }    // ��������������Ϊ��
            };

            if (!rules.ContainsKey(element))
                return false;

            var (targetBranch, targetStem, targetStemBranch) = rules[element];

            // ����֧����
            if (branch.Index == targetBranch)
                return true;

            // ��ѧ�ã��ض���֧���
            if (stem.Index == targetStem && branch.Index == targetStemBranch)
                return true;

            return false;
        }

        /// <summary>
        /// Ѫ��
        /// ���³�î��δ���������������꣬����î��δ����
        /// ���³��������磬�����ȣ����º��������磬������
        /// </summary>
        private static bool IsXueRen(EarthBranch monthBranch, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 0, 6 },  // ���¼���
                { 1, 0 },  // ���¼���
                { 2, 1 },  // ���¼���
                { 3, 7 },  // î�¼�δ
                { 4, 2 },  // ���¼���
                { 5, 8 },  // ���¼���
                { 6, 3 },  // ���¼�î
                { 7, 9 },  // δ�¼���
                { 8, 4 },  // ���¼���
                { 9, 10 }, // ���¼���
                { 10, 5 }, // ���¼���
                { 11, 11 } // ���¼���
            };

            return rules.ContainsKey(monthBranch.Index) && rules[monthBranch.Index] == branch.Index;
        }

        /// <summary>
        /// ʮ����
        /// �׳����Һ������������ϡ����硢���硢��������������������δ
        /// </summary>
        private static bool IsShiLing(HeavenStem dayStem, EarthBranch dayBranch)
        {
            var validPairs = new HashSet<string>
            {
                "�׳�", "�Һ�", "����", "����",
                "����", "����", "����", "����",
                "����", "��δ"
            };

            var dayPillar = dayStem.GetName() + dayBranch.GetName();
            return validPairs.Contains(dayPillar);
        }

        /// <summary>
        /// ��ϼ
        /// �����ϣ������磬����δ�������꣬������
        /// �����磬���ճ�������î�����պ���������
        /// </summary>
        private static bool IsLiuXia(HeavenStem dayStem, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 0, 9 },  // �׼���
                { 1, 10 }, // �Ҽ���
                { 2, 7 },  // ����δ
                { 3, 8 },  // ������
                { 4, 5 },  // �����
                { 5, 6 },  // ������
                { 6, 4 },  // ������
                { 7, 3 },  // ����î
                { 8, 11 }, // �ɼ���
                { 9, 2 }   // �����
            };

            return rules.ContainsKey(dayStem.Index) && rules[dayStem.Index] == branch.Index;
        }

        /// <summary>
        /// ����
        /// �����磬�����磬������������δ�����ճ�
        /// ���ճ��������磬�����ϣ������ӣ�������
        /// </summary>
        private static bool IsHongYan(HeavenStem dayStem, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 0, 6 },  // �׼���
                { 1, 6 },  // �Ҽ���
                { 2, 2 },  // ������
                { 3, 7 },  // ����δ
                { 4, 4 },  // �����
                { 5, 4 },  // ������
                { 6, 10 }, // ������
                { 7, 9 },  // ������
                { 8, 0 },  // �ɼ���
                { 9, 8 }   // �����
            };

            return rules.ContainsKey(dayStem.Index) && rules[dayStem.Index] == branch.Index;
        }

        /// <summary>
        /// ͯ��
        /// �������ӹ󣬶���îδ��
        /// ��ľ��î�ϣ�ˮ��Ȯ��
        /// �����곽�ȣ�ͯ�Ӷ�����
        /// </summary>
        private static bool IsTongZi(EarthBranch monthBranch, string yearSound, EarthBranch targetBranch)
        {
            // ��һ�ֲ鷨�����ݼ���
            // �����î���������磩��������
            if (new[] { 2, 3, 4, 8, 9, 10 }.Contains(monthBranch.Index))
            {
                if (new[] { 2, 0 }.Contains(targetBranch.Index)) // ������
                    return true;
            }

            // ���ģ�����δ�����ӳ󣩼�î��δ��
            if (new[] { 5, 6, 7, 11, 0, 1 }.Contains(monthBranch.Index))
            {
                if (new[] { 3, 7, 4 }.Contains(targetBranch.Index)) // î��δ��
                    return true;
            }

            // �ڶ��ֲ鷨������������������
            if (string.IsNullOrEmpty(yearSound) || yearSound.Length < 3)
                return false;

            var element = yearSound.Substring(2, 1);

            // ��ľ�������î
            if ((element == "��" || element == "ľ") && new[] { 6, 3 }.Contains(targetBranch.Index))
                return true;

            // ˮ�������ϻ���
            if ((element == "ˮ" || element == "��") && new[] { 9, 10 }.Contains(targetBranch.Index))
                return true;

            // ������������
            if (element == "��" && new[] { 4, 5 }.Contains(targetBranch.Index))
                return true;

            return false;
        }



        /// <summary>
        /// ���ǹ���
        /// ���ס������ɼ������ӣ��ҡ������ɼ�î�����ɼ��꣬���ɼ�δ�����ɼ��������ɼ��磬���ɼ��ȣ��ɸɼ���
        /// �鷨�������/�ոɲ��ĵ�֧
        /// </summary>
        private static bool IsFuXing(HeavenStem stem, EarthBranch branch)
        {
            var rules = new[]
            {
                new { Gan = new[] { 0, 2 }, Zhi = new[] { 2, 0 } },    // �ס�����������
                new { Gan = new[] { 1, 9 }, Zhi = new[] { 3, 1 } },    // �ҡ����î����
                new { Gan = new[] { 4 }, Zhi = new[] { 8 } },          // �����
                new { Gan = new[] { 5 }, Zhi = new[] { 7 } },          // ����δ
                new { Gan = new[] { 3 }, Zhi = new[] { 11 } },         // ������
                new { Gan = new[] { 6 }, Zhi = new[] { 6 } },          // ������
                new { Gan = new[] { 7 }, Zhi = new[] { 5 } },          // ������
                new { Gan = new[] { 8 }, Zhi = new[] { 4 } }           // �ɼ���
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
        /// ��º�
        /// �����ɣ�î���ȣ����¶������±�����������δ�¼�
        /// �����죬���º��������������¸��������꣬������
        /// </summary>
        private static bool IsTianDeHe(EarthBranch monthBranch, HeavenStem stem)
        {
            var rules = new Dictionary<int, int>
            {
                { 2, 8 },  // ���¼���
                { 4, 3 },  // ���¼���
                { 5, 2 },  // ���¼���
                { 7, 5 },  // δ�¼���
                { 8, 4 },  // ���¼���
                { 10, 7 }, // ���¼���
                { 11, 6 }, // ���¼���
                { 1, 1 }   // ���¼���
            };

            return rules.ContainsKey(monthBranch.Index) && rules[monthBranch.Index] == stem.Index;
        }

        /// <summary>
        /// ��ºϣ���֧�汾��
        /// </summary>
        private static bool IsTianDeHe(EarthBranch monthBranch, EarthBranch branch)
        {
            var rules = new Dictionary<int, int>
            {
                { 3, 5 },  // î�¼���
                { 6, 2 },  // ���¼���
                { 9, 11 }, // ���¼���
                { 0, 8 }   // ���¼���
            };

            return rules.ContainsKey(monthBranch.Index) && rules[monthBranch.Index] == branch.Index;
        }

        /// <summary>
        /// �µº�
        /// �������¼��������ӳ��¼��������ϳ��¼��ң���îδ�¼���
        /// </summary>
        private static bool IsYueDeHe(EarthBranch monthBranch, HeavenStem stem)
        {
            var rules = new Dictionary<int, int[]>
            {
                { 7, new[] { 2, 6, 10 } },  // ����Ӧ������
                { 3, new[] { 8, 0, 4 } },   // ����Ӧ���ӳ�
                { 1, new[] { 5, 9, 1 } },   // �Ҷ�Ӧ���ϳ�
                { 5, new[] { 11, 3, 7 } }   // ����Ӧ��îδ
            };

            foreach (var rule in rules)
            {
                if (stem.Index == rule.Key && rule.Value.Contains(monthBranch.Index))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// �ų�
        /// ���ϡ����ӡ����硢��î�����ϡ���î�����ϡ����ӡ�����
        /// </summary>
        private static bool IsJiuChou(HeavenStem dayStem, EarthBranch dayBranch)
        {
            var validPairs = new HashSet<string>
            {
                "����", "����", "����", "��î", "����",
                "��î", "����", "����", "����"
            };

            var dayPillar = dayStem.GetName() + dayBranch.GetName();
            return validPairs.Contains(dayPillar);
        }

        /// <summary>
        /// ��ר
        /// ��������î����δ�����硢��δ�����ꡢ���ϡ����
        /// </summary>
        private static bool IsBaZhuan(HeavenStem dayStem, EarthBranch dayBranch)
        {
            var validPairs = new HashSet<string>
            {
                "����", "��î", "��δ", "����",
                "��δ", "����", "����", "���"
            };

            var dayPillar = dayStem.GetName() + dayBranch.GetName();
            return validPairs.Contains(dayPillar);
        }

        /// <summary>
        /// �ж�����Ƿ�Ϊ����
        /// </summary>
        private static bool IsYangStem(HeavenStem stem)
        {
            // �ס������졢������Ϊ���ɣ�������0��2��4��6��8��
            return stem.Index % 2 == 0;
        }

        /// <summary>
        /// ��ȡ��֧����ʮ�����е�˳��1-60��
        /// </summary>
        private static int GetJiaZiOrder(string ganZhi)
        {
            var index = Array.IndexOf(JIAZI, ganZhi);
            return index;
        }

        #endregion

        #region ��������

        /// <summary>
        /// ���ݵ�֧���ƻ�ȡ��֧����
        /// </summary>
        private static EarthBranch GetEarthBranchByName(string name)
        {
            return EarthBranch.FromName(name);
        }

        /// <summary>
        /// ����������ƻ�ȡ��ɶ���
        /// </summary>
        private static HeavenStem GetHeavenStemByName(string name)
        {
            return HeavenStem.FromName(name);
        }

        #endregion
    }

    /// <summary>
    /// ��ɷ��չ����
    /// </summary>
    public static class ShenShaExtensions
    {
        /// <summary>
        /// ��ȡ������ɷ
        /// </summary>
        /// <param name="eightChar">���ֶ���</param>
        /// <param name="isMale">�Ա�trueΪ����</param>
        /// <returns>��ɷ�����б�</returns>
        public static List<string> GetShenSha(this EightChar eightChar, bool isMale)
        {
            var yearSound = eightChar.Year.Sound.GetName();

            // ��ѯ������ɷ
            return PersonalGod.QueryShenSha(
                eightChar.Day,
                eightChar,
                isMale,
                PersonalGod.PillarPosition.Day,
                yearSound
            );
        }

        /// <summary>
        /// ��ȡָ����λ����ɷ
        /// </summary>
        /// <param name="eightChar">���ֶ���</param>
        /// <param name="pillar">Ҫ��ѯ����</param>
        /// <param name="position">��λ</param>
        /// <param name="isMale">�Ա�</param>
        /// <returns>��ɷ�����б�</returns>
        public static List<string> GetShenShaByPillar(this EightChar eightChar,
            SixtyCycle pillar,
            PersonalGod.PillarPosition position,
            bool isMale)
        {
            var yearSound = eightChar.Year.Sound.GetName();
            return PersonalGod.QueryShenSha(pillar, eightChar, isMale, position, yearSound);
        }

        /// <summary>
        /// ��ȡ����������ɷ��ȥ�أ�
        /// </summary>
        /// <param name="eightChar">���ֶ���</param>
        /// <param name="isMale">�Ա�</param>
        /// <returns>��ɷ�����б�</returns>
        public static List<string> GetAllShenSha(this EightChar eightChar, bool isMale)
        {
            var yearSound = eightChar.Year.Sound.GetName();
            var allShenSha = new HashSet<string>();

            // ����
            var yearShenSha = PersonalGod.QueryShenSha(
                eightChar.Year,
                eightChar,
                isMale,
                PersonalGod.PillarPosition.Year,
                yearSound
            );
            foreach (var sha in yearShenSha)
                allShenSha.Add(sha);

            // ����
            var monthShenSha = PersonalGod.QueryShenSha(
                eightChar.Month,
                eightChar,
                isMale,
                PersonalGod.PillarPosition.Month,
                yearSound
            );
            foreach (var sha in monthShenSha)
                allShenSha.Add(sha);

            // ����
            var dayShenSha = PersonalGod.QueryShenSha(
                eightChar.Day,
                eightChar,
                isMale,
                PersonalGod.PillarPosition.Day,
                yearSound
            );
            foreach (var sha in dayShenSha)
                allShenSha.Add(sha);

            // ʱ��
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
        /// ��ȡ������ɷ��ֵ�ԣ��������ܣ�
        /// </summary>
        /// <param name="eightChar">���ֶ���</param>
        /// <param name="isMale">�Ա�</param>
        /// <returns>������ɷ�ֵ䣬��Ϊ��λ����("����"/"����"/"����"/"ʱ��")��ֵΪ��ɷ�б�</returns>
        public static Dictionary<string, List<string>> GetShenShaByPillars(this EightChar eightChar, bool isMale)
        {
            var yearSound = eightChar.Year.Sound.GetName();
            var result = new Dictionary<string, List<string>>();

            var pillars = new[]
            {
                ("����", eightChar.Year, PersonalGod.PillarPosition.Year),
                ("����", eightChar.Month, PersonalGod.PillarPosition.Month),
                ("����", eightChar.Day, PersonalGod.PillarPosition.Day),
                ("ʱ��", eightChar.Hour, PersonalGod.PillarPosition.Hour)
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
