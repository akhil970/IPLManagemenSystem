using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using EncryptionDecryption;
using System.Data;
using IPLManagementSystemDoL; // Referring Domain Layer
using System.Data.SqlClient; // SqlClient for sql coperations
using System.Configuration; // Configuration for connecting to the database
using System.Drawing;
//This is Data layer
namespace IPLManagementSystemDL
{
    public class InvalidCredentialsException : Exception
    {
        public override string Message { get; }
        public InvalidCredentialsException(string Message) { this.Message = Message; }
    }
    public class DataLayerClass
    {
        public string dbpath = ConfigurationManager.ConnectionStrings["IPLManagementSystemConnection"].ConnectionString; //Connects to Database
        //string dbpath = "server=localhost;database=IPLManagementSystem;integrated security=true; MultipleActiveResultSets=true";
        public SqlConnection sqlcon;
        public SqlCommand sqlcmd;
        public SqlDataReader sqldr;
        public object UserLoginDl(UserLogin userLogin)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            int userid = 0, roleid = 0;
            string password = "";
            string verifyQuery = "usp_login";
            sqlcmd = new SqlCommand(verifyQuery, sqlcon) { CommandType = CommandType.StoredProcedure };
            sqlcmd.Parameters.AddWithValue("@userName", userLogin.userName);
            sqldr = sqlcmd.ExecuteReader();
            try
            {
                if (!sqldr.HasRows)
                {
                    throw new InvalidCredentialsException("Invalid Username");
                }
                else
                {
                    while (sqldr.Read() is true)
                    {
                        userid = sqldr.GetInt32(0); //we get user id
                        password = sqldr.GetString(2);
                    }

                    if (password == userLogin.password)
                    {
                        string roleidQuery = "usp_roleid";
                        sqlcmd = new SqlCommand(roleidQuery, sqlcon) { CommandType = CommandType.StoredProcedure };
                        sqlcmd.Parameters.AddWithValue("@userid", userid);
                        sqldr = sqlcmd.ExecuteReader();
                        while (sqldr.Read() is true)
                        {
                            roleid = sqldr.GetInt32(0); //we get user id
                        }
                        sqldr.Close();
                        sqlcon.Close();
                        sqlcon.Dispose();
                        return roleid;
                    }
                    else
                    {
                        sqldr.Close();
                        sqlcon.Close();
                        sqlcon.Dispose();
                        throw new InvalidCredentialsException("Invalid Password");
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /*Users table mathods*/
        //Insert user
        public bool InsertUserDl(User users)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string insertusers = "usp_users_insert"; 
                sqlcmd = new SqlCommand(insertusers, sqlcon)
                {
                    CommandType = CommandType.StoredProcedure
                };

                string[] users_to_insert = new string[] { "@UserId", "@UserName", "@Password", "@Firstname", "@Lastname" };
                ArrayList users_to_insert_array = new ArrayList() { users.UserId, users.Username, users.Password, users.Firstname, users.Lastname };
                for (int i = 0; i < users_to_insert.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(users_to_insert[i], users_to_insert_array[i]); //This is for stored procedure parameters
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
        //Update User
        public bool UpdateUserDl(User users)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            //Update Statement
            //To execute the statement
            try
            {
                string updateusers = "usp_Users_Update";
                sqlcmd = new SqlCommand(updateusers, sqlcon) { CommandType = CommandType.StoredProcedure };
                string[] users_to_update = new string[] { "@UserId", "@UserName", "@Password", "@Firstname", "@Lastname" };
                ArrayList users_to_update_array = new ArrayList() { users.UserId, users.Username, users.Password, users.Firstname, users.Lastname };
                for (int i = 0; i < users_to_update.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(users_to_update[i], users_to_update_array[i]);
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
           
        }
        //Delete User
        //public bool DeleteUser(int id)
        //{
        //    sqlcon = new SqlConnection(dbpath);
        //    sqlcon.Open();
        //    try
        //    {
        //        //Delete Statement
        //        string deleteUsers = "usp_Users_delete";
        //        //To execute command
        //        sqlcmd = new SqlCommand(deleteUsers, sqlcon) { CommandType = CommandType.StoredProcedure };
        //        sqlcmd.Parameters.AddWithValue("@UserId", id);
        //        sqlcmd.ExecuteNonQuery();
        //        sqlcon.Close();
        //        sqlcon.Dispose();
        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return false;
        //}
        //View User
        public DataTable ViewUserDl()
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string viewUser = "usp_Users_view";
            sqlcmd = new SqlCommand(viewUser, sqlcon) { CommandType = CommandType.StoredProcedure };
            sqlcmd.ExecuteNonQuery();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd); //Data Adapter is used to take the data recieved from the command and store the values
            DataTable dataTable = new DataTable();
            sqlda.Fill(dataTable); //Fill method is used to store the data into datatable
            sqlcon.Close();
            sqlcon.Dispose();
            return dataTable;
        }

        /*Roles table methods*/
        //Insert Role
        public bool InsertRoleDl(Roles roles)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string insertroles = "usp_roles_insert";
                sqlcmd = new SqlCommand(insertroles, sqlcon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                string[] roles_to_insert = new string[] { "@roleid"  , "@rolename" };
                ArrayList roles_to_insert_array = new ArrayList() { roles.RoleId, roles.Name};
                for (int i = 0; i < roles_to_insert.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(roles_to_insert[i], roles_to_insert_array[i]);
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        public bool UpdateRoleDl(Roles roles)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string insertroles = "usp_roles_update";
                sqlcmd = new SqlCommand(insertroles, sqlcon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                string[] roles_to_update = new string[] { "@roleid", "@rolename" };
                ArrayList roles_to_update_array = new ArrayList() { roles.RoleId, roles.Name };
                for (int i = 0; i < roles_to_update.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(roles_to_update[i], roles_to_update_array[i]);
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        //public bool DeleteRoleDl(int id)
        //{
        //    sqlcon = new SqlConnection(dbpath);
        //    sqlcon.Open();
        //    try
        //    {
        //        Delete Statement
        //        string deleteRoles = "usp_roles_delete";
        //        To execute command
        //        sqlcmd = new SqlCommand(deleteRoles, sqlcon) { CommandType = CommandType.StoredProcedure };
        //        sqlcmd.Parameters.AddWithValue("@roleid", id);
        //        sqlcmd.ExecuteNonQuery();
        //        sqlcon.Close();
        //        sqlcon.Dispose();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return false;
        //}
        public DataTable ViewRoleDl()
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string viewRole = "usp_roles_view";
            sqlcmd = new SqlCommand(viewRole, sqlcon) { CommandType = CommandType.StoredProcedure };
            sqlcmd.ExecuteNonQuery();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            DataTable dataTable = new DataTable("Roles");
            sqlda.Fill(dataTable);
            sqlda.Update(dataTable);
            sqlcon.Close();
            return dataTable;

        }

        /*UserRoles Tabel methods*/
        //Insert UserRole
        public bool InsertUserRoleDl(UserRoles userRoles)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string insertuserroles = "usp_Userroles_insert";
                sqlcmd = new SqlCommand(insertuserroles, sqlcon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                string[] userroles_to_insert = new string[] { "@UserId", "@RoleId" };
                ArrayList userroles_to_insert_array = new ArrayList() { userRoles.UserId, userRoles.RoleId };
                for (int i = 0; i < userroles_to_insert.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(userroles_to_insert[i], userroles_to_insert_array[i]);
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        //Update UserRole
        public bool UpdateUserRoleDl(UserRoles userRoles)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string updateuserroles = "usp_Userroles_update";
                sqlcmd = new SqlCommand(updateuserroles, sqlcon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                string[] userroles_to_update = new string[] { "@UserId", "@RoleId" };
                ArrayList userroles_to_update_array = new ArrayList() { userRoles.UserId, userRoles.RoleId};
                for (int i = 0; i < userroles_to_update.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(userroles_to_update[i], userroles_to_update_array[i]);
                }
                int a = sqlcmd.ExecuteNonQuery();
                if(a==0)
                {
                    throw new Exception("No such user");
                }
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
            
        }
        //View UserRole
        public DataTable ViewUserRoleDl()
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string viewUserRole = "usp_Userroles_view";
            sqlcmd = new SqlCommand(viewUserRole, sqlcon) { CommandType = CommandType.StoredProcedure };
            sqlcmd.ExecuteNonQuery();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            DataTable dataTable = new DataTable();
            sqlda.Fill(dataTable);
            sqlda.Update(dataTable);
            sqlcon.Close();
            return dataTable;
        }
        /*Venue table methods*/
        //Insert Venue
        public bool InsertVenueDl(Venue venue)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string insertvenue = "usp_Venue_insert";
                sqlcmd = new SqlCommand(insertvenue, sqlcon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                string[] venue_to_insert = new string[] { "@Id","@Location","@Description" };
                ArrayList venue_to_insert_array = new ArrayList() { venue.Id, venue.Location, venue.Description };
                for (int i = 0; i < venue_to_insert.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(venue_to_insert[i], venue_to_insert_array[i]);
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
        //Update Venue
        public bool UpdateVenueDl(Venue venue)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string updatevenue = "usp_Venue_update";
                sqlcmd = new SqlCommand(updatevenue, sqlcon) { CommandType = CommandType.StoredProcedure };
                string[] venue_to_insert = new string[] { "@Id", "@Location", "@Description" };
                ArrayList venue_to_insert_array = new ArrayList() { venue.Id, venue.Location, venue.Description };
                for (int i = 0; i < venue_to_insert.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(venue_to_insert[i], venue_to_insert_array[i]);
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                sqlcon.Close();
                sqlcon.Dispose();
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        //Delete Venue
        public bool DeleteVenueDl(int id)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string deleteVenue = "usp_Venue_delete";
                sqlcmd = new SqlCommand(deleteVenue, sqlcon) { CommandType = CommandType.StoredProcedure };
                sqlcmd.Parameters.AddWithValue("@Id", id);
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
        //View Venue
        public DataTable ViewVenueDl()
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string viewVenue = "usp_Venue_view";
            sqlcmd = new SqlCommand(viewVenue, sqlcon) { CommandType = CommandType.StoredProcedure };
            sqlcmd.ExecuteNonQuery();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            DataTable dataTable = new DataTable();
            sqlda.Fill(dataTable);
            //sqlda.Update(dataTable);
            sqlcon.Close();
            return dataTable;
        }

        /* Ticket table methods */
        //Insert Ticket
        public bool InsertTicketDl(Ticket ticket)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string insertticket = "usp_Ticket_insert";
                sqlcmd = new SqlCommand(insertticket, sqlcon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                string[] ticket_to_insert = new string[] { "@Id","@MatchId","@CategoryId","@Price" };
                ArrayList ticket_to_insert_array = new ArrayList() { ticket.Id, ticket.MatchId, ticket.CategoryId, ticket.Price };
                for (int i = 0; i < ticket_to_insert.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(ticket_to_insert[i], ticket_to_insert_array[i]);
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch(Exception ex)
            {
                sqlcon.Close();
                sqlcon.Dispose();
                return false;
            }
            
        }
        //update Ticket
        public bool UpdateTicketDl(Ticket ticket)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string updateticket = "usp_Ticket_update";
                sqlcmd = new SqlCommand(updateticket, sqlcon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                string[] ticket_to_insert = new string[] { "@Id", "@MatchId", "@CategoryId", "@Price" };
                ArrayList ticket_to_insert_array = new ArrayList() { ticket.Id, ticket.MatchId, ticket.CategoryId, ticket.Price };
                for (int i = 0; i < ticket_to_insert.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(ticket_to_insert[i], ticket_to_insert_array[i]);
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                sqlcon.Close();
                sqlcon.Dispose();
                return false;
            }
            
        }
        //Delete Ticket
        public bool DeleteTicketDl(int id)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string deleteTicket = "usp_Ticket_Delete";
                sqlcmd = new SqlCommand(deleteTicket, sqlcon) { CommandType = CommandType.StoredProcedure };
                sqlcmd.Parameters.AddWithValue("@Id", id);
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                sqlcon.Close();
                sqlcon.Dispose();
                return false;
            }
            
        }
        //View Ticket
        public DataTable ViewTicketDl()
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string viewTicket = "usp_Ticket_View";
            sqlcmd = new SqlCommand(viewTicket, sqlcon) { CommandType = CommandType.StoredProcedure };
            sqlcmd.ExecuteNonQuery();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            DataTable dataTable = new DataTable();
            sqlda.Fill(dataTable);
            sqlda.Update(dataTable);
            sqlcon.Close();
            return dataTable;

        }

        /* Ticket Category*/
        //Insert TicketCategory
        public bool InsertTicketCategoryDl(TicketCategory ticketcategory)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string insertticketcategory = "usp_TicketCategory_insert";
                sqlcmd = new SqlCommand(insertticketcategory, sqlcon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                string[] ticketcategory_to_insert = new string[] { "@Id", "@Name" };
                ArrayList ticketcategory_to_insert_array = new ArrayList() { ticketcategory.Id, ticketcategory.Name };
                for (int i = 0; i < ticketcategory_to_insert.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(ticketcategory_to_insert[i], ticketcategory_to_insert_array[i]);
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                sqlcon.Close();
                sqlcon.Dispose();
                return false;
            }
            
        }
        //update Ticket
        public bool UpdateTicketCategoryDl(TicketCategory ticketcategory)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string insertticketcategory = "usp_TicketCategory_update";
                sqlcmd = new SqlCommand(insertticketcategory, sqlcon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                string[] ticketcategory_to_update = new string[] { "@Id", "@Name" };
                ArrayList ticketcategory_to_update_array = new ArrayList() { ticketcategory.Id, ticketcategory.Name };
                for (int i = 0; i < ticketcategory_to_update.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(ticketcategory_to_update[i], ticketcategory_to_update_array[i]);
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                sqlcon.Close();
                sqlcon.Dispose();
                return false;
            }
            
        }
        //Delete Ticket
        public bool DeleteTicketCategoryDl(int id)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string deleteTicket = "usp_TicketCategory_Delete";
                sqlcmd = new SqlCommand(deleteTicket, sqlcon) { CommandType = CommandType.StoredProcedure };
                sqlcmd.Parameters.AddWithValue("@Id", id);
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                sqlcon.Close();
                sqlcon.Dispose();
                return false;
            }
           
        }
        //View TicketCategory
        public DataTable ViewTicketCategoryDl()
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string viewTicketCategory = "usp_TicketCategory_View";
            sqlcmd = new SqlCommand(viewTicketCategory, sqlcon) { CommandType = CommandType.StoredProcedure };
            sqlcmd.ExecuteNonQuery();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            DataTable dataTable = new DataTable();
            sqlda.Fill(dataTable);
            //sqlda.Update(dataTable);
            sqlcon.Close();
            return dataTable;
        }
        /*Team table method*/
        //Insert Team
        public bool InsertTeamDl(Team team)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string insertTeam = "usp_Team_insert";
                sqlcmd = new SqlCommand(insertTeam, sqlcon) { CommandType = CommandType.StoredProcedure };
                string[] TeamUpdate = new string[] { "@id", "@Name", "@HomeGround", "@Franchise_Owner", "@Logo_Image" };
                ArrayList TeamUpdateArray = new ArrayList() { team.Id, team.Name, team.HomeGround, team.FranchiseOwner, team.LogoImage };
                for (int i = 0; i < TeamUpdate.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(TeamUpdate[i], TeamUpdateArray[i]);
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch(Exception ex)
            {
                sqlcon.Close();
                sqlcon.Dispose();
                return false;
            }
            
        }
        //Update Team
        public bool UpdateTeamDl(Team team)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                //Update Statement
                //To execute the statement
                string updateTeam = "usp_Team_update";
                sqlcmd = new SqlCommand(updateTeam, sqlcon) { CommandType = CommandType.StoredProcedure };
                string[] TeamUpdate = new string[] { "@id","@Name","@HomeGround","@Franchise_Owner","@Logo_Image" };
                ArrayList TeamUpdateArray = new ArrayList() { team.Id, team.Name, team.HomeGround, team.FranchiseOwner, team.LogoImage };
                for (int i = 0; i < TeamUpdate.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(TeamUpdate[i], TeamUpdateArray[i]);
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                sqlcon.Close();
                sqlcon.Dispose();
                return false;
            }
            
        }
        //Delete Team
        public bool DeleteTeamDl(int id)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                sqlcon = new SqlConnection(dbpath);
                sqlcon.Open();
                //Delete Statement
                string deleteTeam = "usp_Team_delete";
                //To execute command
                sqlcmd = new SqlCommand(deleteTeam, sqlcon) { CommandType = CommandType.StoredProcedure };
                sqlcmd.Parameters.AddWithValue("@id", id);
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        //View Team
        public DataTable ViewTeamDl()
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string viewTeam = "usp_Team_view";
            sqlcmd = new SqlCommand(viewTeam, sqlcon) { CommandType = CommandType.StoredProcedure };
            sqlcmd.ExecuteNonQuery();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            DataTable dataTable = new DataTable();
            sqlda.Fill(dataTable);
            sqlda.Update(dataTable);
            sqlcon.Close();
            return dataTable;
        }

        /*Schedule*/
        //Insert Schedule
        public bool InsertScheduleDl(Schedule schedule)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string insertSchedule = "usp_Schedule_insert";
                sqlcmd = new SqlCommand(insertSchedule, sqlcon) { CommandType = CommandType.StoredProcedure };
                string[] scheduleInsert = new string[] { "@id", "@MatchId", "@VenueID", "@Date", "@Start_Time", "@End_Time" };
                ArrayList scheduleInsertArray = new ArrayList() { schedule.Id, schedule.MatchId, schedule.VenueId, schedule.Date, schedule.StartTime, schedule.EndTime};
                for (int i = 0; i < scheduleInsert.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(scheduleInsert[i], scheduleInsertArray[i]);
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
        //Update Schedule
        public bool UpdateScheduleDl(Schedule schedule)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                //Update Statement
                //To execute the statement
                string updateSchedule = "usp_Schedule_update";
                sqlcmd = new SqlCommand(updateSchedule, sqlcon) { CommandType = CommandType.StoredProcedure };
                string[] scheduleUpdate = new string[] { "@id", "@MatchId", "@VenueID","@Date","@Start_Time","@End_Time" };
                ArrayList scheduleUpdateArray = new ArrayList() { schedule.Id, schedule.MatchId, schedule.VenueId, schedule.Date, schedule.StartTime, schedule.EndTime};
                for (int i = 0; i < scheduleUpdate.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(scheduleUpdate[i], scheduleUpdateArray[i]);
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
        //Delete Schedule
        public bool DeleteScheduleDl(int id)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                //Delete Statement
                string deleteSchedule = "usp_Schedule_Delete";
                //To execute command
                sqlcmd = new SqlCommand(deleteSchedule, sqlcon) { CommandType = CommandType.StoredProcedure };
                sqlcmd.Parameters.AddWithValue("@id", id);
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        //View Schedule
        public DataTable ViewScheduleDl()
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string viewSchedule = "usp_Schedule_view";
            sqlcmd = new SqlCommand(viewSchedule, sqlcon) { CommandType = CommandType.StoredProcedure };
            sqlcmd.ExecuteNonQuery();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            DataTable dataTable = new DataTable();
            sqlda.Fill(dataTable);
            //sqlda.Update(dataTable);
            sqlcon.Close();
            return dataTable;
        }
        /*Speciality table methods*/
        //Insert Speciality
        public bool InsertSpecialityDl(Speciality speciality)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string insertSpeciality = "usp_Speciality_insert";
                sqlcmd = new SqlCommand(insertSpeciality, sqlcon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                string[] specialityInsert = new string[] { "@id","@Description" };
                ArrayList specialityInsertArray = new ArrayList() { speciality.Id, speciality.Description };
                for (int i = 0; i < specialityInsert.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(specialityInsert[i], specialityInsertArray[i]);
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        //Update Speciality
        public bool UpdateSpecialityDl(Speciality speciality)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string insertSpeciality = "usp_Speciality_update";
                sqlcmd = new SqlCommand(insertSpeciality, sqlcon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                string[] specialityUpdate = new string[] { "@id", "@Description" };
                ArrayList specialityUpdateArray = new ArrayList() { speciality.Id, speciality.Description };
                for (int i = 0; i < specialityUpdate.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(specialityUpdate[i], specialityUpdateArray[i]);
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        //Delete Speciality
        public bool DeleteSpecialityDl(int id)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string deleteSpeciality = "usp_Speciality_Delete";
                sqlcmd = new SqlCommand(deleteSpeciality, sqlcon) { CommandType = CommandType.StoredProcedure };
                sqlcmd.Parameters.AddWithValue("@Id", id);
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        //View Speciality
        public DataTable ViewSpecialityDl()
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string viewSpeciality = "usp_Speciality_view";
            sqlcmd = new SqlCommand(viewSpeciality, sqlcon) { CommandType = CommandType.StoredProcedure };
            sqlcmd.ExecuteNonQuery();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            DataTable dataTable = new DataTable("Speciality");
            sqlda.Fill(dataTable);
            sqlda.Update(dataTable);
            sqlcon.Close();
            return dataTable;
        }
        /*Player photo table methods*/
        //Insert PlayerPhoto
        public bool InsertPlayerPhotoDl(PlayerPhoto playerPhoto)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string insertPlayerPhoto = "usp_playerphoto_insert";
                sqlcmd = new SqlCommand(insertPlayerPhoto, sqlcon) { CommandType = CommandType.StoredProcedure };
                sqlcmd.Parameters.AddWithValue("@Id", playerPhoto.Id); 
                sqlcmd.Parameters.AddWithValue("@Photo", playerPhoto.Photo);
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        //Update PlayerPhoto
        public bool UpdatePlayerPhotoDl(PlayerPhoto playerPhoto)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string updatePlayerPhoto = "usp_playerphoto_update";
                sqlcmd = new SqlCommand(updatePlayerPhoto, sqlcon) { CommandType = CommandType.StoredProcedure };
                sqlcmd.Parameters.AddWithValue("@Id", playerPhoto.Id);
                sqlcmd.Parameters.AddWithValue("@Photo", playerPhoto.Photo);
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                sqlcon.Close();
                sqlcon.Dispose();
                return false;
            }
            
        }
        //Delete PlayerPhoto
        public bool DeletePlayerPhotoDl(int id)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string deletePlayerPhoto = "usp_playerphoto_delete";
                sqlcmd = new SqlCommand(deletePlayerPhoto, sqlcon) { CommandType = CommandType.StoredProcedure };
                sqlcmd.Parameters.AddWithValue("@Id", id);
                int a = sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                if (a==0)
                {
                    return false;
                }
                else
                {
                    return true;
                }  
            }
            catch (Exception ex)
            {
                sqlcon.Close();
                sqlcon.Dispose();
                return false;
            }
            
        }
        //View PlayerPhoto
        public DataTable ViewPlayerPhotoDl()
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string viewPlayerPhoto = "usp_playerphoto_view";
            sqlcmd = new SqlCommand(viewPlayerPhoto, sqlcon) { CommandType = CommandType.StoredProcedure };
            sqlcmd.ExecuteNonQuery();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            DataTable dataTable = new DataTable();
            sqlda.Fill(dataTable);
            //sqlda.Update(dataTable);
            sqlcon.Close();
            return dataTable;
        }
        /*Player table methods*/
        //Insert Player
        public bool InsertPlayerDl(Player player)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string insertPlayer = "usp_player_insert";
                sqlcmd = new SqlCommand(insertPlayer, sqlcon) { CommandType = CommandType.StoredProcedure };
                string[] storedProceduresParameters = new string[] { "@Id", "@TeamId", "@Name", "@Age", "@SpecialityId", "@PhotoId" };
                ArrayList userData = new ArrayList() { player.Id, player.TeamId,player.Name, player.Age, player.SpecialityId, player.PhotoId};
                for (int i = 0; i < storedProceduresParameters.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(storedProceduresParameters[i], userData[i]);
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        //Update Player
        public bool UpdatePlayerDl(Player player)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string updatePlayer = "usp_player_update";
                sqlcmd = new SqlCommand(updatePlayer, sqlcon) { CommandType = CommandType.StoredProcedure };
                string[] storedProceduresParameters = new string[] { "@Id", "@TeamId", "@Name", "@Age", "@SpecialityId", "@PhotoId" };
                ArrayList userData = new ArrayList() { player.Id, player.TeamId, player.Name, player.Age, player.SpecialityId, player.PhotoId };
                for (int i = 0; i < storedProceduresParameters.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(storedProceduresParameters[i], userData[i]);
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        //Delete Player
        public bool DeletePlayerDl(int id)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            try
            {
                string DeletePlayer = "usp_player_delete";
                sqlcmd = new SqlCommand(DeletePlayer, sqlcon) { CommandType = CommandType.StoredProcedure };
                sqlcmd.Parameters.AddWithValue("@Id",id);
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        //View Player
        public DataTable ViewPlayerDl()
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string viewPlayer = "usp_player_view";
            sqlcmd = new SqlCommand(viewPlayer, sqlcon) { CommandType = CommandType.StoredProcedure };
            sqlcmd.ExecuteNonQuery();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            DataTable dataTable = new DataTable();
            sqlda.Fill(dataTable);
            sqlda.Update(dataTable);
            sqlcon.Close();
            return dataTable;
        }
        /*Statistics table methods*/
        //Insert statistics
        public bool InsertStatisticsDl(Statistics statistics)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string insertStats = "usp_allStatistics_insert";
            sqlcmd = new SqlCommand(insertStats, sqlcon) { CommandType = CommandType.StoredProcedure };
            string[] storedProcedureParameters = new string[] { "@id", "@TeamId", "@Played", "@Won", "@Lost", "@Tied", "@NoResult", "@NRR", "@Against_TeamId", "@Points" };
            ArrayList dataFromUser = new ArrayList() { statistics.Id, statistics.TeamId, statistics.Played, statistics.Won, statistics.Lost, statistics.Tied, statistics.NoResult, statistics.NetRR, statistics.AgainstTeamId, statistics.Points };
            try
            {
                for (int i = 0; i < storedProcedureParameters.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(storedProcedureParameters[i], dataFromUser[i]);
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            sqlcon.Close();
            sqlcon.Dispose();
            return false;
        }
        //Update statistics
        public bool UpdateStatisticsDl(Statistics statistics)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string updateStats = "usp_allStatistics_update";
            sqlcmd = new SqlCommand(updateStats, sqlcon) { CommandType = CommandType.StoredProcedure };
            string[] storedProcedureParameters = new string[] { "@id", "@TeamId", "@Played", "@Won", "@Lost", "@Tied", "@NoResult", "@NRR", "@Against_TeamId", "@Points" };
            ArrayList dataFromUser = new ArrayList() { statistics.Id, statistics.TeamId, statistics.Played, statistics.Won, statistics.Lost, statistics.Tied, statistics.NoResult, statistics.NetRR, statistics.AgainstTeamId, statistics.Points };
            try
            {
                for (int i = 0; i < storedProcedureParameters.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(storedProcedureParameters[i], dataFromUser[i]);
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            sqlcon.Close();
            sqlcon.Dispose();
            return false;
        }
        //Delete statistics
        public bool DeleteStatisticsDl(int statisticsId)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string deleteStats = "usp_allStatistics_delete";
            sqlcmd = new SqlCommand(deleteStats, sqlcon) { CommandType = CommandType.StoredProcedure };
            try
            {
                sqlcmd.Parameters.AddWithValue("@Id", statisticsId);
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            sqlcon.Close();
            sqlcon.Dispose();
            return false;
        }
        //View statistics
        public DataTable ViewStatisticsDl()
        {
            sqlcon = new SqlConnection(dbpath);
            string viewStats = "usp_allstatistics_teamname";
            sqlcmd = new SqlCommand(viewStats, sqlcon) { CommandType = CommandType.StoredProcedure };
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            DataTable dataTable = new DataTable(); //Using a data table do add the database data
            sqlda.Fill(dataTable); //Filling the data to data adapter using fill method
            return dataTable; //returnig data table to business layer
        }
        /*News table methods*/
        //Insert news
        public bool InsertNewsDl(News news)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string insertNews = "usp_News_insert";
            sqlcmd = new SqlCommand(insertNews, sqlcon) { CommandType = CommandType.StoredProcedure };
            string[] storedProcedureParameters = new string[] { "@Id", "@News_Date", "@MatchId", "@Description" };
            ArrayList dataFromUser = new ArrayList() { news.Id, news.NewsDate, news.MatchId, news.Description };
            try
            {
                for (int i = 0; i < storedProcedureParameters.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(storedProcedureParameters[i], dataFromUser[i]);
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        //Update news
        public bool UpdateNewsDl(News news)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string updateNews = "usp_News_update";
            sqlcmd = new SqlCommand(updateNews, sqlcon) { CommandType = CommandType.StoredProcedure };
            string[] storedProcedureParameters = new string[] { "@Id", "@News_Date", "@MatchId", "@Description" };
            ArrayList dataFromUser = new ArrayList() { news.Id, news.NewsDate, news.MatchId, news.Description };
            try
            {
                for (int i = 0; i < storedProcedureParameters.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(storedProcedureParameters[i], dataFromUser[i]);
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        //Delete News
        public bool DeleteNewsDl(int id)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string deleteNews = "usp_News_Delete";
            sqlcmd = new SqlCommand(deleteNews, sqlcon) { CommandType = CommandType.StoredProcedure };
            try
            {
                sqlcmd.Parameters.AddWithValue("@Id", id);
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        //View news
        public DataTable ViewNewsDl()
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string viewNews = "usp_News_View";
            sqlcmd = new SqlCommand(viewNews, sqlcon) { CommandType = CommandType.StoredProcedure };
            sqlcmd.ExecuteNonQuery();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            DataTable dataTable = new DataTable("News");
            sqlda.Fill(dataTable);
            sqlda.Update(dataTable);
            sqlcon.Close();
            return dataTable;
        }
        /*Match table methods*/
        //Insert match
        public bool InsertMatchDl(Matches match)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string insertMatch = "usp_match_insert";
            sqlcmd = new SqlCommand(insertMatch, sqlcon) { CommandType = CommandType.StoredProcedure };
            string[] storedProcedureParameters = new string[] { "@Id", "@TeamOneId", "@TeamTwoId", "@VenueId", "@ScheduleId", "@MatchPhoto" };
            ArrayList dataFromUser = new ArrayList() { match.Id, match.TeamOneId, match.TeamTwoId, match.VenueId, match.ScheduledId, match.MatchPhoto };
            try
            {
                for (int i = 0; i < storedProcedureParameters.Length; i++)
                {
                    sqlcmd.Parameters.AddWithValue(storedProcedureParameters[i], dataFromUser[i]);
                }
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        //Update match
        public bool UpdateMatchDl(Matches match)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string updateMatch = "usp_match_update";
            sqlcmd = new SqlCommand(updateMatch, sqlcon) { CommandType = CommandType.StoredProcedure };
            string[] storedProcedureParameters = new string[] { "@Id", "@TeamOneId", "@TeamTwoId", "@VenueId", "@ScheduleId", "@MatchPhoto" };
            ArrayList dataFromUser = new ArrayList() { match.Id, match.TeamOneId, match.TeamTwoId, match.VenueId, match.ScheduledId, match.MatchPhoto };
            
            for (int i = 0; i < storedProcedureParameters.Length; i++)
            {
                sqlcmd.Parameters.AddWithValue(storedProcedureParameters[i], dataFromUser[i]);
            }
            sqlcmd.ExecuteNonQuery();
            sqlcon.Close();
            sqlcon.Dispose();
            return true;
        }
        //Delete match
        public bool DeleteMatchDl(int matchId)
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string deleteMatch = "usp_match_delete";
            sqlcmd = new SqlCommand(deleteMatch, sqlcon) { CommandType = CommandType.StoredProcedure };
            try
            {
                sqlcmd.Parameters.AddWithValue("@Id", matchId);
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        //View match
        public DataTable ViewMatchDl()
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string viewMatch = "usp_match_view";
            sqlcmd = new SqlCommand(viewMatch, sqlcon) { CommandType = CommandType.StoredProcedure };
            sqlcmd.ExecuteNonQuery();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            DataTable dataTable = new DataTable();
            sqlda.Fill(dataTable);
            //sqlda.Update(dataTable);
            sqlcon.Close();
            return dataTable;
        }

        //Customer News selection button
        //public DataTable CustomerNewsDl()
        //{
        //    sqlcon = new SqlConnection(dbpath);
        //    sqlcon.Open();
        //    string viewMatch = "usp_news_matchphoto_view";
        //    sqlcmd = new SqlCommand(viewMatch, sqlcon) { CommandType = CommandType.StoredProcedure };
        //    sqlcmd.ExecuteNonQuery();
        //    SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
        //    DataTable dataTable = new DataTable("usp_news_matchphoto_view");
        //    sqlda.Fill(dataTable);
        //    sqlda.Update(dataTable);
        //    sqlcon.Close();
        //    return dataTable;
        //}
        public DataTable CustomerNewsDl()
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string customerViewNews = "customer_news_view";
            sqlcmd = new SqlCommand(customerViewNews, sqlcon) { CommandType = CommandType.StoredProcedure };
            sqlcmd.ExecuteNonQuery();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            DataTable dataTable = new DataTable(); //mind this
            sqlda.Fill(dataTable);
            sqlda.Update(dataTable);
            sqlcon.Close();
            return dataTable;
        }
        //customer_news_view
        // Customer Match Selection
        public DataTable CustomerMatchesDL()
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string viewMatch = "usp_customer_matches";
            sqlcmd = new SqlCommand(viewMatch, sqlcon) { CommandType = CommandType.StoredProcedure };
            sqlcmd.ExecuteNonQuery();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            DataTable dataTable = new DataTable();
            sqlda.Fill(dataTable);
            //sqlda.Update(dataTable);
            sqlcon.Close();
            return dataTable;
        }
        // Customer Statistics Selection
        public DataTable CustomerStatisticsDL()
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string viewMatch = "usp_allstatistics_teamname";
            sqlcmd = new SqlCommand(viewMatch, sqlcon) { CommandType = CommandType.StoredProcedure };
            sqlcmd.ExecuteNonQuery();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            DataTable dataTable = new DataTable("usp_allstatistics_teamname");
            sqlda.Fill(dataTable);
            sqlda.Update(dataTable);
            sqlcon.Close();
            return dataTable;
        }
        public DataTable CustomerSelectTeamDl() //This gives teams list
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string viewMatch = "usp_customer_teamnames";
            sqlcmd = new SqlCommand(viewMatch, sqlcon) { CommandType = CommandType.StoredProcedure };
            sqlcmd.ExecuteNonQuery();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            DataTable dataTable = new DataTable("usp_customer_teamnames");
            sqlda.Fill(dataTable);
            sqlda.Update(dataTable);
            //sqlcon.Close();
            return dataTable;
        }
        //public DataTable CustomerSelectPlayerDl(string teamid) //This gives team player list
        public DataTable CustomerSelectPlayerDl(string teamname) //This gives team player list
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string viewMatch = "usp_customer_team_selection";
            sqlcmd = new SqlCommand(viewMatch, sqlcon) { CommandType = CommandType.StoredProcedure };
            sqlcmd.Parameters.AddWithValue("@TeamName", teamname);
            sqlcmd.ExecuteNonQuery();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            DataTable dataTable = new DataTable();
            sqlda.Fill(dataTable);
            //sqlda.Update(dataTable);
            sqlcon.Close();
            return dataTable;
        }
        public DataTable CustomerPlayerDetails(string playerName) //This gives team player details
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string viewMatch = "usp_customer_player_selection";
            sqlcmd = new SqlCommand(viewMatch, sqlcon) { CommandType = CommandType.StoredProcedure };
            sqlcmd.Parameters.AddWithValue("@PlayerName", playerName);
            sqlcmd.ExecuteNonQuery();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            DataTable dataTable = new DataTable();
            sqlda.Fill(dataTable);
            sqlda.Update(dataTable);
            sqlcon.Close();
            return dataTable;
        }
        public DataTable CustomerTicketCategoryDl()
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string viewMatch = "customer_ticket_category";
            sqlcmd = new SqlCommand(viewMatch, sqlcon) { CommandType = CommandType.StoredProcedure };
            sqlcmd.ExecuteNonQuery();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            DataTable dataTable = new DataTable();
            sqlda.Fill(dataTable);
            sqlda.Update(dataTable);
            sqlcon.Close();
            return dataTable;
        }
        public DataTable CustomerSelectMatchDl()
        {
            sqlcon = new SqlConnection(dbpath);
            sqlcon.Open();
            string viewMatch = "usp_customer_match_selection";
            sqlcmd = new SqlCommand(viewMatch, sqlcon) { CommandType = CommandType.StoredProcedure };
            sqlcmd.ExecuteNonQuery();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            DataTable dataTable = new DataTable();
            sqlda.Fill(dataTable);
            sqlda.Update(dataTable);
            sqlcon.Close();
            return dataTable;
        }
        //public DataTable CustomerTeamSelectionDl()
        //{
        //    sqlcon = new SqlConnection(dbpath);
        //    sqlcon.Open();
        //    string customerselectsTeam = "usp_customer_teamnames";
        //    sqlcmd = new SqlCommand(customerselectsTeam, sqlcon) { CommandType = CommandType.StoredProcedure };
        //    //sqlcmd.Parameters.AddWithValue("@TeamName", teamName);
        //    sqlcmd.ExecuteNonQuery();
        //    SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
        //    DataTable dataTable = new DataTable("usp_customer_teamnames");
        //    sqlda.Fill(dataTable);
        //    //sqlda.Update(dataTable);
        //    sqlcon.Close();
        //    return dataTable;
        //}
        //public DataTable CustomerPlayerSelectionDl(string playerName)
        //{
        //    sqlcon = new SqlConnection(dbpath);
        //    sqlcon.Open();
        //    string customerSelectsPlayer = "usp_customer_player_selection";
        //    sqlcmd = new SqlCommand(customerSelectsPlayer, sqlcon) { CommandType = CommandType.StoredProcedure };
        //    sqlcmd.Parameters.AddWithValue("@PlayerName", playerName);
        //    sqlcmd.ExecuteNonQuery();
        //    SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
        //    DataTable dataTable = new DataTable($"usp_customer_player_selection @PlayerName='{playerName}'");
        //    sqlda.Fill(dataTable);
        //    sqlda.Update(dataTable);
        //    sqlcon.Close();
        //    return dataTable;
        //}
        //public DataTable CustomerPlayersofTeamDl(string teamname)
        //{
        //    sqlcon = new SqlConnection(dbpath);
        //    sqlcon.Open();
        //    string customerSelectsPlayer = "usp_customer_team_selection";
        //    sqlcmd = new SqlCommand(customerSelectsPlayer, sqlcon) { CommandType = CommandType.StoredProcedure };
        //    sqlcmd.Parameters.AddWithValue("@TeamName", teamname);
        //    sqlcmd.ExecuteNonQuery();
        //    SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
        //    DataTable dataTable = new DataTable($"usp_customer_team_selection @TeamName='{teamname}'");
        //    sqlda.Fill(dataTable);
        //    sqlda.Update(dataTable);
        //    sqlcon.Close();
        //    return dataTable;
        //}
    }
}
