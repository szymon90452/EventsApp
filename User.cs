using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsApp
{
    public class User
    {
        readonly String name;
        readonly String surname;
        readonly String login;
        readonly String email;
        readonly String priviledges;

        public User(String name, String surname, String login ,String email, String priviledges)
        {
            this.name = name;
            this.surname = surname;
            this.login = login;
            this.email = email;
            this.priviledges = priviledges;
        }

        public User(String priviledges)
        {
            this.priviledges = priviledges;
        }

        public String getName() 
        {
            return this.name;
        }

        public String getSurname()
        {
            return this.surname;
        }

        public String getLogin()
        {
            return this.login;
        }

        public String getEmail()
        {
            return this.email;
        }

        public String getPriviledges()
        {
            return this.priviledges;
        }

    }
}
