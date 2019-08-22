using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BtcExchanger.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace BtcExchanger.Tests
{
    public class BtcVerificationTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public BtcVerificationTest () {
            _server = new TestServer (new WebHostBuilder ()
                .UseStartup<Startup> ());
            _client = _server.CreateClient ();
        }

        [Fact]
        public async Task CreateTransactionAndVerifyCode() {
            // Act
            var item = new Transaction { btc_quantity = 0.0001, account_number = "0000000000000000000000", email = "batman@gotham.city" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync ("/api/transaction", stringContent);
            // Assert
            Assert.Equal (HttpStatusCode.Created, response.StatusCode);
            // Act
            var verificationItem = new Verification {TransactionId = 1 , verification_code = "1234"};
            stringContent = new StringContent (JsonConvert.SerializeObject (verificationItem), Encoding.UTF8, "application/json");
            response = await _client.PutAsync("/api/verification",stringContent); 
            // Assert          
            Assert.Equal (HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task PutReturnsBadRequestForEmptyVerificationCode() {
            // Act
            var item = new Transaction { btc_quantity = 0.0001, account_number = "0000000000000000000000", email = "batman@gotham.city" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync ("/api/transaction", stringContent);
            // Assert
            Assert.Equal (HttpStatusCode.Created, response.StatusCode);
            // Act
            var verificationItem = new Verification {TransactionId = 1 };
            stringContent = new StringContent (JsonConvert.SerializeObject (verificationItem), Encoding.UTF8, "application/json");
            response = await _client.PutAsync("/api/verification",stringContent); 
            // Assert          
            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task PutReturnsNotFoundForItemNotFound() {
            // Act
            var verificationItem = new Verification {TransactionId = 1 , verification_code = "1234"};
            var stringContent = new StringContent (JsonConvert.SerializeObject (verificationItem), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("/api/verification",stringContent); 
            // Assert          
            Assert.Equal (HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task PutReturnsNotFoundForEmptyTransactionId() {
            // Act
            var verificationItem = new Verification {verification_code = "1234"};
            var stringContent = new StringContent (JsonConvert.SerializeObject (verificationItem), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("/api/verification",stringContent); 
            // Assert          
            Assert.Equal (HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task PutReturnsBadRequestForWrongVerifactionCode() {
            // Act
            var item = new Transaction { btc_quantity = 0.0001, account_number = "0000000000000000000000", email = "batman@gotham.city" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync ("/api/transaction", stringContent);
            // Assert
            Assert.Equal (HttpStatusCode.Created, response.StatusCode);
            // Act
            var verificationItem = new Verification {TransactionId = 1 , verification_code = "4321"};
            stringContent = new StringContent (JsonConvert.SerializeObject (verificationItem), Encoding.UTF8, "application/json");
            response = await _client.PutAsync("/api/verification",stringContent); 
            // Assert          
            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PutReturnsBadRequestForSecondAtemptToVerifaction() {
            // Act
            var item = new Transaction { btc_quantity = 0.0001, account_number = "0000000000000000000000", email = "batman@gotham.city" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync ("/api/transaction", stringContent);
            // Assert
            Assert.Equal (HttpStatusCode.Created, response.StatusCode);
            // Act
            var verificationItem = new Verification {TransactionId = 1 , verification_code = "1234"};
            stringContent = new StringContent (JsonConvert.SerializeObject (verificationItem), Encoding.UTF8, "application/json");
            response = await _client.PutAsync("/api/verification",stringContent); 
            // Assert          
            Assert.Equal (HttpStatusCode.OK, response.StatusCode);
            // Act
            stringContent = new StringContent (JsonConvert.SerializeObject (verificationItem), Encoding.UTF8, "application/json");
            response = await _client.PutAsync("/api/verification",stringContent); 
            // Assert          
            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task CreateTransactionAndVerifyCodeShouldUpdateStatus() {
            var item = new Transaction { btc_quantity = 0.0001, account_number = "0000000000000000000000", email = "batman@gotham.city" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync ("/api/transaction", stringContent);
            Assert.Equal (HttpStatusCode.Created, response.StatusCode);

            string responseBody = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseBody);
            Assert.Equal ("Waiting for verification.", json["status"]);

            var verificationItem = new Verification {TransactionId = 1 , verification_code = "1234"};
            stringContent = new StringContent (JsonConvert.SerializeObject (verificationItem), Encoding.UTF8, "application/json");
            response = await _client.PutAsync("/api/verification",stringContent);
            // Assert
            Assert.Equal (HttpStatusCode.OK, response.StatusCode);

            responseBody = await response.Content.ReadAsStringAsync();
            json = JObject.Parse(responseBody);
            Assert.Equal ("Waiting for money transfer.", json["status"]);
        }
    }
}
