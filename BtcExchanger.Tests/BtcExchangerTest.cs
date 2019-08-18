using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BtcExchanger.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;

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
            var response = await _client.GetAsync ("/api/order/1");
            // Assert
            Assert.Equal (HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetReturnsOkForCorrectIdValue () {
            var item = new OrderItem { btc_quantity = 0.0001, account_number = "0000000000000000000000", email = "batman@gotham.city" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            await _client.PostAsync ("/api/order", stringContent);
            // Act
            var response = await _client.GetAsync ("/api/order/1");
            // Assert
            Assert.Equal (HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetReturnsCorectObject () {
            var item = new OrderItem { btc_quantity = 0.0001, account_number = "0000000000000000000000", email = "batman@gotham.city" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            await _client.PostAsync ("/api/order", stringContent);
            // Act
            var response = await _client.GetAsync ("/api/order/1");

            var stringItem = await response.Content.ReadAsStringAsync ();
            var item2 = JsonConvert.DeserializeObject<OrderItem> (stringItem);
            Assert.Equal (true, OrderItemComparer (item, item2));
        }    

        [Fact]
        public async Task PostReturnsCreatedForNewItem () {
            var item = new OrderItem { btc_quantity = 0.0001, account_number = "0000000000000000000000", email = "batman@gotham.city" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync ("/api/order", stringContent);
            Assert.Equal (HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task PostReturnsBadRequestForEmptyItem () {
            var stringContent = new StringContent (JsonConvert.SerializeObject (null), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync ("/api/order", stringContent);
            Assert.Equal (HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostReturnsCorrectCreatedItem () {
            var item = new OrderItem { btc_quantity = 0.0001, account_number = "0000000000000000000000", email = "batman@gotham.city" };
            var stringContent = new StringContent (JsonConvert.SerializeObject (item), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync ("/api/order", stringContent);

            var stringItem = await response.Content.ReadAsStringAsync ();
            var item2 = JsonConvert.DeserializeObject<OrderItem> (stringItem);
            Assert.Equal (true, OrderItemComparer (item, item2));
        }

        public bool OrderItemComparer (OrderItem item, OrderItem item2) {
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