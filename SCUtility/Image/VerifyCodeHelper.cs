using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
namespace SCUtility
{
    /// <summary>
    /// 验证码类
    /// </summary>
    public class VerifyCodeHelper : Page
    {
        private bool _chaos = true;

        #region 验证码长度(默认6个验证码的长度)

        /// <summary>
        /// 验证码长度(默认6个验证码的长度) 
        /// </summary>
        public int Length { get; set; }

        #endregion

        #region 验证码字体大小(为了显示扭曲效果，默认40像素，可以自行修改)

        /// <summary>
        /// 验证码字体大小(为了显示扭曲效果，默认40像素，可以自行修改)
        /// </summary>
        public int FontSize { get; set; }

        #endregion

        #region 边框补(默认1像素)

        /// <summary>
        /// 边框补(默认1像素)
        /// </summary>
        public int Padding { get; set; }

        #endregion

        #region 是否输出燥点(默认不输出)

        /// <summary>
        /// 是否输出燥点(默认不输出)
        /// </summary>
        public bool Chaos
        {
            get { return _chaos; }
            set { _chaos = value; }
        }

        #endregion

        #region 输出燥点的颜色(默认灰色)

        /// <summary>
        /// 输出燥点的颜色(默认灰色)
        /// </summary>
        private Color _chaoscolor = Color.LightGray;

        public Color ChaosColor
        {
            get { return _chaoscolor; }
            set { _chaoscolor = value; }
        }

        #endregion

        #region 自定义背景色(默认白色)

        /// <summary>
        /// 自定义背景色(默认白色)
        /// </summary>
        private Color _backgroundcolor = Color.White;

        public Color BackgroundColor
        {
            get { return _backgroundcolor; }
            set { _backgroundcolor = value; }
        }

        #endregion

        #region 自定义随机颜色数组

        /// <summary>
        /// 自定义随机颜色数组
        /// </summary>
        private Color[] _colors = {
                                      Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown,
                                      Color.DarkCyan, Color.Purple
                                  };

        public Color[] Colors
        {
            get { return _colors; }
            set { _colors = value; }
        }

        #endregion

        #region 自定义字体数组

        /// <summary>
        /// 自定义字体数组
        /// </summary>
        private string[] _fonts = {"Arial", "Georgia"};

        public string[] Fonts
        {
            get { return _fonts; }
            set { _fonts = value; }
        }

        #endregion

        #region 自定义随机码字符串序列(使用逗号分隔)

        /// <summary>
        /// 自定义随机码字符串序列(使用逗号分隔)
        /// </summary>
        private string _codeserial = "0,1,2,3,4,5,6,7,8,9";

        //,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";

        public string CodeSerial
        {
            get { return _codeserial; }
            set { _codeserial = value; }
        }

        #endregion

        #region 产生波形滤镜效果

        private const double Pi2 = 6.283185307179586476925286766559;

