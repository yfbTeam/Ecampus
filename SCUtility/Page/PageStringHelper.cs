using System;
using System.Web;

namespace SCUtility
{
    /// <summary>
    /// 分页类
    /// </summary>
    public class PageStringHelper
    {
        #region 分页模式
        public enum PagingStyle
        {
            Tp,
            Digg,
            Yahoo,
            Meneame,
            Flickr,
            Sabrosus,
            Pagination,
            Scott,
            Quotes,
            Black,
            Black2,
            BlackRed,
            Grayr,
            Yellow,
            Jogger,
            Starcraft2,
            Tres,
            Megas512,
            Technorati,
            Youtube,
            Msdn,
            Badoo,
            GreenBlack,
            Viciao,
            Yahoo2
        }
        #endregion

        public interface IWebPaging
        {
            string GetPageData(int nCurrentPage, int nTotalItems);
        }

        public class WebPaging : IWebPaging
        {
            private const int NForeDisplay = 6;
            private const int NTailDisplay = 4;
            public string LpRequestQueryString = "page";
            public string SzNext = "&gt;";
            public string SzPrevious = "&lt;";
            private string _lpLinkPage = string.Empty;
            private int _nPageSize = 2;
            private string cssClass = string.Empty;

            private string lpCSS = string.Empty;
            private PagingStyle ps = PagingStyle.Scott;

            public int PageSize
            {
                get { return _nPageSize; }
                set { _nPageSize = value; }
            }

            /// <summary>
            /// 分页页面Url
            /// </summary>
            public string LinkPage
            {
                get { return _lpLinkPage; }
                set { _lpLinkPage = value; }
            }

            #region CSS样式

            public WebPaging()
            {
                SetCSS();
            }

            /// <summary>
            /// 页面CSS
            /// </summary>
            public string PageCSS
            {
                get { return lpCSS; }
            }

            /// <summary>
            /// Body CSS
            /// </summary>
            public string BodyCSS
            {
                get
                {
                    return
                        "BODY {FONT-SIZE: 12px;FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif;WIDTH: 60%; PADDING-LEFT: 25px;}";
                }
            }

