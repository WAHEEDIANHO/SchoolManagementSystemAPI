namespace SchoolManagementSystemAPI.Services.Parent.Extension
{
    public static class WebApplicationBuilderExt
    {
        public static WebApplicationBuilder AddClient(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient("UserAPI", opt => opt.BaseAddress = new Uri(builder.Configuration["ServiceUrl:UserAPI"]));
            return builder;
        }
    }
}