        /// <summary>
        /// 正弦曲线Wave扭曲图片
        /// </summary>
        /// <param name="srcBmp">图片路径</param>
        /// <param name="bXDir">如果扭曲则选择为True</param>
        /// <param name="dMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        /// <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>
        /// <returns></returns>
        public Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            var destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            // 将位图背景填充为白色
            Graphics graph = Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();
            double dBaseAxisLen = bXDir ? destBmp.Height : destBmp.Width;
            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = bXDir ? (Pi2*j)/dBaseAxisLen : (Pi2*i)/dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);
                    // 取得当前点的颜色
                    int nOldX = bXDir ? i + (int) (dy*dMultValue) : i;
                    int nOldY = bXDir ? j : j + (int) (dy*dMultValue);
                    Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                        && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }
            return destBmp;
        }

        #endregion

        #region 生成校验码图片

        /// <summary>
        /// 生成校验码图片
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Bitmap CreateImageCode(string code)
        {
            int fSize = FontSize;
            int fWidth = fSize + Padding;
            int imageWidth = (code.Length*fWidth) + 4 + Padding*2;
            int imageHeight = fSize*2 + Padding;
            var image = new Bitmap(imageWidth, imageHeight);
            Graphics g = Graphics.FromImage(image);
            g.Clear(BackgroundColor);
            var rand = new Random();
            //给背景添加随机生成的燥点
            if (Chaos)
            {
                var pen = new Pen(ChaosColor, 0);
                int c = Length*10;
                for (int i = 0; i < c; i++)
                {
                    int x = rand.Next(image.Width);
                    int y = rand.Next(image.Height);
                    g.DrawRectangle(pen, x, y, 1, 1);
                }
            }
            int n1 = (imageHeight - FontSize - Padding*2);
            int n2 = n1/4;
            int top1 = n2;
            int top2 = n2*2;
            //随机字体和颜色的验证码字符
            for (int i = 0; i < code.Length; i++)
            {
                int cindex = rand.Next(Colors.Length - 1);
                int findex = rand.Next(Fonts.Length - 1);
                var f = new Font(Fonts[findex], fSize, FontStyle.Bold);
                Brush b = new SolidBrush(Colors[cindex]);
                int top = i%2 == 1 ? top2 : top1;
                int left = i*fWidth;
                g.DrawString(code.Substring(i, 1), f, b, left, top);
            }
            //画一个边框 边框颜色为Color.Black
            // g.DrawRectangle(new Pen(Color.Black, 0), 0, 0, image.Width - 1, image.Height - 1);
            g.Dispose();
            //产生波形
            image = TwistImage(image, true, 3, 0);
            return image;
        }

        #endregion

        #region 将创建好的图片输出到页面

        /// <summary>
        /// 将创建好的图片输出到页面
        /// </summary>
        /// <param name="code"></param>
        /// <param name="context"></param>
        public void CreateImageOnPage(string code, HttpContext context)
        {
            var ms = new MemoryStream();
            Bitmap image = CreateImageCode(code);
            image.Save(ms, ImageFormat.Jpeg);
            context.Response.ClearContent();
            context.Response.ContentType = "image/Jpeg";
            context.Response.BinaryWrite(ms.GetBuffer());
            ms.Close();
            image.Dispose();
        }

        #endregion

        #region 生成随机字符码

        /// <summary>
        /// 生成随机字符码
        /// </summary>
        /// <param name="codeLen"></param>
        /// <returns></returns>
        public string CreateVerifyCode(int codeLen)
        {
            if (codeLen == 0)
            {
                codeLen = Length;
            }

            string[] arr = CodeSerial.Split(',');

            string code = "";

            var rand = new Random(unchecked((int) DateTime.Now.Ticks));

            for (int i = 0; i < codeLen; i++)
            {
                int randValue = rand.Next(0, arr.Length - 1);

                code += arr[randValue];
            }

            return code;
        }

        public string CreateVerifyCode()
        {
            return CreateVerifyCode(0);
        }

        #endregion

        #region 生成汉字字符

        /// <summary>
        /// 生成汉字字符
        /// </summary>
        /// <returns></returns>
        public char CreateZhChar()
        {
            //若提供了汉字集，查询汉字集选取汉字
            //if (ChineseChars.Length > 0)
            //{
            //    return ChineseChars[rnd.Next(0, ChineseChars.Length)];
            //}
            //若没有提供汉字集，则根据《GB2312简体中文编码表》编码规则构造汉字
            //else
            //{
            var rnd = new Random();
            var bytes = new byte[2];

            //第一个字节值在0xb0, 0xf7之间
            bytes[0] = (byte) rnd.Next(0xb0, 0xf8);
            //第二个字节值在0xa1, 0xfe之间
            bytes[1] = (byte) rnd.Next(0xa1, 0xff);

            //根据汉字编码的字节数组解码出中文汉字
            string str1 = Encoding.GetEncoding("gb2312").GetString(bytes);

            return str1[0];
            //}
        }

        #endregion
    }
}