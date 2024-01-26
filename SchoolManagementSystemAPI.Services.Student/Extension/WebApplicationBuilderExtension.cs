namespace SchoolManagementSystemAPI.Services.Student.Extension
{
    public static class WebApplicationBuilderExtension
    {
        public static WebApplicationBuilder AddClient(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient("UserAPI", u => u.BaseAddress = new Uri(builder.Configuration["ServiceUrl:UserAPI"]));
            return builder;
        }
    }
}
