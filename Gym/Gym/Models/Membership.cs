using System.ComponentModel.DataAnnotations;

namespace Gym.Models
{
    public class Membership
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime RegisterDate { get; set; }

        [Required]
        public DateTime ExpireDate { get; set; }
        
        
        public int NbOfDaysRemaining()
        {
            int nbOfDaysRemaining = (ExpireDate - DateTime.Today).Days;
            return (nbOfDaysRemaining > 0) ? nbOfDaysRemaining : 0;
        }

        public bool IsActive()
        {
            return NbOfDaysRemaining() > 0;
        }
    }
}
