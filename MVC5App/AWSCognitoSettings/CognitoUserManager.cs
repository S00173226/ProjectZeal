using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVC5App.AWSCognitoSettings
{
    public class CognitoUserManager : OAuthAuthorizationServerProvider
    {
        private readonly AmazonCognitoIdentityProviderClient _client =
            new AmazonCognitoIdentityProviderClient();
        private readonly string _clientId = ConfigurationManager.AppSettings["CLIENT_ID"];
        private readonly string _poolId = ConfigurationManager.AppSettings["USERPOOL_ID"];
        private string accessToken;
        

        
        public CognitoUserManager(//IUserStore<CognitoUser> store
            )
            //: base(store)
        {
        }
        

        //public override Task<bool> CheckPasswordAsync(CognitoUser user, string password)
        //{
        //    return CheckPasswordAsync(user.UserName, password);
        //}

        private async Task<bool> CheckPasswordAsync(string userName, string password)
        {
            try
            {
                var authReq = new AdminInitiateAuthRequest
                {
                    UserPoolId = ConfigurationManager.AppSettings["USERPOOL_ID"],
                    ClientId = ConfigurationManager.AppSettings["CLIENT_ID"],
                    AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH
                };
                authReq.AuthParameters.Add("USERNAME", userName);
                authReq.AuthParameters.Add("PASSWORD", password);

                AdminInitiateAuthResponse authResp = await _client.AdminInitiateAuthAsync(authReq);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> GetCredsAsync(string userID, string password)
        {
            
            AmazonCognitoIdentityProviderClient provider =
                new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials());
            CognitoUserPool userPool = new CognitoUserPool(_poolId, _clientId, provider);
            CognitoUser user = new CognitoUser(userID, _clientId, userPool, provider);
            
            InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest()
            {
                Password = password
            };
            try
            {
                AuthFlowResponse authResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);
                accessToken = authResponse.AuthenticationResult.AccessToken;
            }
            catch
            {
                accessToken = null;
            }
            
                

            return accessToken;

        }

        public async Task<bool> VerifyUser(string verifyCode, string UserID)
        {
            bool userVerified;

            try
            {
                Amazon.CognitoIdentityProvider.Model.ConfirmSignUpRequest confirmRequest = new ConfirmSignUpRequest()
                {
                    Username = UserID,
                    ClientId = _clientId,
                    ConfirmationCode = verifyCode
                };

                var confirmResult = await _client.ConfirmSignUpAsync(confirmRequest);
                userVerified = true;
            }
            catch 
            {
                userVerified = false;
            }
            return userVerified;
        }

    }

    
}