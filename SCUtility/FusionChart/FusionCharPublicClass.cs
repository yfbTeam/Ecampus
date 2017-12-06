using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCUtility.FusionChart
{
    public class FusionCharPublicClass
    {
        #region 图型处理公共方法

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="c"></param>
        /// <param name="llist"></param>
        /// <param name="slist"></param>
        /// <param name="chartType"></param>
        /// <param name="colors"></param>
        /// <param name="chartName"></param>
        /// <param name="chartWidth"></param>
        /// <param name="chartHeight"></param>
        /// <returns></returns>
        public string GetXMLData(DataTable dt, chart c, IList<line> llist, IList<style> slist, FusionChartType chartType, IList<string> colors, string chartName, string chartWidth, string chartHeight, string isload="")
        {
            IList<string> countershaft = new List<string>();
            return GetXMLData(dt, c, llist, slist, chartType, colors, chartName, chartWidth, chartHeight, countershaft, isload);
        }

        /// <summary>
        /// 获取图表XML字符串(双轴）
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="c"></param>
        /// <param name="llist"></param>
        /// <param name="slist"></param>
        /// <param name="chartType"></param>
        /// <returns></returns>
        public string GetXMLData(DataTable dt, chart c, IList<line> llist, IList<style> slist, FusionChartType chartType, IList<string> colors, string chartName, string chartWidth, string chartHeight, IList<string> countershaft,string isload="")
        {
            GetFusionChartDataXML getXML = new GetFusionChartDataXML();
            StringBuilder sb_XMLData = new StringBuilder();

            colors = new List<string>();
            while (colors.Count < dt.Columns.Count)
            {
                colors.Add("000080");
                colors.Add("ff0000");
                colors.Add("ff8901");
                colors.Add("55FFFF");
                colors.Add("00BF55");
                colors.Add("FFBF00");
                colors.Add("FFFF00");
                colors.Add("FF9FFF");
                colors.Add("00FB00");
                colors.Add("FF9FFF");
                colors.Add("FF9ooo");
                colors.Add("EF9FFF");
                colors.Add("5F9FFF");
                colors.Add("000000");
                colors.Add("55FFFF");
                colors.Add("ffffff");
                colors.Add("000080");
                colors.Add("0ED1DC");
                colors.Add("595959");
                colors.Add("55FFFF");
                colors.Add("00BF55");
                colors.Add("FFBF00");
                colors.Add("FFFF00");
                colors.Add("FF9ooo");
                colors.Add("EF9FFF");
                colors.Add("5F9FFF");
                colors.Add("000000");
                colors.Add("000080");
                colors.Add("ff0000");
                colors.Add("595959");
                colors.Add("55FFFF");
                colors.Add("00BF55");
                colors.Add("FFBF00");
                colors.Add("FFFF00");
                colors.Add("FF9FFF");
                colors.Add("00FB00");
                colors.Add("000080");
                colors.Add("0ED1DC");
                colors.Add("595959");
                colors.Add("55FFFF");
                colors.Add("00BF55");
                colors.Add("FFBF00");
                colors.Add("FFFF00");
                colors.Add("FF9ooo");
                colors.Add("EF9FFF");
                colors.Add("5F9FFF");
            }

            if (chartType == FusionChartType.MSColumn2D)
            {
                sb_XMLData.Append(getXML.GetFusionChartStartXML(c, chartName));
                sb_XMLData.Append(getXML.GetFusionChartGeneralXML(dt, colors));
                //sb_XMLData.Append(getXML.GetFusionChartTrendLinesXML(llist));
                // sb_XMLData.Append(getXML.GetFusionChartStyleXML(slist));
                sb_XMLData.Append(getXML.GetFusionChartEndXML());

                return RenderChart(
                           "../Scripts/FusionChart/Swf/MSColumn2D.swf",
                           "",
                           sb_XMLData.ToString(),
                           chartName,
                           chartWidth,
                           chartHeight,
                           false,
                           true
                           );
            }
            else if (chartType == FusionChartType.Doughnut2D)
            {
                sb_XMLData.Append(getXML.GetFusionChartStartXML(c, chartName));
                sb_XMLData.Append(getXML.GetFusionChartSetXML(dt, colors));
                sb_XMLData.Append(getXML.GetFusionChartStyleXML(slist));
                sb_XMLData.Append(getXML.GetFusionChartEndXML());

                return RenderChart(
                           "../Scripts/FusionChart/Swf/Doughnut2D.swf",
                           "",
                           sb_XMLData.ToString(),
                           chartName,
                           chartWidth,
                           chartHeight,
                           false,
                           true
                           );
            }
            else if (chartType == FusionChartType.Pie2D)
            {
                sb_XMLData.Append(getXML.GetFusionChartStartXML(c, chartName));
                sb_XMLData.Append(getXML.GetFusionChartSetXML(dt, colors));
                sb_XMLData.Append(getXML.GetFusionChartStyleXML(slist));
                sb_XMLData.Append(getXML.GetFusionChartEndXML());
                if (!string.IsNullOrWhiteSpace(isload))
                {
                    return sb_XMLData.ToString();
                }
                else
                {
                    return RenderChart(
                           "../Scripts/FusionChart/Swf/Pie2D.swf",
                           "",
                           sb_XMLData.ToString(),
                           chartName,
                           chartWidth,
                           chartHeight,
                           false,
                           true
                           );
                }
            }
            else if (chartType == FusionChartType.AngularGauge)
            {
                sb_XMLData.Append(getXML.GetFusionChartStartXML(c, chartName));
                sb_XMLData.Append(getXML.GetFusionChartGeneralXML(dt, colors));
                sb_XMLData.Append(getXML.GetFusionChartTrendLinesXML(llist));
                sb_XMLData.Append(getXML.GetFusionChartStyleXML(slist));
                sb_XMLData.Append(getXML.GetFusionChartEndXML());

                return RenderChart(
                           "../Scripts/FusionChart/Swf/AngularGauge.swf",
                           "",
                           sb_XMLData.ToString(),
                           chartName,
                           chartWidth,
                           chartHeight,
                           false,
                           true
                           );
            }
            else if (chartType == FusionChartType.Bubble)
            {
                sb_XMLData.Append(getXML.GetFusionChartStartXML(c, chartName));
                sb_XMLData.Append(getXML.GetFusionChartBubbleXML(dt));
                sb_XMLData.Append(getXML.GetFusionChartStyleXML(slist));
                sb_XMLData.Append(getXML.GetFusionChartEndXML());

                return RenderChart(
                           "../Scripts/FusionChart/Swf/Bubble.swf",
                           "",
                           sb_XMLData.ToString(),
                           chartName,
                           chartWidth,
                           chartHeight,
                           false,
                           true
                           );
            }
            else if (chartType == FusionChartType.Funnel)
            {
                sb_XMLData.Append(getXML.GetFusionChartStartXML(c, chartName));
                sb_XMLData.Append(getXML.GetFusionChartSetXML(dt, colors));
                //sb_XMLData.Append(getXML.GetFusionChartTrendLinesXML(llist));
                // sb_XMLData.Append(getXML.GetFusionChartStyleXML(slist));
                sb_XMLData.Append(getXML.GetFusionChartEndXML());

                return RenderChart(
                           "../Scripts/FusionChart/Swf/Funnel.swf",
                           "",
                           sb_XMLData.ToString(),
                           chartName,
                           chartWidth,
                           chartHeight,
                           false,
                           true
                           );
            }
            else if (chartType == FusionChartType.MSCombiDY2D)
            {
                sb_XMLData.Append(getXML.GetFusionChartStartXML(c, chartName));
                sb_XMLData.Append(getXML.GetFusionChartGeneralXML(dt, colors, countershaft));
                //sb_XMLData.Append(getXML.GetFusionChartStyleXML(slist));
                sb_XMLData.Append(getXML.GetFusionChartEndXML());

                return RenderChart(
                           "../Scripts/FusionChart/Swf/MSCombiDY2D.swf",
                           "",
                           sb_XMLData.ToString(),
                           chartName,
                           chartWidth,
                           chartHeight,
                           false,
                           true
                           );
            }

            else if (chartType == FusionChartType.Line)
            {
                sb_XMLData.Append(getXML.GetFusionChartStartXML(c, chartName));
                sb_XMLData.Append(getXML.GetFusionChartSetXML(dt, colors));
                sb_XMLData.Append(getXML.GetFusionChartEndXML());
                if (!string.IsNullOrWhiteSpace(isload))
                {
                    return sb_XMLData.ToString();
                }
                else
                {
                    return RenderChart(
                           "../Scripts/FusionChart/Swf/Line.swf",
                           "",
                           sb_XMLData.ToString(),
                           chartName,
                           chartWidth,
                           chartHeight,
                           false,
                           true
                           );
                }
            }

            else if (chartType == FusionChartType.Line2)
            {
                sb_XMLData.Append(getXML.GetFusionChartStartXML(c, chartName));
                sb_XMLData.Append(getXML.GetFusionChartGeneralXML(dt, colors));
                //         sb_XMLData.Append(getXML.GetFusionChartTrendLinesXML(llist));
                //      sb_XMLData.Append(getXML.GetFusionChartStyleXML(slist));
                sb_XMLData.Append(getXML.GetFusionChartEndXML());
                if (!string.IsNullOrWhiteSpace(isload))
                {
                    return sb_XMLData.ToString();
                }
                else
                {
                    return RenderChart(
                           "../Scripts/FusionChart/Swf/MSLine.swf",
                           "",
                           sb_XMLData.ToString(),
                           chartName,
                           chartWidth,
                           chartHeight,
                           false,
                           true
                           );
                }
            }
            else if (chartType == FusionChartType.Pie3D)
            {
                sb_XMLData.Append(getXML.GetFusionChartStartXML(c, chartName));
                sb_XMLData.Append(getXML.GetFusionChartSetXML(dt, colors));
                sb_XMLData.Append(getXML.GetFusionChartStyleXML(slist));
                sb_XMLData.Append(getXML.GetFusionChartEndXML());
                if (!string.IsNullOrWhiteSpace(isload))
                {
                    return sb_XMLData.ToString();
                }
                else
                {
                    return RenderChart(
                           "../Scripts/FusionChart/Swf/Pie3D.swf",
                           "",
                           sb_XMLData.ToString(),
                           chartName,
                           chartWidth,
                           chartHeight,
                           false,
                           true
                           );
                }
            }
            return "";
        }


        /// <summary>
        /// 获取仪表盘XML字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="c"></param>
        /// <param name="llist"></param>
        /// <param name="slist"></param>
        /// <param name="chartType"></param>
        /// <returns></returns>
        public string GetXMLData(AngularGaugeChart c, IList<colorRange> colorlist, IList<dials> diallist, IList<style> slist, IList<trendpoints> trendpointList, IList<annotations> annotationList, string chartName, string isload = "")
        {
            GetFusionChartDataXML getXML = new GetFusionChartDataXML();
            StringBuilder sb_XMLData = new StringBuilder();
            sb_XMLData.Append(getXML.GetFusionChartAngularGaugeStartXML(c));
            sb_XMLData.Append(getXML.GetFusionChartAngularGaugeXML(colorlist, diallist, trendpointList, annotationList));
            sb_XMLData.Append(getXML.GetFusionChartStyleXML(slist));
            sb_XMLData.Append(getXML.GetFusionChartEndXML());
            return RenderChart(
                           "../Scripts/FusionChart/Swf/AngularGauge.swf",
                           "",
                           sb_XMLData.ToString(),
                           chartName,
                           "350",
                           "200",
                           false,
                           false
                           );

        }


        /// <summary>
        /// 图型呈现
        /// </summary>
        /// <param name="chartSWF"></param>
        /// <param name="strURL"></param>
        /// <param name="strXML"></param>
        /// <param name="chartId"></param>
        /// <param name="chartWidth"></param>
        /// <param name="chartHeight"></param>
        /// <param name="debugMode"></param>
        /// <param name="registerWithJS"></param>
        /// <returns></returns>
        public static string RenderChart(string chartSWF, string strURL, string strXML, string chartId,
            string chartWidth, string chartHeight, bool debugMode, bool registerWithJS)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("<!-- START Script Block for Chart {0} -->" + Environment.NewLine, chartId);
            builder.AppendFormat("<div id='{0}Div' align='center'>" + Environment.NewLine, chartId);
            builder.Append("Chart." + Environment.NewLine);
            builder.Append("</div>" + Environment.NewLine);
            builder.Append("<script type=\"text/javascript\">" + Environment.NewLine);
            builder.AppendFormat("var chart_{0} = new FusionCharts(\"{1}\", \"{0}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\");" + Environment.NewLine, chartId, chartSWF, chartWidth, chartHeight, boolToNum(debugMode), boolToNum(registerWithJS));
            if (strXML.Length == 0)
            {
                builder.AppendFormat("chart_{0}.setDataURL(\"{1}\");" + Environment.NewLine, chartId, strURL);
            }
            else
            {
                builder.AppendFormat("chart_{0}.setDataXML(\"{1}\");" + Environment.NewLine, chartId, strXML);
            }


            builder.AppendFormat("chart_{0}.render(\"{1}Div\");" + Environment.NewLine, chartId, chartId);
            builder.Append("</script>" + Environment.NewLine);
            builder.AppendFormat("<!-- END Script Block for Chart {0} -->" + Environment.NewLine, chartId);
            return builder.ToString();
        }

        /// <summary>
        /// Transform the meaning of boolean value in integer value
        /// </summary>
        /// <param name="value">true/false value to be transformed</param>
        /// <returns>1 if the value is true, 0 if the value is false</returns>
        public static int boolToNum(bool value)
        {
            return value ? 1 : 0;
        }

        #endregion
    }
}
