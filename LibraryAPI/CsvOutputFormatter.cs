using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;
using Microsoft.Net.Http.Headers;
using System.Globalization;
using System;
using LibraryCore.Models;

namespace LibraryAPI
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
            => typeof(Book).IsAssignableFrom(type)
               || typeof(IEnumerable<Book>).IsAssignableFrom(type);

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<Book>)
            {
                foreach (var Book in (IEnumerable<Book>)context.Object)
                {
                    FormatCsv(buffer, Book);
                }
            }
            else
            {
                FormatCsv(buffer, (Book)context.Object);
            }

            await response.WriteAsync(buffer.ToString(), selectedEncoding);
        }

        private static void FormatCsv(StringBuilder buffer, Book Book)
        {
            buffer.AppendLine($"{Book.Title},\"{Book.Description},\"{Book.Introduction},\"{Book.BookId}\"");
        }
    }
}
