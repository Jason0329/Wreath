using Nop.Web.Framework.Controllers;
using Nop.X0_Widget.Widget.FacebookGame.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Nop.X0_Widget.Widget.FacebookGame.Controllers
{
    public class GamesController : BaseController
    {
        public ActionResult FacebookGame()
        {
            return View();
        }

        public ActionResult FacebookGame1()
        {
            HttpContext.Response.ContentType = "text/plain";
            JavaScriptSerializer searial = new JavaScriptSerializer();
            try
            {
                string strJson = new StreamReader(HttpContext.Request.InputStream).ReadToEnd();

                //deserialize the object
                FacebookDataTest objUsr = searial.Deserialize<FacebookDataTest>(strJson);
                //if (objUsr != null)
                //{
                //    string fullName = objUsr.first_name + " " + objUsr.last_name;
                //    string age = objUsr.age;
                //    string qua = objUsr.qualification;
                //    context.Response.Write(string.Format("Name :{0} , Age={1}, Qualification={2}", fullName, age, qua));
                //}
                //else
                //{
                //    context.Response.Write("No Data");
                //}
            }
            catch (Exception ex)
            {
                HttpContext.Response.Write("Error :" + ex.Message);
            }

            return null;
        }

    }
}
