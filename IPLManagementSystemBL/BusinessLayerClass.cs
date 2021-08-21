using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using IPLManagementSystemDL; // Referring Data Layer
using IPLManagementSystemDoL; // Referring Domain Layer
//This is Business layer
namespace IPLManagementSystemBL
{
    public class BusinessLayerClass
    {
        DataLayerClass dataLayerClass = new DataLayerClass();
        public object UserLoginBl (UserLogin userlogin)
        {
            object solutionFromDatalayer = dataLayerClass.UserLoginDl(userlogin);
            return solutionFromDatalayer;
        }

        //Here Starts Methods for Team table
        public bool InsertTeamBl(Team team)
        {
            bool answer = dataLayerClass.InsertTeamDl(team);
            return answer;
        }
        public bool UpdateTeamBl(Team team)
        {
            bool answer = dataLayerClass.UpdateTeamDl(team);
            return answer;
        }
        public bool DeleteTeamBl(int id)
        {
            bool answer = dataLayerClass.DeleteTeamDl(id);
            return answer;
        }
        public DataTable ViewTeamBl()
        {
            DataTable answer = dataLayerClass.ViewTeamDl();
            return answer;
        }
        //Here Starts Methods for Match table
        public bool InsertMatchBl(Matches match)
        {
            bool insertedMatch = dataLayerClass.InsertMatchDl(match);
            return insertedMatch;
        }
        public bool UpdateMatchBl(Matches match)
        {
            bool updatedMatch = dataLayerClass.UpdateMatchDl(match);
            return updatedMatch;
        }
        public bool DeleteMatchBl(int id)
        {
            bool deletedMatch = dataLayerClass.DeleteMatchDl(id);
            return deletedMatch;
        }
        public DataTable ViewMatchBl()
        {
            DataTable viewMatch = dataLayerClass.ViewMatchDl();
            return viewMatch;
        }
        //Here Starts Methods for Player table
        public bool InsertPlayerBl(Player player)
        {
            bool answer = dataLayerClass.InsertPlayerDl(player);
            return answer;
        }
        public bool UpdatePlayerBl(Player player)
        {
            bool answer = dataLayerClass.UpdatePlayerDl(player);
            return answer;
        }
        public bool DeletePlayerBl(int id)
        {
            bool answer = dataLayerClass.DeletePlayerDl(id);
            return answer;
        }
        public DataTable ViewPlayerBl()
        {
            DataTable answer = dataLayerClass.ViewPlayerDl();
            return answer;
        }
        //Here starts methods for Schedule table
        public bool InsertScheduleBl(Schedule schedule)
        {
            bool answer = dataLayerClass.InsertScheduleDl(schedule);
            return answer;
        }
        public bool UpdateScheduleBl(Schedule schedule)
        {
            bool answer = dataLayerClass.UpdateScheduleDl(schedule);
            return answer;
        }
        public bool DeleteScheduleBl(int id)
        {
            bool answer = dataLayerClass.DeleteScheduleDl(id);
            return answer;
        }
        public DataTable ViewScheduleBl()
        {
            DataTable answer = dataLayerClass.ViewScheduleDl();
            return answer;
        }
       // Here starts methods for statistics
        public bool InsertStatisticsBl(Statistics statistics)
        {
            bool insertedStatistics = dataLayerClass.InsertStatisticsDl(statistics);
            return insertedStatistics;
        }
        public bool UpdateStatisticsBl(Statistics statistics)
        {
            bool updatedStatistics = dataLayerClass.UpdateStatisticsDl(statistics);
            return updatedStatistics;
        }
        public bool DeleteStatisticsBl(int Id)
        {
            bool deletedStatistics = dataLayerClass.DeleteStatisticsDl(Id);
            return deletedStatistics;
        }
        public DataTable ViewStatisticsBl()
        {
            DataTable viewStatistics = dataLayerClass.ViewStatisticsDl();
            return viewStatistics;
        }
        //Here starts methods for tickets
        public bool InsertTicketBl(Ticket ticket)
        {
            bool answer = dataLayerClass.InsertTicketDl(ticket);
            return answer;
        }
        public bool UpdateTicketBl(Ticket ticket)
        {
            bool answer = dataLayerClass.UpdateTicketDl(ticket);
            return answer;
        }
        public bool DeleteTicketBl(int id)
        {
            bool answer = dataLayerClass.DeleteTicketDl(id);
            return answer;
        }
        public DataTable ViewTicketBl()
        {
            DataTable answer = dataLayerClass.ViewTicketDl();
            return answer;
        }
        //Here starts methods for Ticket category table
        public bool InsertTicketCategoryBl(TicketCategory ticketCategory)
        {
            bool answer = dataLayerClass.InsertTicketCategoryDl(ticketCategory);
            return answer;
        }
        public bool UpdateTicketCategoryBl(TicketCategory ticketCategory)
        {
            bool answer = dataLayerClass.UpdateTicketCategoryDl(ticketCategory);
            return answer;
        }
        public bool DeleteTicketCategoryBl(int id)
        {
            bool answer = dataLayerClass.DeleteTicketCategoryDl(id);
            return answer;
        }
        public DataTable ViewTicketCategoryBl()
        {
            DataTable answer = dataLayerClass.ViewTicketCategoryDl();
            return answer;
        }
        //Here starts methods for venue table
        public bool InsertVenueBl(Venue venue)
        {
            bool answer = dataLayerClass.InsertVenueDl(venue);
            return answer;
        }
        public bool UpdateVenueBl(Venue venue)
        {
            bool answer = dataLayerClass.UpdateVenueDl(venue);
            return answer;
        }
        public bool DeleteVenueBl(int id)
        {
            bool answer = dataLayerClass.DeleteVenueDl(id);
            return answer;
        }
        public DataTable ViewVenueBl()
        {
            DataTable answer = dataLayerClass.ViewVenueDl();
            return answer;
        }
        //Here starts methods for Users table
        public bool InsertUserBl(User user)
        {

            bool answer = dataLayerClass.InsertUserDl(user);
            return answer;
        }
        public bool UpdateUserBl(User user)
        {

            bool answer = dataLayerClass.UpdateUserDl(user);
            return answer;
        }
        //public bool DeleteUserBl(int id)
        //{

