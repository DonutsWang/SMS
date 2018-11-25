using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace Common.Confing
{
    public class BasicConfig
    {

        /// <summary>
        /// 中文转pinyin
        /// </summary>
        /// <param name="hzString">中文字符集</param>
        /// <returns></returns>
        public string zhtopy(string hzString)
        {
            int[] pyValue = new int[]    {
    -20319,-20317,-20304,-20295,-20292,-20283,-20265,-20257,-20242,-20230,-20051,-20036,
    -20032,-20026,-20002,-19990,-19986,-19982,-19976,-19805,-19784,-19775,-19774,-19763,
    -19756,-19751,-19746,-19741,-19739,-19728,-19725,-19715,-19540,-19531,-19525,-19515,
    -19500,-19484,-19479,-19467,-19289,-19288,-19281,-19275,-19270,-19263,-19261,-19249,
    -19243,-19242,-19238,-19235,-19227,-19224,-19218,-19212,-19038,-19023,-19018,-19006,
    -19003,-18996,-18977,-18961,-18952,-18783,-18774,-18773,-18763,-18756,-18741,-18735,
    -18731,-18722,-18710,-18697,-18696,-18526,-18518,-18501,-18490,-18478,-18463,-18448,
    -18447,-18446,-18239,-18237,-18231,-18220,-18211,-18201,-18184,-18183, -18181,-18012,
    -17997,-17988,-17970,-17964,-17961,-17950,-17947,-17931,-17928,-17922,-17759,-17752,
    -17733,-17730,-17721,-17703,-17701,-17697,-17692,-17683,-17676,-17496,-17487,-17482,
    -17468,-17454,-17433,-17427,-17417,-17202,-17185,-16983,-16970,-16942,-16915,-16733,
    -16708,-16706,-16689,-16664,-16657,-16647,-16474,-16470,-16465,-16459,-16452,-16448,
    -16433,-16429,-16427,-16423,-16419,-16412,-16407,-16403,-16401,-16393,-16220,-16216,
    -16212,-16205,-16202,-16187,-16180,-16171,-16169,-16158,-16155,-15959,-15958,-15944,
    -15933,-15920,-15915,-15903,-15889,-15878,-15707,-15701,-15681,-15667,-15661,-15659,
    -15652,-15640,-15631,-15625,-15454,-15448,-15436,-15435,-15419,-15416,-15408,-15394,
    -15385,-15377,-15375,-15369,-15363,-15362,-15183,-15180,-15165,-15158,-15153,-15150,
    -15149,-15144,-15143,-15141,-15140,-15139,-15128,-15121,-15119,-15117,-15110,-15109,
    -14941,-14937,-14933,-14930,-14929,-14928,-14926,-14922,-14921,-14914,-14908,-14902,
    -14894,-14889,-14882,-14873,-14871,-14857,-14678,-14674,-14670,-14668,-14663,-14654,
    -14645,-14630,-14594,-14429,-14407,-14399,-14384,-14379,-14368,-14355,-14353,-14345,
    -14170,-14159,-14151,-14149,-14145,-14140,-14137,-14135,-14125,-14123,-14122,-14112,
    -14109,-14099,-14097,-14094,-14092,-14090,-14087,-14083,-13917,-13914,-13910,-13907,
    -13906,-13905,-13896,-13894,-13878,-13870,-13859,-13847,-13831,-13658,-13611,-13601,
    -13406,-13404,-13400,-13398,-13395,-13391,-13387,-13383,-13367,-13359,-13356,-13343,
    -13340,-13329,-13326,-13318,-13147,-13138,-13120,-13107,-13096,-13095,-13091,-13076,
    -13068,-13063,-13060,-12888,-12875,-12871,-12860,-12858,-12852,-12849,-12838,-12831,
    -12829,-12812,-12802,-12607,-12597,-12594,-12585,-12556,-12359,-12346,-12320,-12300,
    -12120,-12099,-12089,-12074,-12067,-12058,-12039,-11867,-11861,-11847,-11831,-11798,
    -11781,-11604,-11589,-11536,-11358,-11340,-11339,-11324,-11303,-11097,-11077,-11067,
    -11055,-11052,-11045,-11041,-11038,-11024,-11020,-11019,-11018,-11014,-10838,-10832,
    -10815,-10800,-10790,-10780,-10764,-10587,-10544,-10533,-10519,-10331,-10329,-10328,
    -10322,-10315,-10309,-10307,-10296,-10281,-10274,-10270,-10262,-10260,-10256,-10254
    };
            string[] pyName = new string[]    {
    "A","Ai","An","Ang","Ao","Ba","Bai","Ban","Bang","Bao","Bei","Ben",
    "Beng","Bi","Bian","Biao","Bie","Bin","Bing","Bo","Bu","Ba","Cai","Can",
    "Cang","Cao","Ce","Ceng","Cha","Chai","Chan","Chang","Chao","Che","Chen","Cheng",
    "Chi","Chong","Chou","Chu","Chuai","Chuan","Chuang","Chui","Chun","Chuo","Ci","Cong",
    "Cou","Cu","Cuan","Cui","Cun","Cuo","Da","Dai","Dan","Dang","Dao","De",
    "Deng","Di","Dian","Diao","Die","Ding","Diu","Dong","Dou","Du","Duan","Dui",
    "Dun","Duo","E","En","Er","Fa","Fan","Fang","Fei","Fen","Feng","Fo",
    "Fou","Fu","Ga","Gai","Gan","Gang","Gao","Ge","Gei","Gen","Geng","Gong",
    "Gou","Gu","Gua","Guai","Guan","Guang","Gui","Gun","Guo","Ha","Hai","Han",
    "Hang","Hao","He","Hei","Hen","Heng","Hong","Hou","Hu","Hua","Huai","Huan",
    "Huang","Hui","Hun","Huo","Ji","Jia","Jian","Jiang","Jiao","Jie","Jin","Jing",
    "Jiong","Jiu","Ju","Juan","Jue","Jun","Ka","Kai","Kan","Kang","Kao","Ke",
    "Ken","Keng","Kong","Kou","Ku","Kua","Kuai","Kuan","Kuang","Kui","Kun","Kuo",
    "La","Lai","Lan","Lang","Lao","Le","Lei","Leng","Li","Lia","Lian","Liang",
    "Liao","Lie","Lin","Ling","Liu","Long","Lou","Lu","Lv","Luan","Lue","Lun",
    "Luo","Ma","Mai","Man","Mang","Mao","Me","Mei","Men","Meng","Mi","Mian",
    "Miao","Mie","Min","Ming","Miu","Mo","Mou","Mu","Na","Nai","Nan","Nang",
    "Nao","Ne","Nei","Nen","Neng","Ni","Nian","Niang","Niao","Nie","Nin","Ning",
    "Niu","Nong","Nu","Nv","Nuan","Nue","Nuo","O","Ou","Pa","Pai","Pan",
    "Pang","Pao","Pei","Pen","Peng","Pi","Pian","Piao","Pie","Pin","Ping","Po",
    "Pu","Qi","Qia","Qian","Qiang","Qiao","Qie","Qin","Qing","Qiong","Qiu","Qu",
    "Quan","Que","Qun","Ran","Rang","Rao","Re","Ren","Reng","Ri","Rong","Rou",
    "Ru","Ruan","Rui","Run","Ruo","Sa","Sai","San","Sang","Sao","Se","Sen",
    "Seng","Sha","Shai","Shan","Shang","Shao","She","Shen","Sheng","Shi","Shou","Shu",
    "Shua","Shuai","Shuan","Shuang","Shui","Shun","Shuo","Si","Song","Sou","Su","Suan",
    "Sui","Sun","Suo","Ta","Tai","Tan","Tang","Tao","Te","Teng","Ti","Tian",
    "Tiao","Tie","Ting","Tong","Tou","Tu","Tuan","Tui","Tun","Tuo","Wa","Wai",
    "Wan","Wang","Wei","Wen","Weng","Wo","Wu","Xi","Xia","Xian","Xiang","Xiao",
    "Xie","Xin","Xing","Xiong","Xiu","Xu","Xuan","Xue","Xun","Ya","Yan","Yang",
    "Yao","Ye","Yi","Yin","Ying","Yo","Yong","You","Yu","Yuan","Yue","Yun",
    "Za", "Zai","Zan","Zang","Zao","Ze","Zei","Zen","Zeng","Zha","Zhai","Zhan",
    "Zhang","Zhao","Zhe","Zhen","Zheng","Zhi","Zhong","Zhou","Zhu","Zhua","Zhuai","Zhuan",
    "Zhuang","Zhui","Zhun","Zhuo","Zi","Zong","Zou","Zu","Zuan","Zui","Zun","Zuo"
    };
            // 匹配中文字符
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^[\u4e00-\u9fa5]$");
            byte[] array = new byte[2];
            string pyString = "";
            int chrAsc = 0;
            int i1 = 0;
            int i2 = 0;
            char[] noWChar = hzString.ToCharArray();

            for (int j = 0; j < noWChar.Length; j++)
            {
                // 中文字符
                if (regex.IsMatch(noWChar[j].ToString()))
                {
                    array = System.Text.Encoding.Default.GetBytes(noWChar[j].ToString());
                    i1 = (short)(array[0]);
                    i2 = (short)(array[1]);
                    chrAsc = i1 * 256 + i2 - 65536;
                    if (chrAsc > 0 && chrAsc < 160)
                    {
                        pyString += noWChar[j];
                    }
                    else
                    {
                        // 修正部分文字
                        if (chrAsc == -9254)  // 修正“圳”字
                            pyString += "Zhen";
                        else
                        {
                            for (int i = (pyValue.Length - 1); i >= 0; i--)
                            {
                                if (pyValue[i] <= chrAsc)
                                {
                                    pyString += pyName[i];
                                    break;
                                }
                            }
                        }
                    }
                }
                // 非中文字符
                else
                {
                    pyString += noWChar[j].ToString();
                }
            }
            return pyString;
        }


        /// <summary> 
        /// 将字符串使用base64算法加密 
        /// </summary> 
        /// <param name="code_type">编码类型（编码名称） 
        /// * 代码页 名称 
        /// * 1200 "UTF-16LE"、"utf-16"、"ucs-2"、"unicode"或"ISO-10646-UCS-2" 
        /// * 1201 "UTF-16BE"或"unicodeFFFE" 
        /// * 1252 "windows-1252" 
        /// * 65000 "utf-7"、"csUnicode11UTF7"、"unicode-1-1-utf-7"、"unicode-2-0-utf-7"、"x-unicode-1-1-utf-7"或"x-unicode-2-0-utf-7" 
        /// * 65001 "utf-8"、"unicode-1-1-utf-8"、"unicode-2-0-utf-8"、"x-unicode-1-1-utf-8"或"x-unicode-2-0-utf-8" 
        /// * 20127 "us-ascii"、"us"、"ascii"、"ANSI_X3.4-1968"、"ANSI_X3.4-1986"、"cp367"、"csASCII"、"IBM367"、"iso-ir-6"、"ISO646-US"或"ISO_646.irv:1991" 
        /// * 54936 "GB18030" 
        /// </param> 
        /// <param name="code">待加密的字符串</param> 
        /// <returns>加密后的字符串</returns> 
        public string EncodeBase64(string code_type, string code)
        {
            string encode = "";
            byte[] bytes = System.Text.Encoding.GetEncoding(code_type).GetBytes(code); //将一组字符编码为一个字节序列. 
            try
            {
                encode = Convert.ToBase64String(bytes); //将8位无符号整数数组的子集转换为其等效的,以64为基的数字编码的字符串形式. 
            }
            catch
            {
                encode = code;
            }
            return encode;
        }
        /// <summary>
        /// 去除html 
        /// </summary>
        /// <param name="html">html</param>
        /// <returns></returns>
        public string CheckHtml(string html)
        {
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" no[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件
            html = regex4.Replace(html, ""); //过滤iframe
            html = regex5.Replace(html, ""); //过滤frameset
            html = regex6.Replace(html, ""); //过滤frameset
            html = regex7.Replace(html, ""); //过滤frameset
            html = regex8.Replace(html, ""); //过滤frameset
            html = regex9.Replace(html, "");
            html = html.Replace(" ", "");
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            return html;
        }
        /// <summary> 
        /// 将字符串使用base64算法解密 
        /// </summary> 
        /// <param name="code_type">编码类型</param> 
        /// <param name="code">已用base64算法加密的字符串</param> 
        /// <returns>解密后的字符串</returns> 
        public string DecodeBase64(string code_type, string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code); //将2进制编码转换为8位无符号整数数组. 
            try
            {
                decode = System.Text.Encoding.GetEncoding(code_type).GetString(bytes); //将指定字节数组中的一个字节序列解码为一个字符串。 
            }
            catch
            {
                decode = code;
            }
            return decode;
        }


        /// <summary>
        /// 切割字符
        /// </summary>
        /// <param name="SplitText">需要切割的文字</param>
        /// <param name="SplitFun">切割的方法（字节位）</param>
        /// <returns></returns>
        public string[] splitRep(string SplitText, string SplitFun)
        {

            string[] h = System.Text.RegularExpressions.Regex.Split(SplitText, SplitFun);
            string[] _T = new string[h.Length];
            try
            {
                for (int i = 0; i < h.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        _T[i] = h[i].Substring(0, h[i].Length - 1);
                    }
                    else
                    {
                        _T[i] = h[i].Substring(1);
                    }
                }
                return _T;
            }
            catch { }
            return _T;
        }



        /// <summary>
        /// 数据转Json
        /// </summary>
        /// <param name="dt">数据</param>
        /// <returns></returns>
        /// 
        public string DataTableToJson(DataTable dt)
        {
            System.Text.StringBuilder jsonBuilder = new System.Text.StringBuilder();
            jsonBuilder.Append("{\"");
            jsonBuilder.Append(dt.TableName);
            jsonBuilder.Append("\":[");
            //jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            if (dt.Rows.Count > 0)
            {
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            }
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        /// <summary>
        /// 数据转Json总条数(ExtJs专用)
        /// </summary>
        /// <param name="dt">数据</param>
        /// <param name="totalNumber">数据总条数</param>
        /// <returns></returns>
        public string DataTableToJson(DataTable dt, int totalNumber)
        {
            System.Text.StringBuilder jsonBuilder = new System.Text.StringBuilder();
            jsonBuilder.Append("{\"");
            jsonBuilder.Append(dt.TableName);
            jsonBuilder.Append("\":[");
            //jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            if (dt.Rows.Count > 0)
            {
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            }
            jsonBuilder.Append("]");
            jsonBuilder.Append(",total:'" + totalNumber + "'");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }

        /// <summary>
        /// 时间转换
        /// </summary>
        /// <param name="date">时间</param>
        /// <param name="dates">转换格式</param>
        /// <returns></returns>
        public string TransformationDate(object date, string dates)
        {
            try
            {
                return Convert.ToDateTime(date).ToString(dates);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 时间计算
        /// </summary>
        /// <param name="DateString">原始时间</param>
        /// <param name="DateCount">天数(增加100天计算：100，减少100天：-100)</param>
        /// <returns></returns>
        public DateTime Todate(string DateString, double DateCount)
        {
            return Convert.ToDateTime(DateString).AddDays(DateCount);
        }



        /// <summary>
        /// 基础配置所需要的参数
        /// </summary>
        public BasicConfig()
        {
            ////
        }
        /// <summary>
        /// 位置
        /// </summary>
        /// <returns></returns>
        public string MapFile()
        {
            //GetDirectories
            //return System.Web.HttpContext.Current.Server.MapPath(".");
            return AppDomain.CurrentDomain.BaseDirectory;
        }
        /// <summary>
        /// url post 查询 如果xxx.aspx/xxx/xxx
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string UrlPage(string url)
        {
            if (url != "")
            {
                return new ShowUrlConfig().GetUrl(url);
            }
            return null;

        }

        /// <summary>
        /// 去除HTML标记
        /// </summary>
        /// <param name="Htmlstring"></param>
        /// <returns></returns>
        public string StripHTML(string Htmlstring)
        {
            //删除脚本

            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);

            //删除HTML

            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");

            Htmlstring.Replace(">", "");

            Htmlstring.Replace("\r\n", "");

            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

            return Htmlstring;

        }


        /// <summary>
        ///md5 加密
        /// </summary>
        /// <param name="str">输入内容</param>
        /// <param name="code">格式　16或32</param>
        /// <returns></returns>
        public string ToMd5(string str, int code)
        {
            if (code == 16) //16位MD5加密（取32位加密的9~25字符） 
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
            }

            if (code == 32) //32位加密 
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
            }

            return "00000000000000000000000000000000";
        }
        /// <summary>
        /// 输出 url 转 字符
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public string ToUrlToText(string Text)
        {
            return HttpUtility.UrlDecode(Text, System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// 输出 字符 转 url 
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public string GetUrlToText(string Url)
        {


            return HttpUtility.UrlEncode(Url, System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// 转符号
        /// </summary>
        /// <param name="ReplaceText">需要转换的　string 数据</param>
        /// <returns></returns>
        public string ToText(string ReplaceText)
        {

            ArrayList arr = ReadBasic(ReplaceText);

            for (int i = 0; i < arr.Count - 1; i++)
            {
                string[] job = (string[])arr[i];
                if (ReplaceText.IndexOf(job[0]) != -1)
                    ReplaceText = @ReplaceText.Replace(job[0], job[1]);

            }
            return ReplaceText;
        }

        public string GetText(string ReplaceText)
        {

            ArrayList arr = ReadBasic(ReplaceText);
            for (int i = 0; i < arr.Count - 1; i++)
            {
                string[] job = (string[])arr[i];
                if (ReplaceText.IndexOf(job[1]) != -1)
                    ReplaceText = @ReplaceText.Replace(job[1], job[0]);
            }

            return ReplaceText;
        }

        private ArrayList ReadBasic(string rtxt)
        {
            ArrayList arr = new ArrayList();
            arr.Add(new string[] { " and ", "　and　" });
            arr.Add(new string[] { " or ", "　or　" });
            arr.Add(new string[] { " exec ", "　exec　" });
            arr.Add(new string[] { " insert ", "　insert　" });
            arr.Add(new string[] { " select ", "　select　" });
            arr.Add(new string[] { " delete ", "　delete　" });
            arr.Add(new string[] { " update ", "　update　" });
            arr.Add(new string[] { "*", "*　" });
            arr.Add(new string[] { "%", "%　" });
            arr.Add(new string[] { "\'", "’" });
            arr.Add(new string[] { "\"", "‘" });
            return arr;
        }

        /// <summary>
        /// 转符号
        /// </summary>
        /// <param name="job">需要转换的　string 数据</param>
        /// <param name="Rex"> 替换</param>
        /// <returns></returns>
        public string ToText(string job, string Text, string Rex)
        {

            return Regex.Replace(job, Text, Rex).ToString();

        }

        /// <summary>
        /// 用户控件下拉菜单
        /// </summary>
        /// <param name="Control">控件名</param>
        /// <param name="Table">Table 数据</param>
        /// <param name="Table_value">value 参数</param>
        /// <param name="Table_name">name 参数名</param>
        public void DataMenu(System.Web.UI.WebControls.DropDownList Control, DataSet Table, string Table_value, string Table_name)
        {

            Control.DataSource = Table.Tables[0].DefaultView;
            Control.DataTextField = Table_name;
            Control.DataValueField = Table_value;
            Control.DataBind();
            Control.Items.Insert(0, new ListItem("请选择", ""));
        }
        /// <summary>
        /// 用户控件下拉菜单
        /// </summary>
        /// <param name="Control">控件名</param>
        /// <param name="Table">Table 数据</param>
        /// <param name="Table_value">value 参数</param>
        /// <param name="Table_name">name 参数名</param>
        public void DataMenu(System.Web.UI.WebControls.DropDownList Control, DataTable Table, string Table_value, string Table_name)
        {
            Control.DataSource = Table.DefaultView;
            Control.DataTextField = Table_name;
            Control.DataValueField = Table_value;
            Control.DataBind();
            Control.Items.Insert(0, new ListItem("请选择", ""));
        }

        /// <summary>
        /// 用户控件下拉菜单
        /// </summary>
        /// <param name="Control">控件名</param>
        /// <param name="Table">Table 数据</param>
        /// <param name="Table_value">value 参数</param>
        /// <param name="Table_name">name 参数名</param>
        public void DataMenu(System.Web.UI.WebControls.DropDownList Control, DataTable Table, string Table_value, string Table_name, bool D)
        {
            Control.DataSource = Table.DefaultView;
            Control.DataTextField = Table_name;
            Control.DataValueField = Table_value;
            Control.DataBind();
            // Control.Items.Insert(0, new ListItem("", ""));
        }
        /// <summary>
        /// 分页(只适合小型数据库)
        /// </summary>
        /// <param name="PageDataView">数据源视图(DefaultView)</param>
        /// <param name="pagecount">请求第几页</param>
        /// <param name="pagesize">每页显示的行数</param>
        /// <returns></returns>
        public PagedDataSource UCpage(DataView PageDataView, string pagecount, int pagesize)
        {
            PagedDataSource page = new PagedDataSource();
            page.DataSource = PageDataView;
            page.AllowPaging = true;
            page.PageSize = pagesize;
            int CurPage;
            if (pagecount != null)
            {
                CurPage = Convert.ToInt32(pagecount);
            }
            else
            {
                CurPage = 1;
            }
            page.CurrentPageIndex = CurPage - 1;
            return page;
        }


        /// <summary>
        /// 列排 列排出不许要的参数
        /// </summary>
        /// <param name="OutRowDate">需要列排的数据</param>
        /// <param name="OutText">列排掉的数据</param>
        /// <param name="SplitStyle">列排分割参数/样式</param>
        /// <returns></returns>
        public string OutRow(String OutRowDate, String OutText, Char SplitStyle)
        {
            try
            {
                string text = null;
                string[] str = OutRowDate.Split(SplitStyle);
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] != OutText)
                    {

                        text += str[i] + ",";
                    }
                }
                int textcount = text.Length - 1;
                return text.Substring(0, textcount);
            }
            catch
            {
                return "";
            }

        }
        /// <summary> 
        /// 发送邮件 
        /// </summary> 
        /// <param name="to">收件人邮件地址</param> 
        /// <param name="from">发件人邮件地址</param> 
        /// <param name="subject">邮件主题</param> 
        /// <param name="body">邮件内容</param> 
        /// <param name="userName">登录smtp主机时用到的用户名,注意是邮件地址'@'以前的部分</param> 
        /// <param name="password">登录smtp主机时用到的用户密码</param> 
        /// <param name="smtpHost">发送邮件用到的smtp主机</param> 
        public void ToMail(string to, string from, string subject, string body, string userName, string password, string smtpHost)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(from);
            message.IsBodyHtml = true;
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body;
            SmtpClient client = new SmtpClient(smtpHost);
            client.Credentials = new NetworkCredential(userName, password);
            client.Send(message);
        }


        /// <summary>
        /// 返回页面
        /// </summary>
        /// <returns></returns>
        public String IEurl()
        {

            string strTemp = "";


            strTemp = strTemp + HttpContext.Current.Request.ServerVariables["URL"];

            if (HttpContext.Current.Request.QueryString != null)
            {
                strTemp = strTemp + "?" + HttpContext.Current.Request.QueryString;
            }
            return strTemp;
        }
        /// <summary>
        /// 返回页面
        /// </summary>
        /// <param name="Fun">方法</param>
        /// <returns></returns>
        public String IEurl(int Fun)
        {

            switch (Fun)
            {
                case 0:
                    return HttpContext.Current.Request.ServerVariables["http_referer"];
                //break;
                case 1:
                    return HttpContext.Current.Request.RawUrl.ToString();
                //break;
            }
            return null;
        }



        /// <summary>
        ///  url 函数
        /// </summary>
        class ShowUrlConfig
        {
            private string UrlConfig;

            public ShowUrlConfig(string url)
            {
                UrlConfig = url;
            }
            public ShowUrlConfig()
            {
                ///
            }
            private string[] SetUrl()
            {

                string url;
                string[] urladd;
                url = HttpContext.Current.Request.Url.LocalPath;
                urladd = url.Split("/".ToCharArray());
                return urladd;
            }
            /// <summary>
            /// 获取 参数
            /// </summary>
            /// <param name="urlname"></param>
            /// <returns></returns>
            private string SetUrl(string urlname)
            {
                try
                {

                    string[] urlclass;
                    int i;
                    urlclass = SetUrl();
                    for (i = 0; i < urlclass.Length; i++)
                    {
                        if (urlclass[i] == urlname)
                        {
                            return urlclass[i + 1];

                        }
                    }
                }
                catch
                {
                    return null;
                }
                return null;
            }
            public string GetUrl()
            {
                return SetUrl(UrlConfig);
            }
            public string GetUrl(string UrlName)
            {
                return SetUrl(UrlName);
            }
        }
        public string GetJSON(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            try
            {
                if (dt.Rows.Count > 0)
                {
                    Hashtable ht = new Hashtable();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        ht.Add(i, dt.Columns[i].ColumnName);
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("{");
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            sb.Append(string.Format("\"{0}\":\"{1}\",",
                            ht[j], dt.Rows[i][j].ToString()));
                        }
                        sb.Remove(sb.ToString().LastIndexOf(","), 1);
                        sb.Append("},");
                    }
                    sb.Remove(sb.ToString().LastIndexOf(","), 1);
                    ht.Clear();
                    ht = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            finally
            {
                sb.Append("]");
            }

            return sb.ToString();

        }

        /// <summary>
        /// 对后台增删改操作的结果以Json的形式返回
        /// </summary>
        /// <param name="success">后台操作结果 一般为True or False</param>
        /// <param name="message">希望前台提示的信息</param>
        /// <returns></returns>
        public string ReturnJson(bool success, string message)
        {
            string json = "";
            if (success.ToString() != "")
            {
                json += "success:" + success.ToString().ToLower();
            }
            if (message != "")
            {
                json += ",message:'" + message + "'";
            }
            return "{" + json + "}";
        }

        /// <summary>
        /// 对后台增加操作和对地区人员人数的校验结果以Json的形式返回
        /// </summary>
        /// <param name="success">后台操作结果 一般为True or False</param>
        /// <param name="number">对人数的校验  一般为True or False</param>
        /// <returns></returns>
        public string ReturnJson(bool success, bool number)
        {
            string json = "";
            if (success.ToString() != "")
            {
                json += "success:" + success.ToString().ToLower();
            }
            if (number.ToString() != "")
            {
                json += ",number:" + number.ToString().ToLower();
            }
            return "{" + json + "}";
        }


        /// <summary>
        ///  list 转换 datetable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public DataTable ListToDataTable<T>(List<T> entitys)
        {

            DataTable dt = new DataTable("Table");
            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                //throw new Exception("需转换的集合为空");
                DataTable Nulldt = new DataTable("Table");
                return Nulldt;
            }
            else
            {
                //取出第一个实体的所有Propertie

            Type entityType = entitys[0].GetType();

            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
               
            for (int i = 0; i < entityProperties.Length; i++)
            {
                //dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                dt.Columns.Add(entityProperties[i].Name);
            }

            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);

                }
                dt.Rows.Add(entityValues);
            }
            }
            return dt;
        }


        /// <summary>
        /// 根据集合直接转换成JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public string DataTableToJson<T>(List<T> entitys)
        {
            DataTable dt = ListToDataTable(entitys);
            return  DataTableToJson(dt);
        }

        /// <summary>
        /// 根据集合直接转换成JSON,带总的条数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public string DataTableToJson<T>(List<T> entitys,int totalnumber)
        {
            DataTable dt = ListToDataTable(entitys);
            return DataTableToJson(dt,totalnumber);
        }


        #region 拼接复杂Json -张辰

        /// <summary>
        /// DataTable转Json- 张辰
        /// </summary>
        /// <param name="TableName">数据集的名称</param>
        /// <param name="dt">要处理的数据Datatable</param>
        /// <param name="isJoin">是否是拼接 true/false</param>
        /// <returns>Json</returns>
        public string DataTableToJson(string TableName, DataTable dt, bool isJoin)
        {
            System.Text.StringBuilder jsonBuilder = new System.Text.StringBuilder();
            if (isJoin)
            {
                jsonBuilder.Append(",");
            }
            jsonBuilder.Append("{\"");
            jsonBuilder.Append(TableName);
            jsonBuilder.Append("\":[");
            //jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                } 
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            if (dt.Rows.Count > 0)
            {
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            }
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }


        /// <summary>
        /// 集合转Json - 张辰
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitys">集合</param>
        /// <param name="TableName">数据集名称</param>
        /// <param name="isJoin">是否是拼接 true/false</param>
        /// <returns></returns>
        public string ListToJson<T>(List<T> entitys, string TableName,bool isJoin)
        {
            DataTable dt = ListToDataTable(entitys);
            return DataTableToJson(TableName, dt, isJoin);
        }

        /// <summary>
        /// 包含全部的Json集 -张辰
        /// </summary>
        /// <param name="Json">Json数据集</param>
        /// <returns></returns>
        public string ContainFather(string Json)
        {
            return "{" + Json + "}";
        }

        /// <summary>
        /// 包含Json集(仅限包含一个类型的数据集) - 张辰
        /// </summary>
        /// <param name="Json">Json数据集</param>
        /// <param name="TableName">数据集名称</param>
        /// <param name="isJoin">是否是拼接 true/false</param>
        /// <returns></returns>
        public string ContainSon(string Json, string TableName, bool isJoin)
        {
            if (isJoin)
            {
                return ",\"" + TableName + "\":[" + Json + "]";
            }
            else
            {
                return "\"" + TableName + "\":[" + Json + "]";
            }
        }


        //实例
        //  string zc = "";
        //  zc = ContainFather
        //    (
        //        ContainSon
        //        (
        //            DataTableToJson("哈哈", postrate, false) + DataTableToJson("哈哈", postrate, true), "第一", false
        //        ) + 
        //        ContainSon
        //        (
        //            DataTableToJson("哈哈", postrate, false) + DataTableToJson("哈哈", postrate, true), "第二", true
        //        )
        //    );

        
        /// <summary>
        ///  数据转Json支持对象拼接
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <param name="sname">格外的列名</param>
        /// <param name="code">格外的列名里的值</param>
        /// <returns></returns>
        public string DataTableToJson(DataTable dt, string[] sname, string[] code)
        {
            System.Text.StringBuilder jsonBuilder = new System.Text.StringBuilder();
            jsonBuilder.Append("{\"");
            jsonBuilder.Append(dt.TableName);
            jsonBuilder.Append("\":[");
            //jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                //jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                for (int a = 0; a < sname.Length; a++)
                {
                    jsonBuilder.Append("\"" + sname[a] + "\":\"" + code[a] + "\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            if (dt.Rows.Count > 0)
            {
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            }

            jsonBuilder.Append("]");  
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }

        /// <summary>
        ///  数据转Json支持对象拼接
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <param name="sname">格外的列名</param>
        /// <param name="code">格外的列名里的值</param>
        /// <returns></returns>
        public string DataTableToJson<T>(List<T> entitys, string[] sname, string[] code)
        {
            DataTable dt = ListToDataTable(entitys);
            return DataTableToJson(dt,sname,code);
        }


        #endregion
    }

}
