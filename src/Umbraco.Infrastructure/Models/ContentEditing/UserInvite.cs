﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using Umbraco.Composing;
using Umbraco.Core;

namespace Umbraco.Web.Models.ContentEditing
{
    /// <summary>
    /// Represents the data used to invite a user
    /// </summary>
    [DataContract(Name = "user", Namespace = "")]
    public class UserInvite : EntityBasic, IValidatableObject
    {
        [DataMember(Name = "userGroups")]
        [Required]
        public IEnumerable<string> UserGroups { get; set; }

        [DataMember(Name = "email", IsRequired = true)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (UserGroups.Any() == false)
                yield return new ValidationResult("A user must be assigned to at least one group", new[] { nameof(UserGroups) });

            if (Current.Configs.Security().UsernameIsEmail == false && Username.IsNullOrWhiteSpace())
                yield return new ValidationResult("A username cannot be empty", new[] { nameof(Username) });
        }
    }
}