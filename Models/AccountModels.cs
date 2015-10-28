using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using System.Linq;
namespace Testloginapp1.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<webpages_Membership> webpages_Memberships { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
   
       
    }
    [Table("webpages_Membership")]
    public class webpages_Membership
    {
        [Key]
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string ConfirmationToken { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime LastPasswordFailureDate { get; set; }
        public int PasswordFailuresSinceLastSuccess { get; set; }
        public string Password { get; set; }
        public DateTime PasswordChangeDate { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordVerificationToken { get; set; }
        public DateTime PasswordVerificationTokenExpirationDate { get; set; }
    }
    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class UserModel
    {

        public int Id { get; set; }
        [Display(Name = "UserName")]
        public string uname { get; set; }
        [Display(Name = "CheckBox")]
        public bool chkbx { get; set; }
        public bool status { get; set; }
        public List<Table> UserTables { get; set; }
    }
    public interface IUserRepository
    {
        IEnumerable<UserModel> GetUsers();
        UserModel GetUserById(int Id);
        void InsertUser(UserModel user);
        void DeleteUser(int userId);
        void UpdateUser(UserModel user);
    }
    public class UserRepository : IUserRepository
    {
        private DataClasses1DataContext _dataContext;
        public UserRepository()
        {
            _dataContext = new DataClasses1DataContext();
        }

        public void InsertUser(UserModel user)
        {
            var userData = new Table()
            {
                Name = user.uname,
                chkbx = (user.chkbx).ToString()
            };
            _dataContext.Tables.InsertOnSubmit(userData);
            _dataContext.SubmitChanges();
        }
        public IEnumerable<UserModel> GetUsers()
        {
            IList<UserModel> userList = new List<UserModel>();
            var query = from user in _dataContext.Tables
                        select user;
            var users = query.ToList();
            foreach (var userData in users)
            {
                userList.Add(new UserModel()
                {
                    Id = userData.Id,
                    uname = userData.Name,
                    chkbx = Convert.ToBoolean(userData.chkbx),
                    status = Convert.ToBoolean(userData.Status)
                });
            }
            return userList;
        }
        public UserModel GetUserById(int Id)
        {
            var query = from u in _dataContext.Tables
                        where u.Id == Id
                        select u;
            var user = query.FirstOrDefault();
            var model = new UserModel()
            {

                uname = user.Name,
                chkbx = Convert.ToBoolean(user.chkbx),
                status = Convert.ToBoolean(user.Status)
            };
            return model;
        }
        public void DeleteUser(int userId)
        {
            Table user = _dataContext.Tables.Where(u => u.Id == userId).SingleOrDefault();
            _dataContext.Tables.DeleteOnSubmit(user);
            _dataContext.SubmitChanges();
        }
        public void UpdateUser(UserModel user)
        {
            Table userData = _dataContext.Tables.Where(u => u.Id == user.Id).SingleOrDefault();
            userData.Name = user.uname;
            userData.chkbx = (user.chkbx).ToString();
            userData.Status = (user.status).ToString();
            _dataContext.SubmitChanges();
        }
    }
    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

       

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "email")]
        public string EmailId { get; set; }

       
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
