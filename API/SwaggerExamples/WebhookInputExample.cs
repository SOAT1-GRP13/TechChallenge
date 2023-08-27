using Application.Pagamentos.MercadoPago.Boundaries;
using Swashbuckle.AspNetCore.Filters;

namespace API.SwaggerExamples
{
    public class WebhookInputExample : IMultipleExamplesProvider<WebHookInput>
    {
        public IEnumerable<SwaggerExample<WebHookInput>> GetExamples()
        {
            yield return SwaggerExample.Create("Dados enviados pelo webhook", new WebHookInput()
            {
                Action = "test.created",
                Api_version = "v1",
                Application_id = "2316387072181620",
                Date_created = DateTime.Now,
                Id = 123456,
                Live_mode = false,
                Type = "test",
                User_id = 185446979,
                Data = new WebHookInputData()
                {
                    Id = 123456789
                }
            });
        }
    }
}
