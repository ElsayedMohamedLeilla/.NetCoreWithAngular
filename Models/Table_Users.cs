using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.Models
{
    public class Table_Users
    {

        public Table_Users()
        {
            IsEmailConfirmed = false;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        public string EmailConfirmationToken { get; set; }

        
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime AddedDate { get; set; }
    }
}
