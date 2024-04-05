using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (check_Name(firstName) || check_Name(lastName))
            {
                return false;
            }

            if (check_Email(email))
            {
                return false;
            }
            
            var age = calculate_Age(dateOfBirth);
            if (age < 21)
            {
                return false;
            }

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
            using (var userCreditService = new UserCreditService())
            {
                switch (client.Type)
                {
                    case "VeryImportantClient":
                    {
                        user.HasCreditLimit = false;
                        break;
                    }
                    case "ImportantClient":
                    {
                        int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                        creditLimit = creditLimit * 2;
                        user.CreditLimit = creditLimit;
                        break;
                    }
                    default:
                    {
                        user.HasCreditLimit = true;
                        int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                        user.CreditLimit = creditLimit;
                        break;
                    }
                        
                }
            }

            if (check_CreditLimit(user))
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private static bool check_Name(string name)
        {
            return string.IsNullOrEmpty(name);
        }

        private static bool check_Email(string email)
        {
            return !email.Contains("@") && !email.Contains(".");
        }

        private static int calculate_Age(DateTime dateOfBirth)
        {
            DateTime now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            return age;

        }

        private static bool check_CreditLimit(User user)
        {
            return user.HasCreditLimit && user.CreditLimit < 500;
        }

    }
}
