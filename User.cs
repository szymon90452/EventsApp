using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsApp
{
    public class User
    {
        readonly int id;
        readonly String name;
        readonly String surname;
        readonly String login;
        readonly String email;
        readonly String priviledges;

        public User(int id, String name, String surname, String login ,String email, String priviledges)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.login = login;
            this.email = email;
            this.priviledges = priviledges;
        }

        public User(String priviledges)
        {
            this.id = 0;
            this.name = "";
            this.surname = "";
            this.login = "";
            this.email = "";
            this.priviledges = priviledges;
        }

        public String GetName() 
        {
            return this.name;
        }

        public String GetSurname()
        {
            return this.surname;
        }

        public String GetLogin()
        {
            return this.login;
        }

        public String GetEmail()
        {
            return this.email;
        }

        public String GetPriviledges()
        {
            return this.priviledges;
        }

        public int GetId()
        {
            return this.id;
        }

    }
}
