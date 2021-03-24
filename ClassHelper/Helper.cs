using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BDSystemSMBIEContext;


namespace WEB_SMBIE.ClassHelper
{

    public static class Helper
    {
        static public BDSystemSMBIEDataContext BDSystem;

        public static void Init()
        {
            BDSystem = new BDSystemSMBIEDataContext();

            if (BDSystem.Positions.ToList().Count == 0)
            {
                Position position = new Position();
                position.Name = "Администратор";
                position.Rang = 0;
                position.Description = "Админ";

                TypeDepartment type = new TypeDepartment();
                type.Name = "BasaType";
                type.Position = position;
                type.IdParent = 0;

                Department department = new Department();
                department.Name = "Basa";
                department.TypeDepartment = type;
                department.Description = "";
                department.IdParentDep = 0;

                Folder folder = new Folder();
                folder.IdParent = 0;
                folder.Name = "admin_folder";

                User user = new User();
                user.Character = "";
                user.Phone = "7955555555";
                user.Login = "admin";
                user.Password = "admin";
                user.FIO = "admin admin admin";
                user.Folder = folder;
                user.Department = department;

                BDSystem.Users.InsertOnSubmit(user);
                Save();
            }
        }

        public static void Save()
        {
            try
            {
                BDSystem.SubmitChanges();
            }
            catch
            {
                
            }
        }

        public static User Auth(string login, string password)
        {
            List<User> users = (from re in BDSystem.Users where re.Login == login && re.Password == password select re).ToList();
            if (users.Count == 1)
                return users[0];
            return null;
        }

        public static User Auth(string token)
        {
            List<User> users = (from re in BDSystem.Users where re.Token == token && re.Datatime.Value > DateTime.Now select re).ToList();
            if (users.Count == 1)
                return users[0];
            return null;
        }

        public static Department GetDepartment(int key)
        {
            var results = (from re in BDSystem.Departments where key == re.Id select re).ToList();
            if (results.Count == 1)
                return results[0];
            return null;
        }

        public static void UserUpdateToken(User user)
        {
            user.Token = Guid.NewGuid().ToString();
            user.Datatime = DateTime.Now.AddHours(1);
            Save();
        }

        public static TypeDepartment GetTypeDepartmentChild(TypeDepartment tdep)
        {
            if (tdep != null)
            {
                var results = (from re in BDSystem.TypeDepartments where tdep.Id == re.IdParent select re).ToList();
                if (results.Count == 1)
                    return results[0];
            }
            return null;
        }

        public static TypeDepartment GetTypeDepartment(int key)
        {
            List<TypeDepartment> types = (from re in BDSystem.TypeDepartments where re.Id == key select re).ToList();
            if (types.Count == 1)
            {
                return types[0];
            }
                return null;
        }

        public static BSLink GetBSLink(int id)
        {
            BSLink sLink = (from re in ClassHelper.Helper.BDSystem.BSLinks where re.Id == id select re).ToList()[0];
            return sLink;
        }
    }
}