            /// <summary>
            /// 分页CSS样式
            /// </summary>
            public PagingStyle PageStyle
            {
                get { return ps; }
                set
                {
                    ps = value;
                    SetCSS();
                }
            }
            /// <summary>
            /// 设置css样式
            /// </summary>
            private void SetCSS()
            {
                switch (ps)
                {
                    case PagingStyle.Tp:
                        {
                            cssClass = "Tp";
                            lpCSS =
                                "DIV.Tp{padding-right: 3px;padding-left: 3px;padding-bottom: 3px;margin: 3px;padding-top: 3px;text-align: center;}DIV.Tp A{border-right: #ddd 1px solid;padding-right: 5px;border-top: #ddd 1px solid;padding-left: 5px;padding-bottom: 2px;border-left: #ddd 1px solid;color: #aaa;margin-right: 2px;padding-top: 2px;border-bottom: #ddd 1px solid;text-decoration: none;}DIV.Tp A:hover{background-position: #ff0099 url(image2.gif);border-right: #aad83e 1px solid;border-top: #aad83e 1px solid;background: #ff0099 url(image2.gif);border-left: #aad83e 1px solid;color: #fff;border-bottom: #aad83e 1px solid;border-color: #ff0099;}DIV.Tp A:active{background-position: url(image2.gif) #aad83e;border-right: #aad83e 1px solid;border-top: #aad83e 1px solid;background: #ff0099 url(image2.gif);border-left: #aad83e 1px solid;color: #fff;border-bottom: #aad83e 1px solid;border-color: #ff0099;}DIV.Tp SPAN.current{background-position: #ff0099 url(image2.gif);border-right: #aad83e 1px solid;padding-right: 5px;border-top: #aad83e 1px solid;padding-left: 5px;font-weight: bold;background: #ff0099 url(image2.gif);padding-bottom: 2px;border-left: #aad83e 1px solid;color: #fff;margin-right: 2px;padding-top: 2px;border-bottom: #aad83e 1px solid;border-color: #ff0099;}DIV.Tp SPAN.disabled{border-right: #f3f3f3 1px solid;padding-right: 5px;border-top: #f3f3f3 1px solid;padding-left: 5px;padding-bottom: 2px;border-left: #f3f3f3 1px solid;color: #ccc;margin-right: 2px;padding-top: 2px;border-bottom: #f3f3f3 1px solid;}";
                            break;
                        }
                    case PagingStyle.Digg:
                        {
                            cssClass = "Digg";
                            lpCSS =
                                "DIV.Digg {PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; MARGIN: 3px; PADDING-TOP: 3px; TEXT-ALIGN: center}DIV.Digg A {BORDER-RIGHT: #aaaadd 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #aaaadd 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; MARGIN: 2px; BORDER-LEFT: #aaaadd 1px solid; COLOR: #000099; PADDING-TOP: 2px; BORDER-BOTTOM: #aaaadd 1px solid; TEXT-DECORATION: none}DIV.Digg A:hover {BORDER-RIGHT: #000099 1px solid; BORDER-TOP: #000099 1px solid; BORDER-LEFT: #000099 1px solid; COLOR: #000; BORDER-BOTTOM: #000099 1px solid}DIV.Digg A:active {BORDER-RIGHT: #000099 1px solid; BORDER-TOP: #000099 1px solid; BORDER-LEFT: #000099 1px solid; COLOR: #000; BORDER-BOTTOM: #000099 1px solid}DIV.Digg SPAN.current {BORDER-RIGHT: #000099 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #000099 1px solid; PADDING-LEFT: 5px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; MARGIN: 2px; BORDER-LEFT: #000099 1px solid; COLOR: #fff; PADDING-TOP: 2px; BORDER-BOTTOM: #000099 1px solid; BACKGROUND-COLOR: #000099}DIV.Digg SPAN.disabled {BORDER-RIGHT: #eee 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #eee 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; MARGIN: 2px; BORDER-LEFT: #eee 1px solid; COLOR: #ddd; PADDING-TOP: 2px; BORDER-BOTTOM: #eee 1px solid}";
                            break;
                        }
                    case PagingStyle.Yahoo:
                        {
                            cssClass = "Yahoo";
                            lpCSS =
                                "DIV.Yahoo {PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; MARGIN: 3px; PADDING-TOP: 3px; TEXT-ALIGN: center}DIV.Yahoo A {BORDER-RIGHT: #fff 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #fff 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; MARGIN: 2px; BORDER-LEFT: #fff 1px solid; COLOR: #000099; PADDING-TOP: 2px; BORDER-BOTTOM: #fff 1px solid; TEXT-DECORATION: underline}DIV.Yahoo A:hover {BORDER-RIGHT: #000099 1px solid; BORDER-TOP: #000099 1px solid; BORDER-LEFT: #000099 1px solid; COLOR: #000; BORDER-BOTTOM: #000099 1px solid}DIV.Yahoo A:active {BORDER-RIGHT: #000099 1px solid; BORDER-TOP: #000099 1px solid; BORDER-LEFT: #000099 1px solid; COLOR: #f00; BORDER-BOTTOM: #000099 1px solid}DIV.Yahoo SPAN.current {BORDER-RIGHT: #fff 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #fff 1px solid; PADDING-LEFT: 5px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; MARGIN: 2px; BORDER-LEFT: #fff 1px solid; COLOR: #000; PADDING-TOP: 2px; BORDER-BOTTOM: #fff 1px solid; BACKGROUND-COLOR: #fff}DIV.Yahoo SPAN.disabled {BORDER-RIGHT: #eee 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #eee 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; MARGIN: 2px; BORDER-LEFT: #eee 1px solid; COLOR: #ddd; PADDING-TOP: 2px; BORDER-BOTTOM: #eee 1px solid}";
                            break;
                        }
                    case PagingStyle.Meneame:
                        {
                            cssClass = "Meneame";
                            lpCSS =
                                "DIV.Meneame {PADDING-RIGHT: 3px; PADDING-LEFT: 3px; FONT-SIZE: 80%; PADDING-BOTTOM: 3px; MARGIN: 3px; COLOR: #ff6500; PADDING-TOP: 3px; TEXT-ALIGN: center}DIV.Meneame A {BORDER-RIGHT: #ff9600 1px solid; PADDING-RIGHT: 7px; BACKGROUND-POSITION: 50% bottom; BORDER-TOP: #ff9600 1px solid; PADDING-LEFT: 7px; BACKGROUND-IMAGE: url(Meneame.jpg); PADDING-BOTTOM: 5px; BORDER-LEFT: #ff9600 1px solid; COLOR: #ff6500; MARGIN-RIGHT: 3px; PADDING-TOP: 5px; BORDER-BOTTOM: #ff9600 1px solid; TEXT-DECORATION: none}DIV.Meneame A:hover {BORDER-RIGHT: #ff9600 1px solid; BORDER-TOP: #ff9600 1px solid; BACKGROUND-IMAGE: none; BORDER-LEFT: #ff9600 1px solid; COLOR: #ff6500; BORDER-BOTTOM: #ff9600 1px solid; BACKGROUND-COLOR: #ffc794}DIV.Meneame A:active {BORDER-RIGHT: #ff9600 1px solid; BORDER-TOP: #ff9600 1px solid; BACKGROUND-IMAGE: none; BORDER-LEFT: #ff9600 1px solid; COLOR: #ff6500; BORDER-BOTTOM: #ff9600 1px solid; BACKGROUND-COLOR: #ffc794}DIV.Meneame SPAN.current {BORDER-RIGHT: #ff6500 1px solid; PADDING-RIGHT: 7px; BORDER-TOP: #ff6500 1px solid; PADDING-LEFT: 7px; FONT-WEIGHT: bold; PADDING-BOTTOM: 5px; BORDER-LEFT: #ff6500 1px solid; COLOR: #ff6500; MARGIN-RIGHT: 3px; PADDING-TOP: 5px; BORDER-BOTTOM: #ff6500 1px solid; BACKGROUND-COLOR: #ffbe94}DIV.Meneame SPAN.disabled {BORDER-RIGHT: #ffe3c6 1px solid; PADDING-RIGHT: 7px; BORDER-TOP: #ffe3c6 1px solid; PADDING-LEFT: 7px; PADDING-BOTTOM: 5px; BORDER-LEFT: #ffe3c6 1px solid; COLOR: #ffe3c6; MARGIN-RIGHT: 3px; PADDING-TOP: 5px; BORDER-BOTTOM: #ffe3c6 1px solid}";
                            break;
                        }
                    case PagingStyle.Flickr:
                        {
                            cssClass = "Flickr";
                            lpCSS =
                                "DIV.Flickr {PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; MARGIN: 3px; PADDING-TOP: 3px; TEXT-ALIGN: center}DIV.Flickr A {BORDER-RIGHT: #dedfde 1px solid; PADDING-RIGHT: 6px; BACKGROUND-POSITION: 50% bottom; BORDER-TOP: #dedfde 1px solid; PADDING-LEFT: 6px; PADDING-BOTTOM: 2px; BORDER-LEFT: #dedfde 1px solid; COLOR: #0061de; MARGIN-RIGHT: 3px; PADDING-TOP: 2px; BORDER-BOTTOM: #dedfde 1px solid; TEXT-DECORATION: none}DIV.Flickr A:hover {BORDER-RIGHT: #000 1px solid; BORDER-TOP: #000 1px solid; BACKGROUND-IMAGE: none; BORDER-LEFT: #000 1px solid; COLOR: #fff; BORDER-BOTTOM: #000 1px solid; BACKGROUND-COLOR: #0061de}DIV.Meneame A:active {BORDER-RIGHT: #000 1px solid; BORDER-TOP: #000 1px solid; BACKGROUND-IMAGE: none; BORDER-LEFT: #000 1px solid; COLOR: #fff; BORDER-BOTTOM: #000 1px solid; BACKGROUND-COLOR: #0061de}DIV.Flickr SPAN.current {PADDING-RIGHT: 6px; PADDING-LEFT: 6px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; COLOR: #ff0084; MARGIN-RIGHT: 3px; PADDING-TOP: 2px}DIV.Flickr SPAN.disabled {PADDING-RIGHT: 6px; PADDING-LEFT: 6px; PADDING-BOTTOM: 2px; COLOR: #adaaad; MARGIN-RIGHT: 3px; PADDING-TOP: 2px}";
                            break;
                        }
                    case PagingStyle.Sabrosus:
                        {
                            cssClass = "Sabrosus";
                            lpCSS =
                                "DIV.Sabrosus {PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; MARGIN: 3px; PADDING-TOP: 3px; TEXT-ALIGN: center}DIV.Sabrosus A {BORDER-RIGHT: #9aafe5 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #9aafe5 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; BORDER-LEFT: #9aafe5 1px solid; COLOR: #2e6ab1; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: #9aafe5 1px solid; TEXT-DECORATION: none}DIV.Sabrosus A:hover {BORDER-RIGHT: #2b66a5 1px solid; BORDER-TOP: #2b66a5 1px solid; BORDER-LEFT: #2b66a5 1px solid; COLOR: #000; BORDER-BOTTOM: #2b66a5 1px solid; BACKGROUND-COLOR: lightyellow}";
                            break;
                        }
                    case PagingStyle.Pagination:
                        {
                            cssClass = "Pagination";
                            lpCSS =
                                "DIV.Pagination A:active {BORDER-RIGHT: #2b66a5 1px solid; BORDER-TOP: #2b66a5 1px solid; BORDER-LEFT: #2b66a5 1px solid; COLOR: #000; BORDER-BOTTOM: #2b66a5 1px solid; BACKGROUND-COLOR: lightyellow}DIV.Sabrosus SPAN.current {BORDER-RIGHT: navy 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: navy 1px solid; PADDING-LEFT: 5px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; BORDER-LEFT: navy 1px solid; COLOR: #fff; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: navy 1px solid; BACKGROUND-COLOR: #2e6ab1}DIV.Sabrosus SPAN.disabled {BORDER-RIGHT: #929292 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #929292 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; BORDER-LEFT: #929292 1px solid; COLOR: #929292; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: #929292 1px solid}";
                            break;
                        }
                    case PagingStyle.Scott:
                        {
                            cssClass = "Scott";
                            lpCSS =
                                "DIV.Scott {PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; MARGIN: 3px; PADDING-TOP: 3px; TEXT-ALIGN: center}DIV.Scott A {BORDER-RIGHT: #ddd 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #ddd 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; BORDER-LEFT: #ddd 1px solid; COLOR: #88af3f; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: #ddd 1px solid; TEXT-DECORATION: none}DIV.Scott A:hover {BORDER-RIGHT: #85bd1e 1px solid; BORDER-TOP: #85bd1e 1px solid; BORDER-LEFT: #85bd1e 1px solid; COLOR: #638425; BORDER-BOTTOM: #85bd1e 1px solid; BACKGROUND-COLOR: #f1ffd6}DIV.Scott A:active {BORDER-RIGHT: #85bd1e 1px solid; BORDER-TOP: #85bd1e 1px solid; BORDER-LEFT: #85bd1e 1px solid; COLOR: #638425; BORDER-BOTTOM: #85bd1e 1px solid; BACKGROUND-COLOR: #f1ffd6}DIV.Scott SPAN.current {BORDER-RIGHT: #b2e05d 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #b2e05d 1px solid; PADDING-LEFT: 5px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; BORDER-LEFT: #b2e05d 1px solid; COLOR: #fff; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: #b2e05d 1px solid; BACKGROUND-COLOR: #b2e05d}DIV.Scott SPAN.disabled {BORDER-RIGHT: #f3f3f3 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #f3f3f3 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; BORDER-LEFT: #f3f3f3 1px solid; COLOR: #ccc; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: #f3f3f3 1px solid}";
                            break;
                        }
                    case PagingStyle.Quotes:
                        {
                            cssClass = "Quotes";
                            lpCSS =
                                "DIV.Quotes {PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; MARGIN: 3px; PADDING-TOP: 3px; TEXT-ALIGN: center}DIV.Quotes A {BORDER-RIGHT: #ddd 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #ddd 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; BORDER-LEFT: #ddd 1px solid; COLOR: #aaa; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: #ddd 1px solid; TEXT-DECORATION: none}DIV.Quotes A:hover {BORDER-RIGHT: #a0a0a0 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #a0a0a0 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; BORDER-LEFT: #a0a0a0 1px solid; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: #a0a0a0 1px solid}DIV.Quotes A:active {BORDER-RIGHT: #a0a0a0 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #a0a0a0 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; BORDER-LEFT: #a0a0a0 1px solid; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: #a0a0a0 1px solid}DIV.Quotes SPAN.current {BORDER-RIGHT: #e0e0e0 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #e0e0e0 1px solid; PADDING-LEFT: 5px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; BORDER-LEFT: #e0e0e0 1px solid; COLOR: #aaa; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: #e0e0e0 1px solid; BACKGROUND-COLOR: #f0f0f0}DIV.Quotes SPAN.disabled {BORDER-RIGHT: #f3f3f3 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #f3f3f3 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; BORDER-LEFT: #f3f3f3 1px solid; COLOR: #ccc; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: #f3f3f3 1px solid}";
                            break;
                        }
                    case PagingStyle.Black:
                        {
                            cssClass = "Black";
                            lpCSS =
                                "DIV.Black {PADDING-RIGHT: 3px; PADDING-LEFT: 3px; FONT-SIZE: 80%; PADDING-BOTTOM: 10px; MARGIN: 3px; COLOR: #a0a0a0; PADDING-TOP: 10px; BACKGROUND-COLOR: #000; TEXT-ALIGN: center}DIV.Black A {BORDER-RIGHT: #909090 1px solid; PADDING-RIGHT: 5px; BACKGROUND-POSITION: 50% bottom; BORDER-TOP: #909090 1px solid; PADDING-LEFT: 5px; BACKGROUND-IMAGE: url(bar.gif); PADDING-BOTTOM: 2px; BORDER-LEFT: #909090 1px solid; COLOR: #c0c0c0; MARGIN-RIGHT: 3px; PADDING-TOP: 2px; BORDER-BOTTOM: #909090 1px solid; TEXT-DECORATION: none}DIV.Black A:hover {BORDER-RIGHT: #f0f0f0 1px solid; BORDER-TOP: #f0f0f0 1px solid; BACKGROUND-IMAGE: url(invbar.gif); BORDER-LEFT: #f0f0f0 1px solid; COLOR: #ffffff; BORDER-BOTTOM: #f0f0f0 1px solid; BACKGROUND-COLOR: #404040}DIV.Black A:active {BORDER-RIGHT: #f0f0f0 1px solid; BORDER-TOP: #f0f0f0 1px solid; BACKGROUND-IMAGE: url(invbar.gif); BORDER-LEFT: #f0f0f0 1px solid; COLOR: #ffffff; BORDER-BOTTOM: #f0f0f0 1px solid; BACKGROUND-COLOR: #404040}DIV.Black SPAN.current {BORDER-RIGHT: #ffffff 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #ffffff 1px solid; PADDING-LEFT: 5px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; BORDER-LEFT: #ffffff 1px solid; COLOR: #ffffff; MARGIN-RIGHT: 3px; PADDING-TOP: 2px; BORDER-BOTTOM: #ffffff 1px solid; BACKGROUND-COLOR: #606060}DIV.Black SPAN.disabled {BORDER-RIGHT: #606060 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #606060 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; BORDER-LEFT: #606060 1px solid; COLOR: #808080; MARGIN-RIGHT: 3px; PADDING-TOP: 2px; BORDER-BOTTOM: #606060 1px solid}";
                            break;
                        }
                    case PagingStyle.Black2:
                        {
                            cssClass = "Black2";
                            lpCSS =
                                "DIV.Black2 {PADDING-RIGHT: 7px; PADDING-LEFT: 7px; PADDING-BOTTOM: 7px; MARGIN: 3px; PADDING-TOP: 7px; TEXT-ALIGN: center}DIV.Black2 A {BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #000000 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; MARGIN: 2px; BORDER-LEFT: #000000 1px solid; COLOR: #000000; PADDING-TOP: 2px; BORDER-BOTTOM: #000000 1px solid; TEXT-DECORATION: none}DIV.Black2 A:hover {BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; COLOR: #fff; BORDER-BOTTOM: #000000 1px solid; BACKGROUND-COLOR: #000}DIV.Black2 A:active {BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; COLOR: #fff; BORDER-BOTTOM: #000000 1px solid; BACKGROUND-COLOR: #000}DIV.Black2 SPAN.current {BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #000000 1px solid; PADDING-LEFT: 5px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; MARGIN: 2px; BORDER-LEFT: #000000 1px solid; COLOR: #fff; PADDING-TOP: 2px; BORDER-BOTTOM: #000000 1px solid; BACKGROUND-COLOR: #000000}DIV.Black2 SPAN.disabled {BORDER-RIGHT: #eee 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #eee 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; MARGIN: 2px; BORDER-LEFT: #eee 1px solid; COLOR: #ddd; PADDING-TOP: 2px; BORDER-BOTTOM: #eee 1px solid}";
                            break;
                        }
                    case PagingStyle.BlackRed:
                        {
                            cssClass = "Black-red";
                            lpCSS =
                                "DIV.Black-red {FONT-SIZE: 11px; COLOR: #fff; FONT-FAMILY: Tahoma, Arial, Helvetica, Sans-serif; BACKGROUND-COLOR: #3e3e3e}DIV.Black-red A {PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; MARGIN: 2px; COLOR: #fff; PADDING-TOP: 2px; BACKGROUND-COLOR: #3e3e3e; TEXT-DECORATION: none}DIV.Black-red A:hover {COLOR: #fff; BACKGROUND-COLOR: #ec5210}DIV.Black-red A:active {COLOR: #fff; BACKGROUND-COLOR: #ec5210}DIV.Black-red SPAN.current {PADDING-RIGHT: 5px; PADDING-LEFT: 5px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; MARGIN: 2px; COLOR: #fff; PADDING-TOP: 2px; BACKGROUND-COLOR: #313131}DIV.Black-red SPAN.disabled {PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; MARGIN: 2px; COLOR: #868686; PADDING-TOP: 2px; BACKGROUND-COLOR: #3e3e3e}";
                            break;
                        }
                    case PagingStyle.Grayr:
                        {
                            cssClass = "Grayr";
                            lpCSS =
                                "DIV.Grayr {PADDING-RIGHT: 2px; PADDING-LEFT: 2px; FONT-SIZE: 11px; PADDING-BOTTOM: 2px; PADDING-TOP: 2px; FONT-FAMILY: Tahoma, Arial, Helvetica, Sans-serif; BACKGROUND-COLOR: #c1c1c1}DIV.Grayr A {PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; MARGIN: 2px; COLOR: #000; PADDING-TOP: 2px; BACKGROUND-COLOR: #c1c1c1; TEXT-DECORATION: none}DIV.Grayr A:hover {COLOR: #000; BACKGROUND-COLOR: #99ffff}DIV.Grayr A:active {COLOR: #000; BACKGROUND-COLOR: #99ffff}DIV.Grayr SPAN.current {PADDING-RIGHT: 5px; PADDING-LEFT: 5px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; MARGIN: 2px; COLOR: #303030; PADDING-TOP: 2px; BACKGROUND-COLOR: #fff}DIV.Grayr SPAN.disabled {PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; MARGIN: 2px; COLOR: #797979; PADDING-TOP: 2px; BACKGROUND-COLOR: #c1c1c1}";
                            break;
                        }
                    case PagingStyle.Yellow:
                        {
                            cssClass = "Yellow";
                            lpCSS =
                                "DIV.Yellow {PADDING-RIGHT: 7px; PADDING-LEFT: 7px; PADDING-BOTTOM: 7px; MARGIN: 3px; PADDING-TOP: 7px; TEXT-ALIGN: center}DIV.Yellow A {BORDER-RIGHT: #ccc 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #ccc 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; MARGIN: 2px; BORDER-LEFT: #ccc 1px solid; COLOR: #000; PADDING-TOP: 2px; BORDER-BOTTOM: #ccc 1px solid; TEXT-DECORATION: none}DIV.Yellow A:hover {BORDER-RIGHT: #f0f0f0 1px solid; BORDER-TOP: #f0f0f0 1px solid; BORDER-LEFT: #f0f0f0 1px solid; COLOR: #000; BORDER-BOTTOM: #f0f0f0 1px solid}DIV.Yellow A:active {BORDER-RIGHT: #f0f0f0 1px solid; BORDER-TOP: #f0f0f0 1px solid; BORDER-LEFT: #f0f0f0 1px solid; COLOR: #000; BORDER-BOTTOM: #f0f0f0 1px solid}DIV.Yellow SPAN.current {BORDER-RIGHT: #d9d300 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #d9d300 1px solid; PADDING-LEFT: 5px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; MARGIN: 2px; BORDER-LEFT: #d9d300 1px solid; COLOR: #fff; PADDING-TOP: 2px; BORDER-BOTTOM: #d9d300 1px solid; BACKGROUND-COLOR: #d9d300}DIV.Yellow SPAN.disabled {BORDER-RIGHT: #eee 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #eee 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; MARGIN: 2px; BORDER-LEFT: #eee 1px solid; COLOR: #ddd; PADDING-TOP: 2px; BORDER-BOTTOM: #eee 1px solid}";
                            break;
                        }
                    case PagingStyle.Jogger:
                        {
                            cssClass = "Jogger";
                            lpCSS =
                                "DIV.Jogger {PADDING-RIGHT: 2px; PADDING-LEFT: 2px; PADDING-BOTTOM: 2px; MARGIN: 7px; PADDING-TOP: 2px; FONT-FAMILY: \"Lucida Sans Unicode\", \"Lucida Grande\", LucidaGrande, \"Lucida Sans\", Geneva, Verdana, sans-serif}DIV.Jogger A {PADDING-RIGHT: 0.64em; PADDING-LEFT: 0.64em; PADDING-BOTTOM: 0.43em; MARGIN: 2px; COLOR: #fff; PADDING-TOP: 0.5em; BACKGROUND-COLOR: #ee4e4e; TEXT-DECORATION: none}DIV.Jogger A:hover {PADDING-RIGHT: 0.64em; PADDING-LEFT: 0.64em; PADDING-BOTTOM: 0.43em; MARGIN: 2px; COLOR: #fff; PADDING-TOP: 0.5em; BACKGROUND-COLOR: #de1818}DIV.Jogger A:active {PADDING-RIGHT: 0.64em; PADDING-LEFT: 0.64em; PADDING-BOTTOM: 0.43em; MARGIN: 2px; COLOR: #fff; PADDING-TOP: 0.5em; BACKGROUND-COLOR: #de1818}DIV.Jogger SPAN.current {PADDING-RIGHT: 0.64em; PADDING-LEFT: 0.64em; PADDING-BOTTOM: 0.43em; MARGIN: 2px; COLOR: #6d643c; PADDING-TOP: 0.5em; BACKGROUND-COLOR: #f6efcc}DIV.Jogger SPAN.disabled {DISPLAY: none}";
                            break;
                        }
                    case PagingStyle.Starcraft2:
                        {
                            cssClass = "Starcraft2";
                            lpCSS =
                                "DIV.Starcraft2 {PADDING-RIGHT: 3px; PADDING-LEFT: 3px; FONT-WEIGHT: bold; FONT-SIZE: 13.5pt; PADDING-BOTTOM: 3px; MARGIN: 3px; COLOR: #fff; PADDING-TOP: 3px; FONT-FAMILY: Arial; BACKGROUND-COLOR: #000; TEXT-ALIGN: center}DIV.Starcraft2 A {MARGIN: 2px; COLOR: #fa0; BACKGROUND-COLOR: #000; TEXT-DECORATION: none}DIV.Starcraft2 A:hover {COLOR: #fff; BACKGROUND-COLOR: #000}DIV.Starcraft2 A:active {COLOR: #fff; BACKGROUND-COLOR: #000}DIV.Starcraft2 SPAN.current {FONT-WEIGHT: bold; MARGIN: 2px; COLOR: #fff; BACKGROUND-COLOR: #000}DIV.Starcraft2 SPAN.disabled {MARGIN: 2px; COLOR: #444; BACKGROUND-COLOR: #000}";
                            break;
                        }
                    case PagingStyle.Tres:
                        {
                            cssClass = "Tres";
                            lpCSS =
                                "DIV.Tres {PADDING-RIGHT: 7px; PADDING-LEFT: 7px; FONT-WEIGHT: bold; FONT-SIZE: 13.2pt; PADDING-BOTTOM: 7px; MARGIN: 3px; PADDING-TOP: 7px; FONT-FAMILY: Arial, Helvetica, sans-serif; TEXT-ALIGN: center}DIV.Tres A {BORDER-RIGHT: #d9d300 2px solid; PADDING-RIGHT: 5px; BORDER-TOP: #d9d300 2px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; MARGIN: 2px; BORDER-LEFT: #d9d300 2px solid; COLOR: #fff; PADDING-TOP: 2px; BORDER-BOTTOM: #d9d300 2px solid; BACKGROUND-COLOR: #d90; TEXT-DECORATION: none}DIV.Tres A:hover {BORDER-RIGHT: #ff0 2px solid; BORDER-TOP: #ff0 2px solid; BORDER-LEFT: #ff0 2px solid; COLOR: #000; BORDER-BOTTOM: #ff0 2px solid; BACKGROUND-COLOR: #ff0}DIV.Tres A:active {BORDER-RIGHT: #ff0 2px solid; BORDER-TOP: #ff0 2px solid; BORDER-LEFT: #ff0 2px solid; COLOR: #000; BORDER-BOTTOM: #ff0 2px solid; BACKGROUND-COLOR: #ff0}DIV.Tres SPAN.current {BORDER-RIGHT: #fff 2px solid; PADDING-RIGHT: 5px; BORDER-TOP: #fff 2px solid; PADDING-LEFT: 5px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; MARGIN: 2px; BORDER-LEFT: #fff 2px solid; COLOR: #000; PADDING-TOP: 2px; BORDER-BOTTOM: #fff 2px solid}DIV.Tres SPAN.disabled {DISPLAY: none}";
                            break;
                        }
                    case PagingStyle.Megas512:
                        {
                            cssClass = "Megas512";
                            lpCSS =
                                "DIV.Megas512 {PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; MARGIN: 3px; PADDING-TOP: 3px; TEXT-ALIGN: center}DIV.Megas512 A {BORDER-RIGHT: #dedfde 1px solid; PADDING-RIGHT: 6px; BACKGROUND-POSITION: 50% bottom; BORDER-TOP: #dedfde 1px solid; PADDING-LEFT: 6px; PADDING-BOTTOM: 2px; BORDER-LEFT: #dedfde 1px solid; COLOR: #99210b; MARGIN-RIGHT: 3px; PADDING-TOP: 2px; BORDER-BOTTOM: #dedfde 1px solid; TEXT-DECORATION: none}DIV.Megas512 A:hover {BORDER-RIGHT: #000 1px solid; BORDER-TOP: #000 1px solid; BACKGROUND-IMAGE: none; BORDER-LEFT: #000 1px solid; COLOR: #fff; BORDER-BOTTOM: #000 1px solid; BACKGROUND-COLOR: #777777}DIV.Megas512 A:active {BORDER-RIGHT: #000 1px solid; BORDER-TOP: #000 1px solid; BACKGROUND-IMAGE: none; BORDER-LEFT: #000 1px solid; COLOR: #fff; BORDER-BOTTOM: #000 1px solid; BACKGROUND-COLOR: #777777}DIV.Megas512 SPAN.current {PADDING-RIGHT: 6px; PADDING-LEFT: 6px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; COLOR: #99210b; MARGIN-RIGHT: 3px; PADDING-TOP: 2px}DIV.Megas512 SPAN.disabled {PADDING-RIGHT: 6px; PADDING-LEFT: 6px; PADDING-BOTTOM: 2px; COLOR: #adaaad; MARGIN-RIGHT: 3px; PADDING-TOP: 2px}";
                            break;
                        }
                    case PagingStyle.Technorati:
                        {
                            cssClass = "Technorati";
                            lpCSS =
                                "DIV.Technorati {PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; MARGIN: 3px; PADDING-TOP: 3px; TEXT-ALIGN: center}DIV.Technorati A {BORDER-RIGHT: #ccc 1px solid; PADDING-RIGHT: 6px; BACKGROUND-POSITION: 50% bottom; BORDER-TOP: #ccc 1px solid; PADDING-LEFT: 6px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; BORDER-LEFT: #ccc 1px solid; COLOR: rgb(66,97,222); MARGIN-RIGHT: 3px; PADDING-TOP: 2px; BORDER-BOTTOM: #ccc 1px solid; TEXT-DECORATION: none}DIV.Technorati A:hover {BACKGROUND-IMAGE: none; COLOR: #fff; BACKGROUND-COLOR: #4261df}DIV.Technorati A:active {BACKGROUND-IMAGE: none; COLOR: #fff; BACKGROUND-COLOR: #4261df}DIV.Technorati SPAN.current {PADDING-RIGHT: 6px; PADDING-LEFT: 6px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; COLOR: #000; MARGIN-RIGHT: 3px; PADDING-TOP: 2px}DIV.Technorati SPAN.disabled {DISPLAY: none}";
                            break;
                        }
                    case PagingStyle.Youtube:
                        {
                            cssClass = "Youtube";
                            lpCSS =
                                "DIV.Youtube {PADDING-RIGHT: 6px; BORDER-TOP: #9c9a9c 1px dotted; PADDING-LEFT: 0px; FONT-SIZE: 13px; PADDING-BOTTOM: 4px; COLOR: #313031; PADDING-TOP: 4px; FONT-FAMILY: Arial, Helvetica, sans-serif; BACKGROUND-COLOR: #cecfce; TEXT-ALIGN: right}DIV.Youtube A {PADDING-RIGHT: 3px; PADDING-LEFT: 3px; FONT-WEIGHT: bold; PADDING-BOTTOM: 1px; MARGIN: 0px 1px; COLOR: #0030ce; PADDING-TOP: 1px; TEXT-DECORATION: underline}DIV.Youtube A:hover {}DIV.Youtube A:active {}DIV.Youtube SPAN.current {PADDING-RIGHT: 2px; PADDING-LEFT: 2px; PADDING-BOTTOM: 1px; COLOR: #000; PADDING-TOP: 1px; BACKGROUND-COLOR: #fff}DIV.Youtube SPAN.disabled {DISPLAY: none}";
                            break;
                        }
                    case PagingStyle.Msdn:
                        {
                            cssClass = "Msdn";
                            lpCSS =
                                "DIV.Msdn {PADDING-RIGHT: 6px; PADDING-LEFT: 0px; FONT-SIZE: 13px; PADDING-BOTTOM: 4px; COLOR: #313031; PADDING-TOP: 4px; FONT-FAMILY: Verdana,Tahoma,Arial,Helvetica,Sans-Serif; BACKGROUND-COLOR: #fff; TEXT-ALIGN: right}DIV.Msdn A {BORDER-RIGHT: #b7d8ee 1px solid; PADDING-RIGHT: 6px; BORDER-TOP: #b7d8ee 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 4px; MARGIN: 0px 3px; BORDER-LEFT: #b7d8ee 1px solid; COLOR: #0030ce; PADDING-TOP: 5px; BORDER-BOTTOM: #b7d8ee 1px solid; TEXT-DECORATION: none}DIV.Msdn A:hover {BORDER-RIGHT: #b7d8ee 1px solid; BORDER-TOP: #b7d8ee 1px solid; BORDER-LEFT: #b7d8ee 1px solid; COLOR: #0066a7; BORDER-BOTTOM: #b7d8ee 1px solid; BACKGROUND-COLOR: #d2eaf6}DIV.Pagination A:active {BORDER-RIGHT: #b7d8ee 1px solid; BORDER-TOP: #b7d8ee 1px solid; BORDER-LEFT: #b7d8ee 1px solid; COLOR: #0066a7; BORDER-BOTTOM: #b7d8ee 1px solid; BACKGROUND-COLOR: #d2eaf6}DIV.Msdn SPAN.current {BORDER-RIGHT: #b7d8ee 1px solid; PADDING-RIGHT: 6px; BORDER-TOP: #b7d8ee 1px solid; PADDING-LEFT: 5px; FONT-WEIGHT: bold; PADDING-BOTTOM: 4px; MARGIN: 0px 3px; BORDER-LEFT: #b7d8ee 1px solid; COLOR: #444444; PADDING-TOP: 5px; BORDER-BOTTOM: #b7d8ee 1px solid; BACKGROUND-COLOR: #d2eaf6}DIV.Msdn SPAN.disabled {DISPLAY: none}";
                            break;
                        }
                    case PagingStyle.Badoo:
                        {
                            cssClass = "Badoo";
                            lpCSS =
                                "DIV.Badoo {PADDING-RIGHT: 0px; PADDING-LEFT: 0px; FONT-SIZE: 13px; PADDING-BOTTOM: 10px; COLOR: #48b9ef; PADDING-TOP: 10px; FONT-FAMILY: Arial, Helvetica, sans-serif; BACKGROUND-COLOR: #fff; TEXT-ALIGN: center}DIV.Badoo A {BORDER-RIGHT: #f0f0f0 2px solid; PADDING-RIGHT: 5px; BORDER-TOP: #f0f0f0 2px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; MARGIN: 0px 2px; BORDER-LEFT: #f0f0f0 2px solid; COLOR: #48b9ef; PADDING-TOP: 2px; BORDER-BOTTOM: #f0f0f0 2px solid; TEXT-DECORATION: none}DIV.Badoo A:hover {BORDER-RIGHT: #ff5a00 2px solid; BORDER-TOP: #ff5a00 2px solid; BORDER-LEFT: #ff5a00 2px solid; COLOR: #ff5a00; BORDER-BOTTOM: #ff5a00 2px solid}DIV.Badoo A:active {BORDER-RIGHT: #ff5a00 2px solid; BORDER-TOP: #ff5a00 2px solid; BORDER-LEFT: #ff5a00 2px solid; COLOR: #ff5a00; BORDER-BOTTOM: #ff5a00 2px solid}DIV.Badoo SPAN.current {BORDER-RIGHT: #ff5a00 2px solid; PADDING-RIGHT: 5px; BORDER-TOP: #ff5a00 2px solid; PADDING-LEFT: 5px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; BORDER-LEFT: #ff5a00 2px solid; COLOR: #fff; PADDING-TOP: 2px; BORDER-BOTTOM: #ff5a00 2px solid; BACKGROUND-COLOR: #ff6c16}DIV.Badoo SPAN.disabled {DISPLAY: none}.manu {PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; MARGIN: 3px; PADDING-TOP: 3px; TEXT-ALIGN: center}.manu A {BORDER-RIGHT: #eee 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #eee 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; MARGIN: 2px; BORDER-LEFT: #eee 1px solid; COLOR: #036cb4; PADDING-TOP: 2px; BORDER-BOTTOM: #eee 1px solid; TEXT-DECORATION: none}.manu A:hover {BORDER-RIGHT: #999 1px solid; BORDER-TOP: #999 1px solid; BORDER-LEFT: #999 1px solid; COLOR: #666; BORDER-BOTTOM: #999 1px solid}.manu A:active {BORDER-RIGHT: #999 1px solid; BORDER-TOP: #999 1px solid; BORDER-LEFT: #999 1px solid; COLOR: #666; BORDER-BOTTOM: #999 1px solid}.manu .current {BORDER-RIGHT: #036cb4 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #036cb4 1px solid; PADDING-LEFT: 5px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; MARGIN: 2px; BORDER-LEFT: #036cb4 1px solid; COLOR: #fff; PADDING-TOP: 2px; BORDER-BOTTOM: #036cb4 1px solid; BACKGROUND-COLOR: #036cb4}.manu .disabled {BORDER-RIGHT: #eee 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #eee 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; MARGIN: 2px; BORDER-LEFT: #eee 1px solid; COLOR: #ddd; PADDING-TOP: 2px; BORDER-BOTTOM: #eee 1px solid}";
                            break;
                        }
                    case PagingStyle.GreenBlack:
                        {
                            cssClass = "green-Black";
                            lpCSS =
                                "DIV.green-Black {PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; MARGIN: 3px; PADDING-TOP: 3px; TEXT-ALIGN: center}DIV.green-Black A {BORDER-RIGHT: #2c2c2c 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #2c2c2c 1px solid; PADDING-LEFT: 5px; BACKGROUND: url(image1.gif) #2c2c2c; PADDING-BOTTOM: 2px; BORDER-LEFT: #2c2c2c 1px solid; COLOR: #fff; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: #2c2c2c 1px solid; TEXT-DECORATION: none}DIV.green-Black A:hover {BORDER-RIGHT: #aad83e 1px solid; BORDER-TOP: #aad83e 1px solid; BACKGROUND: url(image2.gif) #aad83e; BORDER-LEFT: #aad83e 1px solid; COLOR: #fff; BORDER-BOTTOM: #aad83e 1px solid}DIV.green-Black A:active {BORDER-RIGHT: #aad83e 1px solid; BORDER-TOP: #aad83e 1px solid; BACKGROUND: url(image2.gif) #aad83e; BORDER-LEFT: #aad83e 1px solid; COLOR: #fff; BORDER-BOTTOM: #aad83e 1px solid}DIV.green-Black SPAN.current {BORDER-RIGHT: #aad83e 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #aad83e 1px solid; PADDING-LEFT: 5px; FONT-WEIGHT: bold; BACKGROUND: url(image2.gif) #aad83e; PADDING-BOTTOM: 2px; BORDER-LEFT: #aad83e 1px solid; COLOR: #fff; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: #aad83e 1px solid}DIV.green-Black SPAN.disabled {BORDER-RIGHT: #f3f3f3 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #f3f3f3 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; BORDER-LEFT: #f3f3f3 1px solid; COLOR: #ccc; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: #f3f3f3 1px solid}";
                            break;
                        }
                    case PagingStyle.Viciao:
                        {
                            cssClass = "Viciao";
                            lpCSS =
                                "DIV.Viciao {MARGIN-TOP: 20px; MARGIN-BOTTOM: 10px}DIV.Viciao A {BORDER-RIGHT: #8db5d7 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #8db5d7 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; BORDER-LEFT: #8db5d7 1px solid; COLOR: #000; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: #8db5d7 1px solid; TEXT-DECORATION: none}DIV.Viciao A:hover {BORDER-RIGHT: red 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: red 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; BORDER-LEFT: red 1px solid; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: red 1px solid}DIV.Viciao A:active {BORDER-RIGHT: red 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: red 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; BORDER-LEFT: red 1px solid; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: red 1px solid}DIV.Viciao SPAN.current {BORDER-RIGHT: #e89954 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #e89954 1px solid; PADDING-LEFT: 5px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; BORDER-LEFT: #e89954 1px solid; COLOR: #000; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: #e89954 1px solid; BACKGROUND-COLOR: #ffca7d}DIV.Viciao SPAN.disabled {BORDER-RIGHT: #ccc 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #ccc 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; BORDER-LEFT: #ccc 1px solid; COLOR: #ccc; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: #ccc 1px solid}";
                            break;
                        }
                    case PagingStyle.Yahoo2:
                        {
                            cssClass = "Yahoo2";
                            lpCSS =
                                "DIV.Yahoo2 {PADDING-RIGHT: 3px; PADDING-LEFT: 3px; FONT-SIZE: 0.85em; PADDING-BOTTOM: 3px; MARGIN: 3px; PADDING-TOP: 3px; FONT-FAMILY: Tahoma,Helvetica,sans-serif; TEXT-ALIGN: center}DIV.Yahoo2 A {BORDER-RIGHT: #ccdbe4 1px solid; PADDING-RIGHT: 8px; BACKGROUND-POSITION: 50% bottom; BORDER-TOP: #ccdbe4 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 2px; BORDER-LEFT: #ccdbe4 1px solid; COLOR: #0061de; MARGIN-RIGHT: 3px; PADDING-TOP: 2px; BORDER-BOTTOM: #ccdbe4 1px solid; TEXT-DECORATION: none}DIV.Yahoo2 A:hover {BORDER-RIGHT: #2b55af 1px solid; BORDER-TOP: #2b55af 1px solid; BACKGROUND-IMAGE: none; BORDER-LEFT: #2b55af 1px solid; COLOR: #fff; BORDER-BOTTOM: #2b55af 1px solid; BACKGROUND-COLOR: #3666d4}DIV.Yahoo2 A:active {BORDER-RIGHT: #2b55af 1px solid; BORDER-TOP: #2b55af 1px solid; BACKGROUND-IMAGE: none; BORDER-LEFT: #2b55af 1px solid; COLOR: #fff; BORDER-BOTTOM: #2b55af 1px solid; BACKGROUND-COLOR: #3666d4}DIV.Yahoo2 SPAN.current {PADDING-RIGHT: 6px; PADDING-LEFT: 6px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; COLOR: #000; MARGIN-RIGHT: 3px; PADDING-TOP: 2px}DIV.Yahoo2 SPAN.disabled {DISPLAY: none}DIV.Yahoo2 A.next {BORDER-RIGHT: #ccdbe4 2px solid; BORDER-TOP: #ccdbe4 2px solid; MARGIN: 0px 0px 0px 10px; BORDER-LEFT: #ccdbe4 2px solid; BORDER-BOTTOM: #ccdbe4 2px solid}DIV.Yahoo2 A.next:hover {BORDER-RIGHT: #2b55af 2px solid; BORDER-TOP: #2b55af 2px solid; BORDER-LEFT: #2b55af 2px solid; BORDER-BOTTOM: #2b55af 2px solid}DIV.Yahoo2 A.prev {BORDER-RIGHT: #ccdbe4 2px solid; BORDER-TOP: #ccdbe4 2px solid; MARGIN: 0px 10px 0px 0px; BORDER-LEFT: #ccdbe4 2px solid; BORDER-BOTTOM: #ccdbe4 2px solid}DIV.Yahoo2 A.prev:hover {BORDER-RIGHT: #2b55af 2px solid; BORDER-TOP: #2b55af 2px solid; BORDER-LEFT: #2b55af 2px solid; BORDER-BOTTOM: #2b55af 2px solid}";
                            break;
                        }
                }
            }

