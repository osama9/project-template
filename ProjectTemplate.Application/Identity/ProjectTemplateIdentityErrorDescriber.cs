using Microsoft.AspNetCore.Identity;
using ProjectTemplate.Core.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTemplate.Application.Identity
{
    public class ProjectTemplateIdentityErrorDescriber : IdentityErrorDescriber
    {

        //public override IdentityError ConcurrencyFailure()
        //{
        //    return new IdentityError
        //    {
        //        Code = nameof(ConcurrencyFailure),
        //        Description = "Optimistic concurrency failure, object has been modified."
        //    };
        //}

        public override IdentityError DefaultError()
        {
            return new IdentityError
            {
                Code = nameof(DefaultError),
                //Description = $"An unknown failure has occurred."
                Description = $"{MessageText.AnUnknownFailureHasOccurred}"
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = string.Format($"{MessageText.EmailIsAlreadyTaken}", email)
            };
        }

        public override IdentityError DuplicateRoleName(string role)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateRoleName),
                //Description = $"Role name '{role}' is already taken."
                Description = string.Format($"{MessageText.RoleIsAlreadyTaken}", role)
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = string.Format($"{MessageText.UserNameIsAlreadyTaken}", userName)
            };
        }

        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(InvalidEmail),
                Description = string.Format($"{MessageText.InvalidEmail}", email)
            };
        }

        public override IdentityError InvalidRoleName(string role)
        {
            return new IdentityError
            {
                Code = nameof(InvalidRoleName),
                //Description = $"Role name '{role}' is invalid."
                Description = string.Format($"{MessageText.InvalidRole}", role)
            };
        }

        public override IdentityError InvalidToken()
        {
            return new IdentityError
            {
                Code = nameof(InvalidToken),
                //Description = "Invalid token."
                Description = $"{MessageText.InvalidToken}"
            };
        }

        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(InvalidUserName),
                Description = string.Format($"{MessageText.InvlaidUserName}", userName)
            };
        }

        public override IdentityError LoginAlreadyAssociated()
        {
            return new IdentityError
            {
                Code = nameof(LoginAlreadyAssociated),
                //Description = "A user with this login already exists."
                Description = $"{MessageText.AUserWithThisLoginAlreadyExists}"
            };
        }

        public override IdentityError PasswordMismatch()
        {
            return new IdentityError
            {
                Code = nameof(PasswordMismatch),
                //Description = "Incorrect password."
                Description = $"{MessageText.IncorrectPassword}"
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                //Description = "Passwords must have at least one digit ('0'-'9')."
                Description = $"{MessageText.PasswordsMustHaveAtLeastOneDigit}"
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresLower),
                //Description = "Passwords must have at least one lowercase ('a'-'z')."
                Description = $"{MessageText.PasswordsMustHaveAtLeastOneLowercase}"
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                //Description = "Passwords must have at least one non alphanumeric character."
                Description = $"{MessageText.PasswordsMustHaveAtLeastOneNonAlphanumericCharacter}"
            };
        }

        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
        {
            //Passwords must use at least 'uniqueChars' different characters.
            return new IdentityError
            {
                Code = nameof(PasswordTooShort),
                Description = string.Format($"{MessageText.PasswordMustUseAtLeastDifferenctCharacters}", uniqueChars)
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                //Description = "Passwords must have at least one uppercase ('A'-'Z')."
                Description = $"{MessageText.PasswordsMustHaveAtLeastOneUppercase}"
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = nameof(PasswordTooShort),
                Description = string.Format($"{MessageText.PasswordTooShort}", length)
            };
        }

        public override IdentityError UserAlreadyHasPassword()
        {
            return new IdentityError
            {
                Code = nameof(UserAlreadyHasPassword),
                //Description = "User already has a password set."
                Description = $"{MessageText.UserAlreadyHasAPasswordSet}"
            };
        }

        public override IdentityError UserAlreadyInRole(string role)
        {
            return new IdentityError
            {
                Code = nameof(UserAlreadyInRole),
                //Description = $"User already in role '{role}'."
                Description = string.Format($"{MessageText.UserAlreadyInRole}", role)
            };
        }

        public override IdentityError UserLockoutNotEnabled()
        {
            return new IdentityError
            {
                Code = nameof(UserLockoutNotEnabled),
                //Description = "Lockout is not enabled for this user."
                Description = $"{MessageText.LockoutIsNotEnabledForThisUser}"
            };
        }

        public override IdentityError UserNotInRole(string role)
        {
            return new IdentityError
            {
                Code = nameof(UserNotInRole),
                //Description = $"User is not in role '{role}'."
                Description = string.Format($"{MessageText.UserIsNotInRole}", role)
            };
        }

    }
}
