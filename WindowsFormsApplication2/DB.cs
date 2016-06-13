using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication2
{

    

    class DB
    {
        private static string serverName = "localhost";
        private static string userName = "root";
        private static string dbName = "cutit";
        private static string port = "3306";
        private static string password = "";

        private static MySqlConnection conn;

        public DB()
        {
            string connStr = 
                "server="+serverName+
                ";user="+userName+
                ";database=" + dbName +
                ";port=" + port +
                ";password=" + password + ";charset=utf8;";

            conn = new MySqlConnection(connStr);
            conn.Open();
        }

        public ArrayList getClasses()
        {
            string sql = "SELECT * FROM class";
            MySqlCommand sqlCom = new MySqlCommand(sql, conn);
            
            MySqlDataReader reader = sqlCom.ExecuteReader();

            ArrayList result = new ArrayList();

            while (reader.Read())
            {
                result.Add(reader.GetString("name"));
            }

            reader.Close();

            return result;
        }

        public ArrayList getSubjects(string filter)
        {
            string sql = "SELECT * FROM `subject` WHERE `class_id`=(SELECT id FROM `class` WHERE `name`='"+filter+"')";
            MySqlCommand sqlCom = new MySqlCommand(sql, conn);

            MySqlDataReader reader = sqlCom.ExecuteReader();

            ArrayList result = new ArrayList();

            while (reader.Read())
            {
                result.Add(reader.GetString("name"));
            }

            reader.Close();

            return result;
        }

        public ArrayList getTopics(string filter1,string filter2)
        {
            string sql = "SELECT * FROM `topic` WHERE `subject_id`=(SELECT id FROM `subject` WHERE `name`='"+filter2+"' AND `class_id`=(SELECT id FROM `class` WHERE `name`='"+filter1+"'))";
            MySqlCommand sqlCom = new MySqlCommand(sql, conn);

            MySqlDataReader reader = sqlCom.ExecuteReader();

            ArrayList result = new ArrayList();

            while (reader.Read())
            {
                result.Add(reader.GetString("name"));
            }

            reader.Close();

            return result;
        }

        public void save(string name, string link, string _class, string subject, string topic, string complexity, string answer)
        {
            string sql = "INSERT INTO `item`(`name`, `link`, `class_id`, `subject_id`, `topic_id`, `complexity`, `answer`) VALUES (@name,@link,@class,@subject,@topic,@complex,@answer)";
            MySqlCommand sqlCom = new MySqlCommand(sql, conn);
            sqlCom.Parameters.AddWithValue("@name", name);
            sqlCom.Parameters.AddWithValue("@link", link);
            sqlCom.Parameters.AddWithValue("@class", class_id(_class));
            sqlCom.Parameters.AddWithValue("@subject", subject_id(_class,subject));
            sqlCom.Parameters.AddWithValue("@topic", topic_id(_class,subject,topic));
            sqlCom.Parameters.AddWithValue("@complex", complexity);
            sqlCom.Parameters.AddWithValue("@answer", answer);
            sqlCom.Prepare();

            sqlCom.ExecuteNonQuery();

        }

        private string class_id(string filter) {
            string sql = "SELECT id FROM `class` WHERE `name`='" + filter + "'";
            MySqlCommand sqlCom = new MySqlCommand(sql, conn);

            MySqlDataReader reader = sqlCom.ExecuteReader();
            reader.Read();
            string result = reader.GetString("id");

            reader.Close();
            return result;
        }

        private string subject_id(string filter1,string filter2)
        {
            string sql = "SELECT id FROM `subject` WHERE `name`='"+filter2+"' AND `class_id`=(SELECT id FROM `class` WHERE `name`='" + filter1 + "')";
            MySqlCommand sqlCom = new MySqlCommand(sql, conn);

            MySqlDataReader reader = sqlCom.ExecuteReader();
            reader.Read();
            string result = reader.GetString("id");

            reader.Close();
            return result;
        }

        public string topic_id(string filter1, string filter2, string filter3)
        {
            string sql = "SELECT * FROM `topic` WHERE `name`='"+filter3+"' AND `subject_id`=(SELECT id FROM `subject` WHERE `name`='" + filter2 + "' AND `class_id`=(SELECT id FROM `class` WHERE `name`='" + filter1 + "'))";
            MySqlCommand sqlCom = new MySqlCommand(sql, conn);

            MySqlDataReader reader = sqlCom.ExecuteReader();
            reader.Read();
            string result = reader.GetString("id");

            reader.Close();
            return result;
        }

        public static bool checkConnection()
        {
            string connStr =
                "server=" + serverName +
                ";user=" + userName +
                ";database=" + dbName +
                ";port=" + port +
                ";password=" + password + ";charset=utf8;";

            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();

                conn.Close();
                return true;
            } catch {
                return false;
            }
        }

    }
}