            #endregion

            #region IWebPaging 成员

            /// <summary>
            /// 获取分页字符串
            /// </summary>
            /// <param name="nCurrentPage"></param>
            /// <param name="nTotalItems"></param>
            /// <returns></returns>
            public string GetPageData(int nCurrentPage, int nTotalItems)
            {
                int nPages = nTotalItems / _nPageSize;
                nPages = nTotalItems % _nPageSize == 0 ? nPages : ++nPages;
                int nStart = nCurrentPage - NForeDisplay;
                nStart = nStart < 0 ? 0 : nStart;
                int nEnd = nStart + NForeDisplay + NTailDisplay;
                nEnd = nEnd > nPages ? nPages : nEnd;
                //szSplitPage = "<div style=\"margin:3px;font-size:12px;\" class=" + cssClass + ">";
                string szSplitPage = "<div class=" + cssClass + ">";
                if (nCurrentPage > 1)
                {
                    szSplitPage += "<A href='" + _lpLinkPage + "?" + LpRequestQueryString + "=" + (nCurrentPage - 1) +
                                   "' title='上一页'> " + SzPrevious + " </A>";
                }
                else
                {
                    szSplitPage += "<SPAN class=disabled> " + SzPrevious + " </SPAN>";
                }
                for (int i = nStart; i < nEnd; i++)
                {
                    if (i + 1 == nCurrentPage)
                    {
                        szSplitPage += "<SPAN class=current>" + (i + 1) + "</SPAN>";
                    }
                    else
                    {
                        szSplitPage += "<A href='" + _lpLinkPage + "?" + LpRequestQueryString + "=" + (i + 1) + "'>" +
                                       (i + 1) + "</A>";
                    }
                }
                if (nPages > 1 && nCurrentPage < nPages)
                {
                    szSplitPage += "<A href='" + _lpLinkPage + "?" + LpRequestQueryString + "=" + (nCurrentPage + 1) +
                                   "' title='下一页'> " + SzNext + " </A>";
                }
                else
                {
                    szSplitPage += "<SPAN class=disabled> " + SzNext + " </SPAN>";
                }
                szSplitPage += "</div>";
                return szSplitPage;
            }

            /// <summary>
            /// 自动获取分页字符串
            /// </summary>
            /// <param name="nTotalItems"></param>
            /// <returns></returns>
            public string AutoPage(int nTotalItems)
            {
                return HttpContext.Current.Request.QueryString["page"] == null
                           ? GetPageData(1, nTotalItems)
                           : GetPageData(Convert.ToInt32(HttpContext.Current.Request.QueryString["page"]), nTotalItems);
            }

            #endregion
        }
    }
}