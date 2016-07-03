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
        private static SqliteHelper sh = new SqliteHelper();

        public static SqliteHelper Sh
        {
            get
            {
                return sh;
            }
        }

        public Flag GetFlagByName(string name)
        {
            try
            {
                var sql = "select * from flag where name=@name;";
                var cmdparams = new List<SQLiteParameter>()
                {
                    new SQLiteParameter("name", name)
                };
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

        public List<Flag> getListFlag()
        {
            try
            {
                var sql = "select * from flag;";
                DataTable dt = sh.Select(sql);
                List<Flag> listFlag = new List<Flag>();
                foreach (DataRow dr in dt.Rows)
                {
                    Flag f = new Flag();
                    f.id = int.Parse(dr["id"].ToString());
                    f.name = dr["name"].ToString();
                    f.substitute = dr["substitute"].ToString();
                    f.kind = dr["kind"].ToString();
                    f.meaning = dr["meaning"].ToString();
                    listFlag.Add(f);
                }

                return listFlag;
            }
            catch (Exception)
            {
                //Do any logging operation here if necessary
                return null;
            }
        }

        public void UpdateFlag(Flag flag)
        {
            try
            {
                var sql = "update flag set name=@name,kind=@kind,substitute=@substitute,meaning=@meaning where id=@id;";
                var cmdparams = new List<SQLiteParameter>()
                {
                    new SQLiteParameter("name", flag.name),
                    new SQLiteParameter("kind", flag.kind),
                    new SQLiteParameter("substitute", flag.substitute),
                    new SQLiteParameter("meaning", flag.meaning),
                    new SQLiteParameter("id", flag.id)
                    
                };
                sh.Execute(sql, cmdparams);
            }
            catch (Exception)
            {
                //Do any logging operation here if necessary
            }
        }

        public List<Situation> getListSituationByCategory(string category)
        {
            try
            {
                var sql = "select * from situation where category=@category;";
                var cmdparams = new List<SQLiteParameter>()
                {
                    new SQLiteParameter("category", category)
                };
                DataTable dt = sh.Select(sql, cmdparams);
                List<Situation> listS = new List<Situation>();
                foreach(DataRow dr in dt.Rows)
                {
                    Situation s = new Situation();
                    s.Id = int.Parse(dr["id"].ToString());
                    s.Name = dr["name"].ToString();
                    s.Category = dr["category"].ToString();
                    s.Detail = dr["detail"].ToString();
                    listS.Add(s);
                }
                
                return listS;
            }
            catch (Exception)
            {
                //Do any logging operation here if necessary
                return null;
            }
        }
    }

    class Situation
    {
        private int id;
        private string name;
        private string category;
        private string detail;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Category
        {
            get
            {
                return category;
            }

            set
            {
                category = value;
            }
        }

        public string Detail
        {
            get
            {
                return detail;
            }

            set
            {
                detail = value;
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
