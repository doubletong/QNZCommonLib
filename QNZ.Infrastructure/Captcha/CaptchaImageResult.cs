using System;
using System.Drawing.Imaging;
using System.Web.Mvc;

namespace SIG.Infrastructure.Captcha
{
    public class CaptchaImageResult : ActionResult
    {
        /// <summary>
        /// 生成随机码001
        /// </summary>
        /// <returns></returns>
        private string GenerateRandomCode()
        {
            var r = new Random();
            var s = "";
            for (var j = 0; j < 5; j++)
            {
                var i = r.Next(3);
                int ch;
                switch (i)
                {
                    case 1:
                        ch = r.Next(0, 9);
                        s += ch.ToString();
                        break;
                    case 2:
                        ch = r.Next(65, 90);
                        s += Convert.ToChar(ch).ToString();
                        break;
                    case 3:
                        ch = r.Next(97, 122);
                        s += Convert.ToChar(ch).ToString();
                        break;
                    default:
                        ch = r.Next(97, 122);
                        s += Convert.ToChar(ch).ToString();
                        break;
                }

                var nextDouble = r.NextDouble();
                var next = r.Next(100, 1999);
            }
            return s;
        }
        /// <summary>
        /// 生成随机码002
        /// </summary>
        /// <returns></returns>
        public string GetCaptchaString(int length)
        {
            const int intZero = '1';
            const int intNine = '9';
            //int intA = 'A';
            //int intZ = 'Z';
            var intCount = 0;
            var strCaptchaString = "";

            var random = new Random(System.DateTime.Now.Millisecond);

            while (intCount < length)
            {
                var intRandomNumber = random.Next(intZero, intNine);
                if (((intRandomNumber < intZero) || (intRandomNumber > intNine))) continue;
                strCaptchaString += (char)intRandomNumber;
                intCount += 1;
            }
            return strCaptchaString;
        }

        public override void ExecuteResult(ControllerContext context)
        {

          
            var randomString = GetCaptchaString(4);
            context.HttpContext.Session["SigCaptcha"] = randomString;     
         
             var ci = new RandomImage(context.HttpContext.Session["SigCaptcha"].ToString(),120, 38);          

             var response = context.HttpContext.Response;
            response.ContentType = "image/jpeg";
            ci.Image.Save(response.OutputStream, ImageFormat.Jpeg);
            ci.Dispose();

        }

    }
}