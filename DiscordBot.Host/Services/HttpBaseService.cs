using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Header = System.Collections.Generic.KeyValuePair<string, string>;

namespace DiscordBot.Host.Services
{
	public abstract class HttpBaseService
	{
		private readonly HttpClient _client;

		protected HttpBaseService(HttpClient client)
		{
			_client = client;
		}

		protected Task<T> GetApiResultAsync<T>(string url, params Header[] headers)
		{
			if (headers != null)
			{
				url += "?";
				foreach (Header header in headers)
				{
					url += header.Key + "=" + header.Value + "&";
				}
			}
			HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
			return SendRequest<T>(message);
		}

		private async Task<T> SendRequest<T>(HttpRequestMessage message)
		{
			HttpResponseMessage response = await _client.SendAsync(message);
			string json = await response.Content.ReadAsStringAsync();
			T result = JsonConvert.DeserializeObject<T>(json);
			return result;
		}
		protected async Task<string> GetApiResultAsync(string url)
		{
			var response = await _client.GetAsync(new Uri(url));
			response.EnsureSuccessStatusCode();

			string json = await response.Content.ReadAsStringAsync();
			return json;
		}

		protected async Task<T> GetApiResultAsync<T>(string url)
		{
			string json = await GetApiResultAsync(url);
			T result = JsonConvert.DeserializeObject<T>(json);
			return result;
		}
	}
}
