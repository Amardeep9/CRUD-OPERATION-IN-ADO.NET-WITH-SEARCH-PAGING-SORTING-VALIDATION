using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InterviewPracticeforCRUDinADO.Models
{
    public class StudentDataAccessLayer
    {

        string connectionString = "Data Source=DELL;Initial Catalog=Mydatabase; Integrated Security=true;";

        //To View all employees details    
        public IEnumerable<Student> GetAllStudent()
        {
            List<Student> lststudent = new List<Student>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Student student = new Student();

                    student.RollNo = Convert.ToInt32(rdr["RollNo"]);
                    student.Name = rdr["Name"].ToString();
                    student.Emailid= rdr["Emailid"].ToString();
                    student.PassWord = rdr["PassWord"].ToString();
                    student.ConfirmPassword = rdr["ConfirmPassword"].ToString();
                    student.DOB = rdr["DOB"].ToString();
                    student.DOB = Convert.ToDateTime(student.DOB).ToString("dd/MM/yyyy");


                    lststudent.Add(student);
                  
                 
                }
                con.Close();
            }
            return lststudent;
        }

        //To Add new employee record    
        public void AddStudent(Student student)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Emailid", student.Emailid);
                cmd.Parameters.AddWithValue("@PassWord", student.PassWord);
                cmd.Parameters.AddWithValue("@ConfirmPassword", student.ConfirmPassword);
                cmd.Parameters.AddWithValue("@DOB", student.DOB);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //To Update the records of a particluar employee  
        public void UpdateStudent(Student student)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RollNo", student.RollNo);
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Emailid", student.Emailid);
                cmd.Parameters.AddWithValue("@PassWord", student.PassWord);
                cmd.Parameters.AddWithValue("@ConfirmPassword", student.ConfirmPassword);
                cmd.Parameters.AddWithValue("@DOB", student.DOB);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Get the details of a particular employee  
        public Student GetStudentData(int? id)
        {
            Student student = new Student();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM tblStudent WHERE RollNo= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    student.RollNo = Convert.ToInt32(rdr["RollNo"]);
                    student.Name = rdr["Name"].ToString();
                    student.Emailid = rdr["Emailid"].ToString();
                    student.PassWord = rdr["PassWord"].ToString();
                    student.ConfirmPassword = rdr["ConfirmPassword"].ToString();
                    student.DOB = rdr["DOB"].ToString();
                    student.DOB = Convert.ToDateTime(student.DOB).ToString("dd/MM/yyyy");


                }
            }
            return student;
        }

        //To Delete the record on a particular employee  
        public void DeleteStudent(int? id)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RollNo", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}