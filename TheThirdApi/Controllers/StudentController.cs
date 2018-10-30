using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace TheThirdApi.Controllers
{
    public class StudentController : ApiController
    {
        [HttpGet]
        public List<Student> GetAllStudents()
        {
            CSDLAptechStudentsDataContext context = new CSDLAptechStudentsDataContext();
            List<Student> list = context.Students.ToList();
            // Khử đệ quy : xóa classId khỏi từng thằng student.
            foreach (var student in list)
            {
                student.ClassId = null;
            }

            return list;
        }

        [HttpGet]
        public Student GetStudentById(int id)
        {
            CSDLAptechStudentsDataContext context = new CSDLAptechStudentsDataContext();
            Student student = context.Students.FirstOrDefault(stu => stu.StudentId == id);
            if (student != null)
            {
                student.ClassId = null;
            }

            return student;
        }

        [HttpGet]
        public List<Student> GetStudentsFromTo(int id1, int id2)
        {
            CSDLAptechStudentsDataContext context = new CSDLAptechStudentsDataContext();
            List<Student> list = context.Students.Where(stu => stu.StudentId >= id1 && stu.StudentId <= id2).ToList();
            foreach (var student in list)
            {
                student.ClassId = null;
            }

            return list;
        }

        [HttpGet]
        public List<Student> GetStudentsByClassId(string cId)
        {
            CSDLAptechStudentsDataContext context = new CSDLAptechStudentsDataContext();
            List<Student> list = context.Students.Where(stu => stu.ClassId == cId).ToList();
            foreach (var student in list)
            {
                student.ClassId = null;
            }

            return list;
        }

        [HttpPost]
        public bool CreateStudent(int id, string name, string rollNumber, string email, string phone, string address, string cid, int status)
        {
            try
            {
                CSDLAptechStudentsDataContext context = new CSDLAptechStudentsDataContext();
                Student student = new Student
                {
                    StudentId = id,
                    StudentName = name,
                    StudentRollNumber = rollNumber,
                    StudentEmail = email,
                    StudentPhone = phone,
                    StudentAddress = address,
                    ClassId = cid,
                    StudentStatus = status,
                    CreatedAt = DateTime.Now.Ticks,
                    UpdatedAt = 0

                };
                context.Students.InsertOnSubmit(student);
                context.SubmitChanges();
                return true;

            }
            catch (Exception e)
            {
               
            }
            return false;
        }

        [HttpPost]
        public bool CreateAStudent(string json)
        {
            try
            {
                var student = JsonConvert.DeserializeObject<Student>(json);
                CSDLAptechStudentsDataContext context = new CSDLAptechStudentsDataContext();
				
                context.Students.InsertOnSubmit(student);
                context.SubmitChanges();

                return true;
            }
            catch (Exception e)
            {
                
            }

            return  false;
        }

        [HttpPut]
        public bool UpdateStudent(int id, string name, string rollNumber, string email, string phone, string address, string cid, int status)
        {
            try
            {
                CSDLAptechStudentsDataContext context = new CSDLAptechStudentsDataContext();
                Student student = context.Students.FirstOrDefault(stu => stu.StudentId == id);
                if (student != null)
                {
                    student.StudentName = name;
                    student.StudentRollNumber = rollNumber;
                    student.StudentEmail = email;
                    student.StudentPhone = phone;
                    student.StudentAddress = address;
                    student.ClassId = cid;
                    student.StudentStatus = status;

                    student.UpdatedAt = DateTime.Now.Ticks;

                    context.SubmitChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return false;
        }

        [HttpPut]
        public bool UpdateAStudent(Student student)
        {
            try
            {
                CSDLAptechStudentsDataContext context = new CSDLAptechStudentsDataContext();
                Student stu = context.Students.FirstOrDefault(s => s.StudentId == student.StudentId);
                if (stu != null)
                {
                    stu.StudentId = student.StudentId;
                    stu.ClassId = student.ClassId;
                    stu.StudentName = student.StudentName;
                    stu.StudentAddress = student.StudentAddress;
                    stu.StudentEmail = stu.StudentEmail;
                    stu.StudentPhone = student.StudentPhone;
                    stu.StudentRollNumber = student.StudentRollNumber;
                    stu.CreatedAt = student.CreatedAt;
                    stu.UpdatedAt = student.UpdatedAt;
                    stu.StudentStatus = student.StudentStatus;
                    context.SubmitChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                
            }

            return false;
        }

        //[HttpPatch]
        //public bool UpdateStudent1(int id, )
        //{
        //    return false;
        //}

        [HttpDelete]
        public bool DeleteAStudent(int id)
        {
            CSDLAptechStudentsDataContext context = new CSDLAptechStudentsDataContext();
            Student student = context.Students.FirstOrDefault(stu => stu.StudentId == id);
            if (student != null)
            {
                context.Students.DeleteOnSubmit(student);
                context.SubmitChanges();
                return true;
            }

            return false;
        }
    }
}
