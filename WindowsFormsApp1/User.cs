using System.Collections.Generic;

namespace WindowsFormsApp1
{
    public class User
    {
        public User()
        {
            Datas = new List<Data>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public List<Data> Datas { get; set; }
    }

    public class Data
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public User User { get; set; }
    }
}