        //    bool answer = dataLayerClass.DeleteUser(id);
        //    return answer;
        //}
        public DataTable ViewUserBl()
        {
            DataTable answer = dataLayerClass.ViewUserDl();
            return answer;
        }
        //Here starts methods for roles table
        public bool InsertRoleBl(Roles roles)
        {

            bool answer = dataLayerClass.InsertRoleDl(roles);
            return answer;
        }
        public bool UpdateRoleBl(Roles roles)
        {

            bool answer = dataLayerClass.UpdateRoleDl(roles);
            return answer;
        }
        //public bool DeleteUserRolesBl(int id)
        //{

        //    bool answer = dataLayerClass.DeleteUserRoleDl(id);
        //    return answer;
        //}
        public DataTable ViewRoleBl()
        {

            DataTable answer = dataLayerClass.ViewRoleDl();
            return answer;
        }
        //Here starts methods for user roles table
        public bool InsertUserRolesBl(UserRoles userroles)
        {

            bool answer = dataLayerClass.InsertUserRoleDl(userroles);
            return answer;
        }
        public bool UpdateUserRolesBl(UserRoles userroles)
        {

            bool answer = dataLayerClass.UpdateUserRoleDl(userroles);
            return answer;
        }
        //public bool DeleteUserRolesBl(int id)
        //{

