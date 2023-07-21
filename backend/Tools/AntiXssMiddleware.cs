using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public class AntiXssMiddleware
{
    private readonly RequestDelegate _next;

    public AntiXssMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method == "POST")
        {
            context.Request.EnableBuffering();

            using (var streamReader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                var requestBody = await streamReader.ReadToEndAsync();
                var sanitizedRequestBody = SanitizeInput(requestBody);

                // Remplace la valeur du corps de la requête avec la version sécurisée
                var byteArray = Encoding.UTF8.GetBytes(sanitizedRequestBody);
                context.Request.Body = new MemoryStream(byteArray);
            }
        }

        await _next(context);
    }

    private string SanitizeInput(string input)
    {
        // Échappe ou supprime les caractères spéciaux ou potentiellement dangereux
        // selon les besoins de votre application
        var sanitizedInput = input.Replace("<", "&lt;")
                                  .Replace(">", "&gt;")
                                  .Replace("\"", "&quot;")
                                  .Replace("'", "&#39;");

        return sanitizedInput;
    }
}
