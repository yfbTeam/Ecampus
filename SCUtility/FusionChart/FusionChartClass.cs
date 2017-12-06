using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCUtility.FusionChart
{
    class FusionChartClass
    {
    }
    public class line
    {
        string startValue = string.Empty;

        /// <summary>
        /// 起始值 单个时一条横线，有结束值时，两点连线
        /// </summary>
        public string StartValue
        {
            get { return startValue; }
            set { startValue = value; }
        }
        string endValue = string.Empty;

        /// <summary>
        /// 结束值
        /// </summary>
        public string EndValue
        {
            get { return endValue; }
            set { endValue = value; }
        }
        string displayValue = string.Empty;

        /// <summary>
        /// 线标示
        /// </summary>
        public string DisplayValue
        {
            get { return displayValue; }
            set { displayValue = value; }
        }
        string color = string.Empty;

        /// <summary>
        /// 设置线颜色
        /// </summary>
        public string Color
        {
            get { return color; }
            set { color = value; }
        }
        string isTrendZone = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string IsTrendZone
        {
            get { return isTrendZone; }
            set { isTrendZone = value; }
        }
        string showOnTop = string.Empty;

        public string ShowOnTop
        {
            get { return showOnTop; }
            set { showOnTop = value; }
        }
        string alpha = string.Empty;

        /// <summary>
        /// 透明度
        /// </summary>
        public string Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }
        string valueOnRight = string.Empty;

        /// <summary>
        /// 设置displayvalue显示右边
        /// </summary>
        public string ValueOnRight
        {
            get { return valueOnRight; }
            set { valueOnRight = value; }
        }
    }


    public class style
    {
        string toObject = string.Empty;

        public string ToObject
        {
            get { return toObject; }
            set { toObject = value; }
        }
        string name = string.Empty;

        /// <summary>
        /// 样式名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        string type = string.Empty;

        /// <summary>
        /// 样式类型
        /// </summary>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        string font = string.Empty;

        /// <summary>
        /// 字体
        /// </summary>
        public string Font
        {
            get { return font; }
            set { font = value; }
        }
        string size = string.Empty;

        /// <summary>
        /// 字号
        /// </summary>
        public string Size
        {
            get { return size; }
            set { size = value; }
        }
        string bold = string.Empty;

        public string Bold
        {
            get { return bold; }
            set { bold = value; }
        }
        string underline = string.Empty;

        /// <summary>
        /// 下划线
        /// </summary>
        public string Underline
        {
            get { return underline; }
            set { underline = value; }
        }
        string color = string.Empty;
        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        string param = string.Empty;
        public string Param
        {
            get { return param; }
            set { param = value; }
        }

        string start = string.Empty;
        public string Start
        {
            get { return start; }
            set { start = value; }
        }

        string duration = string.Empty;
        public string Duration
        {
            get { return start; }
            set { start = value; }
        }
    }


    public class chart
    {
        string caption = string.Empty;

        /// <summary>
        /// 标题
        /// </summary>
        public string Caption
        {
            get { return caption; }
            set { caption = value; }
        }




        string xAxisName = string.Empty;

        /// <summary>
        /// X轴名称
        /// </summary>
        public string XAxisName
        {
            get { return xAxisName; }
            set { xAxisName = value; }
        }

        string yAxisName = string.Empty;

        /// <summary>
        /// Y轴名称
        /// </summary>
        public string YAxisName
        {
            get { return yAxisName; }
            set { yAxisName = value; }
        }

        string pYAxisName = string.Empty;
        ///<summary>
        ///主Y轴
        ///</summary>
        public string PYAxisName
        {
            get { return pYAxisName; }
            set { pYAxisName = value; }
        }

        string numberSuffix = string.Empty;
        ///<summary>
        ///Y轴数据后缀
        ///</summary>
        public string NumberSuffix
        {
            get { return numberSuffix; }
            set { numberSuffix = value; }
        }




        string sYAxisName = string.Empty;
        ///<summary>
        ///副Y轴
        ///</summary>
        public string SYAxisName
        {
            get { return sYAxisName; }
            set { sYAxisName = value; }
        }



        string yAxisMaxValue = string.Empty;
        /// <summary>
        /// Y轴最大值
        /// </summary>
        public string YAxisMaxValue
        {
            get { return yAxisMaxValue; }
            set { yAxisMaxValue = value; }
        }
        string numberPrefix = string.Empty;

        /// <summary>
        /// Y轴数据加上前缀
        /// </summary>
        public string NumberPrefix
        {
            get { return numberPrefix; }
            set { numberPrefix = value; }
        }
        string divLineColor = string.Empty;

        /// <summary>
        /// 设置div的颜色
        /// </summary>
        public string DivLineColor
        {
            get { return divLineColor; }
            set { divLineColor = value; }
        }
        string divLineAlpha = string.Empty;

        /// <summary>
        /// 设置div的线条透明度
        /// </summary>
        public string DivLineAlpha
        {
            get { return divLineAlpha; }
            set { divLineAlpha = value; }
        }
        string alternateHGridAlpha = string.Empty;

        public string AlternateHGridAlpha
        {
            get { return alternateHGridAlpha; }
            set { alternateHGridAlpha = value; }
        }
        string canvasBorderColor = string.Empty;
        /// <summary>
        /// 图表背景边框颜色
        /// </summary>
        public string CanvasBorderColor
        {
            get { return canvasBorderColor; }
            set { canvasBorderColor = value; }
        }
        string baseFontColor = string.Empty;

        /// <summary>
        /// Canvas里面的字体颜色
        /// </summary>
        public string BaseFontColor
        {
            get { return baseFontColor; }
            set { baseFontColor = value; }
        }
        string lineColor = string.Empty;

        /// <summary>
        /// 颜色
        /// </summary>
        public string LineColor
        {
            get { return lineColor; }
            set { lineColor = value; }
        }
        string numVDivlines = string.Empty;

        public string NumVDivlines
        {
            get { return numVDivlines; }
            set { numVDivlines = value; }
        }
        string showAlternateVgridColor = string.Empty;

        /// <summary>
        /// 设置垂直div块是否高亮显示
        /// </summary>
        public string ShowAlternateVgridColor
        {
            get { return showAlternateVgridColor; }
            set { showAlternateVgridColor = value; }
        }
        string anchorSides = string.Empty;

        /// <summary>
        /// 设置Anchors是几边形 (默认 3-20)
        /// </summary>
        public string AnchorSides
        {
            get { return anchorSides; }
            set { anchorSides = value; }
        }
        string anchorRadius = string.Empty;

        /// <summary>
        /// 设置Anchors的大小
        /// </summary>
        public string AnchorRadius
        {
            get { return anchorRadius; }
            set { anchorRadius = value; }
        }
        string showValues = string.Empty;

        /// <summary>
        /// 是否显示图表表示的数据
        /// </summary>
        public string ShowValues
        {
            get { return showValues; }
            set { showValues = value; }
        }

        string showLables = string.Empty;

        public string ShowLables
        {
            get { return showLables; }
            set { showLables = value; }
        }
        string decimals = string.Empty;

        /// <summary>
        /// 设置小数点后面保留的位数
        /// </summary>
        public string Decimals
        {
            get { return decimals; }
            set { decimals = value; }
        }

        string pnumberSuffix = string.Empty;

        /// <summary>
        /// 设置主Y轴数据后缀
        /// </summary>
        public string PNumberSuffix
        {
            get { return pnumberSuffix; }
            set { pnumberSuffix = value; }
        }

        string snumberSuffix = string.Empty;

        /// <summary>
        /// 设置次Y轴数据后缀
        /// </summary>
        public string SNumberSuffix
        {
            get { return pnumberSuffix; }
            set { pnumberSuffix = value; }
        }

        string placeValuesInside = string.Empty;

        /// <summary>
        /// 是否在图表内部显示数据
        /// </summary>
        public string PlaceValuesInside
        {
            get { return placeValuesInside; }
            set { placeValuesInside = value; }
        }
        string rotateValues = string.Empty;

        /// <summary>
        /// 是否旋转90度显示图表上的数据
        /// </summary>
        public string RotateValues
        {
            get { return rotateValues; }
            set { rotateValues = value; }
        }

        string showLegend = string.Empty;

        public string ShowLegend
        {
            get { return showLegend; }
            set { showLegend = value; }
        }

        string showPercentInToolTip = string.Empty;
        /// <summary>
        /// tip上数据是否显示为百分比
        /// </summary>

        public string ShowPercentInToolTip
        {
            get { return showpercentvalues; }
            set { showpercentvalues = value; }
        }



        string legendPosition = string.Empty;

        public string LegendPosition
        {
            get { return legendPosition; }
            set { legendPosition = value; }
        }
        string bgcolor = string.Empty;

        public string Bgcolor
        {
            get { return bgcolor; }
            set { bgcolor = value; }
        }
        string basefontsize = string.Empty;

        public string Basefontsize
        {
            get { return basefontsize; }
            set { basefontsize = value; }
        }
        string showpercentvalues = string.Empty;

        public string Showpercentvalues
        {
            get { return showpercentvalues; }
            set { showpercentvalues = value; }
        }
        string bgratio = string.Empty;

        public string Bgratio
        {
            get { return bgratio; }
            set { bgratio = value; }
        }
        string startingangle = string.Empty;

        public string Startingangle
        {
            get { return startingangle; }
            set { startingangle = value; }
        }
        string animation = string.Empty;

        public string Animation
        {
            get { return animation; }
            set { animation = value; }
        }
        string bgalpha = string.Empty;

        public string Bgalpha
        {
            get { return bgalpha; }
            set { bgalpha = value; }
        }
        string numdivlines = string.Empty;
        public string Numdivlines
        {
            get { return numdivlines; }
            set { numdivlines = value; }
        }

        string showZeroPies = string.Empty;
        /// <summary>
        /// 是否显示为0值的饼图
        /// </summary>

        public string ShowZeroPies
        {
            get { return showZeroPies; }
            set { showZeroPies = value; }
        }


        string chartrightmargin = string.Empty;
        public string ChartRightmargin
        {
            get { return chartrightmargin; }
            set { chartrightmargin = value; }
        }

        string slicingDistance = string.Empty;
        /// <summary>
        /// 点击图表时这一片饼离开中心点的距离
        /// </summary>
        public string SlicingDistance
        {
            get { return slicingDistance; }
            set { slicingDistance = value; }
        }


        string isSliced = string.Empty;
        /// <summary>
        /// 被切开
        /// </summary>
        public string IsSliced
        {
            get { return isSliced; }
            set { isSliced = value; }
        }


        string showNames = string.Empty;
        public string ShowNames
        {
            get { return showNames; }
            set { showNames = value; }
        }


        string formatNumberScale = string.Empty;
        public string FormatNumberScale
        {
            get { return formatNumberScale; }
            set { formatNumberScale = value; }
        }


        string sformatNumberScale = string.Empty;
        /// <summary>
        /// 设置次Y轴是否用K,M 表示
        /// </summary>
        public string SFormatNumberScale
        {
            get { return sformatNumberScale; }
            set { sformatNumberScale = value; }
        }


        string pformatNumberScale = string.Empty;
        /// <summary>
        /// 设置主Y轴是否用K,M 表示
        /// </summary>
        public string PFormatNumberScale
        {
            get { return pformatNumberScale; }
            set { pformatNumberScale = value; }
        }


        string rotatesYAxisNames = string.Empty;
        /// <summary>
        /// 设置次Y轴是否旋转标题显示
        /// </summary>
        public string RotateSYAxisNames
        {
            get { return rotatesYAxisNames; }
            set { rotatesYAxisNames = value; }
        }


        string canvasBgAlpha = string.Empty;
        public string CanvasBgAlpha
        {
            get { return canvasBgAlpha; }
            set { canvasBgAlpha = value; }
        }


        string drawQuadrant = string.Empty;
        public string DrawQuadrant
        {
            get { return drawQuadrant; }
            set { drawQuadrant = value; }
        }


        string quadrantLabelTL = string.Empty;
        public string QuadrantLabelTL
        {
            get { return quadrantLabelTL; }
            set { quadrantLabelTL = value; }
        }


        string quadrantLabelTR = string.Empty;
        public string QuadrantLabelTR
        {
            get { return quadrantLabelTR; }
            set { quadrantLabelTR = value; }
        }



        string quadrantLabelBR = string.Empty;
        public string QuadrantLabelBR
        {
            get { return quadrantLabelBR; }
            set { quadrantLabelBR = value; }
        }


        string quadrantLabelBL = string.Empty;
        public string QuadrantLabelBL
        {
            get { return quadrantLabelBL; }
            set { quadrantLabelBL = value; }
        }


        string quadrantLineThickness = string.Empty;
        public string QuadrantLineThickness
        {
            get { return quadrantLineThickness; }
            set { quadrantLineThickness = value; }
        }


        string quadrantLineColor = string.Empty;
        public string QuadrantLineColor
        {
            get { return quadrantLineColor; }
            set { quadrantLineColor = value; }
        }


        string quadrantXVal = string.Empty;
        public string QuadrantXVal
        {
            get { return quadrantXVal; }
            set { quadrantXVal = value; }
        }


        string quadrantYVal = string.Empty;
        public string QuadrantYVal
        {
            get { return quadrantYVal; }
            set { quadrantYVal = value; }
        }

        string howLables = string.Empty;

        public string HowLables
        {
            get { return howLables; }
            set { howLables = value; }


        }

        string labelPadding = string.Empty;

        public string LabelPadding
        {
            get { return labelPadding; }
            set { labelPadding = value; }


        }

        string yAxisValuesPadding = string.Empty;

        public string YAxisValuesPadding
        {
            get { return yAxisValuesPadding; }
            set { yAxisValuesPadding = value; }


        }
        string valuePosition = string.Empty;
        public string ValuePosition
        {
            get { return valuePosition; }
            set { valuePosition = value; }

        }

        string labelDisplay = string.Empty;
        public string LabelDisplay
        {
            get { return labelDisplay; }
            set { labelDisplay = value; }
        }

        string slantLabels = string.Empty;
        public string SlantLabels
        {
            get { return slantLabels; }
            set { slantLabels = value; }
        }
        string anchorBgAlpha = string.Empty;
        public string AnchorBgAlpha
        {
            get { return anchorBgAlpha; }
            set { anchorBgAlpha = value; }
        }
        string limitsDecimalPrecision = string.Empty;
        public string LimitsDecimalPrecision
        {
            get { return limitsDecimalPrecision; }
            set { limitsDecimalPrecision = value; }
        }
    }


    public class ChartDataTable
    {
        string name = string.Empty;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        IList<string> columnlist = new List<string>();

        public IList<string> Columnlist
        {
            get { return columnlist; }
            set { columnlist = value; }
        }
    }




    public class colorRange
    {
        string minValue = string.Empty;

        public string MinValue
        {
            get { return minValue; }
            set { minValue = value; }
        }
        string maxValue = string.Empty;

        public string MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }
        string code = string.Empty;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }
    }

    public class dials
    {
        string value = string.Empty;

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        string rearExtension = string.Empty;

        public string RearExtension
        {
            get { return rearExtension; }
            set { rearExtension = value; }
        }
    }

    public class trendpoints
    {
        string value = string.Empty;

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        string displayValue = string.Empty;

        public string DisplayValue
        {
            get { return displayValue; }
            set { displayValue = value; }
        }
        string fontcolor = string.Empty;

        public string Fontcolor
        {
            get { return fontcolor; }
            set { fontcolor = value; }
        }
        string useMarker = string.Empty;

        public string UseMarker
        {
            get { return useMarker; }
            set { useMarker = value; }
        }
        string dashed = string.Empty;

        public string Dashed
        {
            get { return dashed; }
            set { dashed = value; }
        }
        string dashLen = string.Empty;

        public string DashLen
        {
            get { return dashLen; }
            set { dashLen = value; }
        }
        string dashGap = string.Empty;

        public string DashGap
        {
            get { return dashGap; }
            set { dashGap = value; }
        }
        string valueinside = string.Empty;

        public string Valueinside
        {
            get { return valueinside; }
            set { valueinside = value; }
        }
    }

    public class annotations
    {
        string type = string.Empty;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        string x = string.Empty;

        public string X
        {
            get { return x; }
            set { x = value; }
        }
        string y = string.Empty;

        public string Y
        {
            get { return y; }
            set { y = value; }
        }
        string toX = string.Empty;

        public string ToX
        {
            get { return toX; }
            set { toX = value; }
        }
        string toY = string.Empty;

        public string ToY
        {
            get { return toY; }
            set { toY = value; }
        }
        string radius = string.Empty;

        public string Radius
        {
            get { return radius; }
            set { radius = value; }
        }
        string color = string.Empty;

        public string Color
        {
            get { return color; }
            set { color = value; }
        }
        string showBorder = string.Empty;

        public string ShowBorder
        {
            get { return showBorder; }
            set { showBorder = value; }
        }
    }


    public class AngularGaugeChart
    {
        string caption = string.Empty;

        public string Caption
        {
            get { return caption; }
            set { caption = value; }
        }
        string bgAlpha = string.Empty;

        public string BgAlpha
        {
            get { return bgAlpha; }
            set { bgAlpha = value; }
        }
        string bgColor = string.Empty;

        public string BgColor
        {
            get { return bgColor; }
            set { bgColor = value; }
        }
        string lowerLimit = string.Empty;

        public string LowerLimit
        {
            get { return lowerLimit; }
            set { lowerLimit = value; }
        }
        string upperLimit = string.Empty;

        public string UpperLimit
        {
            get { return upperLimit; }
            set { upperLimit = value; }
        }
        string numberSuffix = string.Empty;

        public string NumberSuffix
        {
            get { return numberSuffix; }
            set { numberSuffix = value; }
        }
        string showBorder = string.Empty;

        public string ShowBorder
        {
            get { return showBorder; }
            set { showBorder = value; }
        }
        string basefontColor = string.Empty;

        public string BasefontColor
        {
            get { return basefontColor; }
            set { basefontColor = value; }
        }
        string chartTopMargin = string.Empty;

        public string ChartTopMargin
        {
            get { return chartTopMargin; }
            set { chartTopMargin = value; }
        }
        string chartBottomMargin = string.Empty;

        public string ChartBottomMargin
        {
            get { return chartBottomMargin; }
            set { chartBottomMargin = value; }
        }
        string chartLeftMargin = string.Empty;

        public string ChartLeftMargin
        {
            get { return chartLeftMargin; }
            set { chartLeftMargin = value; }
        }
        string chartRightMargin = string.Empty;

        public string ChartRightMargin
        {
            get { return chartRightMargin; }
            set { chartRightMargin = value; }
        }
        string toolTipBgColor = string.Empty;

        public string ToolTipBgColor
        {
            get { return toolTipBgColor; }
            set { toolTipBgColor = value; }
        }
        string gaugeFillMix = string.Empty;

        public string GaugeFillMix
        {
            get { return gaugeFillMix; }
            set { gaugeFillMix = value; }
        }
        string gaugeFillRatio = string.Empty;

        public string GaugeFillRatio
        {
            get { return gaugeFillRatio; }
            set { gaugeFillRatio = value; }
        }
    }
}
