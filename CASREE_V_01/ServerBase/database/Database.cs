using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerBase.database
{
    class Database
    {
        public static CASREE_DatabaseDataContext CASREE_DatabaseCtx = new CASREE_DatabaseDataContext();

        public Database() 
        {

        }

        #region casree_user table

        public static Boolean insertUser(string userName, string userPasswd, Int32 userGroupId) 
        {
            if (queryUser(userName) == null)
            {
                CASREE_DatabaseCtx.casree_users.InsertOnSubmit(
                    new casree_user
                    {
                        name = userName,
                        passwd = userPasswd,
                        groupId = userGroupId
                    });
                CASREE_DatabaseCtx.SubmitChanges();
                return true;
            }
            else {
                Console.WriteLine(userName + "exist in the database.");
                return false;
            }
            
        }

        public static User queryUser(string userName)
        {
            var users = from user in CASREE_DatabaseCtx.casree_users
                    where user.name == userName
                    select user;

            if (users.Count() == 0)
            {
                return null;
            }

            return new User(users.First().name.Split(' ')[0], users.First().passwd.Split(' ')[0], users.First().groupId);                      
        }

        public static Boolean updateUser(string userName, string userPasswd, Int32 userGroupId) 
        {
            IQueryable<casree_user> userToUpdate = from user in CASREE_DatabaseCtx.casree_users
                                                   where user.name == userName
                                                   select user;
            if (userToUpdate.Count() != 0) {
                userToUpdate.First().name = userName;
                userToUpdate.First().passwd = userPasswd;
                userToUpdate.First().groupId = userGroupId;
                CASREE_DatabaseCtx.SubmitChanges();
                return true;
            }
            return false;
        }

        public static Boolean deleteUser(string userName) 
        {
            IQueryable<casree_user> usersToDelete = from user in CASREE_DatabaseCtx.casree_users
                                                    where user.name == userName
                                                    select user;
            if (usersToDelete.Count() > 0)
            {
                CASREE_DatabaseCtx.casree_users.DeleteOnSubmit(usersToDelete.First());
                CASREE_DatabaseCtx.SubmitChanges();
                return true;
            }
            return false;
        }

        #endregion

        #region casree_permission table

        public static Boolean insertPermission(string userName, string userSolutionName,string userProjectName)
        {
            if (queryPermission(userName) == null)
            {
                CASREE_DatabaseCtx.casree_permissions.InsertOnSubmit(
                    new casree_permission
                    {
                        name = userName,
                        solutionName = userSolutionName,
                        projectName = userProjectName,
                    });
                CASREE_DatabaseCtx.SubmitChanges();
                return true;
            }
            else 
            {
                Console.WriteLine(userName + " exist.");
                return false;
            }
            
        }

        public static List<Permission> queryPermission(string userName)
        {
            var permissions = from permission in CASREE_DatabaseCtx.casree_permissions
                        where permission.name == userName
                        select permission;

            if (permissions.Count() == 0)
            {
                return null;
            }

            List<Permission> permissionList= new List<Permission>();
            
            foreach (var pm in permissions) {
                permissionList.Add(
                    new Permission(
                        pm.name.Split(' ')[0],
                        pm.solutionName.Split(' ')[0],
                        pm.projectName.Split(' ')[0])
                    );
            }
            return permissionList;

        }

        public static Boolean updatePermission(string userName, string solutionName,string projectName)
        {
            IQueryable<casree_permission> permissionToUpdate = 
                from permission in CASREE_DatabaseCtx.casree_permissions
                where permission.name == userName
                select permission;

            if (permissionToUpdate.Count() > 0) {
                permissionToUpdate.First().name = userName;
                permissionToUpdate.First().solutionName = solutionName;
                permissionToUpdate.First().projectName = projectName;
                CASREE_DatabaseCtx.SubmitChanges();
                return true;
            }
            return false;
            
        }

        public static Boolean deletePermission(string userName)
        {
            IQueryable<casree_permission> permissionToDelete =
                from permission in CASREE_DatabaseCtx.casree_permissions
                where permission.name == userName
                select permission;

            if (permissionToDelete.Count() > 0)
            {
                CASREE_DatabaseCtx.casree_permissions.DeleteOnSubmit(permissionToDelete.First());
                CASREE_DatabaseCtx.SubmitChanges();
                return true;
            }
            return false;
        }

        #endregion

        #region casree_solution&project table
        //插入项目或修改项目工程
        public static Boolean insertSolution(string solutionname, Dictionary<string,int> project)
        {

            IQueryable<casree_solution> solutionToUpdate = from solution in CASREE_DatabaseCtx.casree_solutions
                                                    where solution.solutionName == solutionname
                                                    select solution;
            if (solutionToUpdate != null)
            {
                solutionToUpdate.First().predict = project["predict"];
                solutionToUpdate.First().assign = project["assign"];
                solutionToUpdate.First().analysis = project["analysis"];
                solutionToUpdate.First().design = project["desgin"];
                solutionToUpdate.First().test = project["test"];
                solutionToUpdate.First().assess = project["assess"];

            }
            else 
            {
                CASREE_DatabaseCtx.casree_solutions.InsertOnSubmit(
                    new casree_solution
                    {
                        solutionName = solutionname,
                        predict = project["predict"],
                        assign = project["assign"],
                        analysis = project["analysis"],
                        design = project["design"],
                        test = project["test"],
                        assess = project["assess"]
                    });
            }
            
            CASREE_DatabaseCtx.SubmitChanges();
            return true;
        }

        public static Boolean UpdateProject(string solutionName, string projectName,string version)
        {
            IQueryable<casree_solution> solutionToUpdate = from solution in CASREE_DatabaseCtx.casree_solutions
                                                           where solution.solutionName == solutionName
                                                           select solution;
            if (solutionToUpdate != null)
            { 
                switch(projectName)
                {
                    case "Predict": solutionToUpdate.First().predict++; break;
                    case "Assign": solutionToUpdate.First().assign++; break;
                    case "Analysis": solutionToUpdate.First().analysis++; break;
                    case "Design": solutionToUpdate.First().design++; break;
                    case "Test": solutionToUpdate.First().test++; break;
                    case "Assess": solutionToUpdate.First().assess++; break;
                    default: break;
                }
                //更改版本表
                insertProjectVersion(solutionName, projectName, version);
                CASREE_DatabaseCtx.SubmitChanges();
                return true;
            }
            return false;
        }

        public static Solution queryProjects(string solutionName) 
        {
            var solutions = from solution in CASREE_DatabaseCtx.casree_solutions
                            where solution.solutionName == solutionName
                            select solution;
            if (solutions.Count() == 0)
            {
                return null;
            }
            else 
            {
                return new Solution(
                    solutions.First().predict,
                    solutions.First().assign,
                    solutions.First().analysis,
                    solutions.First().design,
                    solutions.First().test,
                    solutions.First().assess);
            }
        }

        //查询并返回所有项目名称
        public static List<string> querySolution() 
        {
            var solutions = from solution in CASREE_DatabaseCtx.casree_solutions
                            select solution;
            if (solutions.Count() == 0)
            {
                return null;
            }
            List<string> solutionsList = new List<string>();
            foreach (var s in solutions)
            {
                solutionsList.Add(s.solutionName);
            }
            return solutionsList;
        }

        #endregion

        #region casree_project&Version table
        public static Boolean insertProjectVersion(string solution,string projectName, string version)
        {
            CASREE_DatabaseCtx.casree_projectVersions.InsertOnSubmit(
                new casree_projectVersion
                {
                    solution_projectName = solution+"_"+projectName,
                    version = version
                });
            CASREE_DatabaseCtx.SubmitChanges();
            return true;
        }

        //查询并返回项目工程的各版本信息
        public static List<string> queryProjectVersion(string solution, string projectName)
        {
            var projectVersions = from projectVersion in CASREE_DatabaseCtx.casree_projectVersions
                                  where projectVersion.solution_projectName == solution+"_"+projectName
                                  select projectVersion;

            if (projectVersions.Count() == 0)
            {
                return null;
            }
            List<string> versions = new List<string>();
            foreach (var pv in projectVersions)
            {
                versions.Add(pv.version);
            }
            return versions;
        }

        #endregion
    }

    
}
