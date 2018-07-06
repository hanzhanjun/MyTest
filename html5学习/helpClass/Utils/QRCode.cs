using System;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using ThoughtWorks.QRCode.Codec;

namespace helpClass.Utils
{
    public class QRCode
    {
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="data">二维码图片存储内容</param>
        /// <param name="pictureName">二维码图片名称</param>
        /// <returns></returns>
        public static string MakeQRCode(string data, string pictureName)
        {
            if (File.Exists(HttpContext.Current.Server.MapPath("../Images/QRCode/" + pictureName + ".png")))
            {
                return ("../Images/QRCode/" + pictureName + ".png");
            }
            string fileName = string.Empty;
            string filePath = string.Empty;
            if (!string.IsNullOrEmpty(data))
            {
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeScale = 4;
                qrCodeEncoder.QRCodeVersion = 8;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                System.Drawing.Image image = ChangeSize(qrCodeEncoder.Encode(data));
                System.IO.MemoryStream MStream = new System.IO.MemoryStream();
                image.Save(MStream, System.Drawing.Imaging.ImageFormat.Png);

                //代理商二维码中心图片logo
                string logo = "../Images/logo.png";
                //模板所在相对路径
                filePath = "../Images/QRCode/";
                string phyPath = HttpContext.Current.Server.MapPath(filePath);
                //判断路径是否存在,若不存在则创建路径
                DirectoryInfo directoryInfo = new DirectoryInfo(phyPath);
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }
                if (directoryInfo.Exists)
                {
                    fileName = phyPath + pictureName + ".png";
                    CombinImage(image, HttpContext.Current.Server.MapPath(logo)).Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                    filePath = filePath + pictureName + ".png";
                }
                //image.Dispose();  
                MStream.Dispose();
            }
            return filePath;
        }

        public static Image ChangeSize(System.Drawing.Image image)
        {
            int size = 200;
            System.Drawing.Bitmap panel = new System.Drawing.Bitmap(size, size);
            System.Drawing.Graphics graphic0 = System.Drawing.Graphics.FromImage(panel);
            int p_left = 0;
            int p_top = 0;
            if (image.Width <= size) //如果原图比目标形状宽
            {
                p_left = (size - image.Width) / 2;
            }
            if (image.Height <= size)
            {
                p_top = (size - image.Height) / 2;
            }

            //将生成的二维码图像粘贴至绘图的中心位置
            graphic0.DrawImage(image, p_left, p_top, image.Width, image.Height);
            image = new System.Drawing.Bitmap(panel);
            panel.Dispose();
            panel = null;
            graphic0.Dispose();
            graphic0 = null;
            return image;
        }

        public static Image CombinImage(Image imgBack, string destImg)
        {
            //Image img = Image.FromFile(destImg);        //照片图片      
            //if (img.Height != 65 || img.Width != 65)
            //{
            //    img = KiResizeImage(img, 65, 65, 0);}
            //Graphics g = Graphics.FromImage(imgBack);

            //g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);     

            ////g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框    

            ////g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);    

            //g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
            //GC.Collect();
            return imgBack;
        }

        /// <summary>    
        /// Resize图片    
        /// </summary>    
        /// <param name="bmp">原始Bitmap</param>    
        /// <param name="newW">新的宽度</param>    
        /// <param name="newH">新的高度</param>    
        /// <param name="Mode">保留着，暂时未用</param>    
        /// <returns>处理以后的图片</returns>    
        public static Image KiResizeImage(Image bmp, int newW, int newH, int Mode)
        {
            try
            {
                Image b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量    
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }
    }
}