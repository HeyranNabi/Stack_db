using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack
{
    class Program
    {
        public static string querystring;
        static readonly object _object = new object();

        public static  List<string> items = new List<string>();
        static void Main(string[] args)
        {
           var t1= Task.Run(() =>
            {
                StreamReader sr = new StreamReader("stack_items.txt");
                while (!sr.EndOfStream)
                {
                    items.Add(sr.ReadLine());
                }
                sr.Close();
            });
            t1.Wait();
         

            SqlConnection con = new SqlConnection(@"Data Source=S14\SQLEXPRESS;Initial Catalog=Stack;Integrated Security=True");


            var t2 = Task.Run(() =>
            {
                lock (_object)
            {
                    for (var i = 0; i < items.Count; i++)

                    {
                        querystring += $" INSERT INTO stack_t ( items ) VALUES('" + items[i] + "'); ";
                        items.RemoveAt(i);

                    }

                    SqlCommand comm = new SqlCommand(querystring, con);
                comm.Connection.Open();
                comm.ExecuteNonQuery();
                con.Close();
                }
            });

            //-------

            var t3 = Task.Run(() =>
            {
                lock (_object)
                {

                    for (var i = 0; i < items.Count; i++)

                    {
                        querystring += $" INSERT INTO stack_t ( items ) VALUES('" + items[i] + "'); ";
                        items.RemoveAt(i);

                    }

                    SqlCommand comm = new SqlCommand(querystring, con);
                    comm.Connection.Open();
                    comm.ExecuteNonQuery();
                    con.Close();
                }
            });
    

            //----------

            var t4 = Task.Run(() =>
            {
                lock (_object)
                {

                    for (var i = 0; i < items.Count; i++)
                   
                    {
                        querystring += $" INSERT INTO stack_t ( items ) VALUES('" + items[i] + "'); ";
                        items.RemoveAt(i);

                    }

                    SqlCommand comm = new SqlCommand(querystring, con);
                    comm.Connection.Open();
                    comm.ExecuteNonQuery();
                    con.Close();
                }
            });

            Task.WaitAll(t2,t3,t4);



        }
    }
}
