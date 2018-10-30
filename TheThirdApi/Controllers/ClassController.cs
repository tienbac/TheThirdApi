using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TheThirdApi.Controllers
{
    public class ClassController : ApiController
    {
        [HttpGet]
        public List<Class> GetAllCalsses()
        {
            CSDLAptechStudentsDataContext context = new CSDLAptechStudentsDataContext();
            List<Class> list = context.Classes.ToList();
            foreach (var c in list)
            {
                c.Students.Clear();
            }

            return list;
        }

        [HttpGet]
        public Class GetClassById(string id)
        {
            CSDLAptechStudentsDataContext context = new CSDLAptechStudentsDataContext();
            Class cClass = context.Classes.FirstOrDefault(c => c.ClassId == id);
            if (cClass != null)
            {
                cClass.Students.Clear();
            }

            return cClass;
        }

        [HttpGet]
        public List<Class> GetClassFromTo(string ids, string ide)
        {
            CSDLAptechStudentsDataContext context = new CSDLAptechStudentsDataContext();
            List<Class> list = context.Classes.Where(c => c.ClassId == ids && c.ClassId == ide).ToList();
            foreach (var c in list)
            {
                c.Students.Clear();
            }

            return list;
        }

        [HttpPost]
        public bool CreateClass(string id, string name, string rollNumber, int status)
        {
            try
            {
                CSDLAptechStudentsDataContext context = new CSDLAptechStudentsDataContext();
                Class cClass = new Class
                {
                    ClassId = id,
                    ClassName = name,
                    ClassRollNumber = rollNumber,
                    ClassStatus = status,
                    CreatedAt = DateTime.Now.Ticks
                };
                context.Classes.InsertOnSubmit(cClass);
                context.SubmitChanges();
                return true;

            }
            catch (Exception e)
            {
                
            }
            return false;
        }

        [HttpPut]
        public bool UpdateClass(string id, string name, string rollNumber, int status)
        {
            try
            {
                CSDLAptechStudentsDataContext context = new CSDLAptechStudentsDataContext();
                Class cClass = context.Classes.FirstOrDefault(c => c.ClassId == id);
                if (cClass != null)
                {
                    cClass.ClassName = name;
                    cClass.ClassRollNumber = rollNumber;
                    cClass.ClassStatus = status;
                    cClass.UpdatedAt = DateTime.Now.Ticks;

                    context.SubmitChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                
            }

            return false;
        }

        [HttpDelete]
        public bool DeleteClass(string id)
        {
            CSDLAptechStudentsDataContext context = new CSDLAptechStudentsDataContext();
            Class cClass = context.Classes.FirstOrDefault(c => c.ClassId == id);
            if (cClass != null)
            {
                context.Classes.DeleteOnSubmit(cClass);
                context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}