        //    bool answer = dataLayerClass.DeleteUserRoleDl(id);
        //    return answer;
        //}
        public DataTable ViewUserRolesBl()
        {

            DataTable answer = dataLayerClass.ViewUserRoleDl();
            return answer;
        }
        //Here starts methods for playerphoto table
        public bool InsertPlayerPhotoBl(PlayerPhoto playerphoto)
        {

            bool answer = dataLayerClass.InsertPlayerPhotoDl(playerphoto);
            return answer;
        }
        public bool UpdatePlayerPhotoBl(PlayerPhoto playerphoto)
        {

            bool answer = dataLayerClass.UpdatePlayerPhotoDl(playerphoto);
            return answer;
        }
        public bool DeletePlayerPhotoBl(int id)
        {

            bool answer = dataLayerClass.DeletePlayerPhotoDl(id);
            return answer;
        }
        public DataTable ViewPlayerPhotoBl()
        {
            DataTable answer = dataLayerClass.ViewPlayerPhotoDl();
            return answer;
        }
        //Here starts the methods for speciality
        public bool InsertSpecialityBl(Speciality speciality)
        {

            bool answer = dataLayerClass.InsertSpecialityDl(speciality);
            return answer;
        }
        public bool UpdateSpecialityBl(Speciality speciality)
        {

            bool answer = dataLayerClass.UpdateSpecialityDl(speciality);
            return answer;
        }
        public bool DeleteSpecialityBl(int id)
        {
            bool answer = dataLayerClass.DeleteSpecialityDl(id);
            return answer;
        }
        public DataTable ViewSpecialityBl()
        {

            DataTable answer = dataLayerClass.ViewSpecialityDl();
            return answer;
        }
        //Here starts the methods for News
        public bool InsertNewsBl(News news)
        {
            bool insertedNews = dataLayerClass.InsertNewsDl(news);
            return insertedNews;
        }
        public bool UpdateNewsBl(News news)
        {
            bool updatedNews = dataLayerClass.UpdateNewsDl(news);
            return updatedNews;
        }
        public bool DeleteNewsBl(int id)
        {
            bool deletedNews = dataLayerClass.DeleteNewsDl(id);
            return deletedNews;
        }
        public DataTable ViewNewsBl()
        {
            DataTable viewNews = dataLayerClass.ViewNewsDl();
            return viewNews;
        }
        public DataTable CustomerNewsBl()
        {
            DataTable customerViewNews = dataLayerClass.CustomerNewsDl();
            return customerViewNews;
        }
        public DataTable CustomerMatchesBl()
        {
            DataTable customerviewMatches = dataLayerClass.CustomerMatchesDL();
            return customerviewMatches;
        }
        public DataTable CustomerStatisticsBl()
        {
            DataTable customerViewStatistics = dataLayerClass.CustomerStatisticsDL();
            return customerViewStatistics;
        }
        public DataTable CustomerSelectteamBl()  //This gives teams list
        {
            DataTable answer = dataLayerClass.CustomerSelectTeamDl();
            return answer;
        }
        public DataTable CustomerSelectPlayerBl(string teamname) //This gives selected team players list
        {
            DataTable answer = dataLayerClass.CustomerSelectPlayerDl(teamname);
            return answer;
        }
        public DataTable CustomerPlayerDetailsBl(string playername) //This gives selected team player details
        {
            DataTable answer = dataLayerClass.CustomerPlayerDetails(playername);
            return answer;
        }
        public DataTable CustomerTicketCategoryBl()
        {
            DataTable answer = dataLayerClass.CustomerTicketCategoryDl();
            return answer;
        }
        public DataTable CustomerSelectMatchBl()
        {
            DataTable answer = dataLayerClass.CustomerSelectMatchDl();
            return answer;
        }
        //public DataTable CustomerTeamSelectionBl()
        //{
        //    DataTable customerSelectsTeam = dataLayerClass.CustomerTeamSelectionDl();
        //    return customerSelectsTeam;
        //}
        //public DataTable CustomerPlayersofTeamBl(string playername)
        //{
        //    DataTable customerSelectsPlayersofTeam = dataLayerClass.CustomerPlayersofTeamDl(playername);
        //    return customerSelectsPlayersofTeam;
        //}
        //public DataTable CustomerPlayerSelectionBl(string playername)
        //{
        //    DataTable customerSelectsPlayer = dataLayerClass.CustomerPlayerSelectionDl(playername);
        //    return customerSelectsPlayer;
        //}
    }
}
