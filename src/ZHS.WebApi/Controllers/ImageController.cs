using ImageSharp;
using ImageSharp.Drawing;
using ImageSharp.Drawing.Brushes;
using ImageSharp.Drawing.Pens;
using Microsoft.AspNetCore.Mvc;
using SixLabors.Fonts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace ZHS.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ImageController : Controller
    {

        /// <summary>  
        /// 该方法用于生成指定位数的随机数  
        /// </summary>  
        /// <param name="VcodeNum">参数是随机数的位数</param>  
        /// <returns>返回一个随机数字符串</returns>  
        private string RndNum(int VcodeNum)
        {
            //验证码可以显示的字符集合  
            string Vchar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,p" +
                ",q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,P,P,Q" +
                ",R,S,T,U,V,W,X,Y,Z";
            string[] VcArray = Vchar.Split(new Char[] { ',' });//拆分成数组   
            string code = "";//产生的随机数  
            int temp = -1;//记录上次随机数值，尽量避避免生产几个一样的随机数  

            Random rand = new Random();
            //采用一个简单的算法以保证生成随机数的不同  
            for (int i = 1; i < VcodeNum + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));//初始化随机类  
                }
                int t = rand.Next(61);//获取随机数  
                if (temp != -1 && temp == t)
                {
                    return RndNum(VcodeNum);//如果获取的随机数重复，则递归调用  
                }
                temp = t;//把本次产生的随机数记录起来  
                code += VcArray[t];//随机数的位数加一  
            }
            return code;
        }

        /// <summary>  
        /// 该方法是将生成的随机数写入图像文件  
        /// </summary>  
        /// <param name="code">code是一个随机数</param>
        /// <param name="numbers">生成位数（默认4位）</param>  
        [HttpGet]
        public FileStream IdentifyingCode([FromQuery]int numbers = 4)
        {
            var tempTTFName = $"{DateTime.Now.Ticks}.ttf";
            System.IO.File.Copy("hyqh.ttf", tempTTFName);
            var tempCodeName = $"{DateTime.Now.Ticks}.jpg";
            var code = RndNum(numbers);
            Random random = new Random();
            //颜色列表，用于验证码、噪线、噪点 
            Color[] color = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
            //创建画布
            var imageWidth = code.Length * 18;
            using (Image image = new Image(imageWidth, 32))
            using (var pixels = image.Lock())
            using (FileStream output = System.IO.File.OpenWrite(tempCodeName))
            {
                //背景设为白色  
                pixels[imageWidth, 32] = Color.White;
              
                //向画板中绘制贝塞尔样条  
                for (int i = 0; i < 2; i++)
                {
                    var p1 = new Vector2(0, random.Next(image.Height));
                    var p2 = new Vector2(random.Next(image.Width), random.Next(image.Height));
                    var p3 = new Vector2(random.Next(image.Width), random.Next(image.Height));
                    var p4 = new Vector2(image.Width, random.Next(image.Height));
                    Vector2[] p = { p1, p2, p3, p4 };
                    Color clr = color[random.Next(color.Length)];
                    Pen pen = new Pen(clr, 1);
                    image.DrawBeziers(pen, p);
                }
                //画噪点
                for (int i = 0; i < 50; i++)
                {
                    GraphicsOptions noneDefault = new GraphicsOptions();
                    ImageSharp.Rectangle rectangle = new ImageSharp.Rectangle(random.Next(image.Width), random.Next(image.Height), 1, 1);
                    image.Draw(Color.Gray, 1f, rectangle, noneDefault);
                }
                //画验证码字符串 
                for (int i = 0; i < code.Length; i++)
                {
                    int cindex = random.Next(7);//随机颜色索引值  
                    int findex = random.Next(5);//随机字体索引值  
                    var fontCollection = new FontCollection();
                    var fontTemple = fontCollection.Install(tempTTFName);
                    var font = new Font(fontTemple, 16);
                    var brush = new SolidBrush(color[cindex]);//颜色  
                    //var textColor = color[cindex];//颜色  
                    int ii = 4;
                    if ((i + 1) % 2 == 0)//控制验证码不在同一高度  
                    {
                        ii = 2;
                    }
                    image.DrawText(code.Substring(i, 1), font, brush, new System.Numerics.Vector2(3 + (i * 12), ii));//绘制一个验证字符  
                    
                }
                image.Save(output);
            }
            if (System.IO.File.Exists(tempTTFName))
            {
                System.IO.File.Delete(tempTTFName);
            }
            return System.IO.File.OpenRead(tempCodeName);

        }
        /// <summary>
        /// 获取个人的二维码海报
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public FileStream QrcodeImg()
        {
            var tempName = DateTime.Now.Ticks.ToString();
            var tempTemple = $"{tempName}-qrtemple.jpg";
            var tempTTFName = $"{tempName}.ttf";
            var tempMyrcodeName = $"{tempName}-myqrcode.jpg";
            System.IO.File.Copy("qrtemple.jpg", tempTemple);
            System.IO.File.Copy("hyqh.ttf", tempTTFName);
            System.IO.File.Copy("qrcode.jpg", tempMyrcodeName);
            var qrcodeName = $"{tempName}-qrcode.jpg";
            using (FileStream streamTemple = System.IO.File.OpenRead(tempTemple))
            using (FileStream streamQrcode = System.IO.File.OpenRead(tempMyrcodeName))
            using (FileStream output = System.IO.File.OpenWrite(qrcodeName))
            {
                var imageTemple = new ImageSharp.Image(streamTemple);
                var imageQrcode = new ImageSharp.Image(streamQrcode);
                //imageFoo.Blend(imageBar, 100);
                imageTemple.DrawImage(imageQrcode, 100, new ImageSharp.Size(imageQrcode.Width, imageQrcode.Height), new ImageSharp.Point(imageTemple.Width / 3, imageTemple.Height / 2));
                var fontCollection = new FontCollection();
                var font = fontCollection.Install(tempTTFName);
                imageTemple.DrawText($"生成日期{DateTime.Now.ToString("yyyy.MM.dd")}", new SixLabors.Fonts.Font(font, imageTemple.Width / 40, FontStyle.Regular), new ImageSharp.Color(0, 0, 0), new System.Numerics.Vector2(imageTemple.Width* 1/3, imageTemple.Height*9/10));
                imageTemple.Save(output);
            }
            if (System.IO.File.Exists(tempTemple))
            {
                System.IO.File.Delete(tempTemple);
            }
            if (System.IO.File.Exists(tempTTFName))
            {
                System.IO.File.Delete(tempTTFName);
            }
            if (System.IO.File.Exists(tempMyrcodeName))
            {
                System.IO.File.Delete(tempMyrcodeName);
            }
            return System.IO.File.OpenRead(qrcodeName);
        }
    }
}
