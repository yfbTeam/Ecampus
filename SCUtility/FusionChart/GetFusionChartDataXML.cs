using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCUtility.FusionChart
{
    public class GetFusionChartDataXML
    {
        /// <summary>
        /// 获取基础XML数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string GetFusionChartGeneralXML(DataTable dt, IList<string> colors)
        {
            StringBuilder XMLData = new StringBuilder();
            //sb_XMLchart.Append("<chart caption='" + chartName + "' xAxisName='" + xName + "' yAxisName='次数' showLables='1' showvalues='1' decimals='0' numberPrefix='' placeValuesInside='1' rotateValues='1'>");

            IList<StringBuilder> sbList = new List<StringBuilder>();

            StringBuilder sb_XMLCateories = new StringBuilder();
            sb_XMLCateories.Append("<categories>");

            for (int i = 1; i <= dt.Columns.Count - 1; i++)
            {
                StringBuilder sb_XMLDataSet = new StringBuilder();
                sb_XMLDataSet.Append("<dataset seriesName='" + dt.Columns[i].Caption + "' color='" + colors[i - 1] + "'>");
                sbList.Add(sb_XMLDataSet);
            }

            foreach (DataRow dr in dt.Rows)
            {
                sb_XMLCateories.Append("<category label='" + dr[0].ToString() + "' />");
                for (int i = 0; i < sbList.Count; i++)
                {
                    sbList[i].Append("<set value='" + dr[i + 1].ToString() + "'/>");
                }
            }
            sb_XMLCateories.Append("</categories>");
            XMLData.Append(sb_XMLCateories);
            foreach (var sb in sbList)
            {
                sb.Append("</dataset>");
                XMLData.Append(sb); ;
            }

            return XMLData.ToString();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    sb.Append("<set label='" + dr[0].ToString() + "' value='" + dr[1].ToString() + "' />");
            //}
            //sb.Append("</chart>");
            //return sb.ToString();
        }

        /// <summary>
        /// 获取基础XML数据(双轴)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string GetFusionChartGeneralXML(DataTable dt, IList<string> colors, IList<string> countershaft)
        {
            StringBuilder XMLData = new StringBuilder();
            //sb_XMLchart.Append("<chart caption='" + chartName + "' xAxisName='" + xName + "' yAxisName='次数' showLables='1' showvalues='1' decimals='0' numberPrefix='' placeValuesInside='1' rotateValues='1'>");

            IList<StringBuilder> sbList = new List<StringBuilder>();

            StringBuilder sb_XMLCateories = new StringBuilder();
            sb_XMLCateories.Append("<categories>");

            for (int i = 1; i <= dt.Columns.Count - 1; i++)
            {
                StringBuilder sb_XMLDataSet = new StringBuilder();
                sb_XMLDataSet.Append("<dataset seriesName='" + dt.Columns[i].Caption + "' parentYAxis='" + countershaft[i - 1] + "' color='" + colors[i - 1] + "'  renderAs='Line'>");
                sbList.Add(sb_XMLDataSet);
            }

            foreach (DataRow dr in dt.Rows)
            {
                sb_XMLCateories.Append("<category label='" + dr[0].ToString() + "' />");
                for (int i = 0; i < sbList.Count; i++)
                {
                    sbList[i].Append("<set value='" + dr[i + 1].ToString() + "'/>");
                }
            }
            sb_XMLCateories.Append("</categories>");
            XMLData.Append(sb_XMLCateories);
            foreach (var sb in sbList)
            {
                sb.Append("</dataset>");
                XMLData.Append(sb); ;
            }

            return XMLData.ToString();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    sb.Append("<set label='" + dr[0].ToString() + "' value='" + dr[1].ToString() + "' />");
            //}
            //sb.Append("</chart>");
            //return sb.ToString();
        }


        /// <summary>
        ///  基准线
        /// </summary>
        /// <param name="lineList"></param>
        /// <returns></returns>
        public string GetFusionChartTrendLinesXML(IList<line> lineList)
        {
            StringBuilder sb_TrendLines = new StringBuilder();

            sb_TrendLines.Append("<trendlines>");
            foreach (var line in lineList)
            {
                sb_TrendLines.Append("<line startValue='" + line.StartValue + "' endValue='" + line.EndValue + "' displayValue='" + line.DisplayValue + "' color='" + line.Color + "' isTrendZone='" + line.IsTrendZone + "' showOnTop='" + line.ShowOnTop + "' alpha = '+" + line.Alpha + "+' valueOnright='" + line.ValueOnRight + "'/>");
            }
            sb_TrendLines.Append("</trendlines>");

            return sb_TrendLines.ToString();
        }

        /// <summary>
        /// 图表样式
        /// </summary>
        /// <param name="styleList"></param>
        /// <returns></returns>
        public string GetFusionChartStyleXML(IList<style> styleList)
        {
            StringBuilder sb_Style = new StringBuilder();
            StringBuilder sb_definition = new StringBuilder();
            StringBuilder sb_application = new StringBuilder();

            sb_Style.Append("<styles>");
            sb_definition.Append("<definition>");
            sb_application.Append("<application>");

            foreach (var style in styleList)
            {
                sb_definition.Append("<style name='" + style.Name + "' type='" + style.Type + "' font='" + style.Font + "' size='" + style.Size + "' bold='" + style.Bold + "' underline='" + style.Underline + "' />");
                sb_application.Append("<apply toObject='" + style.ToObject + "' styles='" + style.Name + "' />");
            }
            sb_definition.Append("</definition>");
            sb_application.Append("</application>");
            sb_Style.Append(sb_definition);
            sb_Style.Append(sb_application);
            sb_Style.Append("</styles>");
            return sb_Style.ToString();
        }

        /// <summary>
        /// 饼图基本数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string GetFusionChartSetXML(DataTable dt, IList<string> colors)
        {
            StringBuilder sb_Set = new StringBuilder();
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (i > 20)
                {
                    i = 0;
                }
                sb_Set.Append("<set label='" + dr[0].ToString() + "' value='" + dr[1].ToString() + "' color='" + colors[i] + "' />");
                i++;
            }
            return sb_Set.ToString();
        }

        /// <summary>
        /// XML数据头
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public string GetFusionChartStartXML(chart c, string chartName)
        {

            StringBuilder sb_Start = new StringBuilder();
            sb_Start.Append("<chart caption='" + c.Caption + "' xAxisName='" + c.XAxisName + "' yAxisName='" + c.YAxisName + "' yAxisMaxValue='" + c.YAxisMaxValue + "' numberPrefix='" + c.NumberPrefix + "' divLinColor='" + c.DivLineColor + "' divLineAlpha='" + c.DivLineAlpha + "' alternatrHGridAlpha='" + c.AlternateHGridAlpha + "' canvasBorderColor='" + c.CanvasBorderColor + "' baseFontColor='" + c.BaseFontColor + "' lineColor='" + c.LineColor + "' numVDivlines='" + c.NumVDivlines + "' showAlternateVGridColor='" + c.ShowAlternateVgridColor + "' anchorSides='" + c.AnchorSides + "' anchorRadius='" + c.AnchorRadius + "' showValues='" + c.ShowValues + "' showLabels='" + c.ShowLables + "' decimals='" + c.Decimals + "' placeValuesInside='" + c.PlaceValuesInside + "' rotateValues='" + c.RotateValues + "' showLegend='" + c.ShowLegend + "' legendPosition='" + c.LegendPosition + "' bgcolor='" + c.Bgcolor + "' basefontsize='" + c.Basefontsize + "' showpercentvalues='" + c.Showpercentvalues + "' bgratio='" + c.Bgratio + "' startingangle='" + c.Startingangle + "' animation='" + c.Animation + "' bgalpha='" + c.Bgalpha + "' chartrightmargin='" + c.ChartRightmargin + "' slicingDistance='" + c.SlicingDistance + "' isSliced='" + c.IsSliced + "' showNames='" + c.ShowNames + "' formatNumberScale='" + c.FormatNumberScale + "' canvasBgAlpha='" + c.CanvasBgAlpha + "' drawQuadrant='" + c.DrawQuadrant + "' quadrantLabelTL='" + c.QuadrantLabelTL + "' quadrantLabelTR='" + c.QuadrantLabelTR + "' quadrantLabelBR='" + c.QuadrantLabelBR + "' quadrantLabelBL='" + c.QuadrantLabelBL + "' quadrantLineThickness='" + c.QuadrantLineThickness + "' quadrantLineColor='" + c.QuadrantLineColor + "' quadrantXVal='" + c.QuadrantXVal + "' quadrantYVal='" + c.QuadrantYVal + "' pNumberSuffix='" + c.PNumberSuffix + "' sNumberSuffix='" + c.SNumberSuffix + "' NumberSuffix='" + c.NumberSuffix + "' pYAxisName='" + c.PYAxisName + "' sformatNumberScale='" + c.SFormatNumberScale + "' showZeroPies='" + c.ShowZeroPies + "' pformatNumberScale='" + c.PFormatNumberScale + "' showPercentInToolTip='" + c.ShowPercentInToolTip + "' sYAxisName='" + c.SYAxisName + "' rotatesYAxisName='" + c.RotateSYAxisNames + "'  howLables='" + c.HowLables + "' labelPadding='" + c.LabelPadding + "' yAxisValuesPadding='" + c.YAxisValuesPadding + "'  valuePosition='" + c.ValuePosition + "' labelDisplay='" + c.LabelDisplay + "' slantLabels='" + c.SlantLabels + "' anchorBgAlpha='" + c.AnchorBgAlpha + "' limitsDecimalPrecision='" + c.LimitsDecimalPrecision + "'>");
            return sb_Start.ToString();
        }

        /// <summary>
        ///  仪表盘XML数据头
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public string GetFusionChartAngularGaugeStartXML(AngularGaugeChart c)
        {
            StringBuilder sb_AngularGaugeStart = new StringBuilder();
            sb_AngularGaugeStart.Append("<chart caption='" + c.Caption + "'  bgAlpha='" + c.BgAlpha + "' bgColor='" + c.BgColor + "' lowerLimit='" + c.LowerLimit + "' upperLimit='" + c.UpperLimit + "' numberSuffix='" + c.NumberSuffix + "' showBorder='" + c.ShowBorder + "' basefontColor='" + c.BasefontColor + "' chartTopMargin='" + c.ChartTopMargin + "' chartBottomMargin='" + c.ChartBottomMargin + "' chartLeftMargin='" + c.ChartLeftMargin + "' chartRightMargin='" + c.ChartRightMargin + "' toolTipBgColor='" + c.ToolTipBgColor + "' gaugeFillMix='" + c.GaugeFillMix + "' gaugeFillRatio='" + c.GaugeFillRatio + "' >");
            return sb_AngularGaugeStart.ToString();
        }

        /// <summary>
        /// XML数据结束
        /// </summary>
        /// <returns></returns>
        public string GetFusionChartEndXML()
        {
            StringBuilder sb_end = new StringBuilder();
            sb_end.Append("</chart>");
            return sb_end.ToString();
        }

        /// <summary>
        /// 气泡图基础数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string GetFusionChartBubbleXML(DataTable dt)
        {
            StringBuilder sb_Bubble = new StringBuilder();

            StringBuilder sb_XMLCateories = new StringBuilder();
            sb_XMLCateories.Append("<categories>");
            StringBuilder sb_XMLDataSet = new StringBuilder();
            sb_XMLDataSet.Append("<dataSet showValues='1'>");

            float x = 0;

            foreach (DataRow dr in dt.Rows)
            {
                if (x < float.Parse(dr[1].ToString()))
                {
                    x = float.Parse(dr[1].ToString());
                }


                sb_XMLDataSet.Append("<set name='" + dr[0].ToString() + "' x='" + dr[1].ToString() + "' y='" + dr[2].ToString() + "' z='" + dr[3].ToString() + "' />");
            }

            int xPart = (int)Math.Round(x * 1.2 / 5);

            for (int i = 0; i < 6; i++)
            {
                sb_XMLCateories.Append("<category label='" + xPart * i + "' x='" + xPart * i + "' sL='1' />");
            }

            sb_XMLCateories.Append("</categories>");
            sb_Bubble.Append(sb_XMLCateories);
            sb_XMLDataSet.Append("</dataSet>");
            sb_Bubble.Append(sb_XMLDataSet);

            return sb_Bubble.ToString();

        }

        /// <summary>
        /// 仪表盘基础数据
        /// </summary>
        /// <param name="colorList"></param>
        /// <param name="dialList"></param>
        /// <param name="pointList"></param>
        /// <param name="annotationList"></param>
        /// <returns></returns>
        public string GetFusionChartAngularGaugeXML(IList<colorRange> colorList, IList<dials> dialList, IList<trendpoints> pointList, IList<annotations> annotationList)
        {
            StringBuilder sb_AngularGauge = new StringBuilder();

            StringBuilder sb_ColorRange = new StringBuilder();
            sb_ColorRange.Append("<colorRange>");
            foreach (var color in colorList)
            {
                sb_ColorRange.Append("<color minValue='" + color.MinValue + "' maxValue='" + color.MaxValue + "' code='" + color.Code + "' />");
            }
            sb_ColorRange.Append("</colorRange>");

            StringBuilder sb_Dials = new StringBuilder();
            sb_Dials.Append("<dials>");
            foreach (var dial in dialList)
            {
                sb_Dials.Append("<dial value='" + dial.Value + "' rearExtension='" + dial.RearExtension + "' />");
            }
            sb_Dials.Append("</dials>");

            StringBuilder sb_Trendpoints = new StringBuilder();
            sb_Trendpoints.Append("<trendpoints>");
            foreach (var point in pointList)
            {
                sb_Trendpoints.Append("<point value='" + point.Value + "' displayValue='" + point.DisplayValue + "' fontcolor='" + point.Fontcolor + "' useMarker='" + point.UseMarker + "' dashed='" + point.Dashed + "' dashLen='" + point.DashLen + "' dashGap='" + point.DashGap + "' valueinside='" + point.Valueinside + "' />");
            }
            sb_Trendpoints.Append("</trendpoints>");

            StringBuilder sb_Annotations = new StringBuilder();
            sb_Annotations.Append("<annotations>");
            sb_Annotations.Append("<annotationGroup id='Grp1' showBelow='1'>");
            foreach (var annotation in annotationList)
            {
                sb_Annotations.Append("<annotation type='" + annotation.Type + "' x='" + annotation.X + "' y='" + annotation.Y + "' toX='" + annotation.ToX + "' toY='" + annotation.ToY + "' radius='" + annotation.Radius + "' color='" + annotation.Color + "' showBorder='" + annotation.ShowBorder + "' />");
            }
            sb_Annotations.Append("</annotationGroup>");
            sb_Annotations.Append("</annotations>");

            sb_AngularGauge.Append(sb_ColorRange);
            sb_AngularGauge.Append(sb_Dials);
            sb_AngularGauge.Append(sb_Trendpoints);
            sb_AngularGauge.Append(sb_Annotations);

            return sb_AngularGauge.ToString();
        }

    }
}
