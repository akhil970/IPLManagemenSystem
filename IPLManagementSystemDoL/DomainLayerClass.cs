using System;
using System.Drawing; //for inserting images
namespace IPLManagementSystemDoL
{
    public class UserLogin
    {
        public string userName { set; get; }
        public string password { set; get; }
    }
    public class User
    {
        public int UserId { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }
        public string Firstname { set; get; }
        public string Lastname { set; get; }
    }
    public class Roles
    {
        public int RoleId { set; get; }
        public string Name { set; get; }
    }
    public class UserRoles
    {
        public int UserId { set; get; }
        public int RoleId { set; get; }
    }
    public class Team
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string HomeGround { set; get; }
        public string FranchiseOwner { set; get; }
        public string LogoImage { set; get; }
    }
    public class Player
    {
        public int Id { set; get; }
        public int TeamId { set; get; }
        public string Name { set; get; }
        public int Age { set; get; }
        public int SpecialityId { set; get; }
        public int PhotoId { set; get; } 
    }
    public class PlayerPhoto
    {
        public int Id { set; get; }
        public string Photo { set; get; }
    }
    public class Speciality
    {
        public int Id { set; get; }
        public string Description { set; get; }
    }
    public class Matches
    {
        public int Id { set; get; }
        public int TeamOneId { set; get; }
        public int TeamTwoId { set; get; }
        public int VenueId { set; get; }
        public int ScheduledId { set; get; }
        public string MatchPhoto { set; get; }
    }
    // Venue: Id, Location, Description
    public class Venue
    {
        public int Id { set; get; }
        public string Location { set; get; }
        public string Description { set; get; }
    }
    //Schedule: Id, MatchId, VenueId, Date, Start Time, End Time
    public class Schedule
    {
        public int Id { set; get; }
        public int? MatchId { set; get; }
        public int VenueId { set; get; }
        public DateTime Date { set; get; }
        public DateTime StartTime { set; get; }
        public DateTime EndTime { set; get; }
    }
    //  Ticket: Id, MatchId, CategoryId, Price
    public class Ticket
    {
        public int Id { set; get; }
        public int MatchId { set; get; }
        public int CategoryId { set; get; }
        public decimal Price { set; get; }

    }
    //TicketCategory: Id, Name, Description
    public class TicketCategory
    {
        public int Id { set; get; }
        public string Name { set; get; }
    }
    // News :- Id, NewsDate, MatchId, Description, MatchPhotoId
    public class News
    {
        public int Id { set; get; }
        public DateTime NewsDate { set; get; }
        public int MatchId { set; get; }
        public string Description { set; get; }

    }
    //Statistics: TeamId, Played, Won, Lost, Tied, N/R, NetRR, For Against, Pts, From
    public class Statistics
    {
        public int Id { set; get; }
        public int TeamId { set; get; }
        public int Played { set; get; }
        public int Won { set; get; }
        public int Lost { set; get; }
        public int Tied { set; get; }
        public int NoResult { set; get; }
        public double NetRR { set; get; }
        public int AgainstTeamId{ set; get; }
        public int Points { set; get; }
    }
}
