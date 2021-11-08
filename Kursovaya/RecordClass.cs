using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class RecordClass
    {
        DBconnection connect = new DBconnection();
        public bool insertrec(string name, string tryes)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `record`(`name`,`tryes`)" +
         " VALUES (@name,@tryes);", connect.getconnection);
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
            command.Parameters.Add("@tryes", MySqlDbType.VarChar).Value = tryes;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }
        public DataTable getreclist()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `record`", connect.getconnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

    }
}
