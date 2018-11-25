using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Plugins
{
    public class PageNavigation
    {
        private int totalNumber;
        /// <summary>
        /// 总数据条数
        /// </summary>
        public int TotalNumber
        {
            get { return totalNumber; }
            set { totalNumber = value; }
        }
        private int pageCount;
        /// <summary>
        /// 单页显示条数
        /// </summary>
        public int PageCount
        {
            get { return pageCount; }
            set { pageCount = value; }
        }
        private int pageNum;
        /// <summary>
        /// 当前页面
        /// </summary>
        public int PageNum
        {
            get { return pageNum; }
            set { pageNum = value; }
        }
        private int pageMAX;
        /// <summary>
        /// 总页数(只提供容器即可)
        /// </summary>
        public int PageMAX
        {
            get { return pageMAX; }
            set { pageMAX = value; }
        }

        public PageNavigation() { }
        /// <summary>
        /// 翻页参数处理
        /// </summary>
        /// <param name="_totalNumber">总数据条数</param>
        /// <param name="_pageCount">单页显示条数</param>
        /// <param name="_pageNum">当前页面</param>
        /// <param name="_pageMAX">总页数(只提供容器即可)</param>
        public PageNavigation(int _totalNumber, int _pageCount, int _pageNum, int _pageMAX)
        {
            totalNumber = _totalNumber;
            pageCount = _pageCount;
            pageNum = _pageNum;
            pageMAX = _pageMAX;
        }


        /// <summary>
        /// 翻页参数处理
        /// </summary>
        /// <param name="totalNumber">总数据条数</param>
        /// <param name="pageCount">单页显示条数</param>
        /// <param name="pageNum">当前页面</param>
        /// <param name="pageMAX">总页数(只提供容器即可)</param>
        public void GetPageParameter()
        {
            if (pageCount == 0) pageCount = TotalNumber;
            if (pageNum < 1) pageNum = 1;//修正当前页不为负数
            pageMAX = totalNumber / pageCount + 1;//计算最大页数
            if (totalNumber % pageCount == 0) pageMAX = totalNumber / pageCount;//修正最大页数
            if (pageNum > pageMAX) { pageNum = pageMAX; }//修正当前页不超过最大条数
            if (pageNum < 1) pageNum = 1;
        }


        /// <summary>
        /// 基础翻页导航生成
        /// </summary>
        /// <param name="pageMAX">总页数</param>
        /// <param name="pageNum">当前页</param>
        /// <param name="pageURL">超链接地址</param>
        /// <returns></returns>
        //public static string GetPage(int pageMAX, int pageNum, string pageURL)
        //{
        //    string sOut = string.Empty;
        //    int showPage = 9;//导航直接显示的页数
        //    if (pageNum <= 0) pageNum = 1;//当前页面
        //    int previousPage = (pageNum - 1) < 1 ? 1 : pageNum - 1;
        //    sOut = "<a href=\"" + pageURL + 1 + "\">首页</a> <a href=\"" + pageURL + previousPage + "\">上一页</a>";
        //    int pageNumMIN = pageNum - 4;
        //    if (pageNumMIN < 1) { pageNumMIN = 1; }
        //    int pageNumMAX = pageNumMIN + (showPage - 1);
        //    if (pageNumMAX > pageMAX) { pageNumMAX = pageMAX; pageNumMIN = pageMAX - (showPage - 1); }
        //    if (pageNumMIN < 1) { pageNumMIN = 1; }

        //    for (int i = pageNumMIN; i <= pageNumMAX; i++)
        //    {
        //        sOut += " <a href=\"" + pageURL + i + "\">" + i + "</a>";
        //    }
        //    int nextPage = (pageNum + 1 > pageMAX) ? pageMAX : pageNum + 1;
        //    sOut += " <a href=\"" + pageURL + nextPage + "\">下一页</a> <a href=\"" + pageURL + pageMAX + "\">末页</a>";
        //    return sOut;
        //}

        //public static string GetPage() { 

        //}



        public static string GetPage(int pageMAX, int pageNum, string pageURL)
        {
            return GetPage(pageMAX, pageNum, pageURL, "首页", "上一页", "disabled", "下一页", "末页", 5);
        }

        public static string GetPage(int pageMAX, int pageNum, string pageURL, string first, string previous, string numberClass, string next, string last, int showPage)
        {
            string sOut = string.Empty;
            if (pageNum <= 0) pageNum = 1;//当前页面
            int previousPage = (pageNum - 1) < 1 ? 1 : pageNum - 1;
            sOut = "<li><a href=\"" + pageURL + 1 + "\">" + first + "</a></li> <li><a href=\"" + pageURL + previousPage + "\">" + previous + "</a></li>";
            int pageNumMIN = pageNum - (showPage / 2);
            if (pageNumMIN < 1) { pageNumMIN = 1; }
            int pageNumMAX = pageNumMIN + (showPage - 1);
            if (pageNumMAX > pageMAX) { pageNumMAX = pageMAX; pageNumMIN = pageMAX - (showPage - 1); }
            if (pageNumMIN < 1) { pageNumMIN = 1; }

            for (int i = pageNumMIN; i <= pageNumMAX; i++)
            {
                if (i == pageNum)
                {
                    sOut += "<li class=\"" + numberClass + "\" ><a href=\"" + pageURL + i + "\">" + i + "</a></li>";
                }
                else
                {
                    sOut += "<li><a href=\"" + pageURL + i + "\">" + i + "</a></li>";
                }
            }
            int nextPage = (pageNum + 1 > pageMAX) ? pageMAX : pageNum + 1;
            sOut += " <li><a href=\"" + pageURL + nextPage + "\">" + next + "</a></li> <li><a href=\"" + pageURL + pageMAX + "\">" + last + "</a></li>";
            return sOut;
        }
    }
}
