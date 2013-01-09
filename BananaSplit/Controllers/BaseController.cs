using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace BananaSplit.Controllers
{
    public class BaseController : Controller
    {
        protected ContentResult ToJson<T>(T entity) where T : class
        {
            return new ContentResult
            {
                ContentType = "application/json",
                Content = JsonConvert.SerializeObject(entity),
                ContentEncoding = new UTF8Encoding(false)
            };
        }


        protected ContentResult ToXml<T>(T entity) where T : class
        {
            var xml = new XmlSerializer(typeof(T));
            var utF8 = new UTF8Encoding();

            using (var memoryStream = new MemoryStream(500))
            {
                using (var xmlWriter = XmlTextWriter.Create(memoryStream,
                    new XmlWriterSettings()
                    {
                        OmitXmlDeclaration = true,
                        Encoding = utF8,
                        Indent = true
                    }))
                {
                    var serializer = new XmlSerializer(typeof(T), new[] { typeof(object) });
                    serializer.Serialize(xmlWriter, entity);
                }

                return new ContentResult
                {
                    ContentType = "text/xml",
                    Content = utF8.GetString(memoryStream.ToArray()),
                    ContentEncoding = new UTF8Encoding(false)
                };
            }

        }

        /*
        protected Member GetSessionMember()
        {
            Member member = null;
            try
            {
                member = HttpContext.Session["authUser"] as Member;
                if (null == member)
                {
                    throw new Exception("Session end");
                }
            }
            catch (Exception e)
            {
                member = new Member();
                member.Username = "Anonymous";
                member.IsActive = false;
                member.NickName = "Anonymous";
                member.FirstName = "Unknown";
                member.LastName = "User";

            }
            return member;
        }
        */


        protected T FromJson<T>(string input) where T : class
        {
            return JsonConvert.DeserializeObject<T>(input);
        }

    }
}
