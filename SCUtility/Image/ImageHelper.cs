using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace SCUtility
{
    /// <summary>
    /// 图片帮助类
    /// </summary>
    public class ImageHelper
    {
        /// <summary>
        /// 转化到png
        /// </summary>
        /// <param name="png"></param>
        /// <param name="file"></param>
        public static void AttachPng(string png, string file)
        {
            if ((!string.IsNullOrEmpty(png) && File.Exists(png)) && File.Exists(file))
            {
                var oFile = new FileInfo(file);
                string strTempFile = Path.Combine(oFile.DirectoryName, Guid.NewGuid() + oFile.Extension);
                oFile.CopyTo(strTempFile);
                Image img = Image.FromFile(strTempFile);
                ImageFormat thisFormat = img.RawFormat;
                int nHeight = img.Height;
                int nWidth = img.Width;
                var outBmp = new Bitmap(nWidth, nHeight);
                Graphics g = Graphics.FromImage(outBmp);
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, new Rectangle(0, 0, nWidth, nHeight), 0, 0, nWidth, nHeight, GraphicsUnit.Pixel);
                img.Dispose();
                img = Image.FromFile(png);
                Size pngSize = NewSize(nWidth, nHeight, img);
                g.DrawImage(img,
                            new Rectangle((nWidth - pngSize.Width) / 2, (nHeight - pngSize.Height) / 2, pngSize.Width,
                                          pngSize.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
                g.Dispose();
                var encoderParams = new EncoderParameters();
                var quality = new[] { 100L };
                var encoderParam = new EncoderParameter(Encoder.Quality, quality);
                encoderParams.Param[0] = encoderParam;
                ImageCodecInfo[] arrayIci = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegIci = arrayIci.FirstOrDefault(t => t.FormatDescription.Equals("JPEG"));
                if (jpegIci != null)
                {
                    outBmp.Save(file, jpegIci, encoderParams);
                }
                else
                {
                    outBmp.Save(file, thisFormat);
                }
                img.Dispose();
                outBmp.Dispose();
                File.Delete(strTempFile);
            }
        }

        public static void AttachText(string text, string file)
        {
            if (!string.IsNullOrEmpty(text) && File.Exists(file))
            {
                var oFile = new FileInfo(file);
                string strTempFile = Path.Combine(oFile.DirectoryName, Guid.NewGuid() + oFile.Extension);
                oFile.CopyTo(strTempFile);
                Image img = Image.FromFile(strTempFile);
                ImageFormat thisFormat = img.RawFormat;
                int nHeight = img.Height;
                int nWidth = img.Width;
                var outBmp = new Bitmap(nWidth, nHeight);
                Graphics g = Graphics.FromImage(outBmp);
                g.Clear(Color.White);
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, new Rectangle(0, 0, nWidth, nHeight), 0, 0, nWidth, nHeight, GraphicsUnit.Pixel);
                var sizes = new[] { 0x10, 14, 12, 10, 8, 6, 4 };
                Font crFont = null;
                var crSize = new SizeF();
                for (int i = 0; i < 7; i++)
                {
                    crFont = new Font("arial", sizes[i], FontStyle.Bold);
                    crSize = g.MeasureString(text, crFont);
                    if (((ushort)crSize.Width) < ((ushort)nWidth))
                    {
                        break;
                    }
                }
                var yPixlesFromBottom = (int)(nHeight * 0.08);
                float yPosFromBottom = (nHeight - yPixlesFromBottom) - (crSize.Height / 2f);
                float xCenterOfImg = nWidth / 2;
                var strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Center;
                var semiTransBrush2 = new SolidBrush(Color.FromArgb(0x99, 0, 0, 0));
                g.DrawString(text, crFont, semiTransBrush2, new PointF(xCenterOfImg + 1f, yPosFromBottom + 1f),
                             strFormat);
                var semiTransBrush = new SolidBrush(Color.FromArgb(0x99, 0xff, 0xff, 0xff));
                g.DrawString(text, crFont, semiTransBrush, new PointF(xCenterOfImg, yPosFromBottom), strFormat);
                g.Dispose();
                var encoderParams = new EncoderParameters();
                var quality = new[] { 100L };
                var encoderParam = new EncoderParameter(Encoder.Quality, quality);
                encoderParams.Param[0] = encoderParam;
                ImageCodecInfo[] arrayIci = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegIci = arrayIci.FirstOrDefault(t => t.FormatDescription.Equals("JPEG"));
                if (jpegIci != null)
                {
                    try
                    {
                        oFile.Delete();
                        outBmp.Save(file, jpegIci, encoderParams);
                    }
                    catch (Exception oE)
                    {
                        string str = oE.Message;
                    }
                }
                else
                {
                    outBmp.Save(file, thisFormat);
                }
                img.Dispose();
                outBmp.Dispose();
                File.Delete(strTempFile);
            }
        }

        public static void CompressPhoto(string FileName, int size)
        {
            if (File.Exists(FileName))
            {
                int nCount = 0;
                var oFile = new FileInfo(FileName);
                for (long nLen = oFile.Length; (nLen > (size * 0x400)) && (nCount < 10); nLen = oFile.Length)
                {
                    string TempFile = Path.Combine(oFile.Directory.FullName, Guid.NewGuid() + "." + oFile.Extension);
                    oFile.CopyTo(TempFile, true);
                    KiSaveAsJPEG(TempFile, FileName, 70);
                    try
                    {
                        File.Delete(TempFile);
                    }
                    catch
                    {
                    }
                    nCount++;
                    oFile = new FileInfo(FileName);
                }
            }
        }

        public static void CreateSmallPhoto(string filename, int nMaxWidth)
        {
            Image img = Image.FromFile(filename);
            if (img.Width <= nMaxWidth)
            {
                img.Dispose();
            }
            else
            {
                var nMaxHeight = (int)Math.Ceiling((((img.Height * nMaxWidth)) / ((double)img.Width)));
                img.Dispose();
                var oFile = new FileInfo(filename);
                string TempFile = Path.Combine(oFile.Directory.FullName, Guid.NewGuid() + oFile.Extension);
                CreateSmallPhoto(filename, nMaxWidth, nMaxHeight, TempFile, false);
                File.Copy(TempFile, filename, true);
                File.Delete(TempFile);
            }
        }

        public static void CreateSmallPhoto(string filename, int nWidth, int nHeight, string destfile)
        {
            CreateSmallPhoto(filename, nWidth, nHeight, destfile, true);
        }

        public static void CreateSmallPhoto(string filename, int nWidth, int nHeight, string destfile, bool cut)
        {
            Image img = Image.FromFile(filename);
            ImageFormat thisFormat = img.RawFormat;
            Size CutSize = CutRegion(nWidth, nHeight, img);
            if (!cut)
            {
                CutSize = NewSize(nWidth, nHeight, img);
            }
            var outBmp = new Bitmap(CutSize.Width, CutSize.Height);
            Graphics g = Graphics.FromImage(outBmp);
            new ImageAttributes().SetColorKey(Color.White, Color.White);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.Clear(Color.White);
            int nStartX = (img.Width - CutSize.Width) / 2;
            int nStartY = (img.Height - CutSize.Height) / 2;
            g.DrawImage(img, new Rectangle(0, 0, CutSize.Width, CutSize.Height), 0, 0, img.Width, img.Height,
                        GraphicsUnit.Pixel);
            g.Dispose();
            var encoderParams = new EncoderParameters();
            var quality = new[] { 100L };
            var encoderParam = new EncoderParameter(Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;
            ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo jpegICI = arrayICI.FirstOrDefault(t => t.FormatDescription.Equals("JPEG"));
            if (jpegICI != null)
            {
                outBmp.Save(destfile, jpegICI, encoderParams);
            }
            else
            {
                outBmp.Save(destfile, thisFormat);
            }
            img.Dispose();
            outBmp.Dispose();
        }

        public static void CreateSmallPhoto(string filename, int nWidth, int nHeight, string destfile, string sy,
                                            int nType)
        {
            if (nType == 0)
            {
                CreateSmallPhoto(filename, nWidth, nHeight, destfile, sy, "", true);
            }
            else
            {
                CreateSmallPhoto(filename, nWidth, nHeight, destfile, "", sy, true);
            }
        }

        public static void CreateSmallPhoto(string filename, int nWidth, int nHeight, string destfile, string png,
                                            string text)
        {
            CreateSmallPhoto(filename, nWidth, nHeight, destfile, png, text, true);
        }

        public static void CreateSmallPhoto(string filename, int nWidth, int nHeight, string destfile, string sy,
                                            int nType, bool cut)
        {
            if (nType == 0)
            {
                CreateSmallPhoto(filename, nWidth, nHeight, destfile, sy, "", cut);
            }
            else
            {
                CreateSmallPhoto(filename, nWidth, nHeight, destfile, "", sy, cut);
            }
        }

        public static void CreateSmallPhoto(string filename, int nWidth, int nHeight, string destfile, string png,
                                            string text, bool cut)
        {
            Image img = Image.FromFile(filename);
            ImageFormat thisFormat = img.RawFormat;
            Size CutSize = CutRegion(nWidth, nHeight, img);
            if (!cut)
            {
                CutSize = NewSize(nWidth, nHeight, img);
            }
            var outBmp = new Bitmap(nWidth, nHeight);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.White);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            int nStartX = (img.Width - CutSize.Width) / 2;
            int nStartY = (img.Height - CutSize.Height) / 2;
            g.DrawImage(img, new Rectangle(0, 0, nWidth, nHeight), nStartX, nStartY, CutSize.Width, CutSize.Height,
                        GraphicsUnit.Pixel);
            g.Dispose();
            var encoderParams = new EncoderParameters();
            var quality = new[] { 100L };
            var encoderParam = new EncoderParameter(Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;
            ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo jpegICI = arrayICI.FirstOrDefault(t => t.FormatDescription.Equals("JPEG"));
            if (jpegICI != null)
            {
                outBmp.Save(destfile, jpegICI, encoderParams);
            }
            else
            {
                outBmp.Save(destfile, thisFormat);
            }
            img.Dispose();
            outBmp.Dispose();
            if (!string.IsNullOrEmpty(png))
            {
                AttachPng(png, destfile);
            }
            if (!string.IsNullOrEmpty(text))
            {
                AttachText(text, destfile);
            }
        }

        public static Size CutRegion(int nWidth, int nHeight, Image img)
        {
            double width = 0.0;
            double height = 0.0;
            double nw = nWidth;
            double nh = nHeight;
            double pw = img.Width;
            double ph = img.Height;
            if ((nw / nh) > (pw / ph))
            {
                width = pw;
                height = (pw * nh) / nw;
            }
            else if ((nw / nh) < (pw / ph))
            {
                width = (ph * nw) / nh;
                height = ph;
            }
            else
            {
                width = pw;
                height = ph;
            }
            return new Size(Convert.ToInt32(width), Convert.ToInt32(height));
        }

        private static ImageCodecInfo GetCodecInfo(string mimeType)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in CodecInfo)
            {
                if (ici.MimeType == mimeType)
                {
                    return ici;
                }
            }
            return null;
        }

        public static bool KiSaveAsJPEG(string SourceFile, string FileName, int Qty)
        {
            var bmp = new Bitmap(SourceFile);
            try
            {
                var ps = new EncoderParameters(1);
                var p = new EncoderParameter(Encoder.Quality, Qty);
                ps.Param[0] = p;
                bmp.Save(FileName, GetCodecInfo("image/jpeg"), ps);
                bmp.Dispose();
                return true;
            }
            catch
            {
                bmp.Dispose();
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public static Size NewSize(int nWidth, int nHeight, Image img)
        {
            double w = 0.0;
            double h = 0.0;
            double sw = Convert.ToDouble(img.Width);
            double sh = Convert.ToDouble(img.Height);
            double mw = Convert.ToDouble(nWidth);
            double mh = Convert.ToDouble(nHeight);
            if ((sw < mw) && (sh < mh))
            {
                w = sw;
                h = sh;
            }
            else if ((sw / sh) > (mw / mh))
            {
                w = nWidth;
                h = (w * sh) / sw;
            }
            else
            {
                h = nHeight;
                w = (h * sw) / sh;
            }
            return new Size(Convert.ToInt32(w), Convert.ToInt32(h));
        }
    }
}