using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sign.util
{
    class SqliteService
    {
        public Flag GetFlagByName(string name)
        {
            try
            {
                var sql = "select * from flag where name=@name;";
                var cmdparams = new List<SQLiteParameter>()
                {
                    new SQLiteParameter("name", name)
                };
                SqliteHelper sh = new SqliteHelper();
                DataTable dt = sh.Select(sql, cmdparams);
                if (dt.Rows.Count > 0)
                {
                    Flag flag = new Flag();
                    flag.id = int.Parse(dt.Rows[0]["id"].ToString());
                    flag.name = dt.Rows[0]["name"].ToString();
                    flag.substitute = dt.Rows[0]["substitute"].ToString();
                    flag.kind = dt.Rows[0]["kind"].ToString();
                    flag.meaning = dt.Rows[0]["meaning"].ToString();
                    return flag;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                //Do any logging operation here if necessary
                return null;
            }

        }

        public Flag GetFlagById(int id)
        {
            try
            {
                var sql = "select * from flag where id=@id;";
                var cmdparams = new List<SQLiteParameter>()
                {
                    new SQLiteParameter("id", id)
                };
                SqliteHelper sh = new SqliteHelper();
                DataTable dt = sh.Select(sql, cmdparams);
                if (dt.Rows.Count > 0)
                {
                    Flag flag = new Flag();
                    flag.id = int.Parse(dt.Rows[0]["id"].ToString());
                    flag.name = dt.Rows[0]["name"].ToString();
                    flag.substitute = dt.Rows[0]["substitute"].ToString();
                    flag.kind = dt.Rows[0]["kind"].ToString();
                    flag.meaning = dt.Rows[0]["meaning"].ToString();
                    return flag;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                //Do any logging operation here if necessary
                return null;
            }

        }
    }

    class Flag
    {
        private int _id;
        private string _name;
        private string _substitute;
        private string _kind;
        private string _meaning;

        public int id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public string name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public string substitute
        {
            get
            {
                return _substitute;
            }

            set
            {
                _substitute = value;
            }
        }

        public string kind
        {
            get
            {
                return _kind;
            }

            set
            {
                _kind = value;
            }
        }

        public string meaning
        {
            get
            {
                return _meaning;
            }

            set
            {
                _meaning = value;
            }
        }
    }
}
