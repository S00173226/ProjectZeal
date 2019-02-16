using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVC5App.AWSCognitoSettings
{
    public class CognitoUserStore : IUserStore<CognitoUser>,
                                    IUserLockoutStore<CognitoUser, string>,
                                    IUserTwoFactorStore<CognitoUser, string>
    {
        private readonly AmazonCognitoIdentityProviderClient _client =
            new AmazonCognitoIdentityProviderClient();
        private readonly string _clientId = ConfigurationManager.AppSettings["CLIENT_ID"];
        private readonly string _poolId = ConfigurationManager.AppSettings["USERPOOL_ID"];


        //public async Task<SignUpResponse> CreateAsync(CognitoUser user)
        //{
        //    // Register the user using Cognito
        //    var signUpRequest = new SignUpRequest
        //    {
        //        ClientId = ConfigurationManager.AppSettings["CLIENT_ID"],
        //        Password = user.Password,
        //        Username = user.Email,

        //    };

        //    var emailAttribute = new AttributeType
        //    {
        //        Name = "email",
        //        Value = user.Email
        //    };
        //    signUpRequest.UserAttributes.Add(emailAttribute);
        //    Console.WriteLine("Done");
        //    var response = await _client.SignUpAsync(signUpRequest);
        //    return response;
        //}

        public Task<SignUpResponse> CreateAsync(CognitoUser user)
        {
            // Register the user using Cognito
            var signUpRequest = new SignUpRequest
            {
                ClientId = ConfigurationManager.AppSettings["CLIENT_ID"],
                Password = user.Password,
                Username = user.Email,

            };

            var emailAttribute = new AttributeType
            {
                Name = "email",
                Value = user.Email
            };
            signUpRequest.UserAttributes.Add(emailAttribute);

            return _client.SignUpAsync(signUpRequest);
        }

        public Task DeleteAsync(CognitoUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<CognitoUser> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<CognitoUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetAccessFailedCountAsync(CognitoUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetLockoutEnabledAsync(CognitoUser user)
        {
            throw new NotImplementedException();
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(CognitoUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetTwoFactorEnabledAsync(CognitoUser user)
        {
            throw new NotImplementedException();
        }

        public Task<int> IncrementAccessFailedCountAsync(CognitoUser user)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(CognitoUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEnabledAsync(CognitoUser user, bool enabled)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEndDateAsync(CognitoUser user, DateTimeOffset lockoutEnd)
        {
            throw new NotImplementedException();
        }

        public Task SetTwoFactorEnabledAsync(CognitoUser user, bool enabled)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CognitoUser user)
        {
            throw new NotImplementedException();
        }

        Task IUserStore<CognitoUser, string>.CreateAsync(CognitoUser user)
        {
            throw new NotImplementedException();
        }
    }

    public class CognitoUser : IdentityUser
    {
        public string Password { get; set; }
        public UserStatusType Status { get; set; }
    }
}