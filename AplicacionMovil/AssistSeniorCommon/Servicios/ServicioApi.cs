using AssistSeniorCommon.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AssistSeniorCommon.Servicios
{
	public class ServicioApi
	{
		public async Task<Response> GetListAsyn<T>(string urlBase, string servicePrefix, string controller)
		{
			try
			{
				var client = new HttpClient()
				{
					BaseAddress = new Uri(urlBase)
				};
				var url = $"{servicePrefix}{controller}";
				var response = await client.GetAsync(url);
				var result = await response.Content.ReadAsStringAsync();
				if (!response.IsSuccessStatusCode)
				{
					return new Response
					{
						IsSucces = false,
						Message = result,

					};
				}
				var list = JsonConvert.DeserializeObject<List<T>>(result);
				return new Response
				{
					IsSucces = true,
					Result = list,
				};

			}
			catch (Exception ex)
			{
				return new Response
				{
					IsSucces = false,
					Message = ex.Message,
				};


			}



		}
	}
}
