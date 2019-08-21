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
    public class BtcExchangerTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public BtcExchangerTest () {
            _server = new TestServer (new WebHostBuilder ()
                .UseStartup<Startup> ());
            _client = _server.CreateClient ();
        }

        [Fact]
        public async Task GetReturnsNotFoundForMissingIdValue () {
            // Act
            var response = await _client.GetAsync ("/api/transaction/1");
            // Assert
            Assert.Equal (HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetReturnsOkForCorrectIdValue () {
            var item = new Transaction { btc_quantity = 0.0001, account_number = "0000000000000000000000", email = "batman@gotham.city" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            await _client.PostAsync ("/api/transaction", stringContent);
            // Act
            var response = await _client.GetAsync ("/api/transaction/1");
            // Assert
            Assert.Equal (HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetReturnsCorectObject () {
            var item = new Transaction { btc_quantity = 0.0001, account_number = "0000000000000000000000", email = "batman@gotham.city" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            await _client.PostAsync ("/api/transaction", stringContent);
            // Act
            var response = await _client.GetAsync ("/api/transaction/1");

            var stringItem = await response.Content.ReadAsStringAsync ();
            var item2 = JsonConvert.DeserializeObject<Transaction> (stringItem);
            Assert.True ( TransactionItemComparer (item, item2));
        }    

        [Fact]
        public async Task PostReturnsCreatedForNewItemEmail () {
            var item = new Transaction { btc_quantity = 0.0001, account_number = "0000000000000000000000", email = "batman@gotham.city" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync ("/api/transaction", stringContent);
            Assert.Equal (HttpStatusCode.Created, response.StatusCode);
        }
        [Fact]
        public async Task PostReturnsCreatedForNewItemPhoneNumber () {
            var item = new Transaction { btc_quantity = 0.0001, account_number = "0000000000000000000000", phone_number = "11111111" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync ("/api/transaction", stringContent);
            Assert.Equal (HttpStatusCode.Created, response.StatusCode);
        }
        [Fact]
        public async Task PostReturnsBadRequestForEmptyItem () {
            var stringContent = new StringContent (JsonConvert.SerializeObject (null), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync ("/api/transaction", stringContent);
            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostReturnsBadRequestForBtcBelowZero () {
            var item = new Transaction { btc_quantity = -0.0001, account_number = "0000000000000000000000", email = "batman@gotham.city" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync ("/api/transaction", stringContent);
            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);
            string responseBody = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseBody);
            Assert.Equal ("Value for btc_quantity must be > 0.", json["errors"]["btc_quantity"][0]);
        }

        [Fact]
        public async Task PostReturnsBadRequestForWrongAccountNumber () {
            var item = new Transaction { btc_quantity = 0.0001, account_number = "a0000000000000000000000", email = "batman@gotham.city" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync ("/api/transaction", stringContent);
            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);
            string responseBody = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseBody);
            Assert.Equal ("The account_number field is not a valid credit card number.", json["errors"]["account_number"][0]);
        }

        [Fact]
        public async Task PostReturnsBadRequestForEmptyAccountNumber () {
            var item = new Transaction { btc_quantity = 0.0001, email = "batman@gotham.city" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync ("/api/transaction", stringContent);
            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);
            string responseBody = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseBody);
            Assert.Equal ("The account_number field is required.", json["errors"]["account_number"][0]);
        }

        [Fact]
        public async Task PostReturnsBadRequestForWrongEmail () {
            var item = new Transaction { btc_quantity = 0.0001, account_number = "0000000000000000000000", email = "batman" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync ("/api/transaction", stringContent);
            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);
            string responseBody = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseBody);
            Assert.Equal ("The email field is not a valid e-mail address.", json["errors"]["email"][0]);
        }
        [Fact]
        public async Task PostReturnsBadRequestForEmptyEmailAndEmptyPhone () {
            var item = new Transaction { btc_quantity = 0.0001, account_number = "0000000000000000000000" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync ("/api/transaction", stringContent);
            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);
            string responseBody = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseBody);
            Assert.Equal ("One contact method should be specified.", json["errors"]["contact"]);
        }
        [Fact]
        public async Task PostReturnsBadRequestForBothEmailAndPhoneNumberSpecified () {
            var item = new Transaction { btc_quantity = 0.0001, account_number = "0000000000000000000000", email = "batman@gotham.city",phone_number = "111111111"};
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync ("/api/transaction", stringContent);
            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);
            string responseBody = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseBody);
            Assert.Equal ("One contact method should be specified.", json["errors"]["contact"]);
        }
        [Fact]
        public async Task PostReturnsBadRequestForWrongPhoneNumber () {
            var item = new Transaction { btc_quantity = 0.0001, account_number = "0000000000000000000000", phone_number = "a111111111"};
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync ("/api/transaction", stringContent);
            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);
            string responseBody = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseBody);
            Assert.Equal ("The phone_number field is not a valid phone number.", json["errors"]["phone_number"][0]);
        }
        [Fact]
        public async Task PostReturnsCorrectCreatedItem () {
            var item = new Transaction { btc_quantity = 0.0001, account_number = "0000000000000000000000", email = "batman@gotham.city" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync ("/api/transaction", stringContent);
            var stringItem = await response.Content.ReadAsStringAsync ();
            var item2 = JsonConvert.DeserializeObject<Transaction> (stringItem);
            Assert.True ( TransactionItemComparer (item, item2));
        }

        [Fact]
        public async Task PutReturnsBadRequestForInvalidItem () {
            var stringContent = new StringContent (JsonConvert.SerializeObject (null), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync ("/api/transaction/1", stringContent);
            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PutReturnsNoContentForItemNotFound () {
            var item = new Transaction { btc_quantity = 0.0001, account_number = "0000000000000000000000", email = "batman@gotham.city" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync ("/api/transaction/1", stringContent);
            Assert.Equal (HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PutReturnsOkForCorrectUpdatedItem () {
            var item = new Transaction { btc_quantity = 0.0001, account_number = "0000000000000000000000", email = "batman@gotham.city" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync ("/api/transaction", stringContent);
            item.btc_quantity = 1.0;
            item.Id = 1;
            stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            response = await _client.PutAsync ("/api/transaction/1", stringContent);
            Assert.Equal (HttpStatusCode.OK, response.StatusCode);
        }
        
        [Fact]
        public async Task PutReturnsCorrectUpdatedItem () {
            var item = new Transaction { btc_quantity = 0.0001, account_number = "0000000000000000000000", email = "batman@gotham.city" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync ("/api/transaction", stringContent);
            item.btc_quantity = 1.0;
            item.Id = 1;
            stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            response = await _client.PutAsync ("/api/transaction/1", stringContent);

            var stringItem = await response.Content.ReadAsStringAsync ();
            var item2 = JsonConvert.DeserializeObject<Transaction> (stringItem);
            Assert.True ( TransactionItemComparer (item, item2));
        }

        [Fact]
        public async Task DeleteReturnsNotFoundForNonExistingItem () {
            var response = await _client.DeleteAsync ("/api/transaction/1");
            Assert.Equal (HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteReturnsOkForCorrectDeletedItem () {
            var item = new Transaction { btc_quantity = 0.0001, account_number = "0000000000000000000000", email = "batman@gotham.city" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync ("/api/transaction", stringContent);
            response = await _client.DeleteAsync ("/api/transaction/1");
            Assert.Equal (HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteCorrectDeletsItem () {
            var item = new Transaction { btc_quantity = 0.0001, account_number = "0000000000000000000000", email = "batman@gotham.city" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync ("/api/transaction", stringContent);
            response = await _client.DeleteAsync ("/api/transaction/1");
            response = await _client.GetAsync("/api/transaction/1");
            Assert.Equal (HttpStatusCode.NotFound, response.StatusCode);
        }

        public bool TransactionItemComparer (Transaction item, Transaction item2) {
            if (
                item.btc_quantity == item2.btc_quantity &&
                item.account_number == item2.account_number &&
                item.email == item2.email &&
                item.phone_number == item2.phone_number
            ) {
                return true;
            }
            return false;
        }
    }
